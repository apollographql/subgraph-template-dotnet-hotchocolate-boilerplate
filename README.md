# .NET HotChocolate Federated GraphQL Subgraph

[![Deploy on Railway](https://railway.app/button.svg)](https://railway.app/new/template/Jomu73?referralCode=xsbY2R)

This is an example application template that can be used to create Federated GraphQL subgraph using [HotChocolate](https://chillicream.com/docs/hotchocolate/v13). You can use this template from [Rover](https://www.apollographql.com/docs/rover/commands/template/) with `rover template use --template subgraph-dotnet-hotchocolate`.

This example application implements following GraphQL schema:

```graphql
directive @contact(
    "Contact title of the subgraph owner"
    name: String!
    "URL where the subgraph's owner can be reached"
    url: String
    "Other relevant notes can be included here; supports markdown links"
    description: String
) on SCHEMA

schema
@contact(
    name: "FooBar Server Team"
    url: "https://myteam.slack.com/archives/teams-chat-room-url"
    description: "send urgent issues to [#oncall](https://yourteam.slack.com/archives/oncall)"
)
@link(
    url: "https://specs.apollo.dev/federation/v2.5",
    import: ["@key"]
) {
    query: Query
}

type Query {
    foo(id: ID!): Foo
}
type Foo @key(fields: "id") {
    id: ID!
    name: String
}
```

## Build

This project uses [.NET CLI](https://learn.microsoft.com/en-us/dotnet/core/tools/) and requires .NET 7.0+ runtime. In order to build the project locally run

```shell
dotnet build
```

To test the project run

```shell
dotnet test
```

### Continuous Integration

This project comes with some example build actions that will trigger on PR requests and commits to the main branch.

## Run

To start the GraphQL server:

```shell script
# from root directory
dotnet run --project src/HotChocolateSubgraph/HotChocolateSubgraph.csproj

# from src/HotChocolateSubgraph directory
dotnet run
```

Once the app has started you can explore the example schema by opening the Banana Cake Pop IDE at http://localhost:5000/graphql and begin developing your supergraph with `rover dev --url http://localhost:5000/graphql --name my-sugraph`.

## Apollo Studio Integration

1. Set these secrets in GitHub Actions:
    1. APOLLO_KEY: An Apollo Studio API key for the supergraph to enable schema checks and publishing of the
       subgraph.
    2. APOLLO_GRAPH_REF: The name of the supergraph in Apollo Studio.
    3. PRODUCTION_URL: The URL of the deployed subgraph that the supergraph gateway will route to.
2. Set `SUBGRAPH_NAME` in .github/workflows/checks.yaml and .github/workflows/deploy.yaml
3. Remove the `if: false` lines from `.github/workflows/checks.yaml` and `.github/workflows/deploy.yaml` to enable schema checks and publishing.
4. Write your custom deploy logic in `.github/workflows/deploy.yaml`.
5. Send the `Router-Authorization` header [from your Cloud router](https://www.apollographql.com/docs/graphos/routing/cloud-configuration#managing-secrets) and set the `ROUTER_SECRET` environment variable wherever you deploy this to.

## Additional Resources

* [HotChocolate documentation](https://chillicream.com/docs/hotchocolate/v13)
* [.NET SDL documentation](https://learn.microsoft.com/en-us/dotnet/core/sdk)
* [.NET CLI documentation](https://learn.microsoft.com/en-us/dotnet/core/tools/)
