using AuthServer.Models.Content.Requests;
using AuthServer.Models.Title;

namespace FW_ASP.Net.Models
{
    public class MainPageModel
    {
        public FWRequest WRequest { get; set; }
        public FWTitle Title { get; set; }

        public MainPageModel()
        {
            WRequest = new FWRequest();
            Title = new FWTitle();
        }

        public MainPageModel(FWRequest wRequest, FWTitle title)
        {
            WRequest = wRequest;
            Title = title;
        }
    }
}
