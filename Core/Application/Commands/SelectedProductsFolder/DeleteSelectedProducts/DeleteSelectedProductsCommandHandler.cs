using Application.Commands.SelectedProductsFolder.CreateSelectedProducts;
using Application.Common.Exceptions;
using Application.Interfaces;
using Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.SelectedProductsFolder.DeleteSelectedProducts
{
    public class DeleteSelectedProductsCommandHandler : IRequestHandler<DeleteSelectedProductsCommand>
    {
        private readonly IApplicationDbContext context;
        public DeleteSelectedProductsCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(DeleteSelectedProductsCommand request, CancellationToken cancellationToken)
        {
            var entity = context.SelectedProducts.FirstOrDefault(x => x.Id == request.SelectedProductId && x.UserId == request.CurrentUserId)
                ?? throw new NotFoundException(nameof(SelectedProducts), request.SelectedProductId);

            context.SelectedProducts.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
