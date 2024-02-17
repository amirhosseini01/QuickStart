using Api.Modules.Shared;

namespace Api.Modules.Product;

public class ProductVm
{

}

public class ProductListVm
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Thumbnail { get; set; }
    public int Price { get; set; }
}

public class ProductListFilter: PaginatedListFilter
{

}