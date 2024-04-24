namespace FW_ASP.Net.Models
{
    public static class CustomUser
    {
        public static bool IsAuthenticated { get; set; }
        public static string UserName { get; set; } = string.Empty;
    }
}
