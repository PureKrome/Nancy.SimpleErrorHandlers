using Nancy.Bootstrapper;
using Nancy.TinyIoc;

namespace Nancy.SimpleErrorHandlers.Tests
{
    public class JsonErrorsBootstrapper : DefaultNancyBootstrapper
    {
        private readonly bool _includeSecretInformation;

        public JsonErrorsBootstrapper(bool includeSecretInformation = false)
        {
            _includeSecretInformation = includeSecretInformation;
        }

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            pipelines.AnyExceptionAsJson(_includeSecretInformation);
        }
    }
}