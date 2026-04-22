using Aplication.Dtos.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Queries.Products.GetYpkListByYpk
{
    public class GetYpkListByYpkQuery : IRequest<ProductListVm>
    {
        public Guid YpkId { get; set; }
    }
}
