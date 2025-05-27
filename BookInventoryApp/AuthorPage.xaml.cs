using CommunityToolkit.Maui.Views;

namespace BookInventoryApp;

public partial class AuthorPage : ContentPage
{
    Data.BookDatabase _db;
    public AuthorPage(Data.BookDatabase db)
    {
        InitializeComponent();
        _db = db;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        AuthorsView.ItemsSource = await _db.GetAuthorsAsync();
    }

    private async void OnAddAuthorClicked(object sender, EventArgs e)
    {
        var popup = new AddAuthorPopup(_db);
        await this.ShowPopupAsync(popup);
    }
}