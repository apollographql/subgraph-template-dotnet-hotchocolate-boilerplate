using HotChocolate.AspNetCore;
using HotChocolate.Execution;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services
    .AddSingleton<Data>();

builder.Services
    .AddGraphQLServer()
    .AddGraphQLServices()
    .AddHttpRequestInterceptor<RouterAuthInterceptor>();

var app = builder.Build();

app.MapGraphQL();

app.RunWithGraphQLCommands(args);

public sealed class RouterAuthInterceptor : DefaultHttpRequestInterceptor
{
    public override ValueTask OnCreateAsync(HttpContext context, IRequestExecutor requestExecutor, IQueryRequestBuilder requestBuilder, CancellationToken cancellationToken)
    {
        var incomingHeader = context.Request.Headers["router-authorization"];
        var routerAuth = Environment.GetEnvironmentVariable("ROUTER_SECRET");
        if (routerAuth != null && incomingHeader != routerAuth)
        {
            throw new Exception("Missing router authentication");
        }

        return base.OnCreateAsync(context, requestExecutor, requestBuilder, cancellationToken);
    }
}