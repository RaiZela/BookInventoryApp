using BookInventoryApp.Services;
using System.Collections.ObjectModel;

namespace BookInventoryApp;

public partial class AddBookPopup : Popup
{
    IBookService _service;
    IAuthorService _authorService;
    ICategoriesService _categoriesService;
    ILanguagesService _languagesService;
    public ObservableCollection<AuthorDTO> FilteredAuthors { get; set; } = new();
    public ObservableCollection<AuthorDTO> SelectedAuthors { get; set; } = new();
    public ObservableCollection<CategoryDTO> FilteredCategories { get; set; } = new();
    public ObservableCollection<CategoryDTO> SelectedCategories { get; set; } = new();
    public ObservableCollection<LanguageDTO> FilteredLanguages { get; set; } = new();
    public ObservableCollection<LanguageDTO> SelectedLanguages { get; set; } = new();
    private BookDTO Book { get; set; }

    public AddBookPopup(IBookService service, IAuthorService authorService, ICategoriesService categoriesService, ILanguagesService languagesService, BookDTO book)
    {
        InitializeComponent();
        _service = service;
        _authorService = authorService;
        _categoriesService = categoriesService;
        _languagesService = languagesService;
        TypeEntry.ItemsSource = Enum.GetValues(typeof(BookType));
        StatusEntry.ItemsSource = Enum.GetValues(typeof(Status));
        Book = book ?? new();
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        Book = new BookDTO
        {
            Title = TitleEntry.Text,
            AuthorIds = SelectedAuthors == null ? new() : SelectedAuthors.Select(a => a.Id).ToList(),
            CategoriesIds = SelectedCategories == null ? new() : SelectedCategories.Select(c => c.Id).ToList(),
            LanguageIds = SelectedLanguages == null ? new() : SelectedLanguages.Select(l => l.Id).ToList(),
            Status = StatusEntry.SelectedItem is null ? Status.Unread : (Status)StatusEntry.SelectedItem,
            Type = TypeEntry.SelectedItem is null ? BookType.Paperback : (BookType)TypeEntry.SelectedItem,
        };

        await _service.SaveBookAsync(Book);

        Close();
    }

