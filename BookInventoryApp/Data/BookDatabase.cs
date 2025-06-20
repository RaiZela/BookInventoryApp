
namespace BookInventoryApp.Data;

public class BookDatabase
{
    public BookDatabase(string dbPath, SQLiteAsyncConnection _connection)
    {
        //_connection.DeleteAllAsync<Book>().Wait();
        //_connection.DeleteAllAsync<Author>().Wait();
        //_connection.DeleteAllAsync<Category>().Wait();
        //_connection.DeleteAllAsync<Friend>().Wait();
        //_connection.DeleteAllAsync<Language>().Wait();
        //_connection.DeleteAllAsync<BookLoan>().Wait();
        //_connection.DeleteAllAsync<BookAuthor>().Wait();
        //_connection.DeleteAllAsync<BookCategory>().Wait();
        //_connection.DeleteAllAsync<BookLanguage>().Wait();


        _connection.CreateTableAsync<Book>().Wait();
        _connection.CreateTableAsync<Author>().Wait();
        _connection.CreateTableAsync<Category>().Wait();
        _connection.CreateTableAsync<Friend>().Wait();
        _connection.CreateTableAsync<Language>().Wait();
        _connection.CreateTableAsync<BookLoan>().Wait();
        _connection.CreateTableAsync<BookAuthor>().Wait();
        _connection.CreateTableAsync<BookCategory>().Wait();
        _connection.CreateTableAsync<BookLanguage>().Wait();

    }
}
