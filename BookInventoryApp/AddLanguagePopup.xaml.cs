namespace BookInventoryApp;

public partial class AddLanguagePopup : Popup
{
    ILanguagesService _languagesService;
    public AddLanguagePopup(ILanguagesService languagesService)
    {
        InitializeComponent();
        _languagesService = languagesService;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        var language = new Language
        {
            Name = Name.Text,
            Code = Code.Text,
        };
        await _languagesService.SaveLanguageAsync(language);
        Close();
    }
}