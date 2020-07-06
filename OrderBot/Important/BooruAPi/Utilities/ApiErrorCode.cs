namespace BooruAPI.Core.Utilities
{
    /// <summary> Possible API error codes taht can be called within a booru api.</summary>
    public enum ApiErrorCode
    {
        /// <summary> The response result of the query has returned with no results.</summary>
        NoResulsForQuery = 0,
        /// <summary> The client could not establish a connection with the api.</summary>
        NoConnection = -1,
        /// <summary> The client needs to be authorized for the action.</summary>
        NotAuthorized = -2,
        /// <summary> A problem occured during deserialization of objects</summary>
        DeserializationError = -3,
        /// <summary> The user does not have the rights for the action.</summary>
        NotAllowed = -4,
        /// <summary> The post count could not be fetched.</summary>
        PostCountFetchingError = -5,
        /// <summary> The request sent to the booru server cannot be executed.</summary>
        BadRequest = -6,
        /// <summary> The credetials entereddo not match an existing user on the booru server.</summary>
        InvalidCredentials = -7,
        /// <summary> A non standard error has occured.</summary>
        /// <remarks> Adding a message is adviced when sending this error to the logger.</remarks>
        Generic = -16,
    }
}
