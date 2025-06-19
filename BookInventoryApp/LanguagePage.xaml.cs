using BookInventoryApp.Services;

namespace BookInventoryApp;

public partial class LanguagePage : ContentPage
{
    private readonly ILanguagesService _service;
    public LanguagePage(ILanguagesService service)
    {
        InitializeComponent();
        _service = service;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        LanguageView.ItemsSource = await _service.GetLanguagesAsync();
    }

    private async void OnAddLanguageClicked(object sender, EventArgs e)
    {
        var popup = new AddLanguagePopup(_service);
        await this.ShowPopupAsync(popup);
        LanguageView.ItemsSource = await _service.GetLanguagesAsync();
    }

    private async void OnDeleteLanguageClicked(object sender, EventArgs e)
    {
        if (sender is SwipeItem item && item.CommandParameter is LanguageDTO Language)
        {
            bool confirm = await DisplayAlert("Confirm Delete", $"Delete Language '{Language.Name}'?", "Yes", "No");
            if (confirm)
            {
                await _service.DeleteLanguageAsync(Language.Id);
                LanguageView.ItemsSource = await _service.GetLanguagesAsync();
            }
        }
    }

}