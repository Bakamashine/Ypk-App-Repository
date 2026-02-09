using Aplication.Commands.Orders.CreateOrder;
using Aplication.Commands.Orders.DeleteOrder;
using Aplication.Commands.Orders.UpdateOrder;
using Aplication.Commands.Products.CreateProduct;
using Aplication.Commands.Products.DeleteProduct;
using Aplication.Commands.Products.UpdateProduct;
using Aplication.Dtos.Feedbacks;
using Aplication.Dtos.Products;
using Aplication.Queries.Feedbacks.GetFeedback;
using Aplication.Queries.Feedbacks.GetFeedbackList;
using Aplication.Queries.Products.GetCreatedProductList;
using Aplication.Queries.Products.GetProduct;
using Aplication.Queries.Products.GetProductLis;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductsWebApi.Models.Order;
using ProductsWebApi.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsWebApi.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : BaseController
    {
        private readonly IMapper mapper;

        public ProductController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        [HttpGet("All")]
        public async Task<ActionResult<ProductListVm>> GetAll()
        {
            var query = new GetAllProductQuery()
            {

            };

            var vm = await Mediator.Send(query);
            return Ok(vm);
        }
        [Authorize]
        [HttpGet("All/created")]
        public async Task<ActionResult<ProductListVm>> GetAllCreated()
        {
            var query = new GetAllCreatedProductQuery()
            {

            };

            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductLookupDto>> Get(Guid id)
        {
            var query = new GetDetailProductQuery
            {
                Id = id,
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [Authorize(Roles = "Manager,Admin")]
        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateProductDto createProductDto)
        {
            var command = mapper.Map<CreateProductCommand>(createProductDto);
            command.CurrentUserId = UserId;
            var commandId = await Mediator.Send(command);
            return Ok(commandId);
        }

        [Authorize(Roles = "Manager,Admin")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateProductDto updateProductDto)
        {
            var command = mapper.Map<UpdateProductCommand>(updateProductDto);
            command.CurrentUserId = UserId;
            await Mediator.Send(command);
            return NoContent();
        }

        [Authorize(Roles = "Manager,Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteProductCommand
            {
                Id = id,
                CurrentUserId = UserId
            };

            await Mediator.Send(command);
            return NoContent();
        }
    }
}