    private async void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        var query = e.NewTextValue?.ToLower() ?? "";
        FilteredAuthors.Clear();
        if (!string.IsNullOrEmpty(query))
        {
            var results = await _authorService.GetFilteredAuthorsAsync(query);
            results = results.Where(r => !SelectedAuthors.Any(s => s.Id == r.Id)).ToList();
            foreach (var item in results)
                FilteredAuthors.Add(item);
            FilteredAuthorsView.IsVisible = true;
            FilteredAuthorsView.ItemsSource = FilteredAuthors;
            FilteredAuthorsView.IsRefreshing = true;
            FilteredAuthorsView.VerticalScrollBarVisibility = ScrollBarVisibility.Always;
            FilteredAuthorsView.ItemTemplate = new DataTemplate(() =>
            {
                var label = new Label
                {
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                    Margin = 10,
                    Padding = 10
                };
                label.SetBinding(Label.TextProperty, "FullName");

                var viewCell = new ViewCell { View = label };
                return viewCell;
            });
        }
        else
        {
            FilteredAuthorsView.IsVisible = false;
        }
    }

    private void Authors_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var author = e.SelectedItem as AuthorDTO;
        SelectedAuthors.Add(author);
        FilteredAuthors.Clear();
        AuthorsSearch.Text = string.Empty;
        FilteredAuthorsView.IsVisible = false;

        SelectedAuthorsView.ItemsSource = SelectedAuthors;
        SelectedAuthorsView.ItemTemplate = new DataTemplate(() =>
        {
            var label = new Label
            {
                FontSize = 16,
                VerticalOptions = LayoutOptions.Center,
                Margin = 10,
                Padding = 10,
                BackgroundColor = Colors.MediumPurple
            };
            label.SetBinding(Label.TextProperty, "FullName");

            var viewCell = new ViewCell { View = label };
            return viewCell;
        });

        SelectedAuthorsView.IsVisible = true;
    }

    private async void OnCategoriesSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        var query = e.NewTextValue?.ToLower() ?? "";
        FilteredCategories.Clear();
        if (!string.IsNullOrEmpty(query))
        {
            var results = await _categoriesService.GetFilteredCategoriesAsync(query);
            results = results.Where(r => !SelectedCategories.Any(s => s.Id == r.Id)).ToList();
            foreach (var item in results)
                FilteredCategories.Add(item);
            FilteredCategoriesView.IsVisible = true;
            FilteredCategoriesView.ItemsSource = FilteredCategories;
            FilteredCategoriesView.IsRefreshing = true;
            FilteredCategoriesView.VerticalScrollBarVisibility = ScrollBarVisibility.Always;
            FilteredCategoriesView.ItemTemplate = new DataTemplate(() =>
            {
                var label = new Label
                {
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                    Margin = 10,
                    Padding = 10,
                    TextColor = Colors.White
                };
                label.SetBinding(Label.TextProperty, "Name");

                var viewCell = new ViewCell { View = label };
                return viewCell;
            });
        }
        else
        {
            FilteredCategoriesView.IsVisible = false;
        }
    }

    private void Categories_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var category = e.SelectedItem as CategoryDTO;
        SelectedCategories.Add(category);
        FilteredCategories.Clear();
        CategoriesSearch.Text = string.Empty;
        FilteredCategoriesView.IsVisible = false;

        SelectedCategoriesView.ItemsLayout = new LinearItemsLayout(ItemsLayoutOrientation.Horizontal)
        {
            ItemSpacing = 8
        };

        SelectedCategoriesView.ItemTemplate = new DataTemplate(() =>
        {
            var nameLabel = new Label
            {
                FontSize = 14,
                TextColor = Colors.White,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Start
            };
            nameLabel.SetBinding(Label.TextProperty, "Name");

            var removeButton = new Button
            {
                Text = "X",
                BackgroundColor = Colors.Transparent,
                WidthRequest = 20,
                HeightRequest = 20,
                Padding = 0,
                Margin = new Thickness(5, 5, 5, 5),
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center
            };
            removeButton.Clicked += (s, e) =>
            {
                var button = s as ImageButton;
                if (button?.BindingContext is CategoryDTO category)
                {
                    SelectedCategories.Remove(category);
                    SelectedCategoriesView.ItemsSource = null;
                    SelectedCategoriesView.ItemsSource = SelectedCategories;
                }
            };

            var layout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Spacing = 5,
                VerticalOptions = LayoutOptions.Center,
                Children = { nameLabel, removeButton }
            };

            var frame = new Frame
            {
                BackgroundColor = Colors.MediumPurple,
                CornerRadius = 16,
                Padding = new Thickness(10, 5),
                Margin = new Thickness(5, 0),
                Content = layout,
                HasShadow = false
            };

            frame.SetBinding(BindingContextProperty, ".");

            return frame;
        });

        SelectedCategoriesView.ItemsSource = SelectedCategories;
        SelectedCategoriesView.IsVisible = true;
    }

    private async void OnLanguagesSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        var query = e.NewTextValue?.ToLower() ?? "";
        FilteredLanguages.Clear();
        if (!string.IsNullOrEmpty(query))
        {
            var results = await _languagesService.GetFilteredLanguagesAsync(query);
            results = results.Where(r => !SelectedLanguages.Any(s => s.Id == r.Id)).ToList();
            foreach (var item in results)
                FilteredLanguages.Add(item);
            FilteredLanguagesView.IsVisible = true;
            FilteredLanguagesView.ItemsSource = FilteredLanguages;
            FilteredLanguagesView.IsRefreshing = true;
            FilteredLanguagesView.VerticalScrollBarVisibility = ScrollBarVisibility.Always;
            FilteredLanguagesView.ItemTemplate = new DataTemplate(() =>
            {
                var label = new Label
                {
                    FontSize = 16,
                    VerticalOptions = LayoutOptions.Center,
                    Margin = 10,
                    Padding = 10,
                    TextColor = Colors.White
                };
                label.SetBinding(Label.TextProperty, "Name");

                var viewCell = new ViewCell { View = label };
                return viewCell;
            });
        }
        else
        {
            FilteredLanguagesView.IsVisible = false;
        }
    }

    private void Languages_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var Languagy = e.SelectedItem as LanguageDTO;
        SelectedLanguages.Add(Languagy);
        FilteredLanguages.Clear();
        LanguagesSearch.Text = string.Empty;
        FilteredLanguagesView.IsVisible = false;

        SelectedLanguagesView.ItemsLayout = new LinearItemsLayout(ItemsLayoutOrientation.Horizontal)
        {
            ItemSpacing = 8
        };

        SelectedLanguagesView.ItemTemplate = new DataTemplate(() =>
        {
            var nameLabel = new Label
            {
                FontSize = 14,
                TextColor = Colors.White,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Start
            };
            nameLabel.SetBinding(Label.TextProperty, "Name");

            var removeButton = new Button
            {
                Text = "X",
                BackgroundColor = Colors.Transparent,
                WidthRequest = 20,
                HeightRequest = 20,
                Padding = 0,
                Margin = new Thickness(5, 5, 5, 5),
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center
            };
            removeButton.Clicked += (s, e) =>
            {
                var button = s as ImageButton;
                if (button?.BindingContext is LanguageDTO language)
                {
                    SelectedLanguages.Remove(language);
                    SelectedLanguagesView.ItemsSource = null;
                    SelectedLanguagesView.ItemsSource = SelectedLanguages;
                }
            };

            var layout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Spacing = 5,
                VerticalOptions = LayoutOptions.Center,
                Children = { nameLabel, removeButton }
            };

            var frame = new Frame
            {
                BackgroundColor = Colors.MediumPurple,
                CornerRadius = 16,
                Padding = new Thickness(10, 5),
                Margin = new Thickness(5, 0),
                Content = layout,
                HasShadow = false
            };

            frame.SetBinding(BindingContextProperty, ".");

            return frame;
        });

        SelectedLanguagesView.ItemsSource = SelectedLanguages;
        SelectedLanguagesView.IsVisible = true;
    }
}

