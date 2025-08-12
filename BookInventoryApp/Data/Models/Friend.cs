namespace BookInventoryApp.Data.Models;

public class Friend : Entity
{

    [StringLength(20)]
    public string Name { get; set; }

    [StringLength(20)]
    public string LastName { get; set; }

    [StringLength(200)]
    public string Nickname { get; set; }

    [StringLength(200)]
    public string Address { get; set; }
    public string Image { get; set; }

    //[StringLength(100)]
    //public Guid? LibraryID { get; set; }
}
