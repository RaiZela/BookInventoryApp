using BookInventoryApp.Services;
using System.Collections.ObjectModel;

namespace BookInventoryApp;

public partial class Home : ContentPage
{
    private ObservableCollection<BooksDTO> _filteredItems = new ObservableCollection<BooksDTO>();
    IBookService _bookService;
    public Home(IBookService bookService)
    {
        InitializeComponent();
        ResultsCollectionView.ItemsSource = _filteredItems;
        _bookService = bookService;
    }

    private async void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        await UpdateFilteredItems(e.NewTextValue);
    }

    private async Task UpdateFilteredItems(string query)
    {
        _filteredItems.Clear();
        if (!string.IsNullOrEmpty(query))
        {
            var results = await _bookService.GetFilteredBooksAsync(query);
            foreach (var item in results)
                _filteredItems.Add(item);
        }
        ResultsCollectionView.IsVisible = true;
    }

    private async void ResultsCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var book = e.CurrentSelection.FirstOrDefault() as BooksDTO;
        ResultsCollectionView.SelectedItem = null;
        ResultsCollectionView.IsVisible = false;
        SearchBar.Text = null;
        if (book is not null && book.Id != Guid.Empty)
            await Shell.Current.GoToAsync($"{nameof(BookDetailsPage)}?bookId={book.Id}");

    }
}