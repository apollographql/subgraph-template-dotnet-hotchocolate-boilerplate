using ApolloGraphQL.HotChocolate.Federation.Two;
using HotChocolate.AspNetCore;
using HotChocolate.Execution;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddSingleton<Data>();

builder.Services
    .AddGraphQLServer()
    .AddApolloFederationV2(new CustomSchema())
    .AddType<ContactDirectiveType>()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .RegisterService<Data>()
    .AddHttpRequestInterceptor<RouterAuthInterceptor>();

var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
builder.WebHost.UseUrls($"http://*:{port}");

var app = builder.Build();

app.MapGraphQL();
app.MapBananaCakePop("/");
app.Run();

public sealed class RouterAuthInterceptor : DefaultHttpRequestInterceptor
{
    public override ValueTask OnCreateAsync(HttpContext context, IRequestExecutor requestExecutor, IQueryRequestBuilder requestBuilder, CancellationToken cancellationToken)
    {
        var incomingHeader = context.Request.Headers["router-authorization"];
        var routerAuth = Environment.GetEnvironmentVariable("router-authorization");
        if (routerAuth != null && incomingHeader != routerAuth)
        {
            throw new Exception("Missing router authentication");
        }

        return base.OnCreateAsync(context, requestExecutor, requestBuilder, cancellationToken);
    }
}