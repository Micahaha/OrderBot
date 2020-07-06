using System.Threading.Tasks;

namespace BooruAPI.Core
{
    /// <summary> A representation of a booru user.</summary>
    public interface IBooruUser
    {
        /// <summary> The identifier of the user.</summary>
        int UserId { get; }
    }

    /// <summary> A representation of a booru user.</summary>
    /// <typeparam name="TBooru"> The API to use for routing calls.</typeparam>
    public interface IBooruUser<TBooru> : IBooruUser
        where TBooru : IBooruPostApi<TBooru>
    { }

    /// <summary> A representation of a booru user with user info API calls.</summary>
    /// <typeparam name="TBooru"> The API to use for routing calls.</typeparam>
    /// <typeparam name="TBooruSelfUser"> The self user type to use.</typeparam>
    /// <typeparam name="TBooruUserInfo"> The user info type to use.</typeparam>
    public interface IBooruUser<TBooru, TBooruSelfUser, TBooruUserInfo> : IBooruUser<TBooru>
        where TBooru : IBooruSelfUserInfoApi<TBooru, TBooruSelfUser, TBooruUserInfo>
        where TBooruSelfUser : IBooruSelfUser<TBooru, TBooruSelfUser>
        where TBooruUserInfo : IBooruUserInfo<TBooru>
    {
        /// <summary> Gets the user info of this user.</summary>
        /// <param name="booruApi"> The api to use for calls.</param>
        /// <returns> The user info of this user.</returns>
        Task<TBooruUserInfo> GetUserInfoAsync(TBooru booruApi);
    }
}
