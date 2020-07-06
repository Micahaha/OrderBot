using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooruAPI.Core
{
    /// <summary> A representation of a self user.</summary>
    public interface IBooruSelfUser : IBooruUser
    {
        /// <summary> The password hash used for authentication.</summary>
        string PasswordHash { get; }

        /// <summary> The list of blacklisted tags of the user.</summary>
        IReadOnlyList<string> BlacklistedTags { get; }

        /// <summary> The collection of settings </summary>
        ISettingsHolder Settings { get; }
    }

    /// <summary> A representation of a self user with added self user API calls.</summary>
    /// <typeparam name="TBooru"></typeparam>
    /// <typeparam name="TBooruSelfUser"></typeparam>
    public interface IBooruSelfUser<TBooru, TBooruSelfUser> : IBooruSelfUser
        where TBooru : IBooruSelfUserApi<TBooru, TBooruSelfUser>
        where TBooruSelfUser : IBooruSelfUser<TBooru, TBooruSelfUser>
    {
        /// <summary> Adds a tag to the blacklist.</summary>
        /// <param name="booruApi"> The api to use for calls.</param>
        /// <param name="tag"> The tag to add to the blacklist.</param>
        Task AddBlacklistedTagAsync(TBooru booruApi, string tag);

        /// <summary> Adds tags to the blacklist.</summary>
        /// <param name="booruApi"> The api to use for calls.</param>
        /// <param name="tags"> The tags to add to the blacklist.</param>
        Task AddBlacklistedTagsAsync(TBooru booruApi, IEnumerable<string> tags);

        /// <summary> Removes a tag to the blacklist.</summary>
        /// <param name="booruApi"> The api to use for calls.</param>
        /// <param name="tag"> The tag to remove to the blacklist.</param>
        Task RemoveBlacklistedTagAsync(TBooru booruApi, string tag);

        /// <summary> Removes tags to the blacklist.</summary>
        /// <param name="booruApi"> The api to use for calls.</param>
        /// <param name="tags"> The tags to remove to the blacklist.</param>
        Task RemoveBlacklistedTagsAsync(TBooru booruApi, IEnumerable<string> tags);

        /// <summary> Sets the blacklisted tags to the given tags.</summary>
        /// <param name="booruApi"> The api to use for calls.</param>
        /// <param name="blacklistedTags"> The tags to set as the blacklist.</param>
        Task SetBlacklistedTagsAsync(TBooru booruApi, List<string> blacklistedTags);
    }
}
