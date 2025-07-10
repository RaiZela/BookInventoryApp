namespace BookInventoryApp.Data.ViewModels;

public class BookDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public bool IsRead { get; set; } = false;
    public Status Status { get; set; }
    public BookType Type { get; set; }
    public int NrOfCopies { get; set; } = 1;
    public string Description { get; set; }
    public string Adress { get; set; }
    public IEnumerable<Guid> AuthorIds { get; set; } = new List<Guid>();
    public IEnumerable<Guid> CategoriesIds { get; set; } = new List<Guid>();
    public IEnumerable<Guid> LanguageIds { get; set; } = new List<Guid>();

}


