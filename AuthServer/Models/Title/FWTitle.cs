namespace AuthServer.Models.Title
{
    public class FWTitle
    {
        public Guid Id { get; set; }
        public string MainTitle { get; set; } = string.Empty;
        public string QuoteTitle { get; set; } = string.Empty;
        public string ButtonTitle { get; set; } = string.Empty;
    }
}
