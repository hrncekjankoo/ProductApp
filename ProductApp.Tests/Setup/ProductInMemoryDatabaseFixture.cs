using ProductApp.Model.Context;

namespace ProductApp.Tests.Setup;

public class ProductInMemoryDatabaseFixture
{
    protected readonly ProductContext Context = CreateProductContext();
    
    protected async Task PersistAsync(params object[] entities)
    {
        Context.AddRange(entities);
        await Context.SaveChangesAsync();
    }

    private static ProductContext CreateProductContext()
    {
        return ProductContextFactory.CreateProductContext();
    }
}