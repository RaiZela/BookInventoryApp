using BookInventoryApp.Services;

namespace BookInventoryApp;

public partial class MainPage : ContentPage
{
    IBookService _service;
    IAuthorService _authorService;

    public MainPage(IBookService service, IAuthorService authorService)
    {
        InitializeComponent();
        _service = service;
        _authorService = authorService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        BooksView.ItemsSource = await _service.GetBooksAsync();
    }

    private async void OnAddBookClicked(object sender, EventArgs e)
    {
        var popup = new AddBookPopup(_service, _authorService);
        await this.ShowPopupAsync(popup);
        BooksView.ItemsSource = await _service.GetBooksAsync();
    }
}
