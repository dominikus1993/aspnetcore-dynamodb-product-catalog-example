using Sample.Core.Model;
using Sample.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.Infrastructure.Persistence.Repositories
{
    internal class FakeProductRepository : IProductRepository
    {
        public async Task<Product?> GetProduct(int id, int shopNumber)
        {
            return new Product(id, "xD", new List<int> { shopNumber }, new List<string>());
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
