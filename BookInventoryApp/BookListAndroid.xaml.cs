using BookInventoryApp.Services;
using System.Collections.ObjectModel;

namespace BookInventoryApp;

public partial class BookListAndroid : ContentPage
{
    IBookService _service;
    IAuthorService _authorService;
    ICategoriesService _categoriesService;
    ILanguagesService _languagesService;
    private ObservableCollection<BooksDTO> Books = new ObservableCollection<BooksDTO>();
    private int PageNumber = 1;

    public BookListAndroid(IBookService service, IAuthorService authorService, ICategoriesService categoriesService, ILanguagesService languagesService)
    {
        InitializeComponent();
        _service = service;
        _authorService = authorService;
        _categoriesService = categoriesService;
        _languagesService = languagesService;
        BindingContext = this;

    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        Books.Clear();
        var books = await _service.GetPaginatedBooks(PageNumber, 10);
        foreach (var book in books.Records)
        {
            Books.Add(book);
        }

        if (books.PageNumber == 1)
            PreviousButton.IsEnabled = false;
        else
            PreviousButton.IsEnabled = true;

        if (books.PageNumber * books.PageSize >= books.TotalItems)
            NextButton.IsEnabled = false;
        else
            NextButton.IsEnabled = true;
        BooksView.IsVisible = true;
        BooksView.ItemsSource = Books;
        BooksView.VerticalScrollBarVisibility = ScrollBarVisibility.Always;
        BooksView.BackgroundColor = Color.FromHex("#362b7d");
        BooksView.Opacity = 0.7;
        BooksView.Margin = 10;
    }

    private async Task UpdateBooksView()
    {
        Books.Clear();
        var results = await _service.GetPaginatedBooks(PageNumber, 10);
        foreach (var item in results.Records)
            Books.Add(item);

        if (results.PageNumber == 1)
            PreviousButton.IsEnabled = false;
        else
            PreviousButton.IsEnabled = true;

        if (results.PageNumber * results.PageSize >= results.TotalItems)
            NextButton.IsEnabled = false;
        else
            NextButton.IsEnabled = true;
    }

    private async void OnAddBookClicked(object sender, EventArgs e)
    {
        if (DeviceInfo.Platform == DevicePlatform.WinUI)
        {
            var popup = new BookPopup(_service, _authorService, _categoriesService, _languagesService, new BookDTO());
            await this.ShowPopupAsync(popup);
        }
        else if (DeviceInfo.Platform == DevicePlatform.Android)
        {
            var popup = new BookPopupAndroid(_service, _authorService, _categoriesService, _languagesService, new BookDTO());
            await this.ShowPopupAsync(popup);
        }
        else
        {
            var popup = new BookPopupAndroid(_service, _authorService, _categoriesService, _languagesService, new BookDTO());
            await this.ShowPopupAsync(popup);
        }
        await UpdateBooksView();
    }


    private async void OnNextClicked(object sender, EventArgs e)
    {
        PageNumber++;
        await UpdateBooksView();
    }

    private async void OnPreviusClicked(object sender, EventArgs e)
    {
        Books.Clear();
        PageNumber--;
        await UpdateBooksView();
    }

    //private async void BooksView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    //{
    //    var book = e.SelectedItem as BooksDTO;
    //    await Shell.Current.GoToAsync($"{nameof(BookDetailsPage)}?bookId={book.Id}");
    //}

    private async void BooksView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        var book = e.Item as BooksDTO;
        await Shell.Current.GoToAsync($"{nameof(BookDetailsPage)}?bookId={book.Id}");
    }
}
