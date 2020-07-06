using BooruAPI.Core.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooruAPI.Core
{
    /// <summary> A representation of a post object.</summary>
    public interface IBooruPost
    {
        /// <summary> The identifier of the post.</summary>
        int Id { get; }
        /// <summary> The full scale image information.</summary>
        Common.BooruImage Image { get; }
        /// <summary> The image information for the thumbnail image.</summary>
        Common.BooruImage PreviewImage { get; }
        /// <summary> The image information for the scaled down image.</summary>
        Common.BooruImage SampleImage { get; }
        /// <summary> The id of the creator.</summary>
        int CreatorId { get; }
        /// <summary> Determines if the post has a parent post.</summary>
        bool HasParent { get; }
        /// <summary> The id of the parent post.</summary>
        int? ParentId { get; }
        /// <summary> Determines if the post has child posts.</summary>
        bool HasChildren { get; }
        /// <summary> Determines if the post has notes.</summary>
        bool HasNotes { get; }
        /// <summary> Determines if the post has comments.</summary>
        bool HasComments { get; }
        /// <summary> The source of the image.</summary>
        string Source { get; }
        /// <summary> The MD5 hash used for image comparison.</summary>
        string MD5 { get; }
        /// <summary> The score given to a post.</summary>
        int Score { get; }
        /// <summary> The NSFW rating of the image.</summary>
        Rating Rating { get; }
        /// <summary> The list of tags on the post.</summary>
        IReadOnlyList<string> Tags { get; }
    }

    /// <summary> A representation of a post object.</summary>
    public interface IBooruPost<TBooru> : IBooruPost
    { }

    /// <summary> A representation of a post object with added API calls.</summary>
    /// <typeparam name="TBooru"> The api to use for calls.</typeparam>
    /// <typeparam name="TBooruPost"> The post type to use.</typeparam>
    public interface IBooruPost<TBooru, TBooruPost> : IBooruPost<TBooru>
        where TBooru : IBooruPostApi<TBooru, TBooruPost>
        where TBooruPost : IBooruPost<TBooru, TBooruPost>
    {
        /// <summary> Gets a collection of posts with this post as parent.</summary>
        /// <param name="booruApi"> The api to use for calls.</param>
        /// <returns> A collection of posts with this post as parent.</returns>
        IAsyncEnumerable<TBooruPost> GetChildrenAsync(TBooru booruApi);
    }

    /// <summary> A representation of a post object with added API calls.</summary>
    /// <typeparam name="TBooru"> The api to use for calls.</typeparam>
    /// <typeparam name="TBooruPost"> The post type to use.</typeparam>
    /// <typeparam name="TBooruComment"> The comment type to use.</typeparam>
    public interface IBooruPost<TBooru, TBooruPost, TBooruComment> : IBooruPost<TBooru, TBooruPost>
        where TBooru : IBooruCommentApi<TBooru, TBooruComment>, IBooruPostApi<TBooru, TBooruPost>
        where TBooruComment : IBooruComment<TBooru>
        where TBooruPost : IBooruPost<TBooru, TBooruPost>
    {

        /// <summary> Gets The comments on a post.</summary>
        /// <param name="booruApi"></param>
        /// <returns> Collection of comments on the post</returns>
        Task<List<TBooruComment>> GetComments(TBooru booruApi);
    }

    /// <summary> A representation of a post object with added API calls.</summary>
    /// <typeparam name="TBooru"> The api to use for calls.</typeparam>
    /// <typeparam name="TBooruPost"> The post type to use.</typeparam>
    /// <typeparam name="TBooruComment"> The comment type to use.</typeparam>
    /// <typeparam name="TBooruSelfUser"> The self user type to use.</typeparam>
    public interface IBooruPost<TBooru, TBooruPost, TBooruComment, TBooruSelfUser> : IBooruPost<TBooru, TBooruPost, TBooruComment>
        where TBooru : IBooruCommentApi<TBooru, TBooruComment>, IBooruPostApi<TBooru, TBooruPost, TBooruSelfUser>, IBooruSelfUserApi<TBooru, TBooruSelfUser>
        where TBooruComment : IBooruComment<TBooru>
        where TBooruPost : IBooruPost<TBooru, TBooruPost>
        where TBooruSelfUser : IBooruSelfUser<TBooru, TBooruSelfUser>
    {
        /// <summary> Adds a tag to the post.</summary>
        /// <param name="booruApi"> The api to use for the call.</param>
        /// /// <param name="user"> The user to add the tag.</param>
        /// <param name="tag"> The tag to add.</param>
        Task AddTagAsync(TBooru booruApi, TBooruSelfUser user, string tag);

        /// <summary> Adds tags to the post.</summary>
        /// <param name="booruApi"> The api to use for the call.</param>
        /// /// <param name="user"> The user to add the tags.</param>
        /// <param name="tags"> The tags to add.</param>
        Task AddTagsAsync(TBooru booruApi, TBooruSelfUser user, IEnumerable<string> tags);

        /// <summary> Removes a tag to the post.</summary>
        /// <param name="booruApi"> The api to use for the call.</param>
        /// /// <param name="user"> The user to remove the tag.</param>
        /// <param name="tag"> The tag to remove.</param>
        Task RemoveTagAsync(TBooru booruApi, TBooruSelfUser user, string tag);

        /// <summary> Remove tags to the post.</summary>
        /// <param name="booruApi"> The api to use for the call.</param>
        /// /// <param name="user"> The user to remove the tags.</param>
        /// <param name="tags"> The tags to remove.</param>
        Task RemoveTagsAsync(TBooru booruApi, TBooruSelfUser user, IEnumerable<string> tags);

        /// <summary> Sets the tags of the post.</summary>
        /// <param name="booruApi"> The api to use for the call.</param>
        /// <param name="user"> The user to set the tags.</param>
        /// <param name="tags"> The tags to set.</param>
        Task SetTagsAsync(TBooru booruApi, TBooruSelfUser user, IEnumerable<string> tags);

        /// <summary> Adds the post to the user's list of favorites</summary>
        /// <param name="booruApi"> The api to use for the call.</param>
        /// <param name="user"> The user to add the favorite to.</param>
        /// <returns> True on succes, False on failure.</returns>
        Task<bool> AddToFavoritesAsync(TBooru booruApi, TBooruSelfUser user);

        /// <summary> Removes the post from the user's list of favorites.</summary>
        /// <param name="booruApi"> The api to use for the call.</param>
        /// <param name="user"> The user to remove the favorite from.</param>
        /// <returns> True on succes, False on failure.</returns>
        Task<bool> RemoveFromFavoritesAsync(TBooru booruApi, TBooruSelfUser user);

        /// <summary> Adds a comment to the post.</summary>
        /// <param name="booruApi"> The api to use for the call.</param>
        /// <param name="user"> The user to post the comment.</param>
        /// <param name="comment"> The content of the comment.</param>
        /// <param name="isAnonymous"> Determines if the comment should be posted as Anonymous.</param>
        Task AddCommentAsync(TBooru booruApi, TBooruSelfUser user, string comment, bool isAnonymous = false);
    }
}
