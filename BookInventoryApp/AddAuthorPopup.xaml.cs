using BookInventoryApp.Services;

namespace BookInventoryApp;

public partial class AddAuthorPopup : Popup
{
    IAuthorService _service;
    Author Author = new();
    public AddAuthorPopup(IAuthorService service, AuthorDTO author)
    {
        InitializeComponent();
        _service = service;
        if (author != null && author.Id != Guid.Empty)
        {
            AuthorReverseMap(author, Author);
            //DeleteButton.IsVisible = true;
        }
        else
        {
            //DeleteButton.IsVisible = false;
        }
        Name.Text = Author.Name;
        MiddleName.Text = Author.MiddleName;
        LastName.Text = Author.LastName;
    }
    private async void OnSaveClicked(object sender, EventArgs e)
    {
        var author = new Author
        {
            Id = Author.Id,
            Name = Name.Text,
            MiddleName = MiddleName.Text,
            LastName = LastName.Text
        };

        var authorCheck = await _service.GetAuthorAsync(Author.Id);
        if (authorCheck is not null)
        {
            var authors = await _service.SaveAuthorAsync(author);
            if (authors > 0)
            {
                //await AnimateSuccessAsync();
                //await ShowSuccessCheckAsync();
            }
            else
            {
                //await AnimateErrorAsync();
                //await ShowErrorAsync();
            }
        }

        else
        {
            var authors = await _service.SaveNewAuthorAsync(author);
            if (authors > 0)
            {

                //await AnimateSuccessAsync();
                //await ShowSuccessCheckAsync();
            }
            else
            {
                //await AnimateErrorAsync();
                //await ShowErrorAsync();
            }
        }

        Close();
    }
    private static void AuthorReverseMap(AuthorDTO authorDto, Author Author)
    {
        var nameSplit = new string[] { authorDto.FullName }.FirstOrDefault()?.Split(' ', StringSplitOptions.RemoveEmptyEntries);


        Author.Id = authorDto.Id;
        Author.Name = nameSplit[0];
        Author.MiddleName = nameSplit.Length > 2 ? string.Join(' ', nameSplit.Skip(1).Take(nameSplit.Length - 2)) : string.Empty;
        Author.LastName = nameSplit.Length >= 3 ? nameSplit[2] :
                nameSplit.Length == 2 ? nameSplit[1] :
                string.Empty;
    }
    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {

        bool confirm = await Application.Current.MainPage.DisplayAlert("Warning", "Are you sure?", "Yes", "No");

        if (confirm)
        {
            var authors = await _service.DeleteAuthorAsync(Author);

            if (authors > 0)
            {
                //await AnimateSuccessAsync();
                //await ShowSuccessCheckAsync();
            }
            else
            {
                //await AnimateErrorAsync();
                //await ShowErrorAsync();
            }

        }
        Close();
        await Shell.Current.GoToAsync($"///{nameof(AuthorPage)}");


    }
    //private async Task AnimateSuccessAsync()
    //{
    //    await MainLayout.ScaleTo(1.05, 100, Easing.CubicOut);
    //    await MainLayout.ScaleTo(1.0, 100, Easing.CubicIn);
    //    await MainLayout.FadeTo(0.9, 100);
    //    await MainLayout.FadeTo(1.0, 100);
    //}
    //private async Task ShowSuccessCheckAsync()
    //{
    //    SuccessOverlay.Opacity = 0;
    //    SuccessOverlay.IsVisible = true;

    //    //SuccessIcon.Opacity = 0;
    //    //SuccessIcon.Scale = 0.5;

    //    await Task.WhenAll(
    //        SuccessOverlay.FadeTo(1, 150),
    //        SuccessIcon.FadeTo(1, 200),
    //        SuccessIcon.ScaleTo(1.2, 200, Easing.SpringOut)
    //    );

    //    await Task.Delay(1000);

    //    await SuccessOverlay.FadeTo(0, 300);
    //    SuccessOverlay.IsVisible = false;
    //}
    //private async Task ShowErrorAsync(string message = "Something went wrong")
    //{
    //    ErrorMessage.Text = message;

    //    ErrorOverlay.Opacity = 0;
    //    ErrorOverlay.IsVisible = true;

    //    ErrorIcon.Opacity = 0;
    //    ErrorIcon.Scale = 0.5;
    //    ErrorMessage.Opacity = 0;
    //    ErrorMessage.Scale = 0.5;

    //    await Task.WhenAll(
    //        ErrorOverlay.FadeTo(1, 150),
    //        ErrorIcon.FadeTo(1, 200),
    //        ErrorIcon.ScaleTo(1.2, 200, Easing.SpringOut),
    //        ErrorMessage.FadeTo(1, 200),
    //        ErrorMessage.ScaleTo(1.2, 200, Easing.SpringOut)
    //    );

    //    await Task.Delay(2000);

    //    await ErrorOverlay.FadeTo(0, 300);
    //    ErrorOverlay.IsVisible = false;
    //}
    //private async Task AnimateErrorAsync()
    //{
    //    uint duration = 50;
    //    await MainLayout.TranslateTo(-15, 0, duration);
    //    await MainLayout.TranslateTo(15, 0, duration);
    //    await MainLayout.TranslateTo(-10, 0, duration);
    //    await MainLayout.TranslateTo(10, 0, duration);
    //    await MainLayout.TranslateTo(0, 0, duration);
    //}

}