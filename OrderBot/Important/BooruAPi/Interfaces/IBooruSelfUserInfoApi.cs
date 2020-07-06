using System.Threading.Tasks;

namespace BooruAPI.Core
{
    /// <summary> The interface for an API object responsible for routing user information related calls.</summary>
    /// <typeparam name="TBooru"> The API to use for routing.</typeparam>
    /// <typeparam name="TBooruSelfUser"> The self user type to use.</typeparam>
    /// <typeparam name="TBooruUser"> The user type to use.</typeparam>
    public interface IBooruSelfUserInfoApi<TBooru, TBooruSelfUser, TBooruUser> : IBooruSelfUserApi<TBooru, TBooruSelfUser>
        where TBooru : IBooruSelfUserApi<TBooru, TBooruSelfUser>
        where TBooruSelfUser : IBooruSelfUser<TBooru, TBooruSelfUser>
        where TBooruUser : IBooruUserInfo<TBooru>
    {
        /// <summary> Get the additional user information using an id as filter.</summary>
        /// <param name="id">The id of the user.</param>
        /// <returns> The additional user information.</returns>
        Task<TBooruUser> GetUserInfoByIdAsync(int id);

        /// <summary> Get the additional user information using a username as a filter.</summary>
        /// <param name="username"> The username of the user.</param>
        /// <returns> The additional user information.</returns>
        Task<TBooruUser> GetUserInfoByUsernameAsync(string username);

        /// <summary> Get the additional user information using a query as a filter.</summary>
        /// <param name="query"> The filter to use.</param>
        /// <returns> The additional user information.</returns>
        Task<TBooruUser> GetUserInfoAsync(string query);
    }
}
