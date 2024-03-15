using System.ComponentModel.DataAnnotations;
using Common.Commons;
using Microsoft.AspNetCore.Http;

namespace Common.Modules.Product;

public class ProductSellerVm
{

}

public class ProductSellerListDto
{
    public string Title { get; set; }
    public string PhoneNumber { get; set; }
    public string Logo { get; set; }
    public string PostalAddress { get; set; }
}

public class ProductSellerListFilterDto : PaginatedListFilter
{

}

public class ProductSellerDetailDto
{
    public string Title { get; set; }
    public string PhoneNumber { get; set; }
    public string Logo { get; set; }
    public string PostalAddress { get; set; }
}

public class ProductSellerAdminInputDto
{
    [Required]
    [StringLength(maximumLength: ModelStatics.UserRequiredLength, MinimumLength = ModelStatics.UserMinimumLength)]
    public string UserId { get; set; }

    [Required]
    [StringLength(maximumLength: ModelStatics.TitleRequiredLength, MinimumLength = ModelStatics.TitleMinimumLength)]
    public string Title { get; set; }

    [Required]
    [StringLength(maximumLength: ModelStatics.PhoneNumberRequiredLength, MinimumLength = ModelStatics.PhoneNumberRequiredLength)]
    public string PhoneNumber { get; set; }

    public IFormFile? Logo { get; set; }

    [StringLength(maximumLength: ModelStatics.TitleRequiredLength, MinimumLength = ModelStatics.TitleMinimumLength)]
    public string? PostalAddress { get; set; }
}