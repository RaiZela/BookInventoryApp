using SQLite;
using System.ComponentModel.DataAnnotations;

namespace BookInventoryApp.Data.Models;

public class Nationality
{
    [PrimaryKey]
    public Guid Id { get; set; } = Guid.NewGuid();

    [StringLength(4)]
    public string Code { get; set; }

    [StringLength(20)]
    public string Name { get; set; }
    public List<Author> Authors { get; set; }
}
