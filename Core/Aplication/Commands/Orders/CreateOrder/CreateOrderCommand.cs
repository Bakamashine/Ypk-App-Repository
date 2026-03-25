using Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Commands.Orders.CreateOrder
{
    public class CreateOrderCommand : IRequest<Guid>
    {
        public Guid CurrentUserId { get; set; }
        public Guid ProductId { get; set; }


        public string? CustomersComment { get; set; }
        public string? UserComment { get; set; }
    }
}
