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

        public async Task Execute(CancellationToken cancellationToken)
        {
            var faker = new Faker<Product>()
                            .RuleFor(x => x.Id, f => f.UniqueIndex)
                            .RuleFor(x => x.Name, f => f.Commerce.ProductName())
                            .RuleFor(x => x.AvailableIn, _ => Enumerable.Range(1, 500).ToList())
                            .RuleFor(x => x.Ean, f => new List<string>() { f.Commerce.Ean8() });
            var data = faker.Generate(1000);
            await _productRepository.AddProductsAsync(data, cancellationToken);
        }
    }
}
