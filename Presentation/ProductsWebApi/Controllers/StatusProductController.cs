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
        [Authorize(Roles = "Manager,Admin")]
        [HttpGet("All")]
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
