namespace BookInventoryApp.Data.ViewModels;

public class BookLoanInfo
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Title { get; set; }
    public DateTime DateBorrowed { get; set; }
    public DateTime? DateReturned { get; set; } = null;
    public bool IsBorrowedByFriend { get; set; }
}
