namespace AuthServer.Models.Responce
{
    public class ErrorResponce
    {
        public IEnumerable<string>? ErrorMessages { get; set; }

        public ErrorResponce(string errorMessage) : this(new List<string>() { errorMessage })
        {

        }

        public ErrorResponce(IEnumerable<string> errorMessages)
        {
            ErrorMessages = errorMessages;
        }
    }
}
