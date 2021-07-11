using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sample.Api.Responses;
using Sample.Core.Dto;
using Sample.Core.UseCase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet("shops/{shopNumber:int}/products/{id:int}")]
        public async Task<ActionResult<ProductResponse?>> GetProduct(int shopNumber, int id, [FromServices]GetProductUseCase useCase, CancellationToken cancellationToken)
        {
            var result = await useCase.GetProduct(new GetProduct(id, shopNumber), cancellationToken);
            if (result is null)
            {
                return NotFound();
            }

            return Ok(new ProductResponse(result));
        }

        [HttpGet("shops/{shopNumber:int}/products")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts(int shopNumber, [FromQuery] IEnumerable<int> productIds, [FromServices] GetProductsUseCase useCase, CancellationToken cancellationToken)
        {
            var result = await useCase.GetProducts(new GetProducts(productIds, shopNumber), cancellationToken);
            if (result is null)
            {
                return NoContent();
            }

            return Ok(result);
        }
    }
}
