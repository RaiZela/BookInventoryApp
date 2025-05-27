using BookInventoryApp.Data;
using CommunityToolkit.Maui.Views;

namespace BookInventoryApp;

public partial class MainPage : ContentPage
{
    BookDatabase _db;

    public MainPage(BookDatabase db)
    {
        InitializeComponent();
        _db = db;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        BooksView.ItemsSource = await _db.GetBooksAsync();
    }

    private async void OnAddBookClicked(object sender, EventArgs e)
    {
        var popup = new AddBookPopup(_db);
        await this.ShowPopupAsync(popup);
    }
}
