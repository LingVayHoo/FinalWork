using AuthServer.Models.Requests;

namespace AuthServer.Interfaces
{
    public interface IAuth
    {
        FWRegisterRequest GetFWRegisterRequest();
        Task<HttpResponseMessage> PostRegister(FWRegisterRequest fWRegisterRequest);
        FWLoginRequest GetFWLoginRequest();
        Task<HttpResponseMessage> PostLogin(FWLoginRequest fWLoginRequest);

        Task<HttpResponseMessage> Logout(string token, string id);
        IEnumerable<WeatherForecast> GetData(string token);

    }
}
