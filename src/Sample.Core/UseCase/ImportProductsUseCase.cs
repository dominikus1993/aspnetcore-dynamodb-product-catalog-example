using Bogus;
using MoreLinq;
using Sample.Core.Model;
using Sample.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.Core.UseCase
{
    public sealed class ImportProductsUseCase
    {
        private IProductRepository _productRepository;

        public ImportProductsUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task Execute(CancellationToken cancellationToken = default)
        {
            var ids = Enumerable.Range(1, 1000).ToList();
            var faker = new Faker<Product>()
                            .CustomInstantiator(f =>
                            {
                                return new Product(f.UniqueIndex, f.Commerce.ProductName(), ids, new List<string>() { f.Commerce.Ean8() });
                            });
            var data = faker.Generate(10000);
            var pageSize = 100;       
            var tasks = new List<Task>(data.Count);
            var pages = Math.Ceiling((double)data.Count / pageSize);
            Console.WriteLine(pages);
            for (int i = 0; i < pages; i++)
            {
                var products = data.Skip(pageSize * (i - 1))
                                    .Take(pageSize)
                                    .ToList();
                var t = _productRepository.AddProductsAsync(products, cancellationToken);
            }
            await Task.WhenAll(tasks);
            
        }
    }
}
