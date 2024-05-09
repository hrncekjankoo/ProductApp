using Microsoft.EntityFrameworkCore;
using ProductApp.Model;
using ProductApp.Model.Context;
using ProductApp.Model.Requests;
using ProductApp.Model.Responses;
using ProductApp.Model.Responses.Selectors;
using ProductApp.Services.Contracts;

namespace ProductApp.Services;

public class ProductService : IProductService
{
    private readonly ProductContext _context;

    public ProductService(ProductContext context)
    {
        _context = context;
    }
    
    public async Task<ProductResponse> GetProductResponse(int productId, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .Where(p => p.ProductId == productId)
            .Select(ProductSelectors.FromProduct)
            .FirstOrDefaultAsync(cancellationToken);

        ThrowIfProductNotFound(productId, wasNotFound: product == null);
        
        return product!;
    }

    public async Task<List<ProductResponse>> GetProductsResponse(CancellationToken cancellationToken)
    {
        return await _context.Products
            .Select(ProductSelectors.FromProduct)
            .ToListAsync(cancellationToken);
    }

    public async Task<ProductResponse> CreateProduct(ProductRequest productRequest, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = productRequest.Name!,
            Price = productRequest.Price!.Value,
            Provider = productRequest.Provider == null ? null : new Provider
            {
                Name = productRequest.Provider.Name
            }
        };
        
        _context.Products.Add(product);
        await _context.SaveChangesAsync(cancellationToken);
        
        return await GetProductResponse(product.ProductId, cancellationToken);
    }

    public async Task<ProductResponse> UpdateProduct(int productId, ProductRequest productRequest, CancellationToken cancellationToken)
    {
        var product = await GetProduct(productId);
        
        product.Name = productRequest.Name!;
        product.Price = productRequest.Price!.Value;
        product.Provider = productRequest.Provider == null
            ? null
            : new Provider
            {
                Name = productRequest.Provider.Name
            };
        
        await _context.SaveChangesAsync(cancellationToken);
        
        return await GetProductResponse(productId, cancellationToken);
    }

    public async Task DeleteProduct(int productId, CancellationToken cancellationToken)
    {
        var product = await GetProduct(productId);
        
        _context.Products.Remove(product);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private async Task<Product> GetProduct(int productId)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == productId);
        
        ThrowIfProductNotFound(productId, wasNotFound: product == null);

        return product!;
    }
    
    private static void ThrowIfProductNotFound(int productId, bool wasNotFound)
    {
        if (wasNotFound)
        {
            throw new InvalidOperationException($"Product with id {productId} not found");
        }
    }
}