namespace ColorBackend.Model
{
    /// <summary>
    /// Represents a standardized response object for API operations.
    /// </summary>
    /// <typeparam name="T">The type of the result data included in the response.</typeparam>
    public class ApiResponse<T>
    {
        /// <summary>
        /// Gets or sets a value indicating whether the API operation was successful.
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Gets or sets the HTTP status code returned by the API operation.
        /// </summary>
        public int ReturnedCode { get; set; }

        /// <summary>
        /// Gets or sets a message that provides additional information about the result of the API operation.
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// Gets or sets the result data returned by the API operation. 
        /// This can be null if there is no result to return or if the operation failed.
        /// </summary>
        public T? Result { get; set; }
    }
}
