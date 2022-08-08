using Amazon.DynamoDBv2.DataModel;
using Sample.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Infrastructure.Persistence.Model
{

    [DynamoDBTable("ProductCatalog")]
    internal class DynamoDbProduct
    {
        [DynamoDBHashKey]
        public int Id {get; set; }
        [DynamoDBProperty]
        public string Name { get; set; }

        public DynamoDbProduct()
        {

        }

        public DynamoDbProduct(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public DynamoDbProduct(Product product) : this(product.Id, product.Name)
        {

        }

        public Product MapToProduct()
        {
            return new Product(Id, Name);
        }
    }
}
