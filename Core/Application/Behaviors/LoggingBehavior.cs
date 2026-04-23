using Application.Interfaces;
using MediatR;
using Serilog;

namespace Application.Behaviors;

public class LoggingBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ICurrentUserService currentUserService;

    public LoggingBehavior(ICurrentUserService currentUserService)
    {
        this.currentUserService = currentUserService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var userId = currentUserService.UserId;

        Log.Information("Client Request : {Name} {@UserId} {@Reuqest}", requestName, userId, request);
        var responce = await next();

        return responce;
    }
}