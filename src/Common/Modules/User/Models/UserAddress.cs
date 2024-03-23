using System.ComponentModel.DataAnnotations;
using Common.Commons;
using Common.Data;
using Common.Modules.Sale;
using Microsoft.EntityFrameworkCore;

namespace Common.Modules.User;

public class UserAddress : BaseEntity
{
    [StringLength(ModelStatics.UserRequiredLength)]
    public string UserId { get; set; }

    [StringLength(ModelStatics.TitleRequiredLength)]
    public string Title { get; set; }

    [StringLength(ModelStatics.PhoneNumberRequiredLength)]
    public string PhoneNumber { get; set; }

    [StringLength(ModelStatics.PhoneNumberRequiredLength)]
    public string Tel { get; set; }

    [StringLength(ModelStatics.DescriptionRequiredLength)]
    public string? PostalAddress { get; set; }

    [DeleteBehavior(DeleteBehavior.Restrict)]
    public AppUser User { get; set; }

    public ICollection<Order> Orders { get; }
}