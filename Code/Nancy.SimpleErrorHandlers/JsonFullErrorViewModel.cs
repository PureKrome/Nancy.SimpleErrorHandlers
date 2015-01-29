namespace Nancy.SimpleErrorHandlers
{
    public class JsonFullErrorViewModel : JsonErrorViewModel
    {
        public string StackTrace { get; set; }
        public string Source { get; set; }
    }
}