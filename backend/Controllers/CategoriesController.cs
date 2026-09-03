
using backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly AppDbContext _context;

    public CategoriesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await _context.Categories.ToListAsync();
        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategory(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null)
        {
            return NotFound();
        }
        return Ok(category);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory(CategoryCreateDto categoryDto)
    {
        var category = new Category
        {
            Name = categoryDto.Name,
            Slug = categoryDto.Slug
        };

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(int id, CategoryUpdateDto categoryDto)
    {
        var existingCategory = await _context.Categories.FindAsync(id);

        if (existingCategory == null)
        {
            return NotFound();
        }

        existingCategory.Name = categoryDto.Name;
        existingCategory.Slug = categoryDto.Slug;

        await _context.SaveChangesAsync();

        return Ok(existingCategory);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var category = await _context.Categories.FindAsync(id);

        if (category == null)
        {
            return NotFound();
        }

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}