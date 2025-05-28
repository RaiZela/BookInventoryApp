namespace BookInventoryApp;

public partial class AddBookPopup : Popup
{
    IBookService _service;

    public AddBookPopup(IBookService service)
    {
        InitializeComponent();
        _service = service;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        var book = new BookDTO
        {
            Title = TitleEntry.Text,
            YearPublished = int.Parse(YearEntry.Text),
            IsRead = IsReadSwitch.IsToggled
        };

        await _service.SaveBookAsync(book);

        Close();
    }
}
