using Bogus;
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
            var ids = Enumerable.Range(1, 500).ToList();
            var faker = new Faker<Product>()
                            .CustomInstantiator(f =>
                            {
                                return new Product(f.UniqueIndex, f.Commerce.ProductName(), ids, new List<string>() { f.Commerce.Ean8() });
                            });
            for (int i = 0; i < 10; i++)
            {
                var data = faker.Generate(1);
                await _productRepository.AddProductsAsync(data, cancellationToken);
            }
        }
    }
}
