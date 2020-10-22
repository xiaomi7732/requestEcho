
using Microsoft.AspNetCore.Mvc;

namespace Microsoft.RequestEcho
{
    [ApiController]
    [Route("/")]
    public class ProviderRegisterController : ControllerBase
    {
        /// <summary>
        /// This route is required for the provider to be registered successfully.
        /// Notice, without returning 200, it will bring down the registering of the whole RP.
        /// Refer to the contrace of subscription updating: https://github.com/Azure/azure-resource-manager-rpc/blob/master/v1.0/subscription-lifecycle-api-reference.md
        /// </summary>
        [Route("subscriptions/{subscriptionId}")]
        [HttpPut]
        public IActionResult ARMPut()
        {
            return Ok();
        }
    }
}