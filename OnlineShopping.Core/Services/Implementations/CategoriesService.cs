
namespace OnlineShopping.Core.Services.Implementations
{
    public class CategoriesService : ICategoriesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoriesService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<RestDto<List<CategoryDto>>> GetCategories()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync();

            var dto = _mapper.Map<List<CategoryDto>>(categories);

            var result = new RestDto<List<CategoryDto>>(StatusCodes.Status200OK, dto);
            return result;
        }
    }
}
