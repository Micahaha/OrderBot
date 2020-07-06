using System;

namespace BooruAPI.Core.Utilities
{
    /// <summary> Provides functions to help handle UNIX.</summary>
    public static class UnixUtility
    {
        /// <summary> The start date of the UNIX time system.</summary>
        public readonly static DateTime UNIX_EPOCH = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        private const int TICKS_PER_SECOND = 10000000;

        /// <summary> Creates a UNIX timestamp based on the <see cref="DateTime"/>'s current time.</summary>
        /// <param name="dateTime"> The moment in time to convert to a unix timestamp.</param>
        /// <returns> A UNIX timestamp matching the provided <paramref name="dateTime"/>.</returns>
        public static long ToUnixTimestamp(this DateTime dateTime)
        {
            var utcDateTime = dateTime.ToUniversalTime();
            if (utcDateTime < UNIX_EPOCH)
                throw new ArgumentException($"{nameof(dateTime)} cannot be sooner than the UNIX epoch.");

            return (long)(utcDateTime - UNIX_EPOCH).TotalSeconds;
        }

        /// <summary> Creates a new <see cref="DateTime"/> based on the UNIX timestamp.</summary>
        /// <param name="unixTimestamp"> The moment in time in the UNIX time format.</param>
        /// <returns> A new <see cref="DateTime"/> instance based on the <paramref name="unixTimestamp"/>.</returns>
        public static DateTime ConvertUnixToDateTime(long unixTimestamp) =>
            UNIX_EPOCH.AddTicks(unixTimestamp * TICKS_PER_SECOND);

        /// <summary> Creates a new <see cref="DateTime"/> based on the UNIX timestamp with a specific <see cref="DateTimeKind"/>.</summary>
        /// <param name="unixTimestamp"> The moment in time in the UNIX time format.</param>
        /// <param name="kind"> Determines what kind the created <see cref="DateTime"/> will be.</param>
        /// <returns> A new <see cref="DateTime"/> instance with a specified <see cref="DateTimeKind"/> based on the <paramref name="unixTimestamp"/>.</returns>
        public static DateTime ConvertUnixToDateTime(long unixTimestamp, DateTimeKind kind) =>
            new DateTime(ConvertUnixToDateTime(unixTimestamp).Ticks, kind);
    }
}
