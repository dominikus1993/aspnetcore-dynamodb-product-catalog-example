using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
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
            List<ScanCondition> GetIdScanCondition(IEnumerable<int> idsV)
            {
                return idsV.Select(id => new ScanCondition(nameof(DynamoDbProduct.Id), ScanOperator.Equal, id)).ToList();
            }

            var req = new DynamoDBOperationConfig { ConditionalOperator = ConditionalOperatorValues.Or, QueryFilter = GetIdScanCondition(ids) };
            var products = _dynamoDBContext.CreateBatchGet<DynamoDbProduct>()
        }

        public IAsyncEnumerable<(int Id, bool Exists)> GetProductsExistenceStatus(IEnumerable<int> productIds, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}
