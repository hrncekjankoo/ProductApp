namespace ProductApp.Model.Responses;

public class ProductResponse
{ 
    public int ProductId { get; set; } 
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public ProviderResponse? Provider { get; set; } 
}
