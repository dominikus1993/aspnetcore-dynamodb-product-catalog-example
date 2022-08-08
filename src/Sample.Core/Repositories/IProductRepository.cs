using Sample.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.Core.Repositories
{
    public interface IProductRepository
    {
        IAsyncEnumerable<(int Id, bool Exists)> GetProductsExistenceStatus(IEnumerable<int> productIds, CancellationToken token = default);
        IAsyncEnumerable<Product> GetProducts(IEnumerable<int> ids, int shopNumber, CancellationToken token = default);
        Task<Product?> GetProduct(int id, CancellationToken token = default);
        Task AddProductsAsync(IEnumerable<Product> products, CancellationToken token);
    }
}
