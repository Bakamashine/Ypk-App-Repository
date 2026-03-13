using Aplication.Commands.Orders.CreateOrder;
using Aplication.Interfaces;
using Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Commands.Users.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IProductsDbContext context;
        private readonly IPasswordHasherServise hasherServise;

        public CreateUserCommandHandler(IProductsDbContext context, IPasswordHasherServise hasherServise)
        {
            this.context = context;
            this.hasherServise = hasherServise;
        }
        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var newUser = new User
            {
                Id = Guid.NewGuid(),
                HashPassword = hasherServise.HashPasword(request.Password),
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
}
