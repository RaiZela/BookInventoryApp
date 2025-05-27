using BookInventoryApp.Data;
using BookInventoryApp.Data.Models;
using CommunityToolkit.Maui.Views;
namespace BookInventoryApp;

public partial class AddAuthorPopup : Popup
{
    BookDatabase _db;
    public AddAuthorPopup(BookDatabase db)
    {
        InitializeComponent();
        _db = db;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        var author = new Author
        {
            Name = Name.Text,
            MiddleName = MiddleName.Text,
            LastName = LastName.Text,
        };

        await _db.SaveAuthorAsync(author);
        Close();
    }
}