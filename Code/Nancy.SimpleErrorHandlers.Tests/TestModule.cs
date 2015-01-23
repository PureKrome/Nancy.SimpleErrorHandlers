using System;
using System.Collections.Generic;

namespace Nancy.SimpleErrorHandlers.Tests
{
    public class TestModule : NancyModule
    {
        public TestModule()
        {
            Get["/simpleError"] = _ => GetSimpleError();
            Get["/complexError"] = _ => GetComplexError();
        }

        private static dynamic GetSimpleError()
        {
            throw new NotImplementedException("Oh noes! This isn't implemented :(");
        }

        private dynamic GetComplexError()
        {
            var model = new JsonErrorViewModel()
            {
                Message = "This is an error message",
                Errors = new Dictionary<string, string>
                {
                    {"name", "name contains some invalid characters"},
                    {"age", "age has to be a number greater than 0"}
                }
            };

            return Response.AsJsonError(model, HttpStatusCode.BadRequest);
        }
    }
}