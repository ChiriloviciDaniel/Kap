using backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly AppDbContext _context;
    public ProductsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _context.Products.ToListAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(int id)
    {
        var product = await _context.Products
        .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(ProductCreateDto productDto)
    {
        var product = new Product
        {
            Name = productDto.Name,
            Slug = productDto.Slug,
            Description = productDto.Description,
            Price = productDto.Price,
            StockQuantity = productDto.StockQuantity,
            CategoryId = productDto.CategoryId,
            CreatedAt = DateTime.UtcNow

        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, ProductUpdateDto productDto)
    {
        var existingProduct = await _context.Products.FindAsync(id);

        if (existingProduct == null)
        {
            return NotFound();
        }

        existingProduct.Name = productDto.Name;
        existingProduct.Slug = productDto.Slug;
        existingProduct.Description = productDto.Description;
        existingProduct.Price = productDto.Price;
        existingProduct.StockQuantity = productDto.StockQuantity;
        existingProduct.CategoryId = productDto.CategoryId;

        await _context.SaveChangesAsync();

        return Ok(existingProduct);

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpGet("{id}/images")]
    public async Task<IActionResult> GetProductImages(int id)
    {
        var product = await _context.Products
            .Include(p => p.ProductImages)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        return Ok(product.ProductImages);
    }

    [HttpPost("{id}/images")]
    public async Task<IActionResult> CreateProductImage(int id, ProductImageDto imageDto)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        var productImage = new ProductImage
        {
            Url = imageDto.Url,
            ProductId = id
        };

        _context.ProductImages.Add(productImage);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProductImages), new { id = product.Id }, productImage);
    }

    [HttpDelete("{productId}/images/{imageId}")]
    public async Task<IActionResult> DeleteProductImage(int productId, int imageId)
    {
        var productImage = await _context.ProductImages
            .FirstOrDefaultAsync(pi => pi.Id == imageId && pi.ProductId == productId);

        if (productImage == null)
        {
            return NotFound();
        }

        _context.ProductImages.Remove(productImage);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}