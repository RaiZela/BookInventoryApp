namespace BookInventoryApp.Services;


public interface ILendedBooksService
{
    Task<List<BookLoanInfo>> GetBookLoanInfoByFriendIdAsync(Guid friendId);
}
public class LendedBooksService : ILendedBooksService
{
    private readonly SQLiteAsyncConnection _connection;
    public LendedBooksService(SQLiteAsyncConnection connection)
    {
        _connection = connection;
    }
    public async Task<List<BookLoanInfo>> GetBookLoanInfoByFriendIdAsync(Guid friendId)
    {
        var query = @$"
        SELECT f.Name AS {nameof(Friend.Name)},
               f.LastName AS {nameof(Friend.LastName)},
               b.Title AS {nameof(Book.Title)},
               l.DateBorrowed,
               l.DateReturned
        FROM BookLoan l
        JOIN Friend f ON f.Id = l.{nameof(BookLoan.FriendId)}
        JOIN Book b ON b.Id = l.{nameof(BookLoan.BookId)}
        WHERE f.Id = ?";

        return await _connection.QueryAsync<BookLoanInfo>(query, friendId);
    }

}
