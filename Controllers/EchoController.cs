
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Microsoft.RequestEcho
{
    [ApiController]
    [Route("[controller]")]
    public class EchoController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get()
        {
            string echoContent = JsonConvert.SerializeObject(Request.Headers);
            return Ok(echoContent);
        }
    }
}