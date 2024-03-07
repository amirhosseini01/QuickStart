using System.ComponentModel.DataAnnotations;
using Api.Common;

namespace Api.Modules.Product;

public class ProductStockDto
{

}

public class ProductStockListDto
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public int ProductId { get; set; }
    public int ProductModelId { get; set; }
    public int Value { get; set; }
    public DateTime CreateDate { get; set; }
}

public class ProductStockDetailDto
{
    public string UserId { get; set; }
    public int ProductId { get; set; }
    public int ProductModelId { get; set; }
    public int Value { get; set; }
    public DateTime CreateDate { get; set; }
}

public class ProductStockListFilterDto: PaginatedListFilter
{

}
public class ProductStockAdminInputDto
{
    [Required]
    [StringLength(maximumLength: ModelStatics.UserRequiredLength, MinimumLength = ModelStatics.UserMinimumLength)]
    public string UserId { get; set; }
    
    [Required]
    [Range(minimum: ModelStatics.MinimumIdRange, maximum: ModelStatics.MaximumIdRange)]
    public int ProductId { get; set; }
    
    [Required]
    [Range(minimum: ModelStatics.MinimumIdRange, maximum: ModelStatics.MaximumIdRange)]
    public int ProductModelId { get; set; }
    
    [Required]
    public int Value { get; set; }
    
    [Required]
    public DateTime CreateDate { get; set; }
}