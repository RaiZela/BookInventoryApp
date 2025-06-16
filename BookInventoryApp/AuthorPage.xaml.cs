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
    }

    private async void OnAddAuthorClicked(object sender, EventArgs e)
    {
        var popup = new AddAuthorPopup(_service, new AuthorDTO());
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


        ResultsCollectionView.ItemsSource = FilteredAuthors;
        ResultsCollectionView.VerticalScrollBarVisibility = ScrollBarVisibility.Always;
        ResultsCollectionView.BackgroundColor = Color.FromArgb("#362b7d");
        ResultsCollectionView.Opacity = 0.7;
        ResultsCollectionView.IsVisible = true;
        ResultsCollectionView.Margin = new Thickness(0, 0, 0, 0);

    }

    private async void ResultsCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selection = e.CurrentSelection.FirstOrDefault() as AuthorDTO ?? new AuthorDTO();
        ResultsCollectionView.SelectedItem = null;
        ResultsCollectionView.IsVisible = false;
        SearchBar.Text = null;
        if (selection is not null && selection.Id != Guid.Empty)
        {
            var popup = new AddAuthorPopup(_service, selection);
            await this.ShowPopupAsync(popup);
        }

    }
}