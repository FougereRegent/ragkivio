namespace Ragkivio.Graphql.Types;

public partial class Query
{
    public Book GetBook() => new Book { Title = "Test" };
}

public class Book { public string Title { get; set; } = string.Empty; }

public class BookType : ObjectType<Book>
{
    protected override void Configure(IObjectTypeDescriptor<Book> descriptor)
    {
        descriptor.Field(pre => pre.Title)
            .Type<StringType>();
    }
}

public class QueryType : ObjectType<Query>
{

    protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
    {
        descriptor.Authorize();
        descriptor.Field(pre => pre.GetBook())
            .Type<BookType>();
    }
}