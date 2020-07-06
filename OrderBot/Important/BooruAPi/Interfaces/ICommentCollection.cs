using System.Collections.Generic;

namespace BooruAPI.Core
{
    /// <summary> A representation of a comment wrapper for XML serialization.</summary>
    /// <typeparam name="TBooruComment"> The comment type to use.</typeparam>
    public interface ICommentCollection<TBooruComment>
        where TBooruComment : IBooruComment
    {
        /// <summary> The collection of comments.</summary>
        List<TBooruComment> Comments { get; set; }
    }
}
