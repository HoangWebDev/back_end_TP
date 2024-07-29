using Microsoft.EntityFrameworkCore;
using System;
using TechPhone.Models;

namespace TechPhone.Repository
{
    public class SeedData
    {
        public static void SeedingData(DataContext _context)
        {
            _context.Database.Migrate();
            if (!_context.Products.Any())
            {

                CategoryModel macbook = new CategoryModel { Name="Macbook", Slug="macbook", Description="Macbook is Large Product in the world", Status=1 };
                CategoryModel pc = new CategoryModel { Name="Pc", Slug="pc", Description="Pc is Large Product in the world", Status=1 };

                BrandModel apple = new BrandModel { Name = "Apple", Slug = "apple", Description = "Apple is Large Brand in the world", Status = 1 };
                BrandModel samsung = new BrandModel { Name = "Samsung", Slug = "samsung", Description = "Samsung is Large Brand in the world", Status = 1 };

                _context.Products.AddRange
                    (
                        new ProductModel { Name = "Macbook", Slug = "Macbook", Description = "Macbook is Best", Image = "1.jpg", Brand = apple, Category = macbook, Price = 1234 },
                        new ProductModel { Name = "Pc", Slug = "Pc", Description = "Pc is Best", Image = "2.jpg", Brand = samsung, Category = pc, Price = 1234 }
                    );

                _context.SaveChanges();
            }
        }
    }
}
