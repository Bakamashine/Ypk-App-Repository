using Application.Interfaces;
using Domain.Model;
using MediatR;

namespace Application.Commands.Users.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IApplicationDbContext context;
    private readonly IPasswordHasherServise hasherService;
    private readonly IFileStorageService _fileStorage;

    public CreateUserCommandHandler(IApplicationDbContext context, IPasswordHasherServise hasherService, IFileStorageService fileStorage)
    {
        this.context = context;
        this.hasherService = hasherService;
        _fileStorage = fileStorage;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        string? photoPath = null;

        // Сохраняем фото, если оно есть
        if (request.Avatar != null) photoPath = await _fileStorage.SaveFileAsync(request.Avatar, "avatars");

        var newUser = new User
        {
            Id = Guid.NewGuid(),
            HashPassword = hasherService.HashPasword(request.Password),
            PhoneNumber = request.PhoneNumber,
            RoleId = request.RoleId,
            UserInfo = request.UserInfo,
            IsActive = true,
            AvatarPath = photoPath
        };

        await context.Users.AddAsync(newUser, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return newUser.Id;
    }
}