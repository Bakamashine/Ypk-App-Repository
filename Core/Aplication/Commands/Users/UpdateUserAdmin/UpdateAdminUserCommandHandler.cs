using Aplication.Commands.Users.UpdateUser;
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

namespace Aplication.Commands.Users.UpdateUserAdmin
{
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

            var dublicate = await context.Users.AnyAsync(x => (x.PhoneNumber == entity.PhoneNumber) && x.Id != entity.Id, cancellationToken);

            if (!dublicate)
            {
                if(!string.IsNullOrEmpty(request.Role.Id.ToString()))
                    entity.RoleId = request.Role.Id;
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
}
