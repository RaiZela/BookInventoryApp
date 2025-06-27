namespace BookInventoryApp.Data.ViewModels;

public class PaginatedViewModel<T>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public int TotalItems { get; set; }
    public List<T> Records { get; set; }
}
