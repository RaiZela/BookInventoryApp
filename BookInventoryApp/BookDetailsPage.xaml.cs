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

        if (Guid.TryParse(BookId, out Guid id))
            await _bookService.DeleteBookAsync(id);

        await Shell.Current.GoToAsync($"///{nameof(BookList)}");
    }

    private async void EditButtonClicked(object sender, EventArgs e)
    {
        var popup = new BookPopup(_bookService, _authorService, _categoriesService, _languagesService, Book);
        await this.ShowPopupAsync(popup);
    }
}