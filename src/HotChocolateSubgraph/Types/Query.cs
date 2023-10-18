namespace Example;

public class Query
{
    public Foo? GetFoo([ID] string id, Data repository)
        => repository.Foos.FirstOrDefault(t => t.Id.Equals(id));
}
