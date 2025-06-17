using BookInventoryApp.Services;

namespace BookInventoryApp;

public partial class CategoryPage : ContentPage
{
    private readonly ICategoriesService _service;
    public CategoryPage(ICategoriesService service)
    {
        InitializeComponent();
        _service = service;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        CategoryView.ItemsSource = await _service.GetCategoriesAsync();
    }

    private async void OnAddCategoryClicked(object sender, EventArgs e)
    {
        var popup = new AddCategoryPopup(_service);
        await this.ShowPopupAsync(popup);
    }

    private async void OnDeleteCategoryClicked(object sender, EventArgs e)
    {
        if (sender is SwipeItem item && item.CommandParameter is CategoryDTO category)
        {
            bool confirm = await DisplayAlert("Confirm Delete", $"Delete category '{category.Name}'?", "Yes", "No");
            if (confirm)
            {
                await _service.DeleteCategoryAsync(category.Id);
                CategoryView.ItemsSource = await _service.GetCategoriesAsync();
            }
        }
    }

}