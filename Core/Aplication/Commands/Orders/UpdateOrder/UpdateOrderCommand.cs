using Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Commands.Orders.UpdateOrder
{
    public class UpdateOrderCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid CurrentUserId { get; set; }

        public Product? Product { get; set; }
        public StatusOrder? StatusOrder { get; set; }

        public string? CustomersComment { get; set; }
        public string? UserComment { get; set; }
    }
}
