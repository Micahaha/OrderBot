using System.Threading.Tasks;

namespace BooruAPI.Core
{
    /// <summary> The extension mechanism to add user based searches.</summary>
    public static class IUserInfoBooruApiExtension
    {
        /// <summary> Get the additional user information using an id as filter.</summary>
        /// <typeparam name="TBooru"> The API to use for routing.</typeparam>
        /// <typeparam name="TBooruSelfUser"> The self user type to use.</typeparam>
        /// <typeparam name="TBooruUser"> The user type to use.</typeparam>
        /// <param name="booruApi"> The api to use for the call.</param>
        /// <param name="user"> The user to get the id from.</param>
        /// <returns> The additional user information.</returns>
        public static async Task<TBooruUser> GetUserInfoByIdAsync<TBooru, TBooruSelfUser, TBooruUser>(this IBooruSelfUserInfoApi<TBooru, TBooruSelfUser, TBooruUser> booruApi, IBooruUser<TBooru> user)
            where TBooru : IBooruSelfUserApi<TBooru, TBooruSelfUser>
            where TBooruSelfUser : IBooruSelfUser<TBooru, TBooruSelfUser>
            where TBooruUser : IBooruUserInfo<TBooru> =>
                await booruApi.GetUserInfoByIdAsync(user.UserId);

        /// <summary> Get the additional user information using a username as filter.</summary>
        /// <typeparam name="TBooru"> The API to use for routing.</typeparam>
        /// <typeparam name="TBooruSelfUser"> The self user type to use.</typeparam>
        /// <typeparam name="TBooruUser"> The user type to use.</typeparam>
        /// <param name="booruApi"> The api to use for the call.</param>
        /// <param name="userInfo"> The user information to get the username from.</param>
        /// <returns> The additional user information.</returns>
        public static async Task<TBooruUser> GetUserInfoByUsernameAsync<TBooru, TBooruSelfUser, TBooruUser>(this IBooruSelfUserInfoApi<TBooru, TBooruSelfUser, TBooruUser> booruApi, IBooruUserInfo<TBooru> userInfo)
            where TBooru : IBooruSelfUserApi<TBooru, TBooruSelfUser>
            where TBooruSelfUser : IBooruSelfUser<TBooru, TBooruSelfUser>
            where TBooruUser : IBooruUserInfo<TBooru> =>
                await booruApi.GetUserInfoByUsernameAsync(userInfo.Username);
    }
}
