namespace BookInventoryApp.Data.Services;

public interface IBookService
{
    Task<List<BooksDTO>> GetBooksAsync();
    Task<BookDTO> GetBookAsync(Guid id);
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
                Authors = string.Join(',', (await GetBookAuthorNames(book.Id)))
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
            YearPublished = book.YearPublished,
            IsBorrowed = book.IsBorrowed,
            IsOwned = book.IsOwned,
            Status = book.Status,
            Type = book.Type,
            IsLended = book.IsLended,
            LendedTo = book.IsLended ? (await GetPresentBookHolder(id)) : null,
            BorrowedBy = book.IsBorrowed ? (await GetBorrowedBookOwner(id)) : null,
            Authors = string.Join(',', await GetBookAuthorNames(id)),
            Categories = await GetBookCategories(id),
            Languages = await GetBookLanguages(id)
        };
    }

    public Task<int> SaveBookAsync(BookDTO bookObj)
    {
        var existingBook = _connection.Table<Book>().FirstOrDefaultAsync(b => b.Id == bookObj.Id);
        var book = new Book
        {
            Title = bookObj.Title,
            YearPublished = bookObj.YearPublished,
            IsBorrowed = bookObj.IsBorrowed,
            IsOwned = bookObj.IsOwned,
            Status = bookObj.Status,
            Type = bookObj.Type,
            IsLended = bookObj.IsLended,
            IsRead = bookObj.IsRead
        };

        //var bookAuthors = bookObj.Authors.Select(authorId => new BookAuthor
        //{
        //    BookId = book.Id,
        //    AuthorId = authorId
        //});

        var bookCategories = bookObj.Categories.Select(categoryId => new BookCategory
        {
            BookId = book.Id,
            CategoryId = categoryId
        });

        var bookLanguages = bookObj.Languages.Select(languageId => new BookLanguage
        {
            BookId = book.Id,
            LanguageId = languageId
        });

        //_connection.InsertOrReplaceAsync(bookAuthors);
        _connection.InsertOrReplaceAsync(bookCategories);
        _connection.InsertOrReplaceAsync(bookLanguages);
        return _connection.InsertOrReplaceAsync(book);
    }

    public Task<int> DeleteBookAsync(Book Book) =>
        _connection.DeleteAsync(Book);

    #region Helpers
    private async Task<IEnumerable<string>> GetBookAuthorNames(Guid bookId)
    {
        var authors = (await _connection.Table<BookAuthor>()
                .Where(x => x.BookId == bookId)
                .ToListAsync()).Select(x => x.AuthorId);

        var authorNames = (await _connection.Table<Author>()
            .Where(x => authors.Contains(x.Id))
            .ToListAsync()).Select(x => string.Join(' ', x.Name, x.MiddleName, x.LastName));

        return authorNames;
    }

    private async Task<List<Guid>> GetBookCategories(Guid bookId)
    {
        var bookCategories = await _connection.Table<BookCategory>()
                .Where(x => x.BookId == bookId)
                .ToListAsync();

        return bookCategories.Select(x => x.CategoryId).ToList();
    }

    private async Task<IEnumerable<Guid>> GetBookLanguages(Guid bookId)
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
