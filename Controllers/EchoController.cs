
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Microsoft.RequestEcho
{
    [ApiController]
    [Route("[controller]")]
    [Route("/")]
    public class EchoController : ControllerBase
    {

        [HttpGet]
        [Route("subscriptions/{subscriptionId}/providers/Microsoft.Profiler/stampToken")]
        public IActionResult Get(string subscriptionId)
        {
            string echoContent = JsonConvert.SerializeObject(Request.Headers);
            return Ok(subscriptionId + ":" + echoContent);
        }
    }
}