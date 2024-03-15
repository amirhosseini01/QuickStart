using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Common.Commons;

public class IdDto
{
    [FromRoute]
    [Required]
    [Range(minimum: ModelStatics.MinimumIdRange, maximum: ModelStatics.MaximumIdRange)]
    public int Id { get; set; }
}