using System.Collections.Generic;

namespace BooruAPI.Core
{
    /// <summary> An interface of an API object responsible for fetching favorite pages.</summary>
    /// <typeparam name="TBooru"> The api to use for calls.</typeparam>
    /// <typeparam name="TBooruSelfUser"> The self user type to use.</typeparam>
    /// <typeparam name="TBooruPost"> The post type use.</typeparam>
    public interface IUserFavoritePostBooruApi<TBooru, TBooruSelfUser, TBooruPost> : IBooruSelfUserApi<TBooru, TBooruSelfUser>
        where TBooru : IBooruSelfUserApi<TBooru, TBooruSelfUser>, IBooruPostApi<TBooru, TBooruPost>
        where TBooruSelfUser : IBooruSelfUser<TBooru, TBooruSelfUser>
        where TBooruPost : IBooruPost<TBooru, TBooruPost>
    {
        /// <summary> Gets a page of favorite posts of a specified user.</summary>
        /// <param name="id"> The id used for fetching favorties of a user.</param>
        /// <param name="skippedPosts"> The amount of posts to skip for the favorite page.</param>
        /// <returns> A collection of favorite posts on one page.</returns>
        IAsyncEnumerable<TBooruPost> GetUserFavoritePageByIdAsync(int id, int skippedPosts = 0);

        /// <summary> Gets a page of favorite posts of a specified user.</summary>
        /// <param name="username"> The id used for fetching favorties of a user.</param>
        /// <param name="skippedPosts"> The amount of posts to skip for the favorite page.</param>
        /// <returns> A collection of favorite posts on one page.</returns>
        IAsyncEnumerable<TBooruPost> GetUserFavoritePageByUsernameAsync(string username, int skippedPosts = 0);

        /// <summary> Gets a page of favorite posts of a specified user.</summary>
        /// <param name="query"> The filter to use for fetching posts.</param>
        /// <returns> A collection of favorite posts on one page.</returns>
        IAsyncEnumerable<TBooruPost> GetUserFavoritePageAsync(string query);

        /// <summary> Gets the favorite posts of a user.</summary>
        /// <param name="id"> The id used for fetching favorties of a user.</param>
        /// <returns> A collection of all favorite posts of a user.</returns>
        IAsyncEnumerable<TBooruPost> GetUserFavoritePagesByIdAsync(int id);

        /// <summary> Gets the favorite posts of a user.</summary>
        /// <param name="username"> The id used for fetching favorties of a user.</param>
        /// <returns> A collection of all favorite posts of a user.</returns>
        IAsyncEnumerable<TBooruPost> GetUserFavoritePagesByUsernameAsync(string username);

        /// <summary> Gets the favorite posts of a user.</summary>
        /// <param name="query"> The filter to use for fetching posts.</param>
        /// <returns> A collection of all favorite posts of a user.</returns>
        IAsyncEnumerable<TBooruPost> GetUserFavoritePagesAsync(string query);
    }
}
