using System.ComponentModel.DataAnnotations;

namespace Api;

public class BaseEntity<T>
{
    [Key]
    public T Id { get; set; }
}

public class BaseEntity: BaseEntity<int>
{
}