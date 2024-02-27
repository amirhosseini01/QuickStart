using Api.Common;

namespace Api.Modules.Product;

public class ProductVm
{

}

public class ProductListDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Thumbnail { get; set; }
    public bool Saleable { get; set; }
    public int Price { get; set; }
}

public class ProductListFilterDto : PaginatedListFilter
{

}

public class ProductDetailDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Image { get; set; }
    public bool Saleable { get; set; }
    public string ShortDescription { get; set; }
    public string Description { get; set; }
    public ProductProductSellerDto ProductSeller { get; set; }
    public ProductProductBrandDto ProductBrand { get; set; }
    public ProductProductCategoryDto ProductCategory { get; set; }
    public ICollection<ProductProductModelDto> ProductModels { get; set; }

}
public class ProductProductSellerDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Logo { get; set; }
}
public class ProductProductCategoryDto
{
    public int Id { get; set; }
    public string Title { get; set; }
}
public class ProductProductBrandDto
{
    public int Id { get; set; }
    public string Title { get; set; }
}

public class ProductProductModelDto
{
    public int? ViewOrder { get; set; }
    public string Title { get; set; }
    public ProductModelType Type { get; set; }
    public string Value { get; set; }
    public int Price { get; set; }
}