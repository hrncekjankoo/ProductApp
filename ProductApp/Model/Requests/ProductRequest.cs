using System.ComponentModel.DataAnnotations;

namespace ProductApp.Model.Requests;

public class ProductRequest
{ 
    [Required] 
    public string? Name { get; set; } 
    
    [Required] 
    public decimal? Price { get; set; }
    
    public ProviderRequest? Provider { get; set; } 
}
