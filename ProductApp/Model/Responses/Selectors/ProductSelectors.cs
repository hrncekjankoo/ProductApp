using System.Linq.Expressions;

namespace ProductApp.Model.Responses.Selectors;

public static class ProductSelectors
{
    public static readonly Expression<Func<Product, ProductResponse>> FromProduct = p => new ProductResponse
    {
        ProductId = p.ProductId,
        Name = p.Name,
        Price = p.Price,
        Provider = p.Provider == null ? null : new ProviderResponse
        {
            Name = p.Provider.Name,
            ProviderId = p.Provider.ProviderId
        }
    };
}