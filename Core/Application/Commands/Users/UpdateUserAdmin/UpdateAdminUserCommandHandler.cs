using Application.Commands.Users.UpdateUser;
using Application.Common.Exceptions;
using Application.Interfaces;
using Domain.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.Users.UpdateUserAdmin;

public class UpdateAdminUserCommandHandler : IRequestHandler<UpdateAdminUserCommand>
{
    private readonly IProductsDbContext context;
    private readonly IPasswordHasherServise passwordHasher;

    public UpdateAdminUserCommandHandler(IProductsDbContext context, IPasswordHasherServise passwordHasher)
    {
        this.context = context;
        this.passwordHasher = passwordHasher;
    }

    public async Task<Unit> Handle(UpdateAdminUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await context.Users.FindAsync(new object[] { request.Id }, cancellationToken)
                     ?? throw new NotFoundException(nameof(User), request.Id);

        var dublicate = await context.Users.AnyAsync(x => x.PhoneNumber == entity.PhoneNumber && x.Id != entity.Id,
            cancellationToken);

        if (!dublicate)
        {
            if (!string.IsNullOrEmpty(request.RoleId.ToString()))
                entity.RoleId = request.RoleId;
            if (!string.IsNullOrEmpty(request.YpkId.ToString()))
                entity.YpkId = request.YpkId;
            if (!string.IsNullOrEmpty(request.Fullname))
                entity.Fullname = request.Fullname;
            if (!string.IsNullOrEmpty(request.PhoneNumber))
                entity.PhoneNumber = request.PhoneNumber;
            if (!string.IsNullOrEmpty(request.UserInfo))
                entity.UserInfo = request.UserInfo;

            await context.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;
    }
}