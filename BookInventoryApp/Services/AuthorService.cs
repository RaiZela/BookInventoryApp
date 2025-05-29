namespace BookInventoryApp.Services;

public interface IAuthorService
{
    Task<List<Author>> GetAuthorsAsync();
    Task<Author> GetAuthorAsync(Guid id);
    Task<int> SaveAuthorAsync(Author author);
    Task<int> DeleteAuthorAsync(Author author);
}

public class AuthorService : IAuthorService
{
    private readonly SQLiteAsyncConnection _connection;
    public AuthorService(SQLiteAsyncConnection connection)
    {
        _connection = connection;
    }

    public Task<List<Author>> GetAuthorsAsync() =>
   _connection.Table<Author>().ToListAsync();

    public Task<Author> GetAuthorAsync(Guid id) =>
         _connection.Table<Author>().FirstOrDefaultAsync(b => b.Id == id);


    public Task<int> SaveAuthorAsync(Author Author) =>
         _connection.InsertOrReplaceAsync(Author);

    public Task<int> DeleteAuthorAsync(Author Author) =>
        _connection.DeleteAsync(Author);
}
