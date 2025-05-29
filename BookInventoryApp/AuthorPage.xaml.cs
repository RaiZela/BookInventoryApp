using BookInventoryApp.Services;

namespace BookInventoryApp;

public partial class AuthorPage : ContentPage
{
    IAuthorService _service;
    public AuthorPage(IAuthorService service)
    {
        InitializeComponent();
        _service = service;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        AuthorsView.ItemsSource = await _service.GetAuthorsAsync();
    }

    private async void OnAddAuthorClicked(object sender, EventArgs e)
    {
        var popup = new AddAuthorPopup(_service);
        await this.ShowPopupAsync(popup);
    }
}