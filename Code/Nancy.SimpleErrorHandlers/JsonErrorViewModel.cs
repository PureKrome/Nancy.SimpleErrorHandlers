using System.Collections.Generic;

namespace Nancy.SimpleErrorHandlers
{
    public class JsonErrorViewModel
    {
        public string Message { get; set; }
        public IDictionary<string, string> Errors { get; set; }
    }
}