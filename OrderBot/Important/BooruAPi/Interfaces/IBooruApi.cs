using BooruAPI.Core.Common;
using BooruAPI.Core.Utilities;
using System;
using System.Net.Http;

namespace BooruAPI.Core
{
    /// <summary> A representation of a booru api.</summary>
    public interface IBooruApi
    {
        /// <summary> The client responsible for sending HTTP messages.</summary>
        HttpClient Client { get; }

        /// <summary> The base url used for the requests.</summary>
        Uri BaseUrl { get; }

        /// <summary> The limit of images that can be fetched by a query.</summary>
        int ImageLimit { get; }

        /// <summary> The specifications of this api.</summary>
        BooruFlags Flags { get; }

        /// <summary> The logger responsible for logging notable api events.</summary>
        IBooruLogger Logger { get; }
    }
}
