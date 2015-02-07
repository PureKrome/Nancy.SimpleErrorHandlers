using Nancy.Testing;
using Shouldly;
using Xunit;

namespace Nancy.SimpleErrorHandlers.Tests
{
    public class TestModuleFacts
    {
        public class GetSimpleErrorFacts
        {
            [Fact]
            public void GivenAGetRequest_GetSimpleError_ReturnsAJsonResult()
            {
                // Arrange.
                var bootstrapper = new TestBootstrapper();
                var browser = new Browser(bootstrapper);

                // Act.
                var result = browser.Get("/simpleError", with => { with.HttpRequest(); });

                // Assert.
                result.StatusCode.ShouldBe(HttpStatusCode.InternalServerError);
                result.ContentType.ShouldBe("application/json; charset=utf-8");

                var model = result.Body.DeserializeJson<JsonErrorViewModel>();
                model.Message.ShouldBe("Oh noes! This isn't implemented :(");
                model.Errors.ShouldBe(null);
            }

            [Fact]
            public void GivenAGetRequestAndIncludeSomeSecretInformation_GetSimpleError_ReturnsAJsonResult()
            {
                // Arrange.
                var bootstrapper = new TestBootstrapper(true);
                var browser = new Browser(bootstrapper);

                // Act.
                var result = browser.Get("/simpleError");

                // Assert.
                result.StatusCode.ShouldBe(HttpStatusCode.InternalServerError);
                result.ContentType.ShouldBe("application/json; charset=utf-8");

                var model = result.Body.DeserializeJson<JsonFullErrorViewModel>();
                model.Message.ShouldBe("Oh noes! This isn't implemented :(");
                model.Errors.ShouldBe(null);
                model.StackTrace.ShouldNotBeEmpty();
                model.Source.ShouldNotBeEmpty();
            }
        }

        public class GetComplexErrorFacts
        {
            [Fact]
            public void GivenAGetRequest_GetComplexError_ReturnsAJsonResult()
            {
                // Arrange.
                var bootstrapper = new TestBootstrapper();
                var browser = new Browser(bootstrapper);

                // Act.
                var result = browser.Get("/complexError");

                // Assert.
                result.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
                result.ContentType.ShouldBe("application/json; charset=utf-8");

                var model = result.Body.DeserializeJson<JsonErrorViewModel>();
                model.Message.ShouldBe("This is an error message");
                model.Errors["Name"].ShouldBe("name contains some invalid characters");
                model.Errors["Age"].ShouldBe("age has to be a number greater than 0");
            }
        }
    }
}