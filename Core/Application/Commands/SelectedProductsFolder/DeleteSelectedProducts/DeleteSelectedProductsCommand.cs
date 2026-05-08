using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.SelectedProductsFolder.DeleteSelectedProducts
{
    public class DeleteSelectedProductsCommand : IRequest
    {
        public Guid CurrentUserId { get; set; }
        public Guid SelectedProductId { get; set; }
    }
}
