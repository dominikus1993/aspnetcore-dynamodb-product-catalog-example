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
        [DynamoDBProperty]
        public List<int> AvailableIn { get; set; }
        [DynamoDBProperty]
        public List<string> Ean { get; set; }

        public DynamoDbProduct()
        {

        }

        public DynamoDbProduct(int id, string name, List<int> availableIn, List<string> ean)
        {
            Id = id;
            Name = name;
            AvailableIn = availableIn;
            Ean = ean;
        }

        public DynamoDbProduct(Product product) : this(product.Id, product.Name, product.AvailableIn, product.Ean)
        {

        }

        public Product MapToProduct()
        {
            return new Product(Id, Name, AvailableIn, Ean);
        }
    }
}
