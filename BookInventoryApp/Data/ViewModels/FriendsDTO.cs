namespace BookInventoryApp.Data.ViewModels;

public class FriendsDTO
{
    public Guid Id { get; set; }

    [StringLength(20)]
    public string Name { get; set; }

    [StringLength(20)]
    public string LastName { get; set; }

    [StringLength(200)]
    public string Nickname { get; set; }

    [StringLength(200)]
    public string Address { get; set; }

    [StringLength(100)]
    public Guid? LibraryID { get; set; }

    public string FullName => $"{Name} {LastName}".Trim();

    public string Image { get; set; }
}
