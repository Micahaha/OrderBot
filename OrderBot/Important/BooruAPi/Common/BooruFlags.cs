using System;

namespace BooruAPI.Core.Common
{
    /// <summary> Indicates what features the API has.</summary>
    [Flags]
    public enum BooruFlags : byte
    {
        /// <summary> Indicates that the Booru API has an API for related based searches.</summary>
        HasRelatedApi = 1 << 0,
        /// <summary> Indicates that the Booru API has an API for wiki searches.</summary>
        HasWikiApi = 1 << 1,
        /// <summary> Indicates that the Booru API has an API for comment searches.</summary>
        HasCommentApi = 1 << 2,
        /// <summary> Indicates that the Booru API has an API for tag-id based searches.</summary>
        HasTagByIdApi = 1 << 3,
        /// <summary> Indicates that the Booru API makes use of SSL encritpion for their connections.</summary>
        HasSSL = 1 << 4
    }
}
