namespace BookInventoryApp.Data.ViewModels;

public class BooksDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Authors { get; set; }
    public string Categories { get; set; }
    public string Languages { get; set; }
    public string Status { get; set; }
}
