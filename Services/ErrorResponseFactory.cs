using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Microsoft.RequestEcho
{
    public static class ErrorResponseFactory
    {
        /// <summary>
        /// Convenience factory method for creating a response with <see cref="HttpStatusCode.BadRequest"/>.
        /// </summary>
        /// <param name="message">A human-readable representation of the error.</param>
        /// <returns>An <see cref="IActionResult"/> with the error object.</returns>
        public static IActionResult BadRequest(string message) => new BadRequestObjectResult(new ErrorResponseObject(HttpStatusCode.BadRequest, message));

        /// <summary>
        /// Convenience factory method for creating a response with <see cref="HttpStatusCode.Forbidden"/>.
        /// </summary>
        /// <param name="message">A human-readable representation of the error.</param>
        /// <returns>An <see cref="IActionResult"/> with the error object.</returns>
        public static IActionResult Forbidden(string message) => new ForbiddenObjectResult(new ErrorResponseObject(HttpStatusCode.Forbidden, message));

        /// <summary>
        /// Convenience factory method for creating a response with <see cref="HttpStatusCode.NotFound"/>.
        /// </summary>
        /// <param name="message">A human-readable representation of the error.</param>
        /// <returns>An <see cref="IActionResult"/> with the error object.</returns>
        public static IActionResult NotFound(string message) => new NotFoundObjectResult(new ErrorResponseObject(HttpStatusCode.NotFound, message));

        /// <summary>
        /// Convenience factory method for creating a response with <see cref="HttpStatusCode.Conflict"/>.
        /// </summary>
        /// <param name="message">A human-readable representation of the error.</param>
        /// <returns>An <see cref="IActionResult"/> with the error object.</returns>
        public static IActionResult Conflict(string message) => new ConflictObjectResult(new ErrorResponseObject(HttpStatusCode.Conflict, message));

        /// <summary>
        /// Convenience factory method for creating a response with <see cref="HttpStatusCode.Unauthorized"/>.
        /// </summary>
        /// <param name="message">A human-readable representation of the error.</param>
        /// <returns>An <see cref="IActionResult"/> with the error object.</returns>
        public static IActionResult Unauthorized(string message) => Unauthorized(HttpStatusCode.Unauthorized, message);

        /// <summary>
        /// Convenience factory method for creating an unauthorized response with a custom error code.
        /// </summary>
        /// <param name="code">The error code.</param>
        /// <param name="message">A human-readable representation of the error.</param>
        /// <returns>An <see cref="IActionResult"/> with representation error object.</returns>
        public static IActionResult Unauthorized(HttpStatusCode code, string message) => new UnauthorizedObjectResult(new ErrorResponseObject(code, message));
    }
}