using SQLite;
using System.ComponentModel.DataAnnotations;

namespace BookInventoryApp.Data.Models;

public class Book
{
    [PrimaryKey]
    public Guid Id { get; set; } = Guid.NewGuid();

    [StringLength(200)]
    public string Title { get; set; }
    //public List<Author> Author { get; set; } = new List<Author>();
    //public List<Category> Categories { get; set; } = new List<Category>();
    public int YearPublished { get; set; }
    public bool IsRead { get; set; } = false;

    public bool IsBorrowed { get; set; } = false;
    public Guid? BorrowerId { get; set; }
    // public Person? Borrower { get; set; }

    public bool IsLended { get; set; } = false;
    public Guid? LenderId { get; set; }
    // public Person? Lender { get; set; }

    //public List<Language> Languages { get; set; } = new List<Language>();

}
