using BookInventoryApp.Services;

namespace BookInventoryApp;

public partial class FriendPopup : Popup
{
    IFriendService _service;
    Friend Friend = new();
    public FriendPopup(IFriendService service, FriendsDTO Friend)
    {
        InitializeComponent();
        _service = service;
        //if (Friend != null && Friend.Id != Guid.Empty)
        //{
        //    DeleteButton.IsVisible = true;
        //}
        //else
        //{
        //    DeleteButton.IsVisible = false;
        //}
        Name.Text = Friend.Name;
        LastName.Text = Friend.LastName;
        Nickname.Text = Friend.Nickname;
        Address.Text = Friend.Address;
        Title.Text = Friend.FullName;
    }
    private async void OnSaveClicked(object sender, EventArgs e)
    {
        Friend = new Friend
        {
            Id = Friend.Id,
            Name = Name.Text,
            LastName = LastName.Text,
            Nickname = Nickname.Text,
            Address = Address.Text
        };

        var FriendCheck = await _service.GetFriendAsync(Friend.Id);
        if (FriendCheck is not null)
        {
            var Friends = await _service.UpdateFriendAsync(Friend);
            if (Friends > 0)
            {
                await AnimateSuccessAsync();
                await ShowSuccessCheckAsync();
            }
            else
            {
                await AnimateErrorAsync();
                await ShowErrorAsync();
            }
        }

        else
        {
            var Friends = await _service.SaveNewFriendAsync(Friend);
            if (Friends > 0)
            {
                await AnimateSuccessAsync();
                await ShowSuccessCheckAsync();
            }
            else
            {
                await AnimateErrorAsync();
                await ShowErrorAsync();
            }
        }

        Close();
    }
    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {

        bool confirm = await Application.Current.MainPage.DisplayAlert("Warning", "Are you sure?", "Yes", "No");

        if (confirm)
        {
            var Friends = await _service.DeleteFriendAsync(Friend.Id);

            if (Friends > 0)
            {
                await AnimateSuccessAsync();
                await ShowSuccessCheckAsync();
            }
            else
            {
                await AnimateErrorAsync();
                await ShowErrorAsync();
            }

        }
        Close();
        await Shell.Current.GoToAsync($"///{nameof(FriendsPage)}");


    }
    private async Task AnimateSuccessAsync()
    {
        await MainLayout.ScaleTo(1.05, 100, Easing.CubicOut);
        await MainLayout.ScaleTo(1.0, 100, Easing.CubicIn);
        await MainLayout.FadeTo(0.9, 100);
        await MainLayout.FadeTo(1.0, 100);
    }
    private async Task ShowSuccessCheckAsync()
    {
        SuccessOverlay.Opacity = 0;
        SuccessOverlay.IsVisible = true;

        //SuccessIcon.Opacity = 0;
        //SuccessIcon.Scale = 0.5;

        await Task.WhenAll(
            SuccessOverlay.FadeTo(1, 150)
        //SuccessIcon.FadeTo(1, 200),
        //SuccessIcon.ScaleTo(1.2, 200, Easing.SpringOut)
        );

        await Task.Delay(1000);

        await SuccessOverlay.FadeTo(0, 300);
        SuccessOverlay.IsVisible = false;
    }
    private async Task ShowErrorAsync(string message = "Something went wrong")
    {
        ErrorMessage.Text = message;

        ErrorOverlay.Opacity = 0;
        ErrorOverlay.IsVisible = true;

        //ErrorIcon.Opacity = 0;
        //ErrorIcon.Scale = 0.5;
        ErrorMessage.Opacity = 0;
        ErrorMessage.Scale = 0.5;

        await Task.WhenAll(
            ErrorOverlay.FadeTo(1, 150),
            //ErrorIcon.FadeTo(1, 200),
            //ErrorIcon.ScaleTo(1.2, 200, Easing.SpringOut),
            ErrorMessage.FadeTo(1, 200),
            ErrorMessage.ScaleTo(1.2, 200, Easing.SpringOut)
        );

        await Task.Delay(2000);

        await ErrorOverlay.FadeTo(0, 300);
        ErrorOverlay.IsVisible = false;
    }
    private async Task AnimateErrorAsync()
    {
        uint duration = 50;
        await MainLayout.TranslateTo(-15, 0, duration);
        await MainLayout.TranslateTo(15, 0, duration);
        await MainLayout.TranslateTo(-10, 0, duration);
        await MainLayout.TranslateTo(10, 0, duration);
        await MainLayout.TranslateTo(0, 0, duration);
    }

}