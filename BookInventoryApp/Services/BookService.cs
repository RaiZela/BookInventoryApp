namespace BookInventoryApp.Services;

public interface IBookService
{
    Task<List<BooksDTO>> GetBooksAsync();
    Task<BookDTO> GetBookAsync(Guid id);
    Task<IEnumerable<BooksDTO>> GetFilteredBooksAsync(string query);
    Task<int> SaveBookAsync(BookDTO book);
    Task<int> DeleteBookAsync(Book book);
}
public class BookService : IBookService
{
    private readonly SQLiteAsyncConnection _connection;

    public BookService(SQLiteAsyncConnection connection)
    {
        _connection = connection;
    }
    public async Task<List<BooksDTO>> GetBooksAsync()
    {
        var booksList = new List<BooksDTO>();
        var books = await _connection.Table<Book>().ToListAsync();
        foreach (var book in books)
        {
            booksList.Add(new BooksDTO
            {
                Title = book.Title,
                Authors = string.Join(',', await GetBookAuthorNames(book.Id))
            });
        }

        return booksList;
    }

    public async Task<BookDTO> GetBookAsync(Guid id)
    {
        var book = await _connection.Table<Book>().FirstOrDefaultAsync(b => b.Id == id);
        return new BookDTO
        {
            Id = id,
            Title = book.Title,
            //YearPublished = book.YearPublished,
            //IsBorrowed = book.IsBorrowed,
            //IsOwned = book.IsOwned,
            Status = book.Status,
            Type = book.Type,
            //IsLended = book.IsLended,
            //LendedTo = book.IsLended ? await GetPresentBookHolder(id) : null,
            //BorrowedBy = book.IsBorrowed ? await GetBorrowedBookOwner(id) : null,
            AuthorIds = await GetBookAuthorIds(id),
            CategoriesIds = await GetBookCategorysIds(id),
            LanguageIds = await GetBookLanguageIds(id)
        };
    }

    public Task<int> SaveBookAsync(BookDTO bookObj)
    {
        var existingBook = _connection.Table<Book>().FirstOrDefaultAsync(b => b.Id == bookObj.Id);
        var book = new Book
        {
            Title = bookObj.Title,
            //YearPublished = bookObj.YearPublished,
            //IsBorrowed = bookObj.IsBorrowed,
            //IsOwned = bookObj.IsOwned,
            Status = bookObj.Status,
            Type = bookObj.Type,
            //IsLended = bookObj.IsLended,
        };

        var bookAuthors = bookObj.AuthorIds.Select(authorId => new BookAuthor
        {
            BookId = book.Id,
            AuthorId = authorId
        });

        var bookCategories = bookObj.CategoriesIds.Select(categoryId => new BookCategory
        {
            BookId = book.Id,
            CategoryId = categoryId
        });

        var bookLanguages = bookObj.LanguageIds.Select(languageId => new BookLanguage
        {
            BookId = book.Id,
            LanguageId = languageId
        });

        _connection.InsertOrReplaceAsync(bookAuthors);
        _connection.InsertOrReplaceAsync(bookCategories);
        _connection.InsertOrReplaceAsync(bookLanguages);
        return _connection.InsertOrReplaceAsync(book);
    }

    public Task<int> DeleteBookAsync(Book Book) =>
        _connection.DeleteAsync(Book);

    public async Task<IEnumerable<BooksDTO>> GetFilteredBooksAsync(string query)
    {
        var books = await _connection.Table<Book>()
            .Where(b => b.Title.Contains(query)
            || (GetBookAuthorNames(b.Id).Result ?? new List<string>()).Any(a => a.Contains(query))
            )
            .ToListAsync();

        return books.Select(b => new BooksDTO
        {
            Title = b.Title,
            Authors = string.Join(',', GetBookAuthorNames(b.Id).Result)
        });
    }


    #region Helpers
    private async Task<IEnumerable<string>> GetBookAuthorNames(Guid bookId)
    {
        var authorNames = (from bookAuthor in await _connection.Table<BookAuthor>().Where(x => x.BookId == bookId).ToListAsync()
                           join author in await _connection.Table<Author>().ToListAsync()
                           on bookAuthor.AuthorId equals author.Id
                           select string.Join(' ', author.Name, author.MiddleName, author.LastName)).ToList();

        return authorNames;
    }

    private async Task<IEnumerable<Guid>> GetBookAuthorIds(Guid bookId)
    {
        var authors = (await _connection.Table<BookAuthor>()
                  .Where(x => x.BookId == bookId)
                  .ToListAsync()).Select(x => x.AuthorId);


        return authors;
    }

    private async Task<List<Guid>> GetBookCategorysIds(Guid bookId)
    {
        var bookCategories = await _connection.Table<BookCategory>()
                .Where(x => x.BookId == bookId)
                .ToListAsync();

        return bookCategories.Select(x => x.CategoryId).ToList();
    }

    private async Task<IEnumerable<Guid>> GetBookLanguageIds(Guid bookId)
    {
        var bookLanguages = await _connection.Table<BookLanguage>()
                .Where(x => x.BookId == bookId)
                .ToListAsync();

        return bookLanguages.Select(x => x.LanguageId);
    }

    private async Task<Guid> GetPresentBookHolder(Guid bookId)
    {

        var personId = (await _connection.Table<LendedBooks>()
                .Where(x => x.BookId == bookId && !x.IsReturned)
                .OrderByDescending(x => x.On)
                .FirstOrDefaultAsync()).PersonId;

        return personId;
    }

    private async Task<Guid> GetBorrowedBookOwner(Guid bookId)
    {

        var bookOwnerId = (await _connection.Table<BorrowedBooks>()
                .Where(x => x.BookId == bookId && !x.IsReturned)
                .OrderByDescending(x => x.On)
                .FirstOrDefaultAsync()).PersonId;

        return bookOwnerId;
    }

    #endregion Helpers
}
