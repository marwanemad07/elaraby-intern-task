
namespace OnlineShopping.Core.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileHelper _fileHelper;

        public ProductService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IFileHelper fileHelper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileHelper = fileHelper;
        }

        public async Task<RestDto<ProductDto?>> AddAsync(NewProductDto productDto)
        {
            var product = _mapper.Map<NewProductDto, Product>(productDto);
            try
            {
                await _unitOfWork.Products.AddAsync(product);
                _unitOfWork.Complete();
                await _fileHelper.SaveFileAsync(productDto.ImageFile, product.ImageUrl);
            }
            catch(Exception ex)
            {
                var errorResult = new RestDto<ProductDto?>(StatusCodes.Status400BadRequest, null, ex.Message);
                return errorResult;
            }

            var createdProductDto = _mapper.Map<Product, ProductDto>(product);
            var result = new RestDto<ProductDto?>(StatusCodes.Status201Created, createdProductDto);
            return result;
        }

        public async Task<RestDto<List<ProductDto>>> GetAllAsync(PageSpecificationParameters parameters)
        {
            var spec = new ProductsWithCategoriesSpecification(parameters);
            var products = await _unitOfWork.Products.GetAllWithSpecAsync(spec);

            var dto = products!.Select(p => _mapper.Map<Product, ProductDto>(p)).ToList();

            var result = new RestDto<List<ProductDto>>(StatusCodes.Status200OK, dto);
            return result;
        }

        public async Task<RestDto<ProductDto?>> GetByIdAsync(int id)
        {
            var spec = new ProductsWithCategoriesSpecification(id);
            var product = await _unitOfWork.Products.GetEntityWithSpecAsync(spec);

            if(product == null)
            {
                var notFoundResult = new RestDto<ProductDto?>(StatusCodes.Status404NotFound, null);
                return notFoundResult;
            }

            var dto = _mapper.Map<Product, ProductDto>(product);
            var result = new RestDto<ProductDto?>(StatusCodes.Status200OK, dto);
            return result;
        }

    }
}
