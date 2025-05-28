namespace BookInventoryApp.Data.ViewModels;

public class BookDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public int YearPublished { get; set; }
    public bool IsBorrowed { get; set; } = false;
    public Guid? BorrowedBy { get; set; }
    public bool IsOwned { get; set; } = true;
    public Status Status { get; set; }
    public Type Type { get; set; }
    public bool IsLended { get; set; } = false;
    public Guid? LendedTo { get; set; }
    public bool IsRead { get; set; } = false;
    public string Authors { get; set; }
    public IEnumerable<Guid> Categories { get; set; } = new List<Guid>();
    public IEnumerable<Guid> Languages { get; set; } = new List<Guid>();
}


