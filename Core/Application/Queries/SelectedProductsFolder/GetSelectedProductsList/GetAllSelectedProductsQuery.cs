using Application.Dtos.SelectedProductsFolder;
using Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.SelectedProductsFolder.GetSelectedProductsList
{
    public class GetAllSelectedProductsQuery : IRequest<SelectedProductsListVm>
    {
        public Guid CurrentUserId { get; set; }
    }
}
