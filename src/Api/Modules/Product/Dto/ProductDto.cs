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
    public int Price { get; set; }
}

public class ProductListFilterDto: PaginatedListFilter
{

}