using BookInventoryApp.Services;
using System.Collections.ObjectModel;

namespace BookInventoryApp;

public partial class AddBookPopup : Popup
{
    IBookService _service;
    IAuthorService _authorService;
    public ObservableCollection<AuthorDTO> FilteredAuthors { get; set; } = new();
    public ObservableCollection<AuthorDTO> SelectedAuthors { get; set; } = new();

    public AddBookPopup(IBookService service, IAuthorService authorService)
    {
        InitializeComponent();
        _service = service;
        _authorService = authorService;
        TypeEntry.ItemsSource = Enum.GetValues(typeof(BookType));
        StatusEntry.ItemsSource = Enum.GetValues(typeof(Status));
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        var book = new BookDTO
        {
            Title = TitleEntry.Text,
            AuthorIds = SelectedAuthors.Select(a => a.Id).ToList(),
            Status = (Status)StatusEntry.SelectedItem,
            Type = (BookType)TypeEntry.SelectedItem,
        };

        await _service.SaveBookAsync(book);

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
}
