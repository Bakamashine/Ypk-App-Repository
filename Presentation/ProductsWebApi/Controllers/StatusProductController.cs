using Aplication.Dtos.Feedbacks;
using Aplication.Dtos.StatusOrders;
using Aplication.Dtos.StatusProducts;
using Aplication.Queries.Feedbacks.GetFeedback;
using Aplication.Queries.Feedbacks.GetFeedbackList;
using Aplication.Queries.StatusOrders.GetStatusOrdersList;
using Aplication.Queries.StatusProducts.GetStatusProductsList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsWebApi.Controllers
{
    [Route("api/[controller]")]
    public class StatusProductController : BaseController
    {
        /// <summary>
        /// Get all status product
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET (HOST)/api/StatusProduct/all
        /// </remarks>
        /// <returns>Returns StatusProductListVm</returns>
        /// <response code="200">Siccess</response>
        /// <response code="401">If the user is unautorized</response>
        /// <response code="403">If the user not have permission</response>
        [Authorize(Roles = "Manager,Admin")]
        [HttpGet("All")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<StatusProductListVm>> GetAll()
        {
            var query = new GetAllStatusProductQuery()
            {

            };

            var vm = await Mediator.Send(query);
            return Ok(vm);
        }
    }
}
