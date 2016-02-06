using System;

namespace Project.Infrastructure.FrameworkCore.WebMvc.Models
{
    [Serializable]
    public class AjaxResponse<TResult>
    {
        /// <summary>
        /// Indicates success status of the result.
        /// Set <see cref="error"/> if this value is false.
        /// </summary>
        public bool success { get; set; }

        /// <summary>
        /// The actual result object of ajax request.
        /// It is set if <see cref="success"/> is true.
        /// </summary>
        public TResult result { get; set; }

        /// <summary>
        /// Error details (Must and only set if <see cref="success"/> is false).
        /// </summary>
        public ErrorInfo error { get; set; }

        /// <summary>
        /// This property can be used to indicate that the current user has no privilege to perform this request.
        /// </summary>
        public bool unauthorizedrequest { get; set; }

        /// <summary>
        /// This property can be used to redirect user to a specified URL.
        /// </summary>
        public string targeturl { get; set; }

        /// <summary>
        /// Creates an <see cref="AjaxResponse"/> object with <see cref="result"/> specified.
        /// <see cref="success"/> is set as true.
        /// </summary>
        /// <param name="result">The actual result object of ajax request</param>
        public AjaxResponse(TResult result)
        {
            result = result;
            success = true;
        }

        /// <summary>
        /// Creates an <see cref="AjaxResponse"/> object.
        /// <see cref="success"/> is set as true.
        /// </summary>
        public AjaxResponse()
        {
            success = true;
        }

        /// <summary>
        /// Creates an <see cref="AjaxResponse"/> object with <see cref="success"/> specified.
        /// </summary>
        /// <param name="success">Indicates success status of the result</param>
        public AjaxResponse(bool success)
        {
            success = success;
        }

        /// <summary>
        /// Creates an <see cref="AjaxResponse"/> object with <see cref="error"/> specified.
        /// <see cref="success"/> is set as false.
        /// </summary>
        /// <param name="error">Error details</param>
        /// <param name="unAuthorizedRequest">Used to indicate that the current user has no privilege to perform this request</param>
        public AjaxResponse(ErrorInfo error, bool unAuthorizedRequest = false)
        {
            error = error;
            unauthorizedrequest = unAuthorizedRequest;
            success = false;
        }
    }
}
