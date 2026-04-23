using Application.Commands.Products.CreateProduct;
using Application.Commands.Products.DeleteProduct;
using Application.Commands.Products.UpdateProduct;
using Application.Dtos.Products;
using Application.Queries.Products.GetCreatedProductList;
using Application.Queries.Products.GetProduct;
using Application.Queries.Products.GetProductLis;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductsWebApi.Models.Product;

namespace ProductsWebApi.Controllers;

[Route("api/[controller]")]
public class ProductController : BaseController
{
    private readonly IMapper mapper;

    public ProductController(IMapper mapper)
    {
        this.mapper = mapper;
    }


    /// <summary>
    ///     Get all published products
    /// </summary>
    /// <remarks>
    ///     Sample request:
    ///     GET (HOST)/api/product/all
    /// </remarks>
    /// <returns>Returns ProductListVm</returns>
    /// <response code="200">Success</response>
    /// <response code="401">If the user is unauthorized</response>
    [HttpGet("All")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ProductListVm>> GetAll()
    {
        var query = new GetAllProductQuery();

        var vm = await Mediator.Send(query);
        if (vm?.Products != null)
            foreach (var product in vm.Products)
                if (!string.IsNullOrEmpty(product.PhotoPath))
                    product.PhotoUrl = $"{Request.Scheme}://{Request.Host}{product.PhotoPath}";

        return Ok(vm);
    }

    /// <summary>
    ///     Get all editing products
    /// </summary>
    /// <remarks>
    ///     Sample request:
    ///     GET (HOST)/api/product/all
    /// </remarks>
    /// <returns>Returns ProductListVm</returns>
    /// <response code="200">Success</response>
    /// <response code="401">If the user is unauthorized</response>
    [Authorize]
    [HttpGet("All/created")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ProductListVm>> GetAllCreated()
    {
        var query = new GetAllCreatedProductQuery();

        var vm = await Mediator.Send(query);
        if (vm?.Products != null)
            foreach (var product in vm.Products)
                if (!string.IsNullOrEmpty(product.PhotoPath))
                    product.PhotoUrl = $"{Request.Scheme}://{Request.Host}{product.PhotoPath}";

        return Ok(vm);
    }

    /// <summary>
    ///     Get info product by id
    /// </summary>
    /// <remarks>
    ///     Sample request:
    ///     GET (HOST)/api/product/AB670EFA-9049-46F2-AA3F-8C5044657851
    /// </remarks>
    /// <param name="id">Product id guid</param>
    /// <returns>Returns ProductLookupDto</returns>
    /// <response code="200">Success</response>
    /// <response code="401">If the user is unauthorized</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ProductLookupDto>> Get(Guid id)
    {
        var query = new GetDetailProductQuery
        {
            Id = id
        };
        var vm = await Mediator.Send(query);
        if (!string.IsNullOrEmpty(vm.PhotoPath)) vm.PhotoUrl = $"{Request.Scheme}://{Request.Host}{vm.PhotoPath}";
        return Ok(vm);
    }

    /// <summary>
    ///     Create object product
    /// </summary>
    /// <remarks>
    ///     Sample request:
    ///     POST (HOST)/api/product
    ///     {
    ///     "ProductInfo": "string",
    ///     "Adress": "string",
    ///     "Photo": "file"
    ///     "ProductCost": "decimal"
    ///     "IsProduct": "bool"
    ///     "YpkId": "3aa45f64-5717-4562-b3fc-2c963f66aff2"
    ///     "StatusProductId": "7ab72f64-5717-4562-bgfc-2c963f663sa2"
    ///     }
    /// </remarks>
    /// <param name="createProductDto">CreateProductDto object</param>
    /// <returns>Returns id (guid)</returns>
    /// <response code="201">Success</response>
    /// <response code="401">If the user is unauthorized</response>
    /// <response code="403">If the user not have permission</response>
    [Authorize(Roles = "Manager,Admin")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<Guid>> Create([FromForm] CreateProductDto createProductDto)
    {
        var command = mapper.Map<CreateProductCommand>(createProductDto);
        command.CurrentUserId = UserId;
        var commandId = await Mediator.Send(command);
        return Ok(commandId);
    }

    /// <summary>
    ///     Update object product
    /// </summary>
    /// <remarks>
    ///     Sample request:
    ///     PUT (HOST)/api/product
    ///     {
    ///     "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    ///     "ProductInfo": "string",
    ///     "Adress": "string",
    ///     "Photo": "file"
    ///     "ProductCost": "decimal"
    ///     "IsProduct": "bool"
    ///     "YpkId": "3aa45f64-5717-4562-b3fc-2c963f66aff2"
    ///     "StatusProductId": "7ab72f64-5717-4562-bgfc-2c963f663sa2"
    ///     }
    /// </remarks>
    /// <param name="updateProductDto">UpdateProductDto object</param>
    /// <returns>Returns NoContent</returns>
    /// <response code="204">Success</response>
    /// <response code="401">If the user is unauthorized</response>
    [Authorize(Roles = "Manager,Admin")]
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Update([FromForm] UpdateProductDto updateProductDto)
    {
        var command = mapper.Map<UpdateProductCommand>(updateProductDto);
        command.CurrentUserId = UserId;
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    ///     Delete object product by id
    /// </summary>
    /// <remarks>
    ///     Sample request:
    ///     DELETE (HOST)/api/product/AB670EFA-9049-46F2-A5BF-8C5044287851
    /// </remarks>
    /// <param name="id">Product id (guid)</param>
    /// <returns>Returns NoContent</returns>
    /// <response code="204">Success</response>
    /// <response code="401">If the user is unauthorized</response>
    /// <response code="403">If the user not have permission</response>
    [Authorize(Roles = "Manager,Admin")]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
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