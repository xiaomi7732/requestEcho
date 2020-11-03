using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using static Microsoft.RequestEcho.ErrorResponseFactory;

namespace Microsoft.RequestEcho
{
    public class DiagnosticServiceAuthorizeFilter : IAsyncAuthorizationFilter
    {
        private readonly IProfilerTokenService _tokenService;
        private readonly PermissionLevel _permissionLevel;
        private readonly ILogger _logger;

        public DiagnosticServiceAuthorizeFilter(
            IProfilerTokenService tokenService,
            PermissionLevel permissionLevel,
            ILogger<DiagnosticServiceAuthorizeFilter> logger)
        {
            _permissionLevel = permissionLevel;
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            RequestHeaders headers = context.HttpContext.Request.GetTypedHeaders();
            AuthenticationHeaderValue authentication = headers.Get<AuthenticationHeaderValue>("DiagnosticServicesAuthorization");

            if (authentication == null)
            {
                context.Result = Unauthorized("Authentication is required.");
                return;
            }

            if (authentication.Scheme != "Bearer")
            {
                context.Result = Forbidden("'Bearer' is the only supported authorization scheme.");
                return;
            }

            string token = authentication.Parameter;
            if (string.IsNullOrEmpty(token))
            {
                context.Result = Forbidden("Token is missing.");
                return;
            }

            try
            {
                TokenContract tokenContract = _tokenService.ValidateSecurityToken(token);

                // TODO: Validate it contains the expected app Id too.
                if (!tokenContract.PermissionLevel.HasFlag(_permissionLevel))
                {
                    context.Result = Forbidden($"Insufficient permission level: {tokenContract.PermissionLevel}");
                    return;
                }

                context.HttpContext.Features.Set<ProfilerContext>(new ProfilerContext() { AppId = tokenContract.AppId });
            }
            catch (SecurityTokenExpiredException ex)
            {
                _logger.LogError(ex, "Token expired.");
                context.Result = Forbidden(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Token validation error");
                context.Result = Forbidden(ex.Message);
            }

            // TODO: Is async op needed?
            await Task.CompletedTask;
        }
    }
}