using Microsoft.AspNetCore.Mvc;

namespace Microsoft.RequestEcho
{
    [ApiController]
    public class ARMClientCertController : ControllerBase
    {
        private readonly IARMCertCache _armCertCache;

        public ARMClientCertController(IARMCertCache armCertCache)
        {
            _armCertCache = armCertCache ?? throw new System.ArgumentNullException(nameof(armCertCache));
        }

        [Route("ValidClientCertThumbprints")]
        [HttpGet]
        public IActionResult List()
        {
            var certs = _armCertCache.ValidThumbprints;
            return Ok(certs);
        }
    }
}