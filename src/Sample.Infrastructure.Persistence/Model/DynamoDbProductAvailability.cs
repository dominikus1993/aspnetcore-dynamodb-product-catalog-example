using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Infrastructure.Persistence.Model
{
    [DynamoDBTable("ProductCatalogAvailability")]
    internal class DynamoDbProductAvailability
    {
        [DynamoDBHashKey]
        public int ShopId { get; set; }

        [DynamoDBRangeKey]
        public int ProductId { get; set; }
    }
}
