using Microsoft.EntityFrameworkCore;
using TechPhone.Contracts;
using TechPhone.Models;

namespace TechPhone.Repository
{
    public class ProductRepositoty: IProductRepository
    {
        private DataContext _context;
        public ProductRepositoty(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductModel>> GetAll()
        {
            return await _context.Products.Include(p => p.Brand)
                                           .Include(p => p.Category)
                                           .ToListAsync();
        }

        public async Task<ProductModel> GetById(int id)
        {
            return await _context.Products.Include(p => p.Brand)
                                           .Include(p => p.Category)
                                           .FirstOrDefaultAsync(p => p.Id == id);
        }
       
        public async Task InsertProduct(ProductModel product)
        {
            // Kiểm tra BrandId tồn tại
            if (!await _context.Brands.AnyAsync(b => b.Id == product.BrandId))
            {
                throw new KeyNotFoundException("BrandId không tồn tại");
            }

            //Kiểm tra Category tồn tại
            if (!await _context.Categories.AnyAsync(c => c.Id == product.CategoryId))
            {
                throw new KeyNotFoundException("Category không tồn tại");
            }

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProduct(ProductModel product, int id)
        {
            // Kiểm tra BrandId tồn tại
            if (!await _context.Brands.AnyAsync(b => b.Id == product.BrandId))
            {
                throw new KeyNotFoundException("BrandId không tồn tại");
            }

            //Kiểm tra Category tồn tại
            if (!await _context.Categories.AnyAsync(c => c.Id == product.CategoryId))
            {
                throw new KeyNotFoundException("Category không tồn tại");
            }

            var existingProduct = await _context.Products.FindAsync(id);
            if (existingProduct != null)
            {
                _context.Entry(existingProduct).CurrentValues.SetValues(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

    }
}
