using BookInventoryApp.Services;
using System.Collections.ObjectModel;

namespace BookInventoryApp;

public partial class MainPage : ContentPage
{
    IBookService _service;
    IAuthorService _authorService;
    ICategoriesService _categoriesService;
    ILanguagesService _languagesService;
    private ObservableCollection<BooksDTO> Books = new ObservableCollection<BooksDTO>();
    private int PageNumber = 1;

    public MainPage(IBookService service, IAuthorService authorService, ICategoriesService categoriesService, ILanguagesService languagesService)
    {
        InitializeComponent();
        _service = service;
        _authorService = authorService;
        _categoriesService = categoriesService;
        _languagesService = languagesService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        Books.Clear();
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
                TextColor = Colors.White,
                FontFamily = "VictorMono-BoldItalic",
                Margin = new Thickness(5, 2),
            };
            titleLabel.SetBinding(Label.TextProperty, "Title");

            var authorLabel = new Label
            {
                FontSize = 14,
                TextColor = Colors.LightGray,
                FontFamily = "VictorMono-Light",
                Margin = new Thickness(5, 0, 5, 5)
            };
            authorLabel.SetBinding(Label.TextProperty, "Authors");

            var categoryLabel = new Label
            {
                FontSize = 14,
                TextColor = Colors.MediumPurple,
                FontFamily = "VictorMono-LightItalic",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };
            categoryLabel.SetBinding(Label.TextProperty, "Categories");

            var languageLabel = new Label
            {
                FontSize = 14,
                TextColor = Colors.MediumPurple,
                FontFamily = "VictorMono-LightItalic",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };
            languageLabel.SetBinding(Label.TextProperty, "Languages");

            var leftColumn = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Children = { titleLabel, authorLabel }
            };

            var grid = new Grid
            {
                ColumnDefinitions =
                                {
                                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                                    new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) },
                                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                                },
                Padding = 10,
                BackgroundColor = Colors.Black,
                Opacity = 0.8,
                Margin = new Thickness(10, 5)
            };

            grid.Children.Add(leftColumn);
            Grid.SetColumn(leftColumn, 0);

            grid.Children.Add(categoryLabel);
            Grid.SetColumn(categoryLabel, 1);


            grid.Children.Add(languageLabel);
            Grid.SetColumn(languageLabel, 2);

            return new ViewCell { View = grid };
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
        var popup = new BookPopup(_service, _authorService, _categoriesService, _languagesService, new BookDTO());
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

    private async void BooksView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var book = e.SelectedItem as BooksDTO;
        await Shell.Current.GoToAsync($"{nameof(BookDetailsPage)}?bookId={book.Id}");
    }
}
