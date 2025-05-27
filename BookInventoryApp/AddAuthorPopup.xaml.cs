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
        NationalityPicker.ItemsSource = _db.GetNationalitiessAsync().Result;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        var selectedNationality = NationalityPicker.SelectedItem;
        var author = new Author
        {
            Name = Name.Text,
            MiddleName = MiddleName.Text,
            LastName = LastName.Text,
            //Nationality = (Nationality)selectedNationality
        };

        await _db.SaveAuthorAsync(author);
        Close();
    }
}