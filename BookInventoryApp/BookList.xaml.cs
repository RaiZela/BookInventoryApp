using BookInventoryApp.Services;
using System.Collections.ObjectModel;

namespace BookInventoryApp;

public partial class BookList : ContentPage
{
    IBookService _service;
    IAuthorService _authorService;
    ICategoriesService _categoriesService;
    ILanguagesService _languagesService;
    private ObservableCollection<BooksDTO> Books = new ObservableCollection<BooksDTO>();
    private int PageNumber = 1;

    public BookList(IBookService service, IAuthorService authorService, ICategoriesService categoriesService, ILanguagesService languagesService)
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
        foreach (var book in books.Records)
        {
            Books.Add(book);
        }
        BooksView.IsVisible = true;
        BooksView.ItemsSource = Books;
        BooksView.VerticalScrollBarVisibility = ScrollBarVisibility.Always;
        BooksView.Margin = 10;
        BooksView.ItemTemplate = new DataTemplate(() =>
        {
            var cover = new Image
            {
                Source = "background_image_library_author.jpg",
                Aspect = Aspect.Center,
                MaximumHeightRequest = 150,
                MaximumWidthRequest = 120
            };

            var titleLabel = new Label
            {
                FontSize = 18,
                TextColor = Colors.White,
                FontFamily = "VictorMono-BoldItalic",
                Margin = new Thickness(5, 5),
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
                FontSize = 12,
                TextColor = Colors.Grey,
                FontFamily = "VictorMono-LightItalic",
                Margin = new Thickness(5, 0, 5, 0)
            };
            categoryLabel.SetBinding(Label.TextProperty, "Categories");

            var languageLabel = new Label
            {
                FontSize = 12,
                TextColor = Colors.Grey,
                FontFamily = "VictorMono-LightItalic",
                Margin = new Thickness(5, 0, 5, 0)
            };
            languageLabel.SetBinding(Label.TextProperty, "Languages");

            var leftColumn = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Children = { titleLabel, authorLabel, categoryLabel, languageLabel },
                HeightRequest = 100
            };

            var statusLabel = new Label
            {
                FontSize = 12,
                TextColor = Colors.GhostWhite,
                FontFamily = "VictorMono-LightItalic",
                Margin = new Thickness(5, 0, 5, 0),
                HorizontalOptions = LayoutOptions.CenterAndExpand

            };
            statusLabel.SetBinding(Label.TextProperty, "Status");

            var imageCol = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Children = { cover }
            };

            var statusCol = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Children = { statusLabel }
            };

            var grid = new Grid
            {
                ColumnDefinitions =
                                {
                                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                                    new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) },
                                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                                },
                Padding = 20,
                BackgroundColor = Colors.Black,
                Opacity = 0.8,
                Margin = new Thickness(10, 5)
            };
            grid.Children.Add(imageCol);
            Grid.SetColumn(imageCol, 0);

            grid.Children.Add(leftColumn);
            Grid.SetColumn(leftColumn, 1);

            grid.Children.Add(statusCol);
            Grid.SetColumn(statusCol, 2);

            return new ViewCell { View = grid };
        });
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

    private async void BooksView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var book = e.SelectedItem as BooksDTO;
        await Shell.Current.GoToAsync($"{nameof(BookDetailsPage)}?bookId={book.Id}");
    }
}
