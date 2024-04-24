namespace AuthServer.Models
{
    public class FileModel
    {
        public string FileName { get; set; } = string.Empty;
        public byte[] TargetFile { get; set; }        
        
    }
}
