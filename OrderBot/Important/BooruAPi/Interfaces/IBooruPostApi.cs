using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooruAPI.Core
{
    /// <summary> A representation of a booru post api.</summary>
    /// <typeparam name="TBooru"> The API to use for routing.</typeparam>
    public interface IBooruPostApi<TBooru> : IBooruApi
    {
        /// <summary> Gets the ammount of posts that can be fetched with the search query.</summary>
        /// <param name="tagQuery"> The query to use for filtering posts.</param>
        /// <returns> The ammount of posts that can be fetched with the search query.</returns>
        Task<int> GetPostCountAsync(string tagQuery);
    }

    /// <summary> A representation of a booru post api.</summary>
    /// <typeparam name="TBooru"> The API to use for routing.</typeparam>
    /// <typeparam name="TBooruPost"> The post type to use.</typeparam>
    public interface IBooruPostApi<TBooru, TBooruPost> : IBooruPostApi<TBooru>
        where TBooru : IBooruPostApi<TBooru, TBooruPost>
        where TBooruPost : IBooruPost<TBooru, TBooruPost>
    {
        /// <summary> Gets the first post with the specific filtering query.</summary>
        /// <param name="tagQuery"> The query used for filtering.</param>
        /// <returns>The first post with the filter query.</returns>
        Task<TBooruPost> GetPostAsync(string tagQuery);

        /// <summary> Gets a page of posts with the given filter query.</summary>
        /// <param name="tagQuery"> The query used for filtering posts.</param>
        /// <param name="pageNumber"> The page number to fetch.</param>
        /// <param name="requestLimit"> The maximum amount of posts to fetch per page.</param>
        /// <returns> A page of posts.</returns>
        Task<List<TBooruPost>> GetSearchPageAsync(string tagQuery, int pageNumber = 0, int requestLimit = 64);

        /// <summary> Gets all posts with the given filter query.</summary>
        /// <param name="tagQuery"> The query used for filtering posts.</param>
        /// <returns> A collection of posts.</returns>
        IAsyncEnumerable<TBooruPost> GetPostsAsync(string tagQuery);
    }

    /// <summary> A representation of a booru post api.</summary>
    /// <typeparam name="TBooru"> The API to use for routing.</typeparam>
    /// <typeparam name="TBooruPost"> The post type to use.</typeparam>
    /// <typeparam name="TBooruSelfUser"> The self user type to use.</typeparam>
    public interface IBooruPostApi<TBooru, TBooruPost, TBooruSelfUser> : IBooruPostApi<TBooru, TBooruPost>
        where TBooru : IBooruPostApi<TBooru, TBooruPost>, IBooruSelfUserApi<TBooru, TBooruSelfUser>
        where TBooruPost : IBooruPost<TBooru, TBooruPost>
        where TBooruSelfUser : IBooruSelfUser<TBooru, TBooruSelfUser>
    {
        /// <summary> Sets the tags for a post.</summary>
        /// <param name="post"> The post to set the tags for.</param>
        /// <param name="selfUser"> The user to set the tags.</param>
        /// <param name="tags"> The tags to attach to the post.</param>
        Task SetTagsAsync(TBooruPost post, TBooruSelfUser selfUser, IEnumerable<string> tags);

        /// <summary> Adds the post to the user's list of favorites</summary>
        /// <param name="post"> The post to add to the user's list of favorites</param>
        /// <param name="user"> The user to add the favorite to.</param>
        /// <returns> True on succes, False on failure.</returns>
        Task<bool> AddToFavoritesAsync(TBooruPost post, TBooruSelfUser user);

        /// <summary> Removes the post from the user's list of favorites.</summary>
        /// <param name="post"> The post to remove from the user's list of favorites.</param>
        /// <param name="user"> The user to remove the favorite from.</param>
        /// <returns> True on succes, False on failure.</returns>
        Task<bool> RemoveFromFavoritesAsync(TBooruPost post, TBooruSelfUser user);

        /// <summary> Adds a comment to the post.</summary>
        /// <param name="post"> The post to add a comment to.</param>
        /// <param name="user"> The user to post the comment.</param>
        /// <param name="comment"> The content of the comment.</param>
        /// <param name="isAnonymous"> Determines if the comment should be posted as Anonymous.</param>
        Task AddCommentAsync(TBooruPost post, TBooruSelfUser user, string comment, bool isAnonymous = false);
    }
}
