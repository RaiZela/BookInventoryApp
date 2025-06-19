using BookInventoryApp.Services;

namespace BookInventoryApp;

public partial class FriendBooksPage : ContentPage
{
    private readonly FriendsDTO _friend;
    private readonly bool _showBooksTheyBorrowed;
    private readonly ILendedBooksService _lendedBookService;

    public string PageTitle { get; set; }

    public FriendBooksPage(FriendsDTO friend, bool showBooksTheyBorrowed, ILendedBooksService lendedBookService)
    {
        InitializeComponent();
        BindingContext = this;
        _lendedBookService = lendedBookService;
        _friend = friend;
        _showBooksTheyBorrowed = showBooksTheyBorrowed;
        PageTitle = _showBooksTheyBorrowed
            ? $"{_friend.Name}'s Borrowed Books"
            : $"{_friend.Name}'s Lent Books";
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var allRecords = await _lendedBookService.GetBookLoanInfoByFriendIdAsync(_friend.Id);

        var relevant = allRecords
            .Where(r => r.IsBorrowedByFriend == _showBooksTheyBorrowed)
            .ToList();

        var current = relevant.Where(r => r.DateReturned == null).ToList();
        var historical = relevant.Where(r => r.DateReturned != null).ToList();

        CurrentBooksCollection.ItemsSource = current;
        HistoricalBooksCollection.ItemsSource = historical;
    }
}