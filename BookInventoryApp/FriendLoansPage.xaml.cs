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
        var enumValues = Enum.GetValues(typeof(LoanDirection));
        ConditionPicker.ItemsSource = enumValues;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        UpdateLoansView();
    }

    private async void UpdateLoansView(string? searchText = "", LoanDirection? loanDirection = null)
    {
        List<BookLoan> allLoans = new();
        if (string.IsNullOrEmpty(searchText))
            allLoans = await _bookLoanService.GetLoansAsync();
        else
            allLoans = await _bookLoanService.GetFilteredLoansAsync(searchText);

        if (loanDirection is not null)
            allLoans = allLoans.Where(x => x.Direction == loanDirection).ToList();

        var relevant = allLoans.Where(l => l.FriendId == _friend.Id).ToList();

        var loansWithBooks = relevant
            .Select(l => new
            {
                l.Id,
                BookTitle = l.Title ?? "Unknown",
                l.DateBorrowed,
                l.DateReturned,
                l.Direction,
                l.IsBorrowedByFriend,
                l.IsReturned
            }).ToList();

        CurrentLoans.ItemsSource = loansWithBooks.Where(l => !l.IsReturned).ToList();
        ReturnedLoans.ItemsSource = loansWithBooks.Where(l => l.IsReturned).ToList();
    }

    private async void OnMarkReturned(object sender, EventArgs e)
    {
        if (sender is SwipeItem item && item.CommandParameter is Guid loanId)
        {
            var loan = (await _bookLoanService.GetLoansAsync()).FirstOrDefault(l => l.Id == loanId);
            if (loan != null)
            {
                loan.DateReturned = DateTime.Now;
                loan.IsReturned = true;
                await _bookLoanService.UpdateLoanAsync(loan);
                UpdateLoansView();
            }
        }
    }

    private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        string searchText = e.NewTextValue?.ToLower() ?? "";
        UpdateLoansView(searchText);
    }

    private void ConditionPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ConditionPicker.SelectedItem is LoanDirection selectedCondition)
        {
            UpdateLoansView(loanDirection: selectedCondition);
        }
    }

}
