using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Net;

namespace VertemNews.Extensions.Filters
{
    public class ErrorFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            AddStatusCode(HttpStatusCode.Unauthorized, operation);

            AddStatusCode(HttpStatusCode.InternalServerError, operation);

            AddBadRequest(operation, context);
        }

        private static void AddStatusCode(HttpStatusCode statusCode, OpenApiOperation operation)
        {
            string key = GetKey(statusCode);

            if (operation.Responses.ContainsKey(key)) return;

            operation.Responses.Add(key, new OpenApiResponse
            {
                Description = statusCode.ToString()
            });
        }

        private static string GetKey(HttpStatusCode statusCode)
        {
            return $"{(int)statusCode}";
        }

        private static void AddBadRequest(OpenApiOperation operation, OperationFilterContext context)
        {
            var key = GetKey(HttpStatusCode.BadRequest);

            if (operation.Responses.ContainsKey(key)) return;

            var response = new OpenApiResponse
            {
                Description = HttpStatusCode.BadRequest.ToString()
            };

            response.Content.Add("application/json", new OpenApiMediaType
            {
                Schema = context.SchemaGenerator.GenerateSchema(typeof(ErrorModel), context.SchemaRepository)
            });

            operation.Responses.Add(key, response);
        }
    }
}
