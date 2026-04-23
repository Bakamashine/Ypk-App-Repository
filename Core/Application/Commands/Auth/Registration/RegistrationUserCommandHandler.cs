using Application;
using Application.Dtos.Auth;
using Application.Interfaces;
using Domain.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.Auth.Registration;

public class RegistrationUserCommandHandler : IRequestHandler<RegistrationUserCommand, TokensDto?>
{
    private readonly IProductsDbContext context;
    private readonly IPasswordHasherServise passwordHasher;
    private readonly IJwtTokenServise tokenServise;

    public RegistrationUserCommandHandler(IProductsDbContext context, IJwtTokenServise tokenServise,
        IPasswordHasherServise passwordHasher)
    {
        this.context = context;
        this.tokenServise = tokenServise;
        this.passwordHasher = passwordHasher;
    }

    public async Task<TokensDto?> Handle(RegistrationUserCommand request, CancellationToken cancellationToken)
    {
        var role = await context.Roles.FirstOrDefaultAsync(x => x.RoleName == nameof(EnumRoles.DefaultUser),
            cancellationToken);

        var user = new User
        {
            Id = Guid.NewGuid(),
            RoleId = role.Id,
            Fullname = request.FullName,
            HashPassword = passwordHasher.HashPasword(request.Password),
            PhoneNumber = request.PhoneNumber,
            IsActive = true
        };

        var dublicate = await context.Users.AnyAsync(x => x.PhoneNumber == user.PhoneNumber, cancellationToken);

        if (dublicate)
            return null;

        await context.Users.AddAsync(user, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return await tokenServise.GenerateToken(user);
    }
}