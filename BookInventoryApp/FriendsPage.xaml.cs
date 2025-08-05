using BookInventoryApp.Services;

namespace BookInventoryApp;

public partial class FriendsPage : ContentPage
{
    private List<FriendsDTO> allFriends = new(); // Original list of all friends
    private readonly IFriendService _service;
    private readonly IBookLoanService _loanService;
    private readonly SQLiteAsyncConnection _db;
    private readonly IBookService _bookService;
    public FriendsPage(
        IFriendService service,
        IBookLoanService loanService,
        SQLiteAsyncConnection db,
        IBookService bookService
        )
    {
        InitializeComponent();
        _service = service;
        _loanService = loanService;
        _db = db;
        _bookService = bookService;
        SearchBar.TextChanged += OnSearchTextChanged;

    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        allFriends = await _service.GetFriendsAsync();
        FriendCollection.ItemsSource = allFriends;

    }

    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        string searchText = e.NewTextValue?.ToLower() ?? "";
        var filtered = allFriends
            .Where(f => (f.Name + " " + f.LastName).ToLower().Contains(searchText) ||
                        (f.Nickname?.ToLower().Contains(searchText) ?? false) ||
                        (f.Address?.ToLower().Contains(searchText) ?? false))
            .ToList();

        FriendCollection.ItemsSource = filtered;
    }

    private async void OnFriendTapped(object sender, EventArgs e)
    {
        if (sender is VisualElement view && view.BindingContext is FriendsDTO friend)
        {
            var popup = new FriendPopup(_service, friend);
            await this.ShowPopupAsync(popup);
        }
    }

    private async void OnAddFriendClicked(object sender, EventArgs e)
    {
        var popup = new FriendPopup(_service, new FriendsDTO());
        await this.ShowPopupAsync(popup);
        FriendCollection.ItemsSource = await _service.GetFriendsAsync();
    }

    private async void SwipeItem_Invoked(object sender, EventArgs e)
    {
        if (sender is SwipeItem swipeItem && swipeItem.CommandParameter is FriendsDTO friend)
        {
            var activeLoans = await _loanService.GetLoansAsync();
            var loansWithThisFriend = activeLoans.Where(l => l.FriendId == friend.Id && l.DateReturned == null).ToList();

            if (loansWithThisFriend.Any())
            {
                await DisplayAlert("Cannot Delete", "This friend has active loans. Return the books first.", "OK");
                return;
            }

            var confirm = await DisplayAlert("Confirm", "Do you want to delete this friend?", "Yes", "No");
            if (confirm)
            {
                await _service.DeleteFriendAsync(friend.Id);
                await DisplayAlert("Success", "Friend deleted.", "OK");
            }
        }
    }

    private async void OnLendBookClicked(object sender, EventArgs e)
    {
        if (sender is SwipeItem item && item.CommandParameter is FriendsDTO friend)
        {
            var popup = new LendBorrowPopup(new Friend
            {
                Id = friend.Id,
                Name = friend.Name,
                LastName = friend.LastName,
                Nickname = friend.Nickname,
                Address = friend.Address
            }, LoanDirection.Out, _db, _loanService, _bookService);

            await App.Current.MainPage.ShowPopupAsync(popup);
        }

    }

    private async void OnBorrowBookClicked(object sender, EventArgs e)
    {
        if (sender is SwipeItem item && item.CommandParameter is FriendsDTO friend)
        {
            var popup = new LendBorrowPopup(new Friend
            {
                Id = friend.Id,
                Name = friend.Name,
                LastName = friend.LastName,
                Nickname = friend.Nickname,
                Address = friend.Address
            }, LoanDirection.In, _db, _loanService, _bookService);


            await App.Current.MainPage.ShowPopupAsync(popup);
        }

    }
    private async void OnViewLoansClicked(object sender, EventArgs e)
    {
        if (sender is SwipeItemView item && item.CommandParameter is FriendsDTO friend)
        {
            if (friend != null)
            {
                await Navigation.PushAsync(new FriendLoansPage(new Friend
                {
                    Id = friend.Id,
                    Name = friend.Name,
                    LastName = friend.LastName,
                    Nickname = friend.Nickname,
                    Address = friend.Address
                }, _loanService));
            }

        }
    }


}