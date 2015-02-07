using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy.ErrorHandling;
using Nancy.Responses;

namespace Nancy.SimpleErrorHandlers
{
    public class JsonErrorStatusCodeHandler : IStatusCodeHandler
    {
        private readonly IEnumerable<ISerializer> _serializers;
        private static ISerializer _jsonSerializer;

        public JsonErrorStatusCodeHandler(IEnumerable<ISerializer> serializers)
        {
            _serializers = serializers;
        }

        public bool HandlesStatusCode(HttpStatusCode statusCode, NancyContext context)
        {
            return statusCode == HttpStatusCode.InternalServerError;
        }

        public void Handle(HttpStatusCode statusCode, NancyContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            // We grab the exception from the normal NANCY ENGINE key we're using.
            Exception exception = null;
            if (context.Items.ContainsKey(NancyEngine.ERROR_EXCEPTION))
            {
                exception = context.Items[NancyEngine.ERROR_EXCEPTION] as Exception;
            }

            bool includeExtraInformation = false;
            if (context.Items.ContainsKey(BootstrapperExtensions.IncludeExtraInformationForExceptionsKey))
            {
                includeExtraInformation = (bool)context.Items[BootstrapperExtensions.IncludeExtraInformationForExceptionsKey];
            }

            var viewModel = includeExtraInformation
                ? new JsonFullErrorViewModel
                {
                    StackTrace = exception == null
                        ? "no stack trace"
                        : exception.StackTrace,
                    Source = exception == null
                        ? "no source"
                        : exception.Source
                }
                : new JsonErrorViewModel();

            viewModel.Message = exception == null
                ? "An error has occured but no exception was provided. So we're not sure what has happened, even though something has. :/"
                : GetLowestInnerException(exception).Message;

            var serializer = _jsonSerializer ?? 
                (_jsonSerializer = _serializers.FirstOrDefault(s => s.CanSerialize("application/json")));

            var response = new JsonResponse<JsonErrorViewModel>(viewModel,serializer)
            {
                StatusCode = statusCode
            };
            context.Response = response;
        }

        private static Exception GetLowestInnerException(Exception exception)
        {
            if (exception == null)
            {
                throw new ArgumentNullException("exception");
            }

            var lowestException = exception;
            while (exception.InnerException != null)
            {
                lowestException = exception.InnerException;
                exception = exception.InnerException;
            }

            return lowestException;
        }
    }
}