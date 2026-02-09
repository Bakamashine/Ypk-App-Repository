using Aplication.Dtos.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Queries.Products.GetCreatedProductList
{
    public class GetAllCreatedProductQuery : IRequest<ProductListVm>
    {

    }
}
