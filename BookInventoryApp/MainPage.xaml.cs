using BookInventoryApp.Services;

namespace BookInventoryApp;

public partial class MainPage : ContentPage
{
    IBookService _service;

    public MainPage(IBookService service)
    {
        InitializeComponent();
        _service = service;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        BooksView.ItemsSource = await _service.GetBooksAsync();
    }

    private async void OnAddBookClicked(object sender, EventArgs e)
    {
        var popup = new AddBookPopup(_service);
        await this.ShowPopupAsync(popup);
        BooksView.ItemsSource = await _service.GetBooksAsync();
    }
}
