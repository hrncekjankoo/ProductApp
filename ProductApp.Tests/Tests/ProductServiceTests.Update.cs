using ProductApp.Model;
using ProductApp.Model.Requests;
using Xunit;

namespace ProductApp.Tests.Tests;

public partial class ProductServiceTests
{
    [Fact]
    public async Task UpdatesProduct()
    {
        // Given
        var product = new Product
        {
            Name = "Product 1",
            Price = 10,
            Provider = new Provider
            {
                Name = "Provider 1"
            }
        };

        await PersistAsync(product);
        
        var updatedProduct = new ProductRequest
        {
            Name = "Product 2",
            Price = 20,
            Provider = new ProviderRequest
            {
                Name = "Provider 2"
            }
        };
        
        var productService = InitializeProductService();

        // When
        var productResponse = await productService.UpdateProduct(product.ProductId, updatedProduct, CancellationToken.None);

        // Then
        ValidateProductRequestWithResponse(updatedProduct, productResponse);
        await ValidateProductRequestWithDatabaseAsync(updatedProduct, productResponse.ProductId);
    }
    
    [Fact]
    public async Task ThrowsNotFoundExceptionWhenUpdatingNotExistingProduct()
    {
        // Given
        const int nonExistingProductId = -1;
        
        var productRequest = new ProductRequest
        {
            Name = "Product q",
            Price = 20,
            Provider = new ProviderRequest
            {
                Name = "Provider 2"
            }
        };

        var service = InitializeProductService();

        // When
        var exception = await Record.ExceptionAsync(() => service.UpdateProduct(nonExistingProductId, productRequest, CancellationToken.None));

        // Then
        Assert.IsAssignableFrom<InvalidOperationException>(exception);
    }
}