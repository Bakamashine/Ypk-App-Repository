using Application.Commands.Users.UpdateUser;
using Application.Common.Exceptions;
using Application.Dtos.Auth;
using Application.Interfaces;
using Domain.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Users.UpdateCurrentUser
{
    public class UpdateCurrentUserCommandHandler : IRequestHandler<UpdateCurrentUserCommand, TokenDto?>
    {
        private readonly IJwtTokenService _tokenService;
        private readonly IApplicationDbContext context;
        private readonly IFileStorageService _fileStorage;

        public UpdateCurrentUserCommandHandler(IApplicationDbContext context
            , IJwtTokenService tokenService
            , IFileStorageService fileStorage)
        {
            this.context = context;
            _tokenService = tokenService;
            _fileStorage = fileStorage;
        }

        public async Task<TokenDto?> Handle(UpdateCurrentUserCommand request, CancellationToken cancellationToken)
        {
            string? photoPath = null;

            // Сохраняем фото, если оно есть
            if (request.Avatar != null) photoPath = await _fileStorage.SaveFileAsync(request.Avatar, "avatars");

            var entity = await context.Users.FindAsync(new object[] { request.CurrentUserId }, cancellationToken)
                         ?? throw new NotFoundException(nameof(User), request.CurrentUserId);

            var dublicate = await context.Users.AnyAsync(x => x.PhoneNumber == entity.PhoneNumber && x.Id != entity.Id,
                cancellationToken);

            if (!dublicate)
            {
                if (!string.IsNullOrEmpty(request.Fullname))
                    entity.Fullname = request.Fullname;
                if (!string.IsNullOrEmpty(request.PhoneNumber))
                    entity.PhoneNumber = request.PhoneNumber;
                if (!string.IsNullOrEmpty(request.UserInfo))
                    entity.UserInfo = request.UserInfo;
                if (!string.IsNullOrEmpty(photoPath))
                    entity.AvatarPath = photoPath;

                await context.SaveChangesAsync(cancellationToken);

                return await _tokenService.GenerateToken(entity);
            }
            return null;
        }
    }
}
