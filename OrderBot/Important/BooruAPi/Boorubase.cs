using BooruAPI.Core.Common;
using BooruAPI.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BooruAPI.Core
{
    /// <summary> A base implementation of handing standard booru API calls.</summary>
    /// <typeparam name="TBooru"> The api to use for calls.</typeparam>
    /// <typeparam name="TBooruPost"> The post type to use.</typeparam>
    /// <typeparam name="TBooruComment"> The comment type to use.</typeparam>
    public abstract class BooruBase<TBooru, TBooruPost, TBooruComment> : IBooruPostApi<TBooru, TBooruPost>, IBooruCommentApi<TBooru, TBooruComment>
        where TBooru : BooruBase<TBooru, TBooruPost, TBooruComment>
        where TBooruPost : IBooruPost<TBooru, TBooruPost>
        where TBooruComment : IBooruComment<TBooru, TBooruPost>
    {
        /// <summary> The client responsible for sending HTTP messages.</summary>
        public HttpClient Client { get; }

        /// <summary> The base url used for the requests.</summary>
        public Uri BaseUrl { get; }

        /// <summary> The limit of images that can be fetched by a query.</summary>
        public int ImageLimit { get; }

        /// <summary> The specifications of this api.</summary>
        public BooruFlags Flags { get; }

        /// <summary> The logger responsible for logging notable api events.</summary>
        public IBooruLogger Logger { get; }

        /// <summary> The formatter used for formatting booru data.</summary>
        protected IBooruFormatter Formatter { get; }

        /// <summary> Sets up the base booru api.</summary>
        /// <param name="baseUrl"> The base url used for the requests.</param>
        /// <param name="imageLimit"> The limit of images that can be fetched by a query.</param>
        /// <param name="flags"> The specifications of this api.</param>
        /// <param name="formatter"> The formatter used for formatting booru data.</param>
        /// <param name="client"> The client responsible for sending HTTP messages.</param>
        /// <param name="logger"> The logger responsible for logging notable api events.</param>
        public BooruBase(string baseUrl, int imageLimit, BooruFlags flags, IBooruFormatter formatter, HttpClient client, IBooruLogger logger)
        {
            BaseUrl = new UriBuilder(baseUrl)
            {
                Scheme = flags.HasFlag(BooruFlags.HasSSL) ? Uri.UriSchemeHttps : Uri.UriSchemeHttp
            }.Uri;

            ImageLimit = imageLimit;
            Logger = logger;
            Flags = flags;
            Formatter = formatter ?? throw new ArgumentNullException(nameof(formatter));
            Client = client;
        }

        /// <summary> Gets the ammount of posts that can be fetched with the search query.</summary>
        /// <param name="tagQuery"> The query to use for filtering posts.</param>
        /// <returns> The ammount of posts that can be fetched with the search query.</returns>
        public virtual async Task<int> GetPostCountAsync(string tagQuery)
        {
            string requestUrl = Formatter.FormCountQuery(BaseUrl.AbsoluteUri, tagQuery);

            HttpResponseMessage response;
            try
            {
                response = await Client.GetAsync(requestUrl);
            }
            catch
            {
                return (int)ApiErrorCode.NoConnection;
            }

            int count;

            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    using (var stream = await response.Content.ReadAsStreamAsync())
                    {
                        count = Formatter.FindPostCount(stream);
                    }
                    break;
                case System.Net.HttpStatusCode.MethodNotAllowed:
                    count = (int)ApiErrorCode.NotAllowed;
                    break;
                case System.Net.HttpStatusCode.BadRequest:
                    count = (int)ApiErrorCode.BadRequest;
                    break;

                default:
                    count = (int)ApiErrorCode.Generic;
                    break;
            }

            response.Dispose();

            return count;
        }

        /// <summary> Gets a page of posts with the given filter query.</summary>
        /// <param name="tagQuery"> The query used for filtering posts.</param>
        /// <param name="pageNumber"> The page number to fetch.</param>
        /// <param name="requestLimit"> The maximum amount of posts to fatch per page.</param>
        /// <returns> A page of posts.</returns>
        public abstract Task<List<TBooruPost>> GetSearchPageAsync(string tagQuery, int pageNumber = 0, int requestLimit = 64);

        /// <summary> Gets the first post with the specific filtering query.</summary>
        /// <param name="tagQuery"> The query used for filtering.</param>
        /// <returns>The first post with the filter query.</returns>
        public async Task<TBooruPost> GetPostAsync(string tagQuery)
        {
            var postCollection = await GetSearchPageAsync(tagQuery, 0, 1);

            if (postCollection?.Count > 0)
                return postCollection[0];

            Logger?.LogApiError(ApiErrorCode.NoResulsForQuery, this);
            return default;
        }

        /// <summary> Gets all posts with the given filter query.</summary>
        /// <param name="tagQuery"> The query used for filtering posts.</param>
        /// <returns> A collection of posts.</returns>
        /// <remarks> The resulting amount of posts can be less than the post count due to the request limit.</remarks>
        public async virtual IAsyncEnumerable<TBooruPost> GetPostsAsync(string tagQuery)
        {
            const int REQUEST_LIMIT = 64;

            int retrievableCount = await GetPostCountAsync(tagQuery);
            if (retrievableCount <= 0)
            {
                Logger?.LogApiError((ApiErrorCode)retrievableCount, this);
                yield break;
            }

            if (retrievableCount > ImageLimit)
                retrievableCount = ImageLimit;

            int pageCount = ((retrievableCount - 1) / REQUEST_LIMIT);

            for (int i = 0; i < pageCount; i++)
            {
                var posts = await GetSearchPageAsync(tagQuery, i, REQUEST_LIMIT);
                if (posts == null || posts.Count == 0)
                {
                    Logger?.LogApiError(ApiErrorCode.Generic, this, $"Payload [{i}] is empty");
                    continue;
                }

                foreach (var post in posts)
                    yield return post;
            }
        }

        /// <summary> Gets The comments on a post.</summary>
        /// <param name="postId"> The post id </param>
        /// <returns> Collection of comments on the post</returns>
        public abstract Task<List<TBooruComment>> GetPostCommentsAsync(int postId);
    }
}
