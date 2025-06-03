using BookInventoryApp.Services;
using System.Collections.ObjectModel;

namespace BookInventoryApp;

public partial class MainPage : ContentPage
{
    IBookService _service;
    IAuthorService _authorService;
    private ObservableCollection<BooksDTO> Books = new ObservableCollection<BooksDTO>();
    private int PageNumber = 1;

    public MainPage(IBookService service, IAuthorService authorService)
    {
        InitializeComponent();
        _service = service;
        _authorService = authorService;
        BooksView.ItemsSource = Books;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var books = await _service.GetPaginatedBooks(PageNumber, 10);
        foreach (var book in books)
        {
            Books.Add(book);
        }
        BooksView.IsRefreshing = true;
    }

    private async Task UpdateBooksView()
    {
        Books.Clear();
        var results = await _service.GetPaginatedBooks(PageNumber, 10);
        foreach (var item in results)
            Books.Add(item);

        BooksView.ItemsSource = Books;
    }

    private async void OnAddBookClicked(object sender, EventArgs e)
    {
        var popup = new AddBookPopup(_service, _authorService);
        await this.ShowPopupAsync(popup);
        await UpdateBooksView();
    }

    private async void OnNextClicked(object sender, EventArgs e)
    {
        PageNumber++;
        await UpdateBooksView();
    }

    private async void OnPreviusClicked(object sender, EventArgs e)
    {
        Books.Clear();
        PageNumber--;
        await UpdateBooksView();
    }
}
