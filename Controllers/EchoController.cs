
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
            string echoContent = GetEchoContent();
            return Ok(subscriptionId + ":" + echoContent);
        }

        private string GetEchoContent()
        {
            return JsonConvert.SerializeObject(Request.Headers);
        }

        [HttpGet]
        [Route("subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/microsoft.insights/components/{componentName}/providers/microsoft.profiler/stampToken")]
        public IActionResult Get(string subscriptionId, string resourceGroupName, string componentName)
        {
            string echoContent = GetEchoContent();
            return Ok(componentName + ":" + echoContent);
        }
    }
}