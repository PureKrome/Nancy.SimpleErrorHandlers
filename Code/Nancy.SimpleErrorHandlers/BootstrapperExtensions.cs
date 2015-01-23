using Nancy.Bootstrapper;

namespace Nancy.SimpleErrorHandlers
{
    public static class BootstrapperExtensions
    {
        internal const string AnyExceptionAsJsonKey = "AnyExceptionAsJsonKey";

        /// <summary>
        /// Wires up any exception to be caught and then rendered as a Json response.
        /// </summary>
        /// <param name="pipelines"></param>
        public static void AnyExceptionAsJson(this IPipelines pipelines)
        {
            pipelines.OnError += (context, exception) =>
            {
                context.Items.Add(AnyExceptionAsJsonKey, exception);

                return null;
            };
        }
    }
}