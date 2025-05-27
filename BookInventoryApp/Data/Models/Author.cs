using System.ComponentModel.DataAnnotations;

namespace BookInventoryApp.Data.Models;

public class Author
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [StringLength(20)]
    public string Name { get; set; }

    [StringLength(20)]
    public string? MiddleName { get; set; }

    [StringLength(20)]
    public string LastName { get; set; }

    //public List<Book> Books { get; set; } = new List<Book>();
    public Guid NationalityId { get; set; }
}
