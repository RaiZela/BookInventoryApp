using SQLite;
using System.ComponentModel.DataAnnotations;

namespace BookInventoryApp.Data.Models;

public class Language
{
    [PrimaryKey]
    public Guid Id { get; set; } = Guid.NewGuid();

    [StringLength(4)]
    public string Code { get; set; }

    [StringLength(20)]
    public string Name { get; set; }

    //public List<Book> Books { get; set; } = new List<Book>();
}
