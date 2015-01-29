using Nancy.Bootstrapper;

namespace Nancy.SimpleErrorHandlers
{
    public static class BootstrapperExtensions
    {
        internal const string AnyExceptionAsJsonKey = "AnyExceptionAsJsonKey";
        internal const string IncludeSecretInformationForExceptionsKey = "IncludeSecretInformationForExceptionsKey";

        /// <summary>
        /// Wires up any exception to be caught and then rendered as a Json response.
        /// </summary>
        /// <param name="pipelines"></param>
        /// <param name="includeSecretInformation">Should we include secret information in the output?</param>
        /// <remarks>Secret information include stacktrace, etc. Default = false.</remarks>
        public static void AnyExceptionAsJson(this IPipelines pipelines,
            bool includeSecretInformation = false)
        {
            pipelines.OnError += (context, exception) =>
            {
                context.Items.Add(AnyExceptionAsJsonKey, exception);
                context.IncludeSecretInformationForJsonErrorExceptions(includeSecretInformation);
                return null;
            };
        }
    }
}