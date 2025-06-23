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
        return await _connection.InsertAsync(loan);
    }

    public async Task<int> InsertAllLoansAsync(IEnumerable<BookLoan> loans)
    {
        return await _connection.InsertAllAsync(loans);
    }
    public Task UpdateLoanAsync(BookLoan loan) => _connection.UpdateAsync(loan);
}
