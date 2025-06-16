namespace BookInventoryApp.Services;

public interface IAuthorService
{
    Task<List<Author>> GetAuthorsAsync();
    Task<Author> GetAuthorAsync(Guid id);
    Task<int> SaveNewAuthorAsync(Author author);
    Task<int> SaveAuthorAsync(Author Author);
    Task<int> DeleteAuthorAsync(Author author);
    Task<IEnumerable<string>> GetBookAuthorNames(Guid bookId);
    Task<IEnumerable<Guid>> GetBookAuthorIds(Guid bookId);
    Task<IEnumerable<AuthorDTO>> GetFilteredAuthorsAsync(string query);
    IEnumerable<AuthorDTO> GetAuthorsById(List<Guid> ids);
}

public class AuthorService : IAuthorService
{
    private readonly SQLiteAsyncConnection _connection;
    public AuthorService(SQLiteAsyncConnection connection)
    {
        _connection = connection;
    }

    public async Task<List<Author>> GetAuthorsAsync() =>
   await _connection.Table<Author>().Take(10).ToListAsync();

    public async Task<Author> GetAuthorAsync(Guid id) =>
         await _connection.Table<Author>().FirstOrDefaultAsync(b => b.Id == id);


    public async Task<int> SaveNewAuthorAsync(Author Author) =>
         await _connection.InsertOrReplaceAsync(Author);

    public async Task<int> SaveAuthorAsync(Author Author) =>
      await _connection.UpdateAsync(Author);

    public async Task<int> DeleteAuthorAsync(Author Author) =>
        await _connection.DeleteAsync(Author);

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


    public IEnumerable<AuthorDTO> GetAuthorsById(List<Guid> ids)
    {
        var authors = _connection.Table<Author>()
            .Where(x => ids.Contains(x.Id)).ToListAsync();

        return authors.Result.Select(a => new AuthorDTO
        {
            Id = a.Id,
            FullName = string.Join(' ', a.Name, a.MiddleName, a.LastName).Trim()
        }).ToList();
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
            FullName = string.Join(' ', a.Name, a.MiddleName, a.LastName).Trim()
        }).ToList();
    }
}
