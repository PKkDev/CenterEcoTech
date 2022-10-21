namespace CenterEcoTech.Domain.DTO
{
    /// <summary>
    /// response object
    /// </summary>
    public class HttpErrorMessage
    {
        /// <summary>
        /// message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="message"></param>
        public HttpErrorMessage(string message)
            => Message = message;
    }
}
