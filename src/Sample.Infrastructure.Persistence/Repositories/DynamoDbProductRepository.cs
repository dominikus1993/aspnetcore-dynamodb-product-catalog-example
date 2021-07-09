using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Sample.Core.Model;
using Sample.Core.Repositories;
using Sample.Infrastructure.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

        public async Task AddProductsAsync(IEnumerable<Product> products, CancellationToken token)
        {
            
            var insertS = _dynamoDBContext.CreateBatchWrite<DynamoDbProductAvailability>();
            var insertP = _dynamoDBContext.CreateBatchWrite<DynamoDbProduct>();
            foreach (var product in products)
            {
                insertP.AddPutItem(new DynamoDbProduct(product));
                insertS.AddPutItems(DynamoDbProductAvailability.FromProduct(product).ToList());
            }

            var batch = _dynamoDBContext.CreateMultiTableBatchWrite(insertS, insertP);
            await batch.ExecuteAsync(token);
        }

        public async Task<Product?> GetProduct(int id, int shopNumber, CancellationToken token = default)
        {
            var existsShop = await _dynamoDBContext.LoadAsync<DynamoDbProductAvailability>(shopNumber, id, token);
            if (existsShop is null)
            {
                return null;
            }
            var product = await _dynamoDBContext.LoadAsync<DynamoDbProduct>(id, token);
            if (product is null)
            {
                return null;
            }
            return product.MapToProduct();
        }

        public async IAsyncEnumerable<Product> GetProducts(IEnumerable<int> ids, int shopNumber, [EnumeratorCancellation] CancellationToken token = default)
        {

            var availibityGet = _dynamoDBContext.CreateBatchGet<DynamoDbProductAvailability>();
            var productsGet = _dynamoDBContext.CreateBatchGet<DynamoDbProduct>();
            foreach (var id in ids)
            {
                availibityGet.AddKey(shopNumber, id);
                productsGet.AddKey(id);
            }
            var batch = _dynamoDBContext.CreateMultiTableBatchGet(availibityGet, productsGet);
            await batch.ExecuteAsync(token);
            foreach (var p in productsGet.Results.Join(availibityGet.Results, a => a.Id, b => b.ProductId, (p, _) => p))
            {
                yield return p.MapToProduct();
            }
        }

        public async IAsyncEnumerable<(int Id, bool Exists)> GetProductsExistenceStatus(IEnumerable<int> productIds, [EnumeratorCancellation]CancellationToken token = default)
        {

            var query = _dynamoDBContext.CreateBatchGet<DynamoDbProduct>();
            foreach (var id in productIds)
            {
                query.AddKey(id);
            }
            await query.ExecuteAsync(token);
            var items = query.Results.Select(x => x.Id).ToHashSet();
            foreach (var id in productIds)
            {
                yield return (id, items.Contains(id));
            }
        }
    }
}
