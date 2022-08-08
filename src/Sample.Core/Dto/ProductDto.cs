using Sample.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Core.Dto
{
    public record ProductDto(int Id, string Name)
    {
        public ProductDto(Product product) : this( product.Id, product.Name)
        {

        }
    }


}
