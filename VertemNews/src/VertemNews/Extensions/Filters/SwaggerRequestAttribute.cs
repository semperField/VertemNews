using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace VertemNews.Extensions.Annotations
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class SwaggerRequestAttribute : Attribute, IFilterMetadata
    {
        public Type Type { get; set; }

        public SwaggerRequestAttribute(Type type)
        {
            Type = type;
        }
    }
}
