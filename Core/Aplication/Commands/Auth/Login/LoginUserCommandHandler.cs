using Aplication.Dtos.Auth;
using Aplication.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Application.Commands.Auth.Login
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, TokensDto?>
    {
        private readonly IProductsDbContext context;
        private readonly IJwtTokenServise tokenServise;
        private readonly IPasswordHasherServise passwordHasher;

        public LoginUserCommandHandler(IProductsDbContext context, IJwtTokenServise tokenServise, IPasswordHasherServise passwordHasher)
        {
            this.context = context;
            this.tokenServise = tokenServise;
            this.passwordHasher = passwordHasher;
        }
        public async Task<TokensDto?> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var users = await context.Users.Where(user=> 
                user.PhoneNumber == request.PhoneNumber).ToListAsync(cancellationToken);
            if (users == null)
                return null;

            var user = users.FirstOrDefault(user => passwordHasher.VerifyBcryptPassword(request.Password, user.HashPassword) && user.IsActive == true);
            if (user == null)
                return null;



            return await tokenServise.GenerateToken(user);
        }
    }
}
