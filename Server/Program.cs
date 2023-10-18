using ApolloGraphQL.HotChocolate.Federation.Two;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddSingleton<Data>();

builder.Services
    .AddGraphQLServer()
    .AddApolloFederationV2(new CustomSchema())
    .AddType<ContactDirectiveType>()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .RegisterService<Data>();

var app = builder.Build();

app.MapGraphQL();
app.MapBananaCakePop("/");
app.Run();