using Microsoft.AspNetCore.Mvc.Filters;
using static Microsoft.RequestEcho.ErrorResponseFactory;

namespace Microsoft.RequestEcho
{
    public class ARMClientAuthorizeFilter : IAuthorizationFilter
    {
        private readonly IARMClientCertificateValidator _certificateValidator;

        public ARMClientAuthorizeFilter(IARMClientCertificateValidator certificateValidator)
        {
            _certificateValidator = certificateValidator ?? throw new System.ArgumentNullException(nameof(certificateValidator));
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var clientCertificate = context.HttpContext.Connection?.ClientCertificate;

            if (clientCertificate==null)
            {
                context.Result = Forbidden("Client certificate is required.");
                return;
            }

            if(!_certificateValidator.Validate(clientCertificate))
            {
                context.Result = Forbidden("Client certificate thumbprint isn't valid");
                return;
            }

        }
    }
}