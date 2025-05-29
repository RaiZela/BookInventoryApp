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
}