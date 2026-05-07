using Application.Common.Exceptions;
using Application.Dtos.Auth;
using Application.Interfaces;
using Domain.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.Users.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, TokenDto>
{
    private readonly IJwtTokenService _tokenService;
    private readonly IApplicationDbContext context;
    private readonly IPasswordHasherServise passwordHasher;
    private readonly IFileStorageService _fileStorage;

    public UpdateUserCommandHandler(IApplicationDbContext context, IJwtTokenService tokenService,
        IPasswordHasherServise passwordHasher, IFileStorageService fileStorage)
    {
        this.context = context;
        _tokenService = tokenService;
        this.passwordHasher = passwordHasher;
        _fileStorage = fileStorage;
    }

    public async Task<TokenDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        string? photoPath = null;

        // Сохраняем фото, если оно есть
        if (request.Avatar != null) photoPath = await _fileStorage.SaveFileAsync(request.Avatar, "avatars");

        var currentUser = await context.Users.FindAsync(new object[] { request.CurrentUserId }, cancellationToken)
                          ?? throw new NotFoundException(nameof(User), request.CurrentUserId);

        var entity = await context.Users.FindAsync(new object[] { request.Id }, cancellationToken)
                     ?? throw new NotFoundException(nameof(User), request.Id);


        if (currentUser.Id == entity.Id)
        {
            var dublicate = await context.Users.AnyAsync(x => x.PhoneNumber == entity.PhoneNumber && x.Id != entity.Id,
                cancellationToken);

            if (!dublicate)
            {
                if (!string.IsNullOrEmpty(request.NewPassword) && !string.IsNullOrEmpty(request.OldPassword))
                {
                    if (passwordHasher.VerifyBcryptPassword(request.OldPassword, entity.HashPassword))
                        entity.HashPassword = passwordHasher.HashPasword(request.NewPassword);
                    else return null;
                }

                if (!string.IsNullOrEmpty(request.Fullname))
                    entity.Fullname = request.Fullname;
                if (!string.IsNullOrEmpty(request.PhoneNumber))
                    entity.PhoneNumber = request.PhoneNumber;
                if (!string.IsNullOrEmpty(request.UserInfo))
                    entity.UserInfo = request.UserInfo;
                if (entity.IsActive != request.IsActive)
                    entity.IsActive = request.IsActive;
                if (!string.IsNullOrEmpty(photoPath))
                    entity.AvatarPath = photoPath;

                await context.SaveChangesAsync(cancellationToken);

                return await _tokenService.GenerateToken(entity);
            }
        }

        throw new AccessException();
    }
}