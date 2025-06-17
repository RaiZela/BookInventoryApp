using BookInventoryApp.Services;

namespace BookInventoryApp;

public partial class BookDetailsPage : ContentPage, IQueryAttributable
{
    private readonly IBookService _bookService;
    private readonly IAuthorService _authorService;
    private readonly ICategoriesService _categoriesService;
    private readonly ILanguagesService _languagesService;
    private string BookId;
    private BookDTO Book;
    public BookDetailsPage(IBookService bookService, IAuthorService authorService, ICategoriesService categoriesService, ILanguagesService languagesService)
    {
        InitializeComponent();
        _bookService = bookService;
        _authorService = authorService;
        _categoriesService = categoriesService;
        _languagesService = languagesService;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("bookId"))
        {
            BookId = query["bookId"].ToString();
            if (Guid.TryParse(BookId, out Guid id))
            {
                LoadBookDetails(id);
            }
            else
            {
                DisplayAlert("Error", "Invalid book ID.", "OK");
            }
        }
    }
    private async void LoadBookDetails(Guid id)
    {
        Book = await _bookService.GetBookAsync(id);
        var bookAuthors = await _authorService.GetBookAuthorNames(id);
        var bookCategories = await _categoriesService.GetBookCategoriesAsync(id);
        var bookLanguages = await _languagesService.GetBookLanguagesAsync(id);

        if (Book != null)
        {
            Title = "DETAILS";
            BookTitleLabel.Text = Book.Title.ToUpper();
            BookStatusLabel.Text = Book.Status.ToString();
            BookTypeLabel.Text = Book.Type.ToString();
            BookAuthorsLabel.Text = string.Join(Environment.NewLine, bookAuthors);
            BookCategoriesLabel.Text = string.Join(Environment.NewLine, bookCategories);
            BookLanguagesLabel.Text = string.Join(Environment.NewLine, bookLanguages);
        }
        else
        {
            await DisplayAlert("Error", "Book not found.", "OK");
        }
    }
    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {

        bool confirm = await Application.Current.MainPage.DisplayAlert("Warning", "Are you sure?", "Yes", "No");

        if (confirm && (Guid.TryParse(BookId, out Guid id)))
        {
            var books = await _bookService.DeleteBookAsync(id);

            if (books > 0)
            {
                await AnimateSuccessAsync();
                await ShowSuccessCheckAsync();
            }
            else
            {
                await AnimateErrorAsync();
                await ShowErrorAsync();
            }

        }

        await Shell.Current.GoToAsync($"///{nameof(BookList)}");
    }
    private async void EditButtonClicked(object sender, EventArgs e)
    {
        if (DeviceInfo.Platform == DevicePlatform.WinUI)
        {
            var popup = new BookPopup(_bookService, _authorService, _categoriesService, _languagesService, Book);
            await this.ShowPopupAsync(popup);
        }
        else if (DeviceInfo.Platform == DevicePlatform.Android)
        {
            var popup = new BookPopupAndroid(_bookService, _authorService, _categoriesService, _languagesService, Book);
            await this.ShowPopupAsync(popup);
        }
        else
        {
            var popup = new BookPopupAndroid(_bookService, _authorService, _categoriesService, _languagesService, Book);
            await this.ShowPopupAsync(popup);
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

        await Task.Delay(1000); // display time

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