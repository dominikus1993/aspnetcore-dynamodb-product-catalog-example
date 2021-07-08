using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sample.Core.Dto;
using Sample.Core.UseCase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet("shops/{shopNumber:int}/products/{id:int}")]
        public async Task<ActionResult<ProductDto?>> GetProduct(int shopNumber, int id, [FromServices]GetProductUseCase useCase)
        {
            var result = await useCase.GetProduct(new GetProduct(shopNumber, id));
            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
