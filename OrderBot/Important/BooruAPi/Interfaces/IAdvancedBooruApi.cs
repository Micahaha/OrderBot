using System.Net;

namespace BooruAPI.Core
{
    /// <summary> A representation of a booru API for setting up non-standard API calls.</summary>
    /// <typeparam name="TBooru"> The API to use.</typeparam>
    public interface IAdvancedBooruApi<TBooru> : IBooruPostApi<TBooru>
        where TBooru : IAdvancedBooruApi<TBooru>
    {
        /// <summary> Gets a collection of stored cookies for this booru site.</summary>
        /// <returns> a collection of stored cookies for this booru site.</returns>
        CookieCollection GetCookies();

        /// <summary> Sets the value of a cookie in the cookie container.</summary>
        /// <param name="cookie"> The cookie to store in the cookie container.</param>
        void SetCookie(Cookie cookie);
    }
}
