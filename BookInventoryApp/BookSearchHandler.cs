namespace BookInventoryApp;

public class BookSearchHandler : SearchHandler
{
    public IEnumerable<BooksDTO> Books { get; set; } = new List<BooksDTO>();
    public Type SelectedItemNavigationTarget { get; set; } //= typeof(BookDetailsPage);

    protected override void OnQueryChanged(string oldValue, string newValue)
    {
        base.OnQueryChanged(oldValue, newValue);
        if (string.IsNullOrWhiteSpace(newValue))
        {
            ItemsSource = new List<BooksDTO>();
        }
        else
        {
            ItemsSource = Books
     .Where(b => b.Title.Contains(newValue, StringComparison.OrdinalIgnoreCase) ||
                 b.Authors.Contains(newValue, StringComparison.OrdinalIgnoreCase))
     .ToList();
        }
    }
}
