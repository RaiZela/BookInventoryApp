using BookInventoryApp.Services;

namespace BookInventoryApp;

public partial class FriendsPage : ContentPage
{
    private List<FriendsDTO> allFriends = new(); // Original list of all friends
    private readonly IFriendService _service;
    public FriendsPage(IFriendService service)
    {
        InitializeComponent();
        _service = service;
        SearchBar.TextChanged += OnSearchTextChanged;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        allFriends = await _service.GetFriendsAsync(); // adjust according to your service
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
        if (sender is SwipeItem item && item.CommandParameter is FriendsDTO friend)
        {
            bool confirm = await DisplayAlert("Confirm Delete", $"Delete friend '{friend.Nickname}'?", "Yes", "No");
            if (confirm)
            {
                await _service.DeleteFriendAsync(friend.Id);
                FriendCollection.ItemsSource = await _service.GetFriendsAsync();
            }
        }
    }
}