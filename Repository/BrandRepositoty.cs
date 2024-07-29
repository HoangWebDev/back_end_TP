using Microsoft.EntityFrameworkCore;
using TechPhone.Contracts;
using TechPhone.Models;

namespace TechPhone.Repository
{
    public class BrandRepositoty: IBrandRepository
    {
        private DataContext context;
        public BrandRepositoty(DataContext context)
        {
            this.context = context;
        }
        
        async Task<IEnumerable<BrandModel>> IBrandRepository.GetAll()
        {
            return context.Brands.ToList();
        }

        async Task<BrandModel> IBrandRepository.GetById(int id)
        {
            return context.Brands.Find(id);
        }        
        async Task IBrandRepository.InsertBrand(BrandModel product)
        {
            context.Brands.Add(product);                        
        }

        async Task IBrandRepository.UpdateBrand(BrandModel product, int id)
        {
            context.Entry(product).State = EntityState.Modified;
            context.SaveChanges();
        }

        async Task IBrandRepository.DeleteBrand(int id)
        {
            BrandModel product = context.Brands.Find(id);
            context.Brands.Remove(product);
        }
    }
}
