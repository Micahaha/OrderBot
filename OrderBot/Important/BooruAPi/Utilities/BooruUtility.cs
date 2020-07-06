using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web;

namespace BooruAPI.Core.Utilities
{
    /// <summary> A helper containing functions useful for booru related actions.</summary>
    public static class BooruUtility
    {
        /// <summary> Turns a collection of tags into a URL encoded string to be used as a search filter argument.</summary>
        /// <param name="tags"> The tags to use inside the argument string.</param>
        /// <returns> A collection of tags into a URL encoded string to be used as a search filter argument.</returns>
        public static string BuildTaghQuery(params string[] tags)
        {
            StringBuilder tagBuilder = new StringBuilder();

            if (tags?.Length > 0)
            {
                tagBuilder.Append(tags[0]);

                for (int i = 1; i < tags.Length; i++)
                {
                    tagBuilder.Append('+' + HttpUtility.UrlEncode(tags[i]));
                }
            }

            return tagBuilder.ToString();
        }

        /// <summary> Forms a cookie instance based on a string.</summary>
        /// <param name="cookieString"> The string containing the information of the cookie.</param>
        /// <returns> A cookie with values from the string.</returns>
        public static Cookie GetCookieFromString(string cookieString)
        {
            var cookie = new Cookie();

            var cookieAssignments = cookieString.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var cookieAssignment in cookieAssignments)
            {
                string[] keyValuePair = cookieAssignment.Trim().Split('=');
                switch (keyValuePair[0])
                {
                    case "domain":
                        cookie.Domain = keyValuePair[1];
                        break;
                    case "path":
                        cookie.Path = keyValuePair[1];
                        break;
                    case "HttpOnly":
                        cookie.HttpOnly = true;
                        break;
                    case "expires":
                        cookie.Expires = DateTime.TryParseExact(keyValuePair[1], "ddd, dd-MMM-yy HH:mm:ss GMT", null, System.Globalization.DateTimeStyles.None, out var date) ?
                            date :
                            DateTime.ParseExact(keyValuePair[1], "ddd, dd-MMM-yyyy HH:mm:ss GMT", null);
                        break;
                    default:
                        if (string.IsNullOrEmpty(cookie.Name) && string.IsNullOrEmpty(cookie.Value))
                        {
                            cookie.Name = keyValuePair[0];
                            cookie.Value = keyValuePair[1];
                        }
                        break;
                }
            }

            return cookie;
        }

        /// <summary> Forms a cookie instance based on a string.</summary>
        /// <param name="cookieString"> The string containing the information of the cookie.</param>
        /// <param name="domain"> The domain the cookie should be assigned to.</param>
        /// <returns> A cookie with values from the string.</returns>
        public static Cookie GetCookieFromString(string cookieString, string domain)
        {
            var cookie = new Cookie
            {
                Domain = domain
            };

            var cookieAssignments = cookieString.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var cookieAssignment in cookieAssignments)
            {
                string[] keyValuePair = cookieAssignment.Trim().Split('=');
                switch (keyValuePair[0])
                {
                    case "domain":
                        break;
                    case "path":
                        cookie.Path = keyValuePair[1];
                        break;
                    case "HttpOnly":
                        cookie.HttpOnly = true;
                        break;
                    case "expires":
                        cookie.Expires = DateTime.TryParseExact(keyValuePair[1], "ddd, dd-MMM-yy HH:mm:ss GMT", null, System.Globalization.DateTimeStyles.None, out var date) ?
                            date :
                            DateTime.ParseExact(keyValuePair[1], "ddd, dd-MMM-yyyy HH:mm:ss GMT", null);
                        break;
                    default:
                        if (string.IsNullOrEmpty(cookie.Name) && string.IsNullOrEmpty(cookie.Value))
                        {
                            cookie.Name = keyValuePair[0];
                            cookie.Value = keyValuePair[1];
                        }
                        break;
                }
            }

            return cookie;
        }

        /// <summary> Forms a cookie instance based on a string.</summary>
        /// <param name="cookieString"> The string containing the information of the cookie.</param>
        /// <param name="domain"> The domain the cookie should be assigned to.</param>
        /// <param name="path"> The path this cookie should be assigned to.</param>
        /// <returns> A cookie with values from the string.</returns>
        public static Cookie GetCookieFromString(string cookieString, string domain, string path)
        {
            var cookie = new Cookie
            {
                Domain = domain,
                Path = path
            };

            var cookieAssignments = cookieString.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var cookieAssignment in cookieAssignments)
            {
                string[] keyValuePair = cookieAssignment.Trim().Split('=');
                switch (keyValuePair[0])
                {
                    case "domain":
                    case "path":
                        break;
                    case "HttpOnly":
                        cookie.HttpOnly = true;
                        break;
                    case "expires":
                        cookie.Expires = DateTime.TryParseExact(keyValuePair[1], "ddd, dd-MMM-yy HH:mm:ss GMT", null, System.Globalization.DateTimeStyles.None, out var date) ?
                            date :
                            DateTime.ParseExact(keyValuePair[1], "ddd, dd-MMM-yyyy HH:mm:ss GMT", null);
                        break;
                    default:
                        if (string.IsNullOrEmpty(cookie.Name) && string.IsNullOrEmpty(cookie.Value))
                        {
                            cookie.Name = keyValuePair[0];
                            cookie.Value = keyValuePair[1];
                        }
                        break;
                }
            }

            return cookie;
        }

        /// <summary> Unrolls an array of tags into a space seperated string of tags.</summary>
        /// <param name="tags"> The tags to be unrolled.</param>
        public static string UnrollTags(IEnumerable<string> tags)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var tag in tags)
            {
                sb.Append(' ').Append(tag);
            }

            if (sb.Length > 0)
                sb.Remove(0, 1);

            return sb.ToString();
        }
    }
}
