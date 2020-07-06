using System.Collections.Generic;

namespace BooruAPI.Core.Extensions
{
    /// <summary> The extension mechanism to add user based searches.</summary>
    public static class IUserFavoritePostBooruApi
    {
        /// <summary> Gets a page of favorite posts of a specified user.</summary>
        /// <typeparam name="TBooru"> The api to use for calls.</typeparam>
        /// <typeparam name="TBooruSelfUser"> The self user type to use.</typeparam>
        /// <typeparam name="TBooruPost"> The post type use.</typeparam>
        /// <typeparam name="TBooruComment"> The comment type to use.</typeparam>
        /// <param name="booruApi"> The api to use for the call.</param>
        /// <param name="user"> The user to get the id from.</param>
        /// <param name="skippedPosts"> The amount of posts to skip for the favorite page.</param>
        /// <returns> A collection of favorite posts on one page.</returns>
        public static IAsyncEnumerable<TBooruPost> GetUserFavoritePageByIdAsync<TBooru, TBooruSelfUser, TBooruPost, TBooruComment>(this IUserFavoritePostBooruApi<TBooru, TBooruSelfUser, TBooruPost> booruApi, IBooruUser<TBooru> user, int skippedPosts = 0)
            where TBooru : IBooruSelfUserApi<TBooru, TBooruSelfUser>, IBooruPostApi<TBooru, TBooruPost>
            where TBooruSelfUser : IBooruSelfUser<TBooru, TBooruSelfUser>
            where TBooruPost : IBooruPost<TBooru, TBooruPost>
            where TBooruComment : IBooruComment<TBooru> =>
                booruApi.GetUserFavoritePageByIdAsync(user.UserId, skippedPosts);

        /// <summary> Gets a page of favorite posts of a specified user.</summary>
        /// <typeparam name="TBooru"> The api to use for calls.</typeparam>
        /// <typeparam name="TBooruSelfUser"> The self user type to use.</typeparam>
        /// <typeparam name="TBooruPost"> The post type use.</typeparam>
        /// <param name="booruApi"> The api to use for the call.</param>
        /// <param name="userInfo"> The user information to get the username from.</param>
        /// <param name="skippedPosts"> The amount of posts to skip for the favorite page.</param>
        /// <returns> A collection of favorite posts on one page.</returns>
        public static IAsyncEnumerable<TBooruPost> GetUserFavoritePageByUsernameAsync<TBooru, TBooruSelfUser, TBooruPost>(this IUserFavoritePostBooruApi<TBooru, TBooruSelfUser, TBooruPost> booruApi, IBooruUserInfo<TBooru> userInfo, int skippedPosts = 0)
            where TBooru : IBooruSelfUserApi<TBooru, TBooruSelfUser>, IBooruPostApi<TBooru, TBooruPost>
            where TBooruSelfUser : IBooruSelfUser<TBooru, TBooruSelfUser>
            where TBooruPost : IBooruPost<TBooru, TBooruPost> =>
                booruApi.GetUserFavoritePageByUsernameAsync(userInfo.Username, skippedPosts);

        /// <summary> Gets the favorite posts of a user.</summary>
        /// <typeparam name="TBooru"> The api to use for calls.</typeparam>
        /// <typeparam name="TBooruSelfUser"> The self user type to use.</typeparam>
        /// <typeparam name="TBooruPost"> The post type use.</typeparam>
        /// <param name="booruApi"> The api to use for the call.</param>
        /// <param name="user"> The user to get the id from.</param>
        /// <returns> A collection of all favorite posts of a user.</returns>
        public static IAsyncEnumerable<TBooruPost> GetUserFavoritePagesByIdAsync<TBooru, TBooruSelfUser, TBooruPost>(this IUserFavoritePostBooruApi<TBooru, TBooruSelfUser, TBooruPost> booruApi, IBooruUser<TBooru> user)
            where TBooru : IBooruSelfUserApi<TBooru, TBooruSelfUser>, IBooruPostApi<TBooru, TBooruPost>
            where TBooruSelfUser : IBooruSelfUser<TBooru, TBooruSelfUser>
            where TBooruPost : IBooruPost<TBooru, TBooruPost> =>
                booruApi.GetUserFavoritePagesByIdAsync(user.UserId);

        /// <summary> Gets the favorite posts of a user.</summary>
        /// <typeparam name="TBooru"> The api to use for calls.</typeparam>
        /// <typeparam name="TBooruSelfUser"> The self user type to use.</typeparam>
        /// <typeparam name="TBooruPost"> The post type use.</typeparam>
        /// <param name="booruApi"> The api to use for the call.</param>
        /// <param name="userInfo"> The user information to get the username from.</param>
        /// <returns> A collection of all favorite posts of a user.</returns>
        public static IAsyncEnumerable<TBooruPost> GetUserFavoritePagesByUsernameAsync<TBooru, TBooruSelfUser, TBooruPost>(this IUserFavoritePostBooruApi<TBooru, TBooruSelfUser, TBooruPost> booruApi, IBooruUserInfo<TBooru> userInfo)
            where TBooru : IBooruSelfUserApi<TBooru, TBooruSelfUser>, IBooruPostApi<TBooru, TBooruPost>
            where TBooruSelfUser : IBooruSelfUser<TBooru, TBooruSelfUser>
            where TBooruPost : IBooruPost<TBooru, TBooruPost> =>
                booruApi.GetUserFavoritePagesByUsernameAsync(userInfo.Username);
    }
}
