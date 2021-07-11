using Sample.Core.Dto;
using Sample.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.Core.UseCase
{
    public record GetProducts(IEnumerable<int> Ids, int ShopNumber);

    public sealed class GetProductsUseCase
    {
        private IProductRepository _productRepository;

        public GetProductsUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductDto>> GetProducts(GetProducts query, CancellationToken cancellationToken = default)
        {
            var result = await _productRepository.GetProducts(query.Ids, query.ShopNumber, cancellationToken)
                                                    .Select(product => new ProductDto(product))
                                                    .ToListAsync(cancellationToken);
            if (result is null || result.Count == 0)
            {
                return Enumerable.Empty<ProductDto>();
            }

            return result;
        }
    }
}
