using Sample.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Api.Responses
{
    public record ProductResponse(int Id, string Name)
    {
        public ProductResponse(ProductDto product) : this(product.Id, product.Name)
        {

        }
    }
}
