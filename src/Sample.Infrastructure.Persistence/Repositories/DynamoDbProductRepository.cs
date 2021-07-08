using Amazon.DynamoDBv2.DataModel;
using Sample.Core.Model;
using Sample.Core.Repositories;
using Sample.Infrastructure.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.Infrastructure.Persistence.Repositories
{
    internal class DynamoDbProductRepository : IProductRepository
    {
        private IDynamoDBContext _dynamoDBContext;

        public DynamoDbProductRepository(IDynamoDBContext dynamoDBContext)
        {
            _dynamoDBContext = dynamoDBContext;
        }

        public async Task<Product?> GetProduct(int id, int shopNumber)
        {
            var product = await _dynamoDBContext.LoadAsync<DynamoDbProduct>(id);
            if (product is null)
            {
                return null;
            }
            return product.MapToProduct();
        }

        public IAsyncEnumerable<Product> GetProducts(IEnumerable<int> ids, int shopNumber)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<(int Id, bool Exists)> GetProductsExistenceStatus(IEnumerable<int> productIds, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}
