using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ProductApp.Model.Context;
using ProductApp.Services;
using ProductApp.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddDbContext<ProductContext>(options =>
{
    options.UseInMemoryDatabase("ProductDb");
});
builder.Services.AddControllers();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Product API",
        Description = "An API application for managing product items",
        Contact = new OpenApiContact
        {
            Name = "Jan Hrncir",
            Email = "honza.hrncir5@seznam.cz"
        }
    });
});

var app = builder.Build();
app.MapControllers();

if (builder.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => // UseSwaggerUI is called only in Development.
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.Run();