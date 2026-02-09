using Aplication.Dtos.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Queries.Products.GetProduct
{
    public class GetDetailProductQuery : IRequest<ProductLookupDto>
    {
        public Guid Id { get; set; }
    }
}
