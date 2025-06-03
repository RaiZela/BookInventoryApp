using BookInventoryApp.Services;
using System.Collections.ObjectModel;

namespace BookInventoryApp;

public partial class AuthorPage : ContentPage
{
    IAuthorService _service;
    private ObservableCollection<AuthorDTO> FilteredAuthors = new ObservableCollection<AuthorDTO>();
    public AuthorPage(IAuthorService service)
    {
        InitializeComponent();
        _service = service;
        ResultsCollectionView.ItemsSource = FilteredAuthors;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        //AuthorsView.ItemsSource = await _service.GetAuthorsAsync();
    }

    private async void OnAddAuthorClicked(object sender, EventArgs e)
    {
        var popup = new AddAuthorPopup(_service);
        await this.ShowPopupAsync(popup);
    }

    private async void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrEmpty(e.NewTextValue))
        {
            ResultsCollectionView.IsVisible = false;
        }
        else
        {
            await UpdateFilteredAuthors(e.NewTextValue);
        }
    }

    private async Task UpdateFilteredAuthors(string query)
    {
        FilteredAuthors.Clear();
        var results = await _service.GetFilteredAuthorsAsync(query);
        foreach (var item in results)
            FilteredAuthors.Add(item);

        ResultsCollectionView.IsVisible = true;
        ResultsCollectionView.ItemsSource = FilteredAuthors;
        ResultsCollectionView.IsRefreshing = true;
        ResultsCollectionView.VerticalScrollBarVisibility = ScrollBarVisibility.Always;
        ResultsCollectionView.ItemTemplate = new DataTemplate(() =>
        {
            var label = new Label
            {
                FontSize = 16,
                VerticalOptions = LayoutOptions.Center,
                Margin = 10,
                Padding = 5,
                TextColor = Colors.GhostWhite
            };
            label.SetBinding(Label.TextProperty, "FullName");

            var viewCell = new ViewCell { View = label };
            return viewCell;
        });

    }
}