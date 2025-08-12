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

    private int _currentStep = 0;
    private readonly List<View> _steps;
    public ObservableCollection<AuthorDTO> FilteredAuthors { get; set; } = new();
    public ObservableCollection<AuthorDTO> SelectedAuthors { get; set; } = new();
    public ObservableCollection<CategoryDTO> FilteredCategories { get; set; } = new();
    public ObservableCollection<CategoryDTO> SelectedCategories { get; set; } = new();
    public ObservableCollection<LanguageDTO> FilteredLanguages { get; set; } = new();
    public ObservableCollection<LanguageDTO> SelectedLanguages { get; set; } = new();
    private BookDTO BookRecord { get; set; }
    private bool _isUpdate = false;
    public BookPopupAndroid(IBookService service,
        IAuthorService authorService,
        ICategoriesService categoriesService,
        ILanguagesService languagesService,
        BookDTO book)
    {
        try
        {
            InitializeComponent();
        }
        catch (Exception ex)
        {

            throw;
        }
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
        IsReadSwitch.IsToggled = BookRecord.IsRead;
        NrOfCopiesEntry.Text = BookRecord.NrOfCopies.ToString();
        DescriptionEntry.Text = BookRecord.Description;
        AdressEntry.Text = BookRecord.Adress;
        if (!string.IsNullOrEmpty(BookRecord.CoverImg))
        {
            UploadedImage.Source = new FileImageSource { File = BookRecord.CoverImg };
        }
        else
        {
            UploadedImage.Source = null;
        }

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
            IsRead = IsReadSwitch.IsToggled,
            NrOfCopies = int.TryParse(NrOfCopiesEntry.Text, out int copies) ? copies : 1,
            Description = DescriptionEntry.Text,
            Adress = AdressEntry.Text,
            CoverImg = UploadedImage.Source is FileImageSource fileImageSource ? fileImageSource.File : null

        };
        if (_isUpdate)
        {
            try
            {
                var result = await _service.SaveBookAsync(BookRecord);
                if (result > 0)
                {
                    await ShowOverlay(SuccessOverlay);
                }
                else
                {
                    await ShowOverlay(ErrorOverlay);
                }
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

            FilteredAuthorsView.ItemsSource = null;
            FilteredAuthorsView.ItemsSource = FilteredAuthors;
            FilteredAuthorsView.VerticalScrollBarVisibility = ScrollBarVisibility.Always;
            FilteredAuthorsView.IsVisible = true;
            FilteredAuthorsView.ItemTemplate = new DataTemplate(() =>
            {
                var label = new Label
                {
                    FontSize = 14,
                    VerticalOptions = LayoutOptions.Center,
                    Margin = 10,
                    Padding = 0,
                    TextColor = Colors.White
                };
                label.SetBinding(Label.TextProperty, "FullName");

                var separator = new BoxView
                {
                    HeightRequest = 0.5,
                    BackgroundColor = Colors.Gray,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Margin = new Thickness(0, 5, 0, 0)
                };

                return new ViewCell
                {
                    View = new StackLayout
                    {
                        Spacing = 0,
                        Children =
                        {
                            label,
                            separator
                        }
                    }
                };
            });
        }
        else
        {
            //FilteredAuthorsView.IsVisible = false;
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

        // Set itemsource first
        SelectedAuthorsView.ItemsSource = null;
        SelectedAuthorsView.ItemsSource = SelectedAuthors;

        // Set the item template only once, ideally outside this method in constructor
        if (SelectedAuthorsView.ItemTemplate == null)
        {
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
                    if ((s as Button)?.BindingContext is AuthorDTO selectedAuthor)
                    {
                        SelectedAuthors.Remove(selectedAuthor);
                        SelectedAuthorsView.ItemsSource = null;
                        SelectedAuthorsView.ItemsSource = SelectedAuthors;
                    }
                };

                var layout = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Spacing = 5,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Start,
                    Children = { nameLabel, removeButton }
                };

                var frame = new Frame
                {
                    BackgroundColor = Colors.MediumPurple,
                    Content = layout,
                    HasShadow = false,
                    Padding = new Thickness(10, 5),
                    Margin = new Thickness(5, 5),
                    CornerRadius = 30,
                    HorizontalOptions = LayoutOptions.Start, // lets width adapt to content
                    MinimumWidthRequest = 50
                };

                frame.SetBinding(BindingContextProperty, ".");

                return frame;
            });
        }

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
            FilteredCategoriesView.VerticalScrollBarVisibility = ScrollBarVisibility.Always;
            FilteredCategoriesView.ItemTemplate = new DataTemplate(() =>
            {
                var label = new Label
                {
                    FontSize = 14,
                    VerticalOptions = LayoutOptions.Center,
                    Margin = 10,
                    Padding = 0,
                    TextColor = Colors.White
                };
                label.SetBinding(Label.TextProperty, "Name");

                var separator = new BoxView
                {
                    HeightRequest = 0.5,
                    BackgroundColor = Colors.Gray,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Margin = new Thickness(0, 5, 0, 0)
                };

                return new ViewCell
                {
                    View = new StackLayout
                    {
                        Spacing = 0,
                        Children =
                        {
                            label,
                            separator
                        }
                    }
                };
            });
        }
        else
        {
            //FilteredCategoriesView.IsVisible = false;
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
                Spacing = 5,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Start,
                Children = { nameLabel, removeButton }
            };

            var frame = new Frame
            {
                BackgroundColor = Colors.MediumPurple,
                Content = layout,
                HasShadow = false,
                Padding = new Thickness(10, 5),
                Margin = new Thickness(5, 5),
                CornerRadius = 30,
                HorizontalOptions = LayoutOptions.Start, // lets width adapt to content
                MinimumWidthRequest = 50
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
            FilteredLanguagesView.VerticalScrollBarVisibility = ScrollBarVisibility.Always;
            FilteredLanguagesView.ItemTemplate = new DataTemplate(() =>
            {
                var label = new Label
                {
                    FontSize = 14,
                    VerticalOptions = LayoutOptions.Center,
                    Margin = 10,
                    Padding = 0,
                    TextColor = Colors.White
                };
                label.SetBinding(Label.TextProperty, "Name");

                var separator = new BoxView
                {
                    HeightRequest = 0.5,
                    BackgroundColor = Colors.Gray,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Margin = new Thickness(0, 5, 0, 0)
                };

                return new ViewCell
                {
                    View = new StackLayout
                    {
                        Spacing = 0,
                        Children =
                        {
                            label,
                            separator
                        }
                    }
                };
            });
        }
        else
        {
            //FilteredLanguagesView.IsVisible = false;
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
                Spacing = 5,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Start,
                Children = { nameLabel, removeButton }
            };

            var frame = new Frame
            {
                BackgroundColor = Colors.MediumPurple,
                Content = layout,
                HasShadow = false,
                Padding = new Thickness(10, 5),
                Margin = new Thickness(5, 5),
                CornerRadius = 30,
                HorizontalOptions = LayoutOptions.Start, // lets width adapt to content
                MinimumWidthRequest = 50
            };

            frame.SetBinding(BindingContextProperty, ".");

            return frame;
        });

        SelectedLanguagesView.ItemsSource = SelectedLanguages;
        SelectedLanguagesView.IsVisible = true;
    }

    private void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (sender is Entry entry)
        {
            string newText = entry.Text;

            // Allow empty text so the user can delete or modify it
            if (string.IsNullOrEmpty(newText))
                return;

            if (!int.TryParse(newText, out int number) || number < 0)
            {
                entry.Text = e.OldTextValue;
            }
        }
    }
    int currentStep = 1;
    int totalSteps = 5;

    // Track which handler is attached
    bool isSaveHandlerAttached = false;

    void OnNextClicked(object sender, EventArgs e)
    {
        if (currentStep < totalSteps)
        {
            currentStep++;
            UpdateStepUI();
        }
    }


    void OnBackClicked(object sender, EventArgs e)
    {
        if (currentStep > 1)
        {
            currentStep--;
            UpdateStepUI();
        }
    }

    void UpdateStepUI()
    {
        // Show the correct step
        Step1.IsVisible = currentStep == 1;
        Step2.IsVisible = currentStep == 2;
        Step3.IsVisible = currentStep == 3;
        Step4.IsVisible = currentStep == 4;
        Step5.IsVisible = currentStep == 5;

        // Update buttons
        BackButton.IsVisible = currentStep > 1;
        NextButton.IsVisible = true;
        NextButton.IsEnabled = true;
        NextButton.Text = currentStep == totalSteps ? "Save" : "Next";

        // Detach all previous handlers
        NextButton.Clicked -= OnNextClicked;
        NextButton.Clicked -= OnSaveClicked;

        // Attach only the correct one
        if (currentStep == totalSteps)
        {
            NextButton.Clicked += OnSaveClicked;
        }
        else
        {
            NextButton.Clicked += OnNextClicked;
        }
    }


    private void TitleEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        NextButton.IsEnabled = !string.IsNullOrWhiteSpace(TitleEntry.Text);
    }

    private async void OnUploadImageClicked(object sender, EventArgs e)
    {
        try
        {
            var result = await FilePicker.Default.PickAsync(new PickOptions
            {
                PickerTitle = "Select an image",
                FileTypes = FilePickerFileType.Images
            });

            if (result != null)
            {
                using var stream = await result.OpenReadAsync();
                UploadedImage.Source = ImageSource.FromStream(() => stream);
                using var memoryStream = new MemoryStream();
                stream.Position = 0;
                await stream.CopyToAsync(memoryStream);
                byte[] imageBytes = memoryStream.ToArray();

                string base64Image = Convert.ToBase64String(imageBytes);


            }
        }
        catch (Exception ex)
        {
            //await DisplayAlert("Error", $"Something went wrong: {ex.Message}", "OK");
        }
    }

}

