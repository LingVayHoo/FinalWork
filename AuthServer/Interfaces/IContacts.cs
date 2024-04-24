using AuthServer.Models.Contacts;

namespace AuthServer.Interfaces
{
    public interface IContacts
    {
        public FWContact GetFWContact();
        IEnumerable<FWContact> GetContacts();
        FWContact GetContact(string? rawContactId);
        Task<HttpResponseMessage> EditContact(FWContact contact);
        Task<HttpResponseMessage> CreateContact(FWContact contact);
        Task<HttpResponseMessage> DeleteContact(string? rawContactId);
    }
}
