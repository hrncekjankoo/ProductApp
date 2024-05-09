using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ProductApp.Model.Context;

namespace ProductApp.Tests.Setup;

public static class ProductContextFactory
{
    public static ProductContext CreateProductContext()
    {
        var options = CreateOptions<ProductContext>();

        return new ProductContext(options);
    }
    
    private static DbContextOptions<T> CreateOptions<T>() where T : DbContext
    {
        return new DbContextOptionsBuilder<T>()
            .EnableSensitiveDataLogging()
            .UseInMemoryDatabase(Guid.NewGuid().ToString(), new InMemoryDatabaseRoot())
            .Options;
    }
}