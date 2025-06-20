using BookInventoryApp.Services;
using System.Globalization;

namespace BookInventoryApp;

public partial class LendBorrowPopup : Popup
{
    private readonly Friend _friend;
    private readonly LoanDirection _direction;
    private readonly SQLiteAsyncConnection _connection;
    private readonly IBookLoanService _loanService;
    private readonly IBookService _bookService;

    private List<BooksDTO> AllBooks;
    private List<BooksDTO> SelectedBooks;
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
        SelectedBooks = new List<BooksDTO>();
        OnAppearing();

    }


    protected async void OnAppearing()
    {
        AllBooks = await _bookService.GetBooksAsync();
    }

    private void OnBookSearchChanged(object sender, TextChangedEventArgs e)
    {
        var keyword = e.NewTextValue?.ToLower() ?? "";

        if (string.IsNullOrWhiteSpace(keyword))
        {
            BookSuggestions.IsVisible = false;
            return;
        }

        var filtered = AllBooks
            .Where(book => book.Title.ToLower().Contains(keyword.ToLower()))
            .ToList();

        if (filtered.Count == 0 || filtered.Any(r => r.Title.ToLower() != keyword.ToLower()))
        {
            Guid id = Guid.NewGuid();
            var newBook = new BooksDTO
            {
                Title = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(keyword.ToLower()),
                Id = id // Temporary ID for the search term
            };

            filtered.Insert(0, newBook);

        }

        if (SelectedBooks is not null && SelectedBooks.Count() > 0)
            filtered = filtered.Where(r => !SelectedBooks.Any(s => s.Id == r.Id)).ToList();

        if (filtered.Count() > 0)
        {

            BookSuggestions.VerticalScrollBarVisibility = ScrollBarVisibility.Always;
            BookSuggestions.ItemTemplate = new DataTemplate(() =>
            {
                var label = new Label
                {
                    FontSize = 14,
                    VerticalOptions = LayoutOptions.Center,
                    Margin = 10,
                    Padding = 0,
                    WidthRequest = 150,
                    HeightRequest = 50,
                    TextColor = Colors.White
                };
                label.SetBinding(Label.TextProperty, "Title");

                var viewCell = new ViewCell { View = label };
                return viewCell;
            });
            BookSuggestions.ItemsSource = filtered;
            BookSuggestions.IsVisible = true;
        }
    }
    private void OnSuggestionSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is BooksDTO book)
        {
            SelectedBooks.Add(book);
            BookSearchBar.Text = book.Title;
            BookSuggestions.IsVisible = false;
        }
        SelectedBooksView.ItemTemplate = new DataTemplate(() =>
        {
            var nameLabel = new Label
            {
                FontSize = 14,
                TextColor = Colors.White,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Start,
                Margin = new Thickness(0, 5)
            };
            nameLabel.SetBinding(Label.TextProperty, "Title");

            var removeButton = new Button
            {
                Text = "X",
                BackgroundColor = Colors.Transparent,
                WidthRequest = 20,
                HeightRequest = 20,
                Padding = 0,
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center
            };
            removeButton.Clicked += (s, e) =>
            {
                var button = s as Button;
                if (button?.BindingContext is BooksDTO book)
                {
                    SelectedBooks.Remove(book);
                    SelectedBooksView.ItemsSource = null;
                    SelectedBooksView.ItemsSource = SelectedBooks;
                }
            };

            var layout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                VerticalOptions = LayoutOptions.Center,
                Children = { nameLabel, removeButton }
            };

            var frame = new Frame
            {
                BackgroundColor = Colors.MediumPurple,
                Content = layout,
                HasShadow = false,
                Padding = new Thickness(10, 5),
                Margin = new Thickness(5, 5),
                CornerRadius = 16,
            };

            frame.SetBinding(BindingContextProperty, ".");

            return frame;
        });
        SelectedBooksView.ItemsSource = SelectedBooks;
        SelectedBooksView.IsVisible = true;
        BookSearchBar.Text = null;
    }

    public async void OnBorrowConfirm(object sender, EventArgs e)
    {
        var bookObj = sender as BooksDTO;

        var bookId = bookObj?.Id ?? Guid.Empty;
        List<BookLoan> loans = new();

        foreach (var book in SelectedBooks)
        {
            loans.Add(new BookLoan
            {
                BookId = bookId,
                Title = book.Title,
                FriendId = _friend.Id,
                DateBorrowed = DateTime.Now,
                Direction = _direction,
                IsBorrowedByFriend = _direction == LoanDirection.Out,
            });
        }

        var result = await _loanService.InsertAllLoansAsync(loans);
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
