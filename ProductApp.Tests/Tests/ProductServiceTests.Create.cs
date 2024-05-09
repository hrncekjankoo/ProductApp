using ProductApp.Model.Requests;
using Xunit;

namespace ProductApp.Tests.Tests;

public partial class ProductServiceTests
{
    [Fact]
    public async Task CreatesProduct()
    {
        // Given
        var productRequest = new ProductRequest
        {
            Name = "Product 1",
            Price = 10,
            Provider = new ProviderRequest
            {
                Name = "Provider 1"
            }
        };
        
        var productService = InitializeProductService();

        // When
        var productResponse = await productService.CreateProduct(productRequest, CancellationToken.None);

        // Then
        ValidateProductRequestWithResponse(productRequest, productResponse);
        await ValidateProductRequestWithDatabaseAsync(productRequest, productResponse.ProductId);
    }
}