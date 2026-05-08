using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.SelectedProductsFolder.CreateSelectedProducts
{
    public class CreateSelectedProductsCommand : IRequest<Guid>
    {
        public Guid CurrentUserId { get; set; }
        public Guid ProductId { get; set; }
    }
}
