namespace OnlineShopping.Core.Services.Interfaces
{
    public interface ICategoriesService
    {
        Task<RestDto<List<CategoryDto>>> GetCategories();
    }
}
