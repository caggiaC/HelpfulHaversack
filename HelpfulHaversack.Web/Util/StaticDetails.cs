namespace HelpfulHaversack.Web.Util
{
    public static class StaticDetails
    {
        public static string TreasuryApiBase { get; set; } = String.Empty;

        public enum ApiType
        {
            GET,
            POST,
            PUT,
            PATCH,
            DELETE
        }
    }
}
