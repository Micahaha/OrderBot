using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooruAPI.Core
{
    /// <summary> Represents a booru API with routing for comments.</summary>
    /// <typeparam name="TBooru"> The API to use for routing.</typeparam>
    /// <typeparam name="TBooruComment"> The comment type to use.</typeparam>
    public interface IBooruCommentApi<TBooru, TBooruComment> : IBooruApi
        where TBooru : IBooruCommentApi<TBooru, TBooruComment>
        where TBooruComment : IBooruComment
    {
        /// <summary> Gets The comments on a post.</summary>
        /// <param name="postId"> The post id </param>
        /// <returns> Collection of comments on the post</returns>
        Task<List<TBooruComment>> GetPostCommentsAsync(int postId);
    }
}
