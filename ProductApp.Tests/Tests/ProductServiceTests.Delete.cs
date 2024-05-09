using Xunit;

namespace ProductApp.Tests.Tests;

public partial class ProductServiceTests
{
    [Fact]
    public async Task DeletesProduct()
    {
        // Given
        var product = await PersistProductAsync("Product 1", 10);
        
        var productService = InitializeProductService();

        // When
        await productService.DeleteProduct(product.ProductId, CancellationToken.None);

        // Then
        var databaseResult = await Context.Products.FindAsync(product.ProductId);
        Assert.Null(databaseResult);
    }
    
    [Fact]
    public async Task ThrowsNotFoundExceptionWhenDeletingNotExistingProduct()
    {
        // Given
        const int nonExistingProductId = -1;

        var service = InitializeProductService();

        // When
        var exception = await Record.ExceptionAsync(() => service.DeleteProduct(nonExistingProductId, CancellationToken.None));

        // Then
        Assert.IsAssignableFrom<InvalidOperationException>(exception);
    }
}