using System.Net;

namespace Microsoft.RequestEcho
{
    /// <summary>
    /// A minimal error response object.
    /// See https://github.com/Microsoft/api-guidelines/blob/master/Guidelines.md#7102-error-condition-responses
    /// </summary>
    public sealed class ErrorResponseObject
    {
        /// <summary>
        /// Construct an error response from an error code and a message.
        /// </summary>
        /// <param name="code">One of a server-defined set of error codes.</param>
        /// <param name="message">A human-readable representation of the error.</param>
        public ErrorResponseObject(HttpStatusCode code, string message) => Error = new { code, message };

        /// <summary>
        /// The error object.
        /// </summary>
        public object Error { get; }
    }
}