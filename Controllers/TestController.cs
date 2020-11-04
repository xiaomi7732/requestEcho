
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Microsoft.RequestEcho
{
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ARMAuthOptions _armAuth;
        private readonly IARMCertCache _armCertCache;

        public TestController(
            IARMCertCache aRMCertCache,
            IOptions<ARMAuthOptions> armAuth
            )
        {
            _armAuth = armAuth.Value ?? throw new ArgumentNullException();
            _armCertCache = aRMCertCache ?? throw new ArgumentNullException(nameof(aRMCertCache));
        }

        [Route("test")]
        [HttpGet]
        public IActionResult RunTest()
        {
            var certs = _armCertCache.ValidThumbprints;
            return Ok(certs.ToList());
        }


        [Route("traceAnalyzeResults")]
        [HttpGet]
        // Requires readonly
        [DiagnosticServicesAuthorize(PermissionLevel.ReadOnly)]
        public IActionResult Get()
        {
            // AppId is on Profiler Context
            ProfilerContext ctx = HttpContext.Features.Get<ProfilerContext>();

            return Ok(new
            {
                Authorized = true,
                Permission = "readOnly",
                Stub = $"The credential is good for listing all the profiler sessions within app id: {ctx.AppId}",
                AppId = ctx.AppId,
            });
        }

        [Route("traceAnalyzeResults")]
        [HttpPost]
        // Requires read-write permission
        [DiagnosticServicesAuthorize(PermissionLevel.ReadWrite)]
        public IActionResult Post()
        {
            ProfilerContext ctx = HttpContext.Features.Get<ProfilerContext>();

            return Ok(new
            {
                Authorized = true,
                Permission = "readWrite",
                Stub = $"The credential is good for read and write profiler session for app id: {ctx.AppId}",
                AppId = ctx.AppId,
            });
        }
    }
}