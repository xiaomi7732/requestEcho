
using Microsoft.AspNetCore.Mvc;

namespace Microsoft.RequestEcho
{
    [ApiController]
    public class TestController : ControllerBase
    {
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