using BookInventoryApp.Services;

namespace BookInventoryApp;

public partial class LendBorrowPopup : Popup
{
    private readonly Friend _friend;
    private readonly LoanDirection _direction;
    private readonly SQLiteAsyncConnection _connection;
    private readonly IBookLoanService _loanService;
    private readonly IBookService _bookService;

    private List<BooksDTO> _allBooks;
    private BooksDTO _selectedBook;

    public LendBorrowPopup(Friend friend,
        LoanDirection direction,
        SQLiteAsyncConnection connection,
        IBookLoanService loanService,
        IBookService bookService)
    {
        InitializeComponent();
        _friend = friend;
        _direction = direction;
        _connection = connection;
        _loanService = loanService;
        _bookService = bookService;
        PopupTitle.Text = direction == LoanDirection.Out ? "Lend Book" : "Borrow Book";

        OnAppearing();

    }


    protected async void OnAppearing()
    {
        _allBooks = await _bookService.GetBooksAsync();
    }

    private void OnBookSearchChanged(object sender, TextChangedEventArgs e)
    {
        var keyword = e.NewTextValue?.ToLower() ?? "";

        if (string.IsNullOrWhiteSpace(keyword))
        {
            BookSuggestions.IsVisible = false;
            _selectedBook = null;
            return;
        }

        var filtered = _allBooks
            .Where(book => book.Title.ToLower().Contains(keyword))
            .ToList();

        BookSuggestions.ItemsSource = filtered;
        BookSuggestions.IsVisible = filtered.Any();
        _selectedBook = null;
    }

    private void OnSuggestionSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is BooksDTO book)
        {
            _selectedBook = book;
            BookSearchBar.Text = book.Title;
            BookSuggestions.IsVisible = false;
        }
    }

    public async void OnBorrowConfirm(object sender, EventArgs e)
    {
        var enteredTitle = BookSearchBar.Text?.Trim();

        var bookId = _selectedBook?.Id ?? Guid.Empty;

        var loan = new BookLoan
        {
            BookId = bookId,
            Title = enteredTitle,
            FriendId = _friend.Id,
            DateBorrowed = DateTime.Now,
            Direction = LoanDirection.In
        };

        var result = await _loanService.InsertLoanAsync(loan);
        if (result > 0)
        {
            await AnimateSuccessAsync();
            await ShowSuccessCheckAsync();
            Close();
        }
        else
        {
            await AnimateErrorAsync();
            await ShowErrorAsync("Failed to record the loan.");
        }
    }
    private async Task AnimateSuccessAsync()
    {
        await MainLayout.ScaleTo(1.05, 100, Easing.CubicOut);
        await MainLayout.ScaleTo(1.0, 100, Easing.CubicIn);
        await MainLayout.FadeTo(0.9, 100);
        await MainLayout.FadeTo(1.0, 100);
    }
    private async Task ShowSuccessCheckAsync()
    {
        SuccessOverlay.Opacity = 0;
        SuccessOverlay.IsVisible = true;

        SuccessIcon.Opacity = 0;
        SuccessIcon.Scale = 0.5;

        await Task.WhenAll(
            SuccessOverlay.FadeTo(1, 150),
            SuccessIcon.FadeTo(1, 200),
            SuccessIcon.ScaleTo(1.2, 200, Easing.SpringOut)
        );

        await Task.Delay(1000);

        await SuccessOverlay.FadeTo(0, 300);
        SuccessOverlay.IsVisible = false;
    }
    private async Task ShowErrorAsync(string message = "Something went wrong")
    {
        ErrorMessage.Text = message;

        ErrorOverlay.Opacity = 0;
        ErrorOverlay.IsVisible = true;

        ErrorIcon.Opacity = 0;
        ErrorIcon.Scale = 0.5;
        ErrorMessage.Opacity = 0;
        ErrorMessage.Scale = 0.5;

        await Task.WhenAll(
            ErrorOverlay.FadeTo(1, 150),
            ErrorIcon.FadeTo(1, 200),
            ErrorIcon.ScaleTo(1.2, 200, Easing.SpringOut),
            ErrorMessage.FadeTo(1, 200),
            ErrorMessage.ScaleTo(1.2, 200, Easing.SpringOut)
        );

        await Task.Delay(2000);

        await ErrorOverlay.FadeTo(0, 300);
        ErrorOverlay.IsVisible = false;
    }
    private async Task AnimateErrorAsync()
    {
        uint duration = 50;
        await MainLayout.TranslateTo(-15, 0, duration);
        await MainLayout.TranslateTo(15, 0, duration);
        await MainLayout.TranslateTo(-10, 0, duration);
        await MainLayout.TranslateTo(10, 0, duration);
        await MainLayout.TranslateTo(0, 0, duration);
    }

}
