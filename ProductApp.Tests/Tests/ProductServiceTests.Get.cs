using Xunit;

namespace ProductApp.Tests.Tests;

public partial class ProductServiceTests
{
    [Fact]
    public async Task GetProductById()
    {
        // Given
        var provider = await PersistProviderAsync("Provider 1");
        var product = await PersistProductAsync("Product 1", 10, provider.ProviderId);
        
        var productService = InitializeProductService();

        // When
        var productResponse = await productService.GetProductResponse(product.ProductId, CancellationToken.None);

        // Then
        ValidateProductWithResponse(product, productResponse);
        await ValidateProductWithDatabaseAsync(product, productResponse.ProductId);
    }
    
    [Fact]
    public async Task GetProducts()
    {
        // Given
        var provider = await PersistProviderAsync("Provider 1");
        var product1 = await PersistProductAsync("Product 1", 10, provider.ProviderId);
        var product2 = await PersistProductAsync("Product 2", 20, provider.ProviderId);
        
        var productService = InitializeProductService();

        // When
        var productsResponse = await productService.GetProductsResponse(CancellationToken.None);

        // Then
        Assert.Equal(2, productsResponse.Count);
        
        var product1Response = productsResponse.FirstOrDefault(p => p.ProductId == product1.ProductId);
        Assert.NotNull(product1Response);
        ValidateProductWithResponse(product1, product1Response);
        await ValidateProductWithDatabaseAsync(product1, product1Response.ProductId);
        
        var product2Response = productsResponse.FirstOrDefault(p => p.ProductId == product2.ProductId);
        Assert.NotNull(product2Response);
        ValidateProductWithResponse(product2, product2Response);
        await ValidateProductWithDatabaseAsync(product2, product2Response.ProductId);
    }
    
    [Fact]
    public async Task ThrowsNotFoundExceptionWhenGettingNotExistingProduct()
    {
        // Given
        const int nonExistingProductId = -1;

        var service = InitializeProductService();

        // When
        var exception = await Record.ExceptionAsync(() => service.GetProductResponse(nonExistingProductId, CancellationToken.None));

        // Then
        Assert.IsAssignableFrom<InvalidOperationException>(exception);
    }
}