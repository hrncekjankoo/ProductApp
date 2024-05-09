namespace ProductApp.Model;

public class Provider
{
    public int ProviderId { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<Product> Products { get; set; } = new List<Product>();
}