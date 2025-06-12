using BookInventoryApp.Services;

namespace BookInventoryApp;

public partial class AddAuthorPopup : Popup
{
    IAuthorService _service;
    public AddAuthorPopup(IAuthorService service)
    {
        InitializeComponent();
        _service = service;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        var author = new Author
        {
            Name = Name.Text,
            MiddleName = MiddleName.Text,
            LastName = LastName.Text,
        };

        await _service.SaveAuthorAsync(author);
        Close();
    }
}