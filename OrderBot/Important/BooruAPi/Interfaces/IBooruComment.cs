using System;
using System.Threading.Tasks;

namespace BooruAPI.Core
{
    /// <summary> A representation of a comment.</summary>
    public interface IBooruComment
    {
        /// <summary> The Id of the post.</summary>
        int PostId { get; }
        /// <summary> The Id of the comment.</summary>
        int CommentId { get; }
        /// <summary> Determines if the commenter is anonymous.</summary>
        bool IsAnonymous { get; }
        /// <summary> The username of the commenting user.</summary>
        string UsernameCommenter { get; }
        /// <summary> The date the comment has been posted on.</summary>
        DateTime PostDate { get; }
        /// <summary> The content of the comment.</summary>
        string Content { get; }
    }

    /// <summary> A representation of a comment.</summary>
    /// <typeparam name="TBooru"> The api to use for calls.</typeparam>
    public interface IBooruComment<TBooru> : IBooruComment
        where TBooru : IBooruPostApi<TBooru>
    { }

    /// <summary> A representation of a comment.</summary>
    /// <typeparam name="TBooru"> The api to use for calls.</typeparam>
    /// <typeparam name="TBooruPost"> The post type to use.</typeparam>
    public interface IBooruComment<TBooru, TBooruPost> : IBooruComment<TBooru>
        where TBooru : IBooruPostApi<TBooru, TBooruPost>
        where TBooruPost : IBooruPost<TBooru, TBooruPost>
    {
        /// <summary> gets the post this comment is commented on.</summary>
        /// <param name="booruApi"> The booru to use for api calls.</param>
        /// <returns> The post this comment is commented on.</returns>
        Task<TBooruPost> GetPostAsync(TBooru booruApi);
    }

    /// <summary> A representation of a comment object with added API calls.</summary>
    /// <typeparam name="TBooru"> The api to use for calls.</typeparam>
    /// <typeparam name="TBooruUserInfo"> The user type to use.</typeparam>
    /// <typeparam name="TBooruPost"> The post type to use.</typeparam>
    public interface IBooruComment<TBooru, TBooruPost, TBooruUserInfo> : IBooruComment<TBooru, TBooruPost>
        where TBooru : IBooruPostApi<TBooru, TBooruPost>, IAdvancedBooruApi<TBooru>
        where TBooruPost : IBooruPost<TBooru, TBooruPost>
        where TBooruUserInfo : IBooruUserInfo<TBooru>
    {
        /// <summary> Gets the user information of the commenter.</summary>
        /// <param name="booruApi"> The booru to use for api calls.</param>
        /// <returns> The user information of the commenter.</returns>
        Task<TBooruUserInfo> GetPosterInfoAsync(TBooru booruApi);
    }
}
