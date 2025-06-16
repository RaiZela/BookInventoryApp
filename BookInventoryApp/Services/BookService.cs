namespace BookInventoryApp.Services;

public interface IBookService
{
    Task<List<BooksDTO>> GetBooksAsync();
    Task<List<BooksDTO>> GetPaginatedBooks(int pageNumber, int numberOfBooks);
    Task<BookDTO> GetBookAsync(Guid id);
    Task<IEnumerable<BooksDTO>> GetFilteredBooksAsync(string query);
    Task<int> SaveNewBookAsync(BookDTO book);
    Task<int> SaveBookAsync(BookDTO book);
    Task<int> DeleteBookAsync(Guid id);
}
public class BookService : IBookService
{
    private readonly SQLiteAsyncConnection _connection;
    private readonly IAuthorService _authorService;
    private readonly ICategoriesService _categoriesService;
    private readonly ILanguagesService _languagesService;
    public BookService(SQLiteAsyncConnection connection, IAuthorService authorService, ICategoriesService categoriesService, ILanguagesService languagesService)
    {
        _connection = connection;
        _authorService = authorService;
        _categoriesService = categoriesService;
        _languagesService = languagesService;
    }
    public async Task<List<BooksDTO>> GetBooksAsync()
    {
        var booksList = new List<BooksDTO>();
        var books = await _connection.Table<Book>().ToListAsync();
        foreach (var book in books)
        {
            var authors = await _authorService.GetBookAuthorNames(book.Id);
            var categories = await _categoriesService.GetBookCategoriesAsync(book.Id);
            var languages = await _languagesService.GetBookLanguagesAsync(book.Id);
            booksList.Add(new BooksDTO
            {
                Id = book.Id,
                Title = book.Title,
                Authors = string.Join(',', authors),
                Categories = string.Join(',', categories),
                Languages = string.Join(',', categories)
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
            Title = book.Title.ToUpper(),
            Status = book.Status,
            Type = book.Type,
            AuthorIds = await _authorService.GetBookAuthorIds(id),
            CategoriesIds = await GetBookCategorysIds(id),
            LanguageIds = await GetBookLanguageIds(id)
        };
    }

    public async Task<Book> GetBookRecordAsync(Guid id)
    {
        var book = await _connection.Table<Book>().FirstOrDefaultAsync(b => b.Id == id);
        return book;
    }

    public async Task<int> SaveNewBookAsync(BookDTO bookObj)
    {
        var existingBook = await _connection.Table<Book>().FirstOrDefaultAsync(b => b.Id == bookObj.Id);
        var book = new Book
        {
            Title = bookObj.Title,
            Status = bookObj.Status,
            Type = bookObj.Type,
        };

        var bookAuthors = bookObj.AuthorIds.Select(authorId => new BookAuthor
        {
            BookId = book.Id,
            AuthorId = authorId
        }).ToList();

        var bookCategories = bookObj.CategoriesIds.Select(categoryId => new BookCategory
        {
            BookId = book.Id,
            CategoryId = categoryId
        }).ToList();

        var bookLanguages = bookObj.LanguageIds.Select(languageId => new BookLanguage
        {
            BookId = book.Id,
            LanguageId = languageId
        }).ToList();

        var test = await _connection.Table<BookAuthor>().ToListAsync();
        try
        {
            foreach (var author in bookAuthors)
            {
                await _connection.InsertOrReplaceAsync(author);
            }

            foreach (var category in bookCategories)
            {
                await _connection.InsertOrReplaceAsync(category);
            }

            foreach (var language in bookLanguages)
            {
                await _connection.InsertOrReplaceAsync(language);
            }

            return await _connection.InsertAsync(book);

        }
        catch (Exception ex)
        {

            throw;
        }
    }
    public async Task<int> SaveBookAsync(BookDTO bookObj)
    {
        var existingBook = await _connection.Table<Book>().FirstOrDefaultAsync(b => b.Id == bookObj.Id);

        existingBook.Title = bookObj.Title;
        existingBook.Status = bookObj.Status;
        existingBook.Type = bookObj.Type;

        var bookAuthors = bookObj.AuthorIds.Select(authorId => new BookAuthor
        {
            BookId = existingBook.Id,
            AuthorId = authorId
        }).ToList();

        var bookCategories = bookObj.CategoriesIds.Select(categoryId => new BookCategory
        {
            BookId = existingBook.Id,
            CategoryId = categoryId
        }).ToList();

        var bookLanguages = bookObj.LanguageIds.Select(languageId => new BookLanguage
        {
            BookId = existingBook.Id,
            LanguageId = languageId
        }).ToList();

        try
        {
            foreach (var author in bookAuthors)
            {
                await _connection.InsertOrReplaceAsync(author);
            }

            foreach (var category in bookCategories)
            {
                await _connection.InsertOrReplaceAsync(category);
            }

            foreach (var language in bookLanguages)
            {
                await _connection.InsertOrReplaceAsync(language);
            }

            return await _connection.UpdateAsync(existingBook);

        }
        catch (Exception ex)
        {

            throw;
        }
    }

    public async Task<int> DeleteBookAsync(Guid id)
    {
        var book = await GetBookRecordAsync(id);
        return await _connection.DeleteAsync(book);
    }

    public async Task<IEnumerable<BooksDTO>> GetFilteredBooksAsync(string query)
    {
        var books = await _connection.Table<Book>()
            .Where(b => b.Title.ToLower().Contains(query.ToLower()))
            .ToListAsync();

        var booksDTOs = new List<BooksDTO>();
        foreach (var book in books)
        {
            var authors = await _authorService.GetBookAuthorNames(book.Id);
            var categories = await _categoriesService.GetBookCategoriesAsync(book.Id);
            var languages = await _languagesService.GetBookLanguagesAsync(book.Id);
            booksDTOs.Add(new BooksDTO
            {
                Id = book.Id,
                Title = book.Title.ToUpper(),
                Authors = string.Join(',', authors),
                Categories = string.Join(',', categories),
                Languages = string.Join(',', categories)
            });
        }
        return booksDTOs;
    }

    public async Task<List<BooksDTO>> GetPaginatedBooks(int pageNumber, int numberOfBooks)
    {
        var booksList = new List<BooksDTO>();

        List<Book> books = new();

        if (pageNumber == 1)
        {
            books = await _connection.Table<Book>()
               .Take(numberOfBooks)
               .ToListAsync();
        }
        else
            books = await _connection.Table<Book>()
                 .Skip((pageNumber - 1) * numberOfBooks)
                 .Take(numberOfBooks)
                 .ToListAsync();

        foreach (var book in books)
        {
            var authors = await _authorService.GetBookAuthorNames(book.Id);
            var categories = await _categoriesService.GetBookCategoriesAsync(book.Id);

            var languages = await _languagesService.GetBookLanguagesAsync(book.Id);
            booksList.Add(new BooksDTO
            {
                Id = book.Id,
                Title = book.Title.ToUpper(),
                Authors = string.Join(Environment.NewLine, authors),
                Categories = string.Join(Environment.NewLine, categories),
                Languages = string.Join(Environment.NewLine, languages)
            });

        }

        return booksList;
    }


    #region Helpers
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
