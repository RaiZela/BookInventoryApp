using BookInventoryApp.Services;
using System.Collections.ObjectModel;

namespace BookInventoryApp;

public partial class MainPage : ContentPage
{
    IBookService _service;
    IAuthorService _authorService;
    private ObservableCollection<BooksDTO> Books = new ObservableCollection<BooksDTO>();
    private int PageNumber = 1;

    public MainPage(IBookService service, IAuthorService authorService)
    {
        InitializeComponent();
        _service = service;
        _authorService = authorService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var books = await _service.GetPaginatedBooks(PageNumber, 10);
        foreach (var book in books)
        {
            Books.Add(book);
        }
        BooksView.IsVisible = true;
        BooksView.ItemsSource = Books;
        BooksView.IsRefreshing = true;
        BooksView.VerticalScrollBarVisibility = ScrollBarVisibility.Always;
        BooksView.BackgroundColor = Color.FromHex("#362b7d");
        BooksView.Opacity = 0.7;
        BooksView.Margin = 10;

        BooksView.ItemTemplate = new DataTemplate(() =>
        {
            var titleLabel = new Label
            {
                FontSize = 18,
                VerticalOptions = LayoutOptions.Center,
                Margin = 10,
                Padding = 5,
                TextColor = Colors.GhostWhite,
                FontAttributes = FontAttributes.Bold,
                FontFamily = "VictorMono-LightItalic"
            };
            titleLabel.SetBinding(Label.TextProperty, "Title");

            var authorLabel = new Label
            {
                FontSize = 14,
                VerticalOptions = LayoutOptions.Center,
                Margin = new Thickness(10, 0, 10, 5),
                Padding = 5,
                TextColor = Colors.LightGray,
                FontAttributes = FontAttributes.Bold,
                FontFamily = "VictorMono-Light"
            };
            authorLabel.SetBinding(Label.TextProperty, "Authors");

            var divider = new BoxView
            {
                HeightRequest = 1,
                Color = Colors.Gray,
                HorizontalOptions = LayoutOptions.Fill,
                Margin = new Thickness(10, 5, 10, 0)
            };

            var stackLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Children = { titleLabel, authorLabel, divider }
            };

            var viewCell = new ViewCell { View = stackLayout };
            return viewCell;
        });
    }

    private async Task UpdateBooksView()
    {
        Books.Clear();
        var results = await _service.GetPaginatedBooks(PageNumber, 10);
        foreach (var item in results)
            Books.Add(item);
    }

    private async void OnAddBookClicked(object sender, EventArgs e)
    {
        var popup = new AddBookPopup(_service, _authorService);
        await this.ShowPopupAsync(popup);
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
}
