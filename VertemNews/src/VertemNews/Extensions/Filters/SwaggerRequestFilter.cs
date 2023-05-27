using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using VertemNews.Extensions.Annotations;

namespace VertemNews.Extensions.Filters
{
    public class SwaggerRequestFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            foreach (var filterDescriptor in context.ApiDescription.ActionDescriptor.FilterDescriptors)
            {
                if (filterDescriptor.Filter is SwaggerRequestAttribute filter)
                {
                    CreateRequestBody(operation, context, filter);
                }
            }
        }

        private static void CreateRequestBody(OpenApiOperation operation, OperationFilterContext context, SwaggerRequestAttribute filter)
        {
            var requestBody = new OpenApiRequestBody();

            requestBody.Content.Add("application/json", new OpenApiMediaType
            {
                Schema = context.SchemaGenerator.GenerateSchema(filter.Type, context.SchemaRepository)
            });

            operation.RequestBody = requestBody;
        }
    }
}