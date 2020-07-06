namespace BooruAPI.Core.Utilities
{
    /// <summary> A representation of a logger for booru specific messages.</summary>
    public interface IBooruLogger
    {
        /// <summary> Logs the API error.</summary>
        /// <param name="errorCode"> The error code signalling what caused the error log.</param>
        /// <param name="issuer"> The api that encountered the error.</param>
        /// <param name="message"> The message to be displayed alongside the error code.</param>
        void LogApiError(ApiErrorCode errorCode, IBooruApi issuer, string message = null);
    }
}
