using BookInventoryApp.Services;

namespace BookInventoryApp;

public partial class AddBookPopup : Popup
{
    IBookService _service;
    

    public AddBookPopup(IBookService service)
    {
        InitializeComponent();
        _service = service;
        TypeEntry.ItemsSource = Enum.GetValues(typeof(BookType));
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        var book = new BookDTO
        {
            Title = TitleEntry.Text
        };

        await _service.SaveBookAsync(book);

        Close();
    }

}
