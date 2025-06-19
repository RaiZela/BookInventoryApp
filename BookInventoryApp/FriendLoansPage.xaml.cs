using BookInventoryApp.Services;

namespace BookInventoryApp;

public partial class FriendLoansPage : ContentPage
{
    private readonly Friend _friend;
    private readonly IBookLoanService _bookLoanService;
    public FriendLoansPage(Friend friend, IBookLoanService bookLoanService)
    {
        InitializeComponent();
        _friend = friend;
        _bookLoanService = bookLoanService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var allLoans = await _bookLoanService.GetLoansAsync();
        var relevant = allLoans.Where(l => l.FriendId == _friend.Id).ToList();

        var books = await _bookLoanService.GetLoansAsync();
        var loansWithBooks = relevant
            .Select(l => new
            {
                l.Id,
                BookTitle = books.FirstOrDefault(b => b.Id == l.BookId)?.Title ?? "Unknown",
                l.DateBorrowed,
                l.DateReturned,
                l.Direction
            }).ToList();

        CurrentLoans.ItemsSource = loansWithBooks.Where(l => l.DateReturned == null).ToList();
        ReturnedLoans.ItemsSource = loansWithBooks.Where(l => l.DateReturned != null).ToList();
    }

    private async void OnMarkReturned(object sender, EventArgs e)
    {
        if ((sender as SwipeItem)?.CommandParameter is Guid loanId)
        {
            var loan = (await _bookLoanService.GetLoansAsync()).FirstOrDefault(l => l.Id == loanId);
            if (loan != null)
            {
                loan.DateReturned = DateTime.Now;
                await _bookLoanService.UpdateLoanAsync(loan);
                OnAppearing();
            }
        }
    }

}
