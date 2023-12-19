namespace HelpfulHaversack.Web.Util
{
    public class StaticDetails
    {
        public static string CouponApiBase { get; set; } = String.Empty;

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
