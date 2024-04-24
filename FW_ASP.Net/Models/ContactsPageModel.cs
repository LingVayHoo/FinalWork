using AuthServer.Models.Contacts;
using AuthServer.Models.Title;

namespace FW_ASP.Net.Models
{
    public class ContactsPageModel(FWTitle title, FWContact contacts)
    {
        public FWTitle Title { get; set; } = title;
        public FWContact Contacts { get; set; } = contacts;
    }
}
