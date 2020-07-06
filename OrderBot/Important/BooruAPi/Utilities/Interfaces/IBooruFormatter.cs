using BooruAPI.Core.Common;
using System.IO;

namespace BooruAPI.Core.Utilities
{
    /// <summary> A representation of a formatter helper class.</summary>
    public interface IBooruFormatter
    {
        /// <summary> Forms an API url to use in API calls.</summary>
        /// <param name="baseUrl"> The base url for the api call.</param>
        /// <param name="topic"> The topic for the API call.</param>
        /// <param name="specifier"> Specifies what search has to be done.</param>
        /// <returns> API base url.</returns>
        string FormApiUrl(string baseUrl, string topic, string specifier = "index");

        /// <summary> Forms an API url with a tag query to recieve the post count.</summary>
        /// <param name="baseUrl"> The base url.</param>
        /// <param name="tagQuery"> The tag query to filter posts with.</param>
        /// <returns> An API url ready to be called.</returns>
        string FormCountQuery(string baseUrl, string tagQuery);

        /// <summary> Forms an API url for a tag filtered search page.</summary>
        /// <param name="baseUrl"> The base url.</param>
        /// <param name="tagQuery"> The tag query to filter posts with.</param>
        /// <param name="pageNumber"> The index of the page to fetch.</param>
        /// <param name="searchLimit"> The amount of posts per fetch.</param>
        /// <returns> An API url ready to be called.</returns>
        string FormSearchQuery(string baseUrl, string tagQuery, int pageNumber = 0, int searchLimit = 64);

        /// <summary> Finds the amount of posts in a string.</summary>
        /// <param name="input"> The string to scan for a post count.</param>
        /// <returns> The post count</returns>
        int FindPostCount(string input);

        /// <summary> Finds the amount of posts in a stream of data.</summary>
        /// <param name="input"> The stream to scan for a post count.</param>
        /// <returns> The post count.</returns>
        int FindPostCount(Stream input);

        /// <summary> Deserializes a string into a <typeparamref name="TBooruPost"/>.</summary>
        /// <typeparam name="TBooruPost"> The post type to deserialize to.</typeparam>
        /// <param name="post"> The post string containing the serialized post.</param>
        /// <returns> Deserialized <typeparamref name="TBooruPost"/>.</returns>
        TBooruPost DeserializePost<TBooruPost>(string post) where TBooruPost : IBooruPost;

        /// <summary> Deserializes a stream of data into a <typeparamref name="TBooruPost"/>.</summary>
        /// <typeparam name="TBooruPost"> The post type to deserialize to.</typeparam>
        /// <param name="post"> The post stream containing the serialized post.</param>
        /// <returns> Deserialized <typeparamref name="TBooruPost"/>.</returns>
        TBooruPost DeserializePost<TBooruPost>(Stream post) where TBooruPost : IBooruPost;

        /// <summary> Deserializes a string into a collection of <typeparamref name="TBooruPost"/>.</summary>
        /// <typeparam name="TPostWrapper"> The wrapper type to deserialize to.</typeparam>
        /// <typeparam name="TBooruPost"> The post type to deserialize to.</typeparam>
        /// <param name="postCollection"> The post collecion string containing the serialized collecion.</param>
        /// <returns> Deserialized collection of <typeparamref name="TBooruPost"/>.</returns>
        TPostWrapper DeserializePostCollection<TPostWrapper, TBooruPost>(string postCollection) where TBooruPost : IBooruPost where TPostWrapper : IPostCollection<TBooruPost>;

        /// <summary> Deserializes a stream of data into a collection of <typeparamref name="TBooruPost"/>.</summary>
        /// <typeparam name="TPostWrapper"> The wrapper type to deserialize to.</typeparam>
        /// <typeparam name="TBooruPost"> The post type to deserialize to.</typeparam>
        /// <param name="postCollection"> The post collecion stream containing the serialized collecion.</param>
        /// <returns> Deserialized collection of <typeparamref name="TBooruPost"/>.</returns>
        TPostWrapper DeserializePostCollection<TPostWrapper, TBooruPost>(Stream postCollection) where TBooruPost : IBooruPost where TPostWrapper : IPostCollection<TBooruPost>;

        /// <summary> Forms a string representation used by the booru for rating a post.</summary>
        /// <param name="rating"> The value of the rating to turn into a string.</param>
        /// <returns> A string representation used by the booru for rating a post.</returns>
        string FormRatingString(Rating rating);
    }
}
