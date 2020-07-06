using BooruAPI.Core.Common;
using BooruAPI.Core.Utilities;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace BooruAPI.Core
{
    /// <summary> A base implementation of handing standard booru API calls and user control.</summary>
    /// <typeparam name="TBooru"> The api to use for calls.</typeparam>
    /// <typeparam name="TBooruPost"> The post type to use.</typeparam>
    /// <typeparam name="TBooruComment"> The comment type to use.</typeparam>
    /// <typeparam name="TBooruSelfUser"> The self user to use.</typeparam>
    public abstract class AdvancedBooruBase<TBooru, TBooruPost, TBooruComment, TBooruSelfUser> : BooruBase<TBooru, TBooruPost, TBooruComment>, 
            IBooruSelfUserApi<TBooru, TBooruSelfUser>, 
            IBooruPostApi<TBooru,TBooruPost,TBooruSelfUser>
        where TBooru : AdvancedBooruBase<TBooru, TBooruPost, TBooruComment, TBooruSelfUser>
        where TBooruSelfUser : IBooruSelfUser<TBooru, TBooruSelfUser>
        where TBooruPost : IBooruPost<TBooru, TBooruPost,TBooruComment,TBooruSelfUser>
        where TBooruComment : IBooruComment<TBooru, TBooruPost>
    {
        /// <summary> The cookie container used for fetching the booru specific cookies.</summary>
        private readonly CookieContainer _cookieContainer;

        /// <summary> Sets up the advanced booru api.</summary>
        /// <param name="baseUrl"> The base url used for the requests.</param>
        /// <param name="imageLimit"> The limit of images that can be fetched by a query.</param>
        /// <param name="flags"> The specifications of this api.</param>
        /// <param name="formatter"> The formatter used for formatting booru data.</param>
        /// <param name="client"> The client responsible for sending HTTP messages.</param>
        /// <param name="logger"> The logger responsible for logging notable api events.</param>
        /// <param name="container"> The cookie container used for fetching the booru specific cookies.</param>
        public AdvancedBooruBase(string baseUrl, int imageLimit, BooruFlags flags, IBooruFormatter formatter, HttpClient client, CookieContainer container, IBooruLogger logger) :
            base(baseUrl, imageLimit, flags, formatter, client, logger)
        {
            _cookieContainer = container;
        }

        /// <summary> Gets a collection of stored cookies for this booru site.</summary>
        /// <returns> a collection of stored cookies for this booru site.</returns>
        public CookieCollection GetCookies() =>
            _cookieContainer.GetCookies(BaseUrl);

        /// <summary> Sets the value of a cookie in the cookie container.</summary>
        /// <param name="cookie"> The cookie to store in the cookie container.</param>
        public void SetCookie(Cookie cookie) =>
            _cookieContainer.Add(cookie);

        /// <summary> Creates a <typeparamref name="TBooruSelfUser"/> based on the credentials used.</summary>
        /// <param name="identifier"> The identifier used for identifying the account.</param>
        /// <param name="password"> The password used for authenticating the account.</param>
        /// <returns> A self user instance on succes.</returns>
        public abstract Task<TBooruSelfUser> LoginAsync(string identifier, string password);

        /// <summary> Sets the settings for the self user.</summary>
        /// <param name="selfUser"> The user to change settings for.</param>
        /// <param name="settings"> The settings to apply.</param>
        public abstract Task SetSettingsAsync<TSettings>(TBooruSelfUser selfUser, TSettings settings) where TSettings : ISettingsHolder;

        /// <summary> Sets the tags for a post.</summary>
        /// <param name="post"> The post to set the tags for.</param>
        /// <param name="selfUser"> The user to set the tags.</param>
        /// <param name="tags"> The tags to attach to the post.</param>
        public abstract Task SetTagsAsync(TBooruPost post, TBooruSelfUser selfUser, IEnumerable<string> tags);

        /// <summary> Adds the post to the user's list of favorites</summary>
        /// <param name="post"> The post to add to the user's list of favorites</param>
        /// <param name="selfUser"> The user to add the favorite to.</param>
        /// <returns> True on succes, False on failure.</returns>
        public abstract Task<bool> AddToFavoritesAsync(TBooruPost post, TBooruSelfUser selfUser);

        /// <summary> Removes the post from the user's list of favorites.</summary>
        /// <param name="post"> The post to remove from the user's list of favorites.</param>
        /// <param name="selfUser"> The user to remove the favorite from.</param>
        /// <returns> True on succes, False on failure.</returns>
        public abstract Task<bool> RemoveFromFavoritesAsync(TBooruPost post, TBooruSelfUser selfUser);

        /// <summary> Adds a comment to the post.</summary>
        /// <param name="post"> The post to add a comment to.</param>
        /// <param name="selfUser"> The user to post the comment.</param>
        /// <param name="comment"> The content of the comment.</param>
        /// <param name="isAnonymous"> Determines if the comment should be posted as Anonymous.</param>
        public abstract Task AddCommentAsync(TBooruPost post, TBooruSelfUser selfUser, string comment, bool isAnonymous = false);
    }
}
