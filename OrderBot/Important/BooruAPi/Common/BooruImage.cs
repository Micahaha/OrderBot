namespace BooruAPI.Core.Common
{
    /// <summary> A structure containing basic information about images found on booru sites.</summary>
    public readonly struct BooruImage
    {
        /// <summary> The total width of the image.</summary>
        public int Width { get; }
        /// <summary> The total height of the image.</summary>
        public int Height { get; }
        /// <summary> The request URL leading to the image.</summary>
        public string FileUrl { get; }

        /// <summary> Constructs the basic information about images found on booru sites.</summary>
        /// <param name="width"> The total width of the image.</param>
        /// <param name="height"> The total height of the image.</param>
        /// <param name="fileUrl"> The request URL leading to the image.</param>
        public BooruImage(int width, int height, string fileUrl)
        {
            Width = width;
            Height = height;
            FileUrl = fileUrl;
        }
    }
}
