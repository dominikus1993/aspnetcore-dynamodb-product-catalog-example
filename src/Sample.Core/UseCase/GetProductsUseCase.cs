﻿using Sample.Core.Dto;
using Sample.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Core.UseCase
{
    public record GetProduct(int Id, int ShopNumber);

    public sealed class GetProductUseCase
    {
        private IProductRepository _productRepository;

        public GetProductUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductDto?> GetProduct(GetProduct query)
        {
            var result = await _productRepository.GetProduct(query.Id, query.ShopNumber);

            if (result is null)
            {
                return null;
            }

            return new ProductDto(result);
        }
    }
}
