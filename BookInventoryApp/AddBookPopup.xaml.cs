using BookInventoryApp.Data;
using BookInventoryApp.Data.Models;
using CommunityToolkit.Maui.Views;

namespace BookInventoryApp;

public partial class AddBookPopup : Popup
{
    BookDatabase _db;

    public AddBookPopup(BookDatabase db)
    {
        InitializeComponent();
        _db = db;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        var book = new Book
        {
            Title = TitleEntry.Text,
            //Author = AuthorEntry.Text,
            //Genre = GenreEntry.Text,
            YearPublished = int.Parse(YearEntry.Text),
            IsRead = IsReadSwitch.IsToggled
        };

        await _db.SaveBookAsync(book);
        //await Navigation.PopAsync(); // return to MainPage
        Close();
    }
}
