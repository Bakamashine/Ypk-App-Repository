using Application.Interfaces;
using Domain.Model;
using MediatR;

namespace Application.Commands.Users.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IApplicationDbContext context;
    private readonly IPasswordHasherServise hasherService;

    public CreateUserCommandHandler(IApplicationDbContext context, IPasswordHasherServise hasherService)
    {
        this.context = context;
        this.hasherService = hasherService;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var newUser = new User
        {
            Id = Guid.NewGuid(),
            HashPassword = hasherService.HashPasword(request.Password),
            PhoneNumber = request.PhoneNumber,
            RoleId = request.RoleId,
            UserInfo = request.UserInfo,
            IsActive = true
        };

        await context.Users.AddAsync(newUser, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return newUser.Id;
    }
}