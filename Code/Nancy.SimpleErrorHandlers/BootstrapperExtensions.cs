using Nancy.Bootstrapper;

namespace Nancy.SimpleErrorHandlers
{
    public static class BootstrapperExtensions
    {
        internal const string IncludeExtraInformationForExceptionsKey = "IncludeExtraInformationForExceptionsKey";

        /// <summary>
        /// Includes any extra information in json error exceptions.
        /// </summary>
        /// <param name="pipelines"></param>
        /// <param name="includeExtraInformation">Should we include secret information in the output?</param>
        /// <remarks>Extra information includes stacktrace, etc. Default = false.</remarks>
        public static void IncludeExtraInformationInJsonErrorExceptions(this IPipelines pipelines,
            bool includeExtraInformation = false)
        {
            pipelines.OnError += (context, exception) =>
            {
                context.IncludeSecretInformationForJsonErrorExceptions(includeExtraInformation);
                return null;
            };
        }
    }
}