using Application.Interfaces;
using Domain.Model;
using MediatR;

namespace Application.Commands.Ypks.CreateYpk;

public class CreateYpkCommandHandler : IRequestHandler<CreateYpkCommand, Guid>
{
    private readonly IProductsDbContext context;

    public CreateYpkCommandHandler(IProductsDbContext context)
    {
        this.context = context;
    }

    public async Task<Guid> Handle(CreateYpkCommand request, CancellationToken cancellationToken)
    {
        var newYpk = new Ypk
        {
            Id = Guid.NewGuid(),
            YpkName = request.YpkName,
            Description = request.Description,
            IsActive = true
        };

        await context.Ypks.AddAsync(newYpk, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return newYpk.Id;
    }
}