namespace AuthServer.Interfaces
{
    public interface IContentModel<T>
    {
        IEnumerable<T> GetAllContent();
        T GetSingleContent(string? rawUserId);
        Task<HttpResponseMessage> EditContent(T content);
        Task<HttpResponseMessage> CreateContent(T content);
        Task<HttpResponseMessage> DeleteContent(string? rawUserId);
    }
}
