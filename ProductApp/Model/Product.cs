namespace ProductApp.Model;

public class Product
{ 
    public int ProductId { get; set; } 
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public int? ProviderId { get; set; } 
    public Provider? Provider { get; set; }
}
