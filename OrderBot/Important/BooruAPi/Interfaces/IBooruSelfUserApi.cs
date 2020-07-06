using System.Threading.Tasks;

namespace BooruAPI.Core
{
    /// <summary> A representation of a booru API responsible for routing self user related calls.</summary>
    /// <typeparam name="TBooru"> The API to use for routing.</typeparam>
    /// <typeparam name="TBooruSelfUser"> The self user type to use.</typeparam>
    public interface IBooruSelfUserApi<TBooru, TBooruSelfUser> : IAdvancedBooruApi<TBooru>
        where TBooru : IBooruSelfUserApi<TBooru, TBooruSelfUser>
        where TBooruSelfUser : IBooruSelfUser<TBooru, TBooruSelfUser>
    {
        /// <summary> Creates a <typeparamref name="TBooruSelfUser"/> based on the credentials used.</summary>
        /// <param name="identifier"> The identifier used for identifying the account.</param>
        /// <param name="password"> The password used for authenticating the account.</param>
        /// <returns> A self user instance on succes.</returns>
        Task<TBooruSelfUser> LoginAsync(string identifier, string password);

        /// <summary> Sets the settings for the self user.</summary>
        /// <param name="user"> The user to change settings for.</param>
        /// <param name="settings"> The settings to apply.</param>
        Task SetSettingsAsync<TSettings>(TBooruSelfUser user, TSettings settings) where TSettings : ISettingsHolder;
    }
}
