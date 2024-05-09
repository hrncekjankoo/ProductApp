using ProductApp.Model.Requests;
using ProductApp.Model.Responses;

namespace ProductApp.Services.Contracts;

public interface IProductService
{

    Task<ProductResponse> GetProductResponse(int productId, CancellationToken cancellationToken);
    
    Task<List<ProductResponse>> GetProductsResponse(CancellationToken cancellationToken);
    
    Task<ProductResponse> CreateProduct(ProductRequest productRequest, CancellationToken cancellationToken);
    
    Task<ProductResponse> UpdateProduct(int productId, ProductRequest productRequest, CancellationToken cancellationToken);
    
    Task DeleteProduct(int productId, CancellationToken cancellationToken);
}