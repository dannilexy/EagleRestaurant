namespace Eagle.Web
{
    public static class SD
    {
        public static string ProductAPIBase { get; set; }

        public enum ApiType
        {
            Get,
            Post,
            Put,
            Delete
        }

    }
}
