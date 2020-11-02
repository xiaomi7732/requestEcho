using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microsoft.RequestEcho
{
    /// <summary>
    /// An <see cref="ObjectResult"/> that returns status Forbidden (403)
    /// </summary>
    public class ForbiddenObjectResult : ObjectResult
    {
        /// <summary>
        /// Construct a <see cref="ForbiddenObjectResult"/> with the given result object.
        /// </summary>
        /// <param name="value">The result object.</param>
        public ForbiddenObjectResult(object value) : base(value) => StatusCode = StatusCodes.Status403Forbidden;
    }
}