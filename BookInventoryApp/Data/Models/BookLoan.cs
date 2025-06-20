namespace BookInventoryApp.Data.Models;

public class BookLoan
{
    [PrimaryKey]
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid BookId { get; set; }
    public string Title { get; set; }
    public Guid FriendId { get; set; }
    public LoanDirection Direction { get; set; }
    public DateTime DateBorrowed { get; set; }
    public DateTime? DateReturned { get; set; }
    public bool IsBorrowedByFriend { get; set; } // True if the friend borrowed the book, false if you lent it to them
    public bool IsReturned { get; set; } // True if the book has been returned, false otherwise
}
