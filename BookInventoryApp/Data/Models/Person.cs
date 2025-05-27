using SQLite;
using System.ComponentModel.DataAnnotations;

namespace BookInventoryApp.Data.Models;

public class Person
{
    [PrimaryKey]
    public Guid Id { get; set; } = Guid.NewGuid();

    [StringLength(20)]
    public string Name { get; set; }

    [StringLength(20)]
    public string LastName { get; set; }

    [StringLength(200)]
    public string Address { get; set; }

    //public List<Book> BorrowedBooks { get; set; } = new List<Book>();
    //public List<Book> LendedBooks { get; set; } = new List<Book>();
}
