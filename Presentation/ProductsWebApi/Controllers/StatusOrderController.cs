using Aplication.Dtos.Feedbacks;
using Aplication.Dtos.StatusOrders;
using Aplication.Queries.Feedbacks.GetFeedback;
using Aplication.Queries.Feedbacks.GetFeedbackList;
using Aplication.Queries.StatusOrders.GetStatusOrdersList;
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
    public class StatusOrderController : BaseController
    {
        [Authorize(Roles = "Manager,Admin")]
        [HttpGet("All")]
        public async Task<ActionResult<StatusOrderListVm>> GetAll()
        {
            var query = new GetAllStatusOrdersQuery()
            {

            };

            var vm = await Mediator.Send(query);
            return Ok(vm);
        }
    }
}
