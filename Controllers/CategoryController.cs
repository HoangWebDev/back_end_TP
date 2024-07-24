using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;
using TechPhone.Models;
using TechPhone.Repository;

namespace TechPhone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly DataContext _context;
        public CategoryController(DataContext context) 
        { 
            _context = context;
        }

        //Get: api/Category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryModel>>> GetCategorys()
        {
            return await _context.Categories.ToListAsync();
        }

        //Get: api/Category/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryModel>> GetCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return category;
        }

        //Put: api/Category/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, CategoryModel category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            //Tìm danh mục theo id
            var existingCategory = await _context.Categories.FindAsync(id);
            if (existingCategory == null)
            {
                return NotFound();
            }

            //Cập nhật thuộc tính 
            existingCategory.Name = category.Name;
            existingCategory.Slug = category.Slug;
            existingCategory.Description = category.Description;
            existingCategory.Status = category.Status;

            //Lưu cơ sở dữ liệu
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //Post: api/Category
        [HttpPost]
        public async Task<ActionResult<CategoryModel>> PostCategory(CategoryModel category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
        }

        //Delete" api/Category/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) { return NotFound(); }
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
