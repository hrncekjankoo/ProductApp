using Microsoft.AspNetCore.Mvc;
using ProductApp.Model.Requests;
using ProductApp.Model.Responses;
using ProductApp.Services.Contracts;

namespace ProductApp;

[Produces("application/json")]
[Route("products")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    /// <summary>
    /// Gets product detail by its ID.
    /// </summary>
    /// <response code="200">Returns product detail.</response>
    /// <response code="404">Product with specified ID does not exist.</response>
    /// <param name="productId">Product ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Product detail.</returns>
    [HttpGet("{productId}")]
    public async Task<ActionResult<ProductResponse>> GetProduct(int productId, CancellationToken cancellationToken)
    {
        var productResponse = await _productService.GetProductResponse(productId, cancellationToken);

        return Ok(productResponse);
    }

    /// <summary>
    /// Gets products.
    /// </summary>
    /// <response code="200">Returns products.</response>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Products details.</returns>
    [HttpGet]
    public async Task<ActionResult<List<ProductResponse>>> GetProducts(CancellationToken cancellationToken)
    {
        var productsResponse = await _productService.GetProductsResponse(cancellationToken);

        return Ok(productsResponse);
    }

    /// <summary>
    /// Creates new product.
    /// </summary>
    /// <response code="201">Returns detail of newly created product.</response>
    /// <param name="productRequest">Request body.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Product detail.</returns>
    [HttpPost]
    public async Task<ActionResult<ProductResponse>> CreateProduct(ProductRequest productRequest, CancellationToken cancellationToken)
    {
        var productResponse = await _productService.CreateProduct(productRequest, cancellationToken);

        return CreatedAtAction(nameof(GetProduct), new { productId = productResponse.ProductId }, productResponse);
    }

    /// <summary>
    /// Update product.
    /// </summary>
    /// <response code="200">Product was successfully updated.</response>
    /// <response code="404">Product with specified ID does not exist.</response>
    /// <param name="productId">Product ID.</param>
    /// <param name="productRequest">Request body.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Updated product detail.</returns>
    [HttpPut("{productId}")]
    public async Task<ActionResult<ProductResponse>> UpdateProduct(int productId, ProductRequest productRequest, CancellationToken cancellationToken)
    {
        var productResponse = await _productService.UpdateProduct(productId, productRequest, cancellationToken);

        return Ok(productResponse);
    }

    /// <summary>
    /// Delete product.
    /// </summary>
    /// <response code="204">Product was successfully deleted.</response>
    /// <response code="404">Product with specified ID does not exist.</response>
    /// <param name="productId">Product ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpDelete("{productId}")]
    public async Task<ActionResult> DeleteProduct(int productId, CancellationToken cancellationToken)
    {
        await _productService.DeleteProduct(productId, cancellationToken);

        return NoContent();
    }
}