using System;
using System.Collections.Generic;

namespace BooruAPI.Core
{
    /// <summary> An interface for a user object with additional information.</summary>
    public interface IBooruUserInfo
    {
        /// <summary> The username of the user.</summary>
        string Username { get; }
        /// <summary> The join date of twhen the user joined the booru.</summary>
        DateTime JoinDate { get; }
    }

    /// <summary> An interface for a user object with additional information.</summary>
    /// <typeparam name="TBooru"> The API responsible for routing.</typeparam>
    public interface IBooruUserInfo<TBooru> : IBooruUserInfo
        where TBooru : IAdvancedBooruApi<TBooru>
    { }

    /// <summary> An interface for a user object with additional API calls.</summary>
    /// <typeparam name="TBooru"> The API responsible for routing.</typeparam>
    /// <typeparam name="TBooruPost"> The post type to use.</typeparam>
    public interface IAdvancedBooruUser<TBooru, TBooruPost> : IBooruUserInfo<TBooru>
        where TBooru : IAdvancedBooruApi<TBooru>
        where TBooruPost : IBooruPost
    {
        /// <summary> Gets a collection of all posts made by the user.</summary>
        /// <param name="booru"> The api to use for the call.</param>
        /// <returns> A collection of all posts made by the user.</returns>
        IAsyncEnumerable<TBooruPost> GetPostsAsync(TBooru booru);

        /// <summary> Gets a collection of all posts favorited by the user.</summary>
        /// <param name="booru"> The api to use for the call.</param>
        /// <returns> A collection of all posts made by the user.</returns>
        IAsyncEnumerable<TBooruPost> GetFavoritesAsync(TBooru booru);
    }
}
