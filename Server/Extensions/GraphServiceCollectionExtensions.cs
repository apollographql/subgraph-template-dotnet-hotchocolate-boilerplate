using ApolloGraphQL.HotChocolate.Federation.Two;
using HotChocolate.Execution.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class GraphServiceCollectionExtensions
{
	public static IRequestExecutorBuilder AddGraphQLServices(this IRequestExecutorBuilder builder)
	{
		return builder
			.AddApolloFederationV2(schemaConfiguration: schema =>
			{
				schema.Contact(
					name: "Thing Server Team",
					url: "https://myteam.slack.com/archives/teams-chat-room-url",
					description: "send urgent issues to [#oncall](https://yourteam.slack.com/archives/oncall)");
			})
			.AddType<ContactDirectiveType>()
			.AddQueryType<Query>()
			.AddMutationType<Mutation>()
			.RegisterService<Data>();
	}
}