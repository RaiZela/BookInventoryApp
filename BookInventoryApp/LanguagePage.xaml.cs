namespace BookInventoryApp;

public partial class LanguagePage : ContentPage
{
    ILanguagesService _languagesService;
    public LanguagePage(ILanguagesService languagesService)
    {
        InitializeComponent();
        _languagesService = languagesService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        LanguageView.ItemsSource = await _languagesService.GetLanguageAsync();
    }

    private async void OnAddLanguageClicked(object sender, EventArgs e)
    {
        var popup = new AddLanguagePopup(_languagesService);
        await this.ShowPopupAsync(popup);
    }
}