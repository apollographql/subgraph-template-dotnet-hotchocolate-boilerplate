using ApolloGraphQL.HotChocolate.Federation;
using ApolloGraphQL.HotChocolate.Federation.Two;

namespace Example;

[Contact(name: "FooBar Server Team", url: "https://myteam.slack.com/archives/teams-chat-room-url", description: "send urgent issues to [#oncall](https://yourteam.slack.com/archives/oncall)")]
public class CustomSchema : FederatedSchema
{

}