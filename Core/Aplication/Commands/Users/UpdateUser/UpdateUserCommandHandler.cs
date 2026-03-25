using Aplication.Dtos.Auth;
using Aplication.Interfaces;
using Application.Common.Exceptions;
using Domain.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Commands.Users.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand , TokensDto>
    {
        private readonly IProductsDbContext context;
        private readonly IJwtTokenServise tokenServise;
        private readonly IPasswordHasherServise passwordHasher;

        public UpdateUserCommandHandler(IProductsDbContext context, IJwtTokenServise tokenServise, IPasswordHasherServise passwordHasher)
        {
            this.context = context;
            this.tokenServise = tokenServise;
            this.passwordHasher = passwordHasher;
        }

        public async Task<TokensDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var currentUser = await context.Users.FindAsync(new object[] { request.CurrentUserId }, cancellationToken)
                ?? throw new NotFoundException(nameof(User), request.CurrentUserId);

            var entity = await context.Users.FindAsync(new object[] { request.Id }, cancellationToken) 
                ?? throw new NotFoundException(nameof(User), request.Id);


            if (currentUser.Id == entity.Id)
            {

                var dublicate = await context.Users.AnyAsync(x => (x.PhoneNumber == entity.PhoneNumber) && x.Id != entity.Id, cancellationToken);

                if (!dublicate) 
                {
                    if (!string.IsNullOrEmpty(request.NewPassword) && !string.IsNullOrEmpty(request.OldPassword))
                    {
                        if (passwordHasher.VerifyBcryptPassword(request.OldPassword, entity.HashPassword))
                            entity.HashPassword = passwordHasher.HashPasword(request.NewPassword);
                        else return null;
                    }
                    if(!string.IsNullOrEmpty(request.Fullname))
                        entity.Fullname = request.Fullname;
                    if (!string.IsNullOrEmpty(request.PhoneNumber))
                        entity.PhoneNumber = request.PhoneNumber;
                    if (!string.IsNullOrEmpty(request.UserInfo))
                        entity.UserInfo = request.UserInfo;
                    if(entity.IsActive != request.IsActive)
                        entity.IsActive = request.IsActive;

                    await context.SaveChangesAsync(cancellationToken);

                    return await tokenServise.GenerateToken(entity);
                }

            }
            throw new AccessException();
        }
    }
}
