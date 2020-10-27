
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Microsoft.RequestEcho
{
    [ApiController]
    public class EchoController : ControllerBase
    {
        private string GetEchoContent()
        {
            return JsonConvert.SerializeObject(Request.Headers);
        }

        [Produces("application/json")]
        [HttpGet]
        [Route(ARMRouteTemplates.ProfilerTokenTemplate)]
        public IActionResult Get(string subscriptionId, string resourceGroupName, string componentName)
        {
            string echoContent = GetEchoContent();
            return Ok(new
            {
                componentName,
                echoContent
            });
        }

        [Produces("application/json")]
        [SwaggerResponse(200, "Read write token is generated successfully.")]
        [SwaggerResponse(401, "Unauthorized access.")]
        [HttpPost]
        [Route(ARMRouteTemplates.ProfilerTokenTemplate)]
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