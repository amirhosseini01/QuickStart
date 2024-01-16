namespace Api.Modules.Product;

public class ProductStock : BaseEntityCreate
{
    public int ProductId { get; set; }
    public int ProductModelId { get; set; }
    public int Value { get; set; }

    public Product Product { get; set; }
    public ProductModel ProductModel { get; set; }
}