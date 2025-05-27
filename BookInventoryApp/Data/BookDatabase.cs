using BookInventoryApp.Data.Models;
using SQLite;

namespace BookInventoryApp.Data;

public class BookDatabase
{
    readonly SQLiteAsyncConnection _connection;

    public BookDatabase(string dbPath)
    {
        _connection = new SQLiteAsyncConnection(dbPath);
        _connection.CreateTableAsync<Book>().Wait();
        _connection.CreateTableAsync<Author>().Wait();
        _connection.CreateTableAsync<Nationality>().Wait();
        _connection.CreateTableAsync<Category>().Wait();
        _connection.CreateTableAsync<Person>().Wait();
        _connection.CreateTableAsync<Language>().Wait();
    }

    #region Books
    public Task<List<Book>> GetBooksAsync() =>
        _connection.Table<Book>().ToListAsync();


    public Task<Book> GetBookAsync(Guid id) =>
         _connection.Table<Book>().FirstOrDefaultAsync(b => b.Id == id);


    public Task<int> SaveBookAsync(Book Book) =>
         _connection.InsertOrReplaceAsync(Book);

    public Task<int> DeleteBookAsync(Book Book) =>
        _connection.DeleteAsync(Book);
    #endregion Books

    #region Authors
    public Task<List<Author>> GetAuthorsAsync() =>
    _connection.Table<Author>().ToListAsync();


    public Task<Author> GetAuthorAsync(Guid id) =>
         _connection.Table<Author>().FirstOrDefaultAsync(b => b.Id == id);


    public Task<int> SaveAuthorAsync(Author Author) =>
         _connection.InsertOrReplaceAsync(Author);

    public Task<int> DeleteAuthorAsync(Author Author) =>
        _connection.DeleteAsync(Author);
    #endregion Authors

    #region Nationalities
    public Task<List<Nationality>> GetNationalitiessAsync() =>
    _connection.Table<Nationality>().ToListAsync();


    public Task<Nationality> GetNationalityAsync(Guid id) =>
         _connection.Table<Nationality>().FirstOrDefaultAsync(b => b.Id == id);


    public Task<int> SaveNationalityAsync(Nationality Nationality) =>
         _connection.InsertOrReplaceAsync(Nationality);

    public Task<int> DeleteNationalityAsync(Nationality Nationality) =>
        _connection.DeleteAsync(Nationality);
    #endregion Nationalities

    #region Categories
    public Task<List<Category>> GetCategoriesAsync() =>
    _connection.Table<Category>().ToListAsync();


    public Task<Category> GetCategoryAsync(Guid id) =>
         _connection.Table<Category>().FirstOrDefaultAsync(b => b.Id == id);


    public Task<int> SaveCategoryAsync(Category Category) =>
         _connection.InsertOrReplaceAsync(Category);

    public Task<int> DeleteCategoryAsync(Category Category) =>
        _connection.DeleteAsync(Category);
    #endregion Categories 

    #region Languages
    public Task<List<Language>> GetLanguageAsync() =>
    _connection.Table<Language>().ToListAsync();


    public Task<Language> GetLanguageAsync(Guid id) =>
         _connection.Table<Language>().FirstOrDefaultAsync(b => b.Id == id);


    public Task<int> SaveLanguageAsync(Language Language) =>
         _connection.InsertOrReplaceAsync(Language);

    public Task<int> DeleteLanguageAsync(Language Language) =>
        _connection.DeleteAsync(Language);
    #endregion Languages

    #region Persons
    public Task<List<Person>> GetPersonsAsync() =>
    _connection.Table<Person>().ToListAsync();


    public Task<Person> GetPersonAsync(Guid id) =>
         _connection.Table<Person>().FirstOrDefaultAsync(b => b.Id == id);


    public Task<int> SavePersonAsync(Person Person) =>
         _connection.InsertOrReplaceAsync(Person);

    public Task<int> DeletePersonAsync(Person Person) =>
        _connection.DeleteAsync(Person);
    #endregion Persons
}
