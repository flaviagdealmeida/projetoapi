using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Swagger;

namespace Projeto.Services
{
    public class AddHeaderParameter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<IParameter>();

            operation.Parameters.Add(new HeaderParameter
            {
                Name = "Authorization",
                In = "header",
                Type = "string",
                Required = false
            });
        }
    }

    class HeaderParameter : NonBodyParameter
    {

    }
}
