namespace AuthServer.Models.Auth
{
    public class CustomRole
    {
        public enum Roles
        {
            Admin,
            Guest
        }
        public Guid Id { get; set; }
        public Roles RoleName { get; set; }
    }
}
