namespace BookInventoryApp.Services;

public interface IBookLoanService
{
    Task<List<BookLoan>> GetLoansAsync();
    Task<int> InsertLoanAsync(BookLoan loan);
    Task<int> InsertAllLoansAsync(IEnumerable<BookLoan> loans);
    Task UpdateLoanAsync(BookLoan loan);
    Task<List<BookLoan>> GetFilteredLoansAsync(string searchText);
}
public class BookLoanService : IBookLoanService
{
    private readonly SQLiteAsyncConnection _connection;

    public BookLoanService(SQLiteAsyncConnection connection)
    {
        _connection = connection;
    }
    public Task<List<BookLoan>> GetLoansAsync() => _connection.Table<BookLoan>().ToListAsync();
    public async Task<List<BookLoan>> GetFilteredLoansAsync(string searchText)
    {
        return await _connection.Table<BookLoan>()
             .Where(t => (t.Title.ToLower()).Contains(searchText.ToLower()))
            .ToListAsync();
    }
    public async Task<int> InsertLoanAsync(BookLoan loan)
    {
        if (loan == null)
        {
            throw new ArgumentNullException(nameof(loan), "Loan cannot be null");
        }

        var book = await _connection.Table<Book>().FirstOrDefaultAsync(b => b.Id == loan.BookId);
        var friend = await _connection.Table<Friend>().FirstOrDefaultAsync(f => f.Id == loan.FriendId);

        book.Adress = friend.Name + " " + friend.LastName;

        await _connection.UpdateAsync(book);

        return await _connection.InsertAsync(loan);
    }

    public async Task<int> InsertAllLoansAsync(IEnumerable<BookLoan> loans)
    {
        return await _connection.InsertAllAsync(loans);
    }
    public Task UpdateLoanAsync(BookLoan loan) => _connection.UpdateAsync(loan);
}
