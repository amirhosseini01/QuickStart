using System.ComponentModel.DataAnnotations;

namespace Common.Data;

public class BaseEntity<T>
{
    [Key]
    public T Id { get; set; }
}

public class BaseEntity : BaseEntity<int>
{
}