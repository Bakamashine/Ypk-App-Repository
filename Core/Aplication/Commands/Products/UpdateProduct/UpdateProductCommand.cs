using Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Commands.Products.UpdateProduct
{
    public class UpdateProductCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid CurrentUserId { get; set; }

        public string ProductName { get; set; } = string.Empty;
        public string ProductInfo { get; set; } = string.Empty;
        public decimal ProductCost { get; set; }
        public bool IsProduct { get; set; }
        public string Adress { get; set; } = string.Empty;
        public string? Photo { get; set; }

        public Ypk? Ypk { get; set; }
        public StatusProduct? StatusProduct { get; set; }
    }
}
