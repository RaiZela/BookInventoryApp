using BookInventoryApp.Services;
using CommunityToolkit.Maui.Alerts;
using System.Collections.ObjectModel;

namespace BookInventoryApp;

public partial class BookPopupAndroid : Popup
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
    private BookDTO BookRecord { get; set; }
    private bool _isUpdate = false;
    public BookPopupAndroid(IBookService service, IAuthorService authorService, ICategoriesService categoriesService, ILanguagesService languagesService, BookDTO book)
    {
        InitializeComponent();
        _service = service;
        _authorService = authorService;
        _categoriesService = categoriesService;
        _languagesService = languagesService;
        TypeEntry.ItemsSource = Enum.GetValues(typeof(BookType));
        StatusEntry.ItemsSource = Enum.GetValues(typeof(Status));
        BookRecord = book;
        if (BookRecord != null && !string.IsNullOrEmpty(BookRecord.Title))
        {
            _isUpdate = true;
            FormUpdate();
        }
        else
            BookRecord = book ?? new BookDTO();
    }

    private void FormUpdate()
    {
        var authors = _authorService.GetAuthorsById(BookRecord.AuthorIds.ToList());
        foreach (var author in authors)
            SelectedAuthorsTemplate(author);

        var categories = _categoriesService.GetCategoriesById(BookRecord.CategoriesIds.ToList());
        foreach (var category in categories)
            SelectedCategoriesTemplate(category);

        var languages = _languagesService.GetLanguagesById(BookRecord.LanguageIds.ToList());
        foreach (var language in languages)
            LanguageSelectedTemplate(language);

        SelectedLanguagesView.ItemsSource = SelectedLanguages;
        SelectedLanguagesView.IsVisible = true;

        TypeEntry.SelectedItem = BookRecord.Type;
        StatusEntry.SelectedItem = BookRecord.Status;
        TitleEntry.Text = BookRecord.Title;

    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        BookRecord = new BookDTO
        {
            Id = _isUpdate ? BookRecord.Id : Guid.NewGuid(),
            Title = TitleEntry.Text,
            AuthorIds = SelectedAuthors == null ? new() : SelectedAuthors.Select(a => a.Id).ToList(),
            CategoriesIds = SelectedCategories == null ? new() : SelectedCategories.Select(c => c.Id).ToList(),
            LanguageIds = SelectedLanguages == null ? new() : SelectedLanguages.Select(l => l.Id).ToList(),
            Status = StatusEntry.SelectedItem is null ? Status.Unread : (Status)StatusEntry.SelectedItem,
            Type = TypeEntry.SelectedItem is null ? BookType.Paperback : (BookType)TypeEntry.SelectedItem,
        };
        if (_isUpdate)
        {
            try
            {
                var result = await _service.SaveBookAsync(BookRecord);
                if (result > 0)
                    await Snackbar.Make("✅ Book is updated!", duration: TimeSpan.FromSeconds(3)).Show();
                else
                    await Snackbar.Make("Failed to update book", duration: TimeSpan.FromSeconds(3)).Show();
            }
            catch
            {
                await ShowOverlay(ErrorOverlay);
            }
        }
        else
        {
            var result = await _service.SaveNewBookAsync(BookRecord);
            if (result > 0)
                await Snackbar.Make("✅ Book is added!", duration: TimeSpan.FromSeconds(3)).Show();
            else
                await Snackbar.Make("Failed to add book", duration: TimeSpan.FromSeconds(3)).Show();
        }

        Close();
    }
    private async Task ShowOverlay(View overlay)
    {
        overlay.IsVisible = true;
        await overlay.FadeTo(1, 300); // Fade in
        await Task.Delay(1500);       // Wait a moment
        await overlay.FadeTo(0, 300); // Fade out
        overlay.IsVisible = false;
    }
    private async void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        var query = e.NewTextValue?.ToLower() ?? "";
        FilteredAuthors.Clear();
        if (string.IsNullOrEmpty(query))
            FilteredAuthorsView.IsVisible = false;
        if (!string.IsNullOrEmpty(query))
        {
            var results = await _authorService.GetFilteredAuthorsAsync(query);
            results = results.Where(r => !SelectedAuthors.Any(s => s.Id == r.Id)).ToList();
            foreach (var item in results)
                FilteredAuthors.Add(item);

            if (FilteredAuthors.Count() > 0)
            {

                FilteredAuthorsView.VerticalScrollBarVisibility = ScrollBarVisibility.Always;
                FilteredAuthorsView.ItemTemplate = new DataTemplate(() =>
                {
                    var label = new Label
                    {
                        FontSize = 14,
                        VerticalOptions = LayoutOptions.Center,
                        Margin = 10,
                        Padding = 0,
                        WidthRequest = 150,
                        TextColor = Colors.White
                    };
                    label.SetBinding(Label.TextProperty, "FullName");

                    var viewCell = new ViewCell { View = label };
                    return viewCell;
                });
                FilteredAuthorsView.ItemsSource = FilteredAuthors;
                //FilteredAuthorsView.IsRefreshing = true;
                FilteredAuthorsView.IsVisible = true;
            }
        }
        else
        {
            FilteredAuthorsView.IsVisible = false;
        }
    }

    private void Authors_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var author = e.SelectedItem as AuthorDTO;
        SelectedAuthorsTemplate(author);

    }

    private void SelectedAuthorsTemplate(AuthorDTO author)
    {
        SelectedAuthors.Add(author);
        FilteredAuthors.Clear();
        FilteredAuthorsView.IsVisible = false;
        AuthorsSearch.Text = string.Empty;

        SelectedAuthorsView.ItemTemplate = new DataTemplate(() =>
        {
            var nameLabel = new Label
            {
                FontSize = 14,
                TextColor = Colors.White,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Start,
                Margin = new Thickness(0, 5)
            };
            nameLabel.SetBinding(Label.TextProperty, "FullName");

            var removeButton = new Button
            {
                Text = "X",
                BackgroundColor = Colors.Transparent,
                WidthRequest = 20,
                HeightRequest = 20,
                Padding = 0,
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center
            };
            removeButton.Clicked += (s, e) =>
            {
                var button = s as Button;
                if (button?.BindingContext is AuthorDTO author)
                {
                    SelectedAuthors.Remove(author);
                    SelectedAuthorsView.ItemsSource = null;
                    SelectedAuthorsView.ItemsSource = SelectedAuthors;
                }
            };

            var layout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                VerticalOptions = LayoutOptions.Center,
                Children = { nameLabel, removeButton }
            };

            var frame = new Frame
            {
                BackgroundColor = Colors.MediumPurple,
                Content = layout,
                HasShadow = false,
                Padding = new Thickness(10, 5),
                Margin = new Thickness(5, 5),
                CornerRadius = 16,
            };

            frame.SetBinding(BindingContextProperty, ".");

            return frame;
        });
        FilteredAuthorsView.IsVisible = false;
        SelectedAuthorsView.ItemsSource = SelectedAuthors;
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

            if (FilteredCategories.Count() > 0)
            {

                FilteredCategoriesView.ItemTemplate = new DataTemplate(() =>
                {
                    var label = new Label
                    {
                        FontSize = 14,
                        VerticalOptions = LayoutOptions.Center,
                        Margin = 10,
                        Padding = 0,
                        WidthRequest = 150,
                        TextColor = Colors.White
                    };
                    label.SetBinding(Label.TextProperty, "Name");

                    var viewCell = new ViewCell { View = label };
                    return viewCell;
                });
                FilteredCategoriesView.ItemsSource = FilteredCategories;
                //FilteredCategoriesView.IsRefreshing = true;
                FilteredCategoriesView.VerticalScrollBarVisibility = ScrollBarVisibility.Always;
                FilteredCategoriesView.IsVisible = true;
            }
        }
        else
        {
            FilteredCategoriesView.IsVisible = false;
        }
    }

    private void Categories_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var category = e.SelectedItem as CategoryDTO;
        SelectedCategoriesTemplate(category);
    }

    private void SelectedCategoriesTemplate(CategoryDTO category)
    {
        SelectedCategories.Add(category);
        FilteredCategories.Clear();
        CategoriesSearch.Text = string.Empty;
        FilteredCategoriesView.IsVisible = false;


        SelectedCategoriesView.ItemTemplate = new DataTemplate(() =>
        {
            var nameLabel = new Label
            {
                FontSize = 14,
                TextColor = Colors.White,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Start,
                Margin = new Thickness(0, 5),

            };
            nameLabel.SetBinding(Label.TextProperty, "Name");

            var removeButton = new Button
            {
                Text = "X",
                BackgroundColor = Colors.Transparent,
                WidthRequest = 20,
                HeightRequest = 20,
                Padding = 0,
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center
            };
            removeButton.Clicked += (s, e) =>
            {
                var button = s as Button;
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
                VerticalOptions = LayoutOptions.Center,
                Children = { nameLabel, removeButton }
            };

            var frame = new Frame
            {
                BackgroundColor = Colors.MediumPurple,
                Content = layout,
                HasShadow = false,
                Padding = new Thickness(10, 5),
                Margin = new Thickness(5, 5),
                CornerRadius = 16
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

            if (FilteredLanguages.Count() > 0)
            {
                FilteredLanguagesView.ItemTemplate = new DataTemplate(() =>
                {
                    var label = new Label
                    {
                        FontSize = 14,
                        VerticalOptions = LayoutOptions.Center,
                        Margin = 10,
                        Padding = 0,
                        WidthRequest = 150,
                        TextColor = Colors.White
                    };
                    label.SetBinding(Label.TextProperty, "Name");

                    var viewCell = new ViewCell { View = label };
                    return viewCell;
                });
            }


            FilteredLanguagesView.ItemsSource = FilteredLanguages;
            //FilteredLanguagesView.IsRefreshing = true;
            FilteredLanguagesView.VerticalScrollBarVisibility = ScrollBarVisibility.Always;
            FilteredLanguagesView.IsVisible = true;
        }
        else
        {
            FilteredLanguagesView.IsVisible = false;
        }
    }

    private void Languages_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var language = e.SelectedItem as LanguageDTO;
        LanguageSelectedTemplate(language);
    }

    private void LanguageSelectedTemplate(LanguageDTO language)
    {
        SelectedLanguages.Add(language);
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
                HeightRequest = 20,
                Padding = 0,
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center,
                Margin = new Thickness(5, 5, 5, 5),
            };
            removeButton.Clicked += (s, e) =>
            {
                var button = s as Button;
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
                VerticalOptions = LayoutOptions.Center,
                Children = { nameLabel, removeButton }
            };

            var frame = new Frame
            {
                BackgroundColor = Colors.MediumPurple,
                Content = layout,
                CornerRadius = 16,
                HasShadow = false,
                Padding = new Thickness(10, 5),
                Margin = new Thickness(5, 0),
            };

            frame.SetBinding(BindingContextProperty, ".");

            return frame;
        });

        SelectedLanguagesView.ItemsSource = SelectedLanguages;
        SelectedLanguagesView.IsVisible = true;
    }
}

