namespace BookInventoryApp.Data.Models;

public class BookLoan
{
    [PrimaryKey]
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public string Title { get; set; }
    public Guid FriendId { get; set; }
    public LoanDirection Direction { get; set; }
    public DateTime DateBorrowed { get; set; }
    public DateTime? DateReturned { get; set; }
}
