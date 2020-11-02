
using Microsoft.AspNetCore.Mvc;

namespace Microsoft.RequestEcho
{
    [ApiController]
    public class TestController : ControllerBase
    {
        [Route("traceAnalyzeResult")]
        [HttpGet]
        [DiagnosticServicesAuthorize(PermissionLevel.ReadWrite)]
        public IActionResult Get()
        {
            return Ok(new
            {
                Authorized = true,
                Permission = "rw"
            });
        }
    }
}