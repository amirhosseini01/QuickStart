using System.ComponentModel.DataAnnotations;

namespace Api;

public class BaseEntity
{
    [Key]
    public int Id { get; set; }
}