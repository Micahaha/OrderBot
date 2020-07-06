namespace BooruAPI.Core.Common
{
    /// <summary> The possible image ratings on a post.</summary>
    public enum Rating : byte
    {
        /// <summary> Marks the image for explicit content.</summary>
        Explicit = 0,
        /// <summary> Marks the image for questionable content.</summary>
        Questionable = 1,
        /// <summary> Marks the image for safe for work content.</summary>
        Safe = 2,
        /// <summary> The shorthand notation for images with explicit content.</summary>
        E = 0,
        /// <summary> The shorthand notation for images with questionable content.</summary>
        Q = 1,
        /// <summary> The shorthand notation for images with safe for work content.</summary>
        S = 2
    }
}
