
namespace BookInventoryApp.Data;

public class BookDatabase
{
    public BookDatabase(string dbPath, SQLiteAsyncConnection _connection)
    {
        _connection.CreateTableAsync<Book>().Wait();
        _connection.CreateTableAsync<Author>().Wait();
        _connection.CreateTableAsync<Category>().Wait();
        _connection.CreateTableAsync<Friend>().Wait();
        _connection.CreateTableAsync<Language>().Wait();

        _connection.CreateTableAsync<LendedBooks>().Wait();
        _connection.CreateTableAsync<BorrowedBooks>().Wait();
        _connection.CreateTableAsync<BookAuthor>().Wait();
        _connection.CreateTableAsync<BookCategory>().Wait();
        _connection.CreateTableAsync<BookLanguage>().Wait();

    }
}
