using System.ComponentModel.DataAnnotations;
using Api.Common;

namespace Api;

public class BaseEntity
{
    [Key]
    public int Id { get; set; }
}

public class BaseEntityCreate
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [StringLength(ModelStatics.UserRequiredLength)]
    public string CreateUserId { get; set; }
    public DateTime CreateDate { get; set; }
    
}