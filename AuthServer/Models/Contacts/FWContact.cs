namespace AuthServer.Models.Contacts
{
    public class FWContact
    {
        public Guid Id { get; set; }
        public string Address { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}
