using System.ComponentModel.DataAnnotations;

namespace Api.Common;

public class IdDto
{
    [Required]
    [Range(minimum: ModelStatics.MinimumIdRange, maximum: ModelStatics.MaximumIdRange)]
    public int Id { get; set; }
}