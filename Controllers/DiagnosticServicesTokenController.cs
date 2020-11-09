using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace Microsoft.RequestEcho
{
    [ApiController]
    public class DiagnosticServicesTokenController : ControllerBase
    {
        private readonly IProfilerTokenService _profilerTokenService;
        private readonly IARMClientCertificateValidator _armClientCertificateValidator;

        public DiagnosticServicesTokenController(
            IProfilerTokenService profilerTokenService,
            IARMClientCertificateValidator armClientCertificateValidator)
        {
            _profilerTokenService = profilerTokenService ?? throw new ArgumentNullException(nameof(profilerTokenService));
            _armClientCertificateValidator = armClientCertificateValidator ?? throw new ArgumentNullException(nameof(armClientCertificateValidator));
        }

        private string GetEchoContent()
        {
            return JsonConvert.SerializeObject(Request.Headers);
        }

        [Produces("application/json")]
        [HttpPost]
        [Route(ARMRouteTemplates.DiagnosticServiceReadOnlyTokenTemplate)]
        [ARMClientAuthorize]
        public IActionResult Get(string subscriptionId, string resourceGroupName, string componentName)
        {
            // TODO: Get AppId somehow
            var appId = new Guid("9c7614fa-c798-4219-9b92-31a938ac91b9");
            TokenContract tokenContract = new TokenContract()
            {
                AppId = appId, // TODO, get appId from CDS,
                PermissionLevel = PermissionLevel.ReadOnly,
            };
            string token = _profilerTokenService.IssueSecurityToken(tokenContract);
            return Ok(new
            {
                token
            });
        }

        [Produces("application/json")]
        [SwaggerResponse(200, "Read write token is generated successfully.")]
        [SwaggerResponse(401, "Unauthorized access.")]
        [HttpPost]
        [Route(ARMRouteTemplates.DiagnosticServiceReadWriteTokenTemplate)]
        [ARMClientAuthorize]
        public IActionResult Post(string subscriptionId, string resourceGroupName, string componentName)
        {
            // TODO: Get AppId somehow
            var appId = new Guid("9c7614fa-c798-4219-9b92-31a938ac91b9");
            TokenContract tokenContract = new TokenContract()
            {
                AppId = appId, // TODO, get appId from CDS,
                PermissionLevel = PermissionLevel.ReadWrite,
            };

            string token = _profilerTokenService.IssueSecurityToken(tokenContract);

            return Ok(new
            {
                token
            });
        }
    }
}