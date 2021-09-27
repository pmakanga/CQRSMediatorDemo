using CQRSMediatorDemo.Features.ProductFeatures.Commands;
using CQRSMediatorDemo.Features.ProductFeatures.Queries;
using CQRSMediatorDemo.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSMediatorDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator mediator;
        public ProductController(IMediator mediator) => this.mediator = mediator;
       

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await mediator.Send(new GetAllProductsQuery());
            return Ok(products);
        }

        [HttpGet("{id:int}", Name = "GetProductById")]
        public async Task<ActionResult> GetProductById(int id)
        {
            var product = await mediator.Send(new GetProductByIdQuery(id));

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody]Product product )
        {
            var productToReturn = await mediator.Send(new CreateProductCommand(product));
            return CreatedAtRoute("GetProductById", new { id = productToReturn.Id }, productToReturn);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>Delete(int id)
        {
            return Ok(await mediator.Send(new DeleteProductByIdCommand(id)));
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult>Update(int id, UpdateProductCommand command)
        {
            command.product.Id = id;
            if (id != command.product.Id) return BadRequest();
            return Ok(await mediator.Send(command));
        }
    }
}
