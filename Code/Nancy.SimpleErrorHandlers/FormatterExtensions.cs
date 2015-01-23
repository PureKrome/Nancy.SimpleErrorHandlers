namespace Nancy.SimpleErrorHandlers
{
    public static class FormatterExtensions
    {
        public static Response AsJsonError(this IResponseFormatter formatter,
            JsonErrorViewModel model,
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        {
            return formatter.AsJson(model, statusCode);
        }
    }
}