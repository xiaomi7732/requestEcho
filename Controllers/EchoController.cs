
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Microsoft.RequestEcho
{
    [ApiController]
    [Route("[controller]")]
    [Route("/")]
    public class EchoController : ControllerBase
    {
        /// <summary>
        /// This method is required for the provider to be registered.
        /// </summary>
        /// <returns></returns>
        [Route("subscriptions/{anything}")]
        public IActionResult ARMPut()
        {
            return Ok();
        }

        [HttpGet]
        [Route("subscriptions/{subscriptionId}/providers/Microsoft.Profiler/stampToken")]
        public IActionResult Get(string subscriptionId)
        {
            string echoContent = GetEchoContent();
            return Ok(new
            {
                subscriptionId = subscriptionId,
                EchoContent = echoContent,
            });
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
            return Ok(new
            {
                componentName,
                echoContent
            });
        }

        [HttpPost]
        [Route("subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/microsoft.insights/components/{componentName}/providers/microsoft.profiler/stampToken")]
        public IActionResult Post(string subscriptionId, string resourceGroupName, string componentName)
        {
            string echoContent = GetEchoContent();

            return Ok(new
            {
                componentName,
                Method = "POST",
                echoContent
            });
        }
    }
}