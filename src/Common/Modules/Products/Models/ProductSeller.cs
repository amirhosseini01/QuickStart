using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Common.Commons;
using Common.Modules.User;
using Common.Data;

namespace Common.Modules.Product;

public class ProductSeller : BaseEntity
{
    [StringLength(ModelStatics.UserRequiredLength)]
    public string UserId { get; set; }

    [StringLength(ModelStatics.TitleRequiredLength)]
    public string Title { get; set; }

    [StringLength(ModelStatics.PhoneNumberRequiredLength)]
    public string PhoneNumber { get; set; }

    [StringLength(ModelStatics.ImageRequiredLength)]
    public string? Logo { get; set; }

    [StringLength(ModelStatics.DescriptionRequiredLength)]
    public string? PostalAddress { get; set; }

    [DeleteBehavior(DeleteBehavior.Restrict)]
    public AppUser User { get; set; }
}