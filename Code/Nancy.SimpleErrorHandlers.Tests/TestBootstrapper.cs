using Nancy.Bootstrapper;
using Nancy.TinyIoc;

namespace Nancy.SimpleErrorHandlers.Tests
{
    public class TestBootstrapper : DefaultNancyBootstrapper
    {
        private readonly bool _includeSecretInformation;

        public TestBootstrapper(bool includeSecretInformation = false)
        {
            _includeSecretInformation = includeSecretInformation;
        }

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            pipelines.IncludeExtraInformationInJsonErrorExceptions(_includeSecretInformation);
        }
    }
}