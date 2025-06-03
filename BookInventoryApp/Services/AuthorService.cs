namespace BookInventoryApp.Services;

public interface IAuthorService
{
    Task<List<Author>> GetAuthorsAsync();
    Task<Author> GetAuthorAsync(Guid id);
    Task<int> SaveAuthorAsync(Author author);
    Task<int> DeleteAuthorAsync(Author author);
    Task<IEnumerable<string>> GetBookAuthorNames(Guid bookId);
    Task<IEnumerable<Guid>> GetBookAuthorIds(Guid bookId);
    Task<IEnumerable<AuthorDTO>> GetFilteredAuthorsAsync(string query);
}

public class AuthorService : IAuthorService
{
    private readonly SQLiteAsyncConnection _connection;
    public AuthorService(SQLiteAsyncConnection connection)
    {
        _connection = connection;
    }

    public Task<List<Author>> GetAuthorsAsync() =>
   _connection.Table<Author>().Take(10).ToListAsync();

    public Task<Author> GetAuthorAsync(Guid id) =>
         _connection.Table<Author>().FirstOrDefaultAsync(b => b.Id == id);


    public Task<int> SaveAuthorAsync(Author Author) =>
         _connection.InsertOrReplaceAsync(Author);

    public Task<int> DeleteAuthorAsync(Author Author) =>
        _connection.DeleteAsync(Author);

    public async Task<IEnumerable<string>> GetBookAuthorNames(Guid bookId)
    {
        var authorNames = (from bookAuthor in await _connection.Table<BookAuthor>().Where(x => x.BookId == bookId).ToListAsync()
                           join author in await _connection.Table<Author>().ToListAsync()
                           on bookAuthor.AuthorId equals author.Id
                           select string.Join(' ', author.Name, author.MiddleName, author.LastName)).ToList();
        if (authorNames == null)
            return new List<string>();

        return authorNames;
    }

    public async Task<IEnumerable<Guid>> GetBookAuthorIds(Guid bookId)
    {
        var authors = (await _connection.Table<BookAuthor>()
                  .Where(x => x.BookId == bookId)
                  .ToListAsync()).Select(x => x.AuthorId);


        return authors;
    }

    public async Task<IEnumerable<AuthorDTO>> GetFilteredAuthorsAsync(string query)
    {
        var authors = await _connection.Table<Author>()
            .Where(a => a.Name.ToLower().Contains(query.ToLower()) || a.LastName.ToLower().Contains(query.ToLower()))
            .Take(10)
            .ToListAsync();

        return authors.Select(a => new AuthorDTO
        {
            Id = a.Id,
            FullName = a.FullName
        }).ToList();
    }
}
