namespace OnlineShopping.Core.Services.Interfaces
{
    public interface IProductService
    {
        Task<RestDto<List<ProductDto>>> GetAllAsync(PageSpecificationParameters parameters);
        Task<RestDto<ProductDto?>> GetByIdAsync(int id);
        Task<RestDto<ProductDto?>> AddAsync(NewProductDto productDto);
    }
}
