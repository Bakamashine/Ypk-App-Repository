using Application.Dtos.Auth;
using Application.Interfaces;
using Domain.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.Auth.Registration;

public class RegistrationUserCommandHandler : IRequestHandler<RegistrationUserCommand, TokenDto?>
{
    private readonly IJwtTokenService _tokenService;
    private readonly IApplicationDbContext context;
    private readonly IPasswordHasherServise passwordHasher;

    public RegistrationUserCommandHandler(IApplicationDbContext context, IJwtTokenService tokenService,
        IPasswordHasherServise passwordHasher)
    {
        this.context = context;
        _tokenService = tokenService;
        this.passwordHasher = passwordHasher;
    }

    public async Task<TokenDto?> Handle(RegistrationUserCommand request, CancellationToken cancellationToken)
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

        var duplicate = await context.Users.AnyAsync(x => x.PhoneNumber == user.PhoneNumber, cancellationToken);

        if (duplicate)
            return null;

        await context.Users.AddAsync(user, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        var accessToken =  await _tokenService.GenerateJwtToken(user, cancellationToken);

        var refreshToken = await _tokenService.GenerateRefreshToken(user, cancellationToken);

        return TokenDto.Create(accessToken, refreshToken);
        // return await _tokenService.(user);
    }
}