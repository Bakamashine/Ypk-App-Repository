using Application.Dtos.Auth;
using Application.Interfaces;
using Application.Interfaces.Repository;
using MediatR;

namespace Application.Commands.Auth.Login;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, TokenDto?>
{
    private readonly IUserRepository _repository;
    private readonly IApplicationDbContext context;
    private readonly IPasswordHasherServise passwordHasher;
    private readonly IJwtTokenService tokenService;

    public LoginUserCommandHandler(IApplicationDbContext context, IJwtTokenService tokenService,
        IPasswordHasherServise passwordHasher, IUserRepository repository)
    {
        this.context = context;
        this.tokenService = tokenService;
        this.passwordHasher = passwordHasher;
        _repository = repository;
    }

    public async Task<TokenDto?> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByPhoneNumber(request.PhoneNumber);

        if (user != null &&
            passwordHasher.VerifyBcryptPassword(request.Password, user.HashPassword) &&
            user.IsActive)
        {
            // var dto = await tokenService.GenerateToken(user);
            TokenDto tokenDto = new()
            {
                AccessToken = await tokenService.GenerateJwtToken(user, cancellationToken),
                RefreshToken = await tokenService.GenerateRefreshToken(user, cancellationToken)
            };
            return tokenDto;
            // return TokenDto.Create(accessToken, tokenService.GenerateRefreshToken());
        }

        return null;
    }
}