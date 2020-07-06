using System.Collections.Generic;

namespace BooruAPI.Core
{
    /// <summary> A representation of a post wrapper for XML serialization.</summary>
    /// <typeparam name="TBooruPost"> The post type to use.</typeparam>
    public interface IPostCollection<TBooruPost>
        where TBooruPost : IBooruPost
    {
        /// <summary> The amount of posts found with a query.</summary>
        int PostCountWithFilter { get; }

        /// <summary> The collection of posts.</summary>
        List<TBooruPost> Posts { get; set; }
    }
}
