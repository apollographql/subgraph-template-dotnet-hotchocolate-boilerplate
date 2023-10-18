using ApolloGraphQL.HotChocolate.Federation;

namespace Example;

[Key("id")]
public class Foo
{
    public Foo(string id, string? name)
    {
        Id = id;
        Name = name;
    }

    [ID]
    public string Id { get; }

    public string? Name { get; }

    [ReferenceResolver]
    public static Foo? GetFooById(
        string id,
        Data repository)
        => repository.Foos.FirstOrDefault(t => t.Id.Equals(id));
}
