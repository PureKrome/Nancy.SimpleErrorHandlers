using Nancy.Bootstrapper;
using Nancy.TinyIoc;

namespace Nancy.SimpleErrorHandlers.Tests
{
    public class JsonErrorsBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            pipelines.AnyExceptionAsJson();
        }
    }
}