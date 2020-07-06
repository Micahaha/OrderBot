using System.Threading.Tasks;

namespace BooruAPI.Core
{
    /// <summary> A representation of a post objectt with additional information.</summary>
    public interface IAdvancedBooruPost : IBooruPost
    {
        /// <summary> The title of the post.</summary>
        string Title { get; }
        /// <summary> The ID of the next post on the booru site.</summary>
        int? NextPostId { get; }
        /// <summary> The ID of the previous post on the booru site.</summary>
        int? PreviousPostId { get; }
    }

    /// <summary> A representation of a post object with additional API calls.</summary>
    /// <typeparam name="TBooru"> The API to use for routing.</typeparam>
    /// <typeparam name="TBooruPost"> The post type to be used.</typeparam>
    public interface IAdvancedBooruPost<TBooru, TBooruPost> : IBooruPost<TBooru, TBooruPost>, IAdvancedBooruPost
        where TBooru : IAdvancedBooruApi<TBooru>, IBooruPostApi<TBooru, TBooruPost>
        where TBooruPost : IBooruPost<TBooru, TBooruPost>
    {
        /// <summary> Gets the information for the poster.</summary>
        /// <param name="booruApi"> The api to use for the call.</param>
        /// <returns> The information about the poster.</returns>
        Task<IBooruUserInfo> GetUserInfoAsync(TBooru booruApi);
    }
}
