using Application.Commands.Feedbacks.CreateFeedback;
using Application.Commands.Feedbacks.DeleteFeedback;
using Application.Commands.Feedbacks.UpdateFeedback;
using Application.Dtos.Feedbacks;
using Application.Extensions;
using Application.Queries.Base;
using Application.Queries.Feedbacks.GetFeedback;
using Application.Queries.Feedbacks.GetFeedbackList;
using Application.Queries.Feedbacks.GetFeedbackPagList;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductsWebApi.Models.Feedback;

namespace ProductsWebApi.Controllers;

[Route("api/[controller]")]
public class FeedbackController : BaseController
{
    private readonly IMapper mapper;

    public FeedbackController(IMapper mapper)
    {
        this.mapper = mapper;
    }

    /// <summary>
    ///     Get all Feedback
    /// </summary>
    /// <remarks>
    ///     Sample request:
    ///     GET (HOST)/api/feedback/all
    /// </remarks>
    /// <returns>Returns FeedbackListVm</returns>
    /// <response code="200">Success</response>
    [HttpGet("all")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<FeedbackListVm>> GetAll()
    {
        var query = new GetAllFeedbackQuery();

        var vm = await Mediator.Send(query);

        if (vm?.Feedbacks != null)
            vm.Feedbacks.SetImageUrls($"{Request.Scheme}://{Request.Host}");
            // foreach (var feedback in vm.Feedbacks)
            //     if (!string.IsNullOrEmpty(feedback.ImagePath))
            //         feedback.ImageUrl = $"{Request.Scheme}://{Request.Host}{feedback.ImagePath}";

        return Ok(vm);
    }

    [HttpGet("pag/all")]
    
    public async Task<ActionResult<PagedList<FeedbackListVm>>> GetWithPaginate(
        [FromQuery] int pageSize = 5,
        [FromQuery] int page = 1
        )
    {
        var query = new GetAllFeedbackPagQuery() {PageSize = pageSize, Page = page};
        var vm = await Mediator.Send(query);
        
        vm.Items.SetImageUrls($"{Request.Scheme}://{Request.Host}");
        return Ok(vm);

    }
    /// <summary>
    ///     Get info Feedback by id
    /// </summary>
    /// <remarks>
    ///     Sample request:
    ///     GET (HOST)/api/feedback/AB670EFA-9049-46F2-AA3F-8C5044657851
    /// </remarks>
    /// <param name="id">Feedback id guid</param>
    /// <returns>Returns FeedbackLookupDto</returns>
    /// <response code="200">Success</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<FeedbackLookupDto>> Get(Guid id)
    {
        var query = new GetDetailsFeedbackQuery
        {
            Id = id
        };
        var vm = await Mediator.Send(query);

        if (!string.IsNullOrEmpty(vm.ImagePath)) vm.ImageUrl = $"{Request.Scheme}://{Request.Host}{vm.ImagePath}";

        return Ok(vm);
    }

    /// <summary>
    ///     Create object feedback
    /// </summary>
    /// <remarks>
    ///     Sample request:
    ///     POST (HOST)/api/feedback
    ///     {
    ///     "Comment": "string",
    ///     "Raiting": "int (1-5)"
    ///     "Image": "file"
    ///     }
    /// </remarks>
    /// <param name="createFeedbackDto">CreateFeedbackDto object</param>
    /// <returns>Returns id (guid)</returns>
    /// <response code="201">Success</response>
    /// <response code="401">If the user is unauthorized</response>
    [Authorize]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Guid>> Create([FromForm] CreateFeedbackDto createFeedbackDto)
    {
        var command = mapper.Map<CreateFeedbackCommand>(createFeedbackDto);
        command.CurrentUserId = UserId;
        var commandId = await Mediator.Send(command);
        return Ok(commandId);
    }

    /// <summary>
    ///     Update object Feedback
    /// </summary>
    /// <remarks>
    ///     Sample request:
    ///     PUT (HOST)/api/feedback
    ///     {
    ///     "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    ///     "Comment": "string",
    ///     "Raiting": "int (1-5)"
    ///     }
    /// </remarks>
    /// <param name="updateFeedbackDto">UpdateFeedbackDto object</param>
    /// <returns>Returns NoContent</returns>
    /// <response code="204">Success</response>
    /// <response code="401">If the user is unauthorized</response>
    [HttpPut]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Update([FromForm] UpdateFeedbackDto updateFeedbackDto)
    {
        var command = mapper.Map<UpdateFeedbackCommand>(updateFeedbackDto);
        command.CurrentUserId = UserId;
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    ///     Delete object Feedback by id
    /// </summary>
    /// <remarks>
    ///     Sample request:
    ///     DELETE (HOST)/api/feedback/AB670EFA-9049-46F2-A5BF-8C5044287851
    /// </remarks>
    /// <param name="id">Feedback id (guid)</param>
    /// <returns>Returns NoContent</returns>
    /// <response code="204">Success</response>
    /// <response code="401">If the user is unauthorized</response>
    [Authorize]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteFeedbackCommand
        {
            Id = id,
            CurrentUserId = UserId
        };

        await Mediator.Send(command);
        return NoContent();
    }
}