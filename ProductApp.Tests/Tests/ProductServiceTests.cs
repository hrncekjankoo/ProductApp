using Microsoft.EntityFrameworkCore;
using ProductApp.Model;
using ProductApp.Model.Requests;
using ProductApp.Model.Responses;
using ProductApp.Services;
using ProductApp.Services.Contracts;
using ProductApp.Tests.Setup;
using Xunit;

namespace ProductApp.Tests.Tests;

public partial class ProductServiceTests : ProductInMemoryDatabaseFixture
{
    private static void ValidateProductWithResponse(Product product, ProductResponse? productResponse)
    {
        Assert.NotNull(productResponse);
        Assert.Equal(product.Name, productResponse.Name);
        Assert.Equal(product.Price, productResponse.Price);
        Assert.Equal(product.ProviderId, productResponse.Provider?.ProviderId);
    }
    
    private async Task ValidateProductWithDatabaseAsync(Product product, int databaseProductId)
    {
        var databaseResult = await GetProductAsync(databaseProductId);
        
        Assert.NotNull(databaseResult);
        Assert.Equal(product.Name, databaseResult.Name);
        Assert.Equal(product.Price, databaseResult.Price);
        Assert.Equal(product.ProviderId, databaseResult.ProviderId);
    }
    
    private static void ValidateProductRequestWithResponse(ProductRequest productRequest, ProductResponse? productResponse)
    {
        Assert.NotNull(productResponse);
        Assert.Equal(productRequest.Name, productResponse.Name);
        Assert.Equal(productRequest.Price, productResponse.Price);
        Assert.Equal(productRequest.Provider?.Name, productResponse.Provider?.Name);
    }
    
    private async Task ValidateProductRequestWithDatabaseAsync(ProductRequest productRequest, int databaseProductId)
    {
        var databaseResult = await GetProductAsync(databaseProductId);
         
        Assert.NotNull(databaseResult);
        Assert.Equal(productRequest.Name, databaseResult.Name);
        Assert.Equal(productRequest.Price, databaseResult.Price);
        Assert.Equal(productRequest.Provider?.Name, databaseResult.Provider?.Name);
    }
    
    private async Task<Product?> GetProductAsync(int productId)
    {
        return await Context.Products.Include(p => p.Provider).FirstOrDefaultAsync(p => p.ProductId == productId);
    }
    
    private async Task<Product> PersistProductAsync(string name, decimal price, int? providerId = null)
    {
        var product = new Product
        {
            Name = name,
            Price = price,
            ProviderId = providerId
        };

        await PersistAsync(product);

        return product;
    }
    
    private async Task<Provider> PersistProviderAsync(string name)
    {
        var provider = new Provider
        {
            Name = name
        };

        await PersistAsync(provider);

        return provider;
    }
    
    private IProductService InitializeProductService()
    {
        return new ProductService(Context);
    }
}