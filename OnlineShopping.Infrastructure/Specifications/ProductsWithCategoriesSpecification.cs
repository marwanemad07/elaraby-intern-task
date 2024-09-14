namespace OnlineShopping.Infrastructure.Specifications
{
    public class ProductsWithCategoriesSpecification : BaseSpecification<Product>
    {
        public ProductsWithCategoriesSpecification(PageSpecificationParameters pageSettings)
        {
            AddInclude( p => p.Category!);
            
            if(!string.IsNullOrEmpty(pageSettings.Search))
            {
                Criteria = p => p.Name.ToLower().Contains(pageSettings.Search);
            }

            if(!string.IsNullOrEmpty(pageSettings.Sort))
            {
                switch(pageSettings.Sort)
                {
                    case nameof(SortOrdering.PriceAsc):
                        AddOrderBy(p => p.Price);
                        break;
                    case nameof(SortOrdering.PriceDesc):
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Id);
                        break;
                }
            }

            if(pageSettings.PageSize > 0)
            {
                Take = pageSettings.PageSize;
                Skip = pageSettings.PageSize * (pageSettings.PageNumber - 1);
            }
        }

        public ProductsWithCategoriesSpecification(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.Category!);
        }
    }
}
