using TechPhone.Models;

namespace TechPhone.Contracts
{
    public interface IBrandRepository
    {
        Task<IEnumerable<BrandModel>> GetAll();
        Task<BrandModel> GetById(int id);        
        Task InsertBrand(BrandModel brand);
        Task UpdateBrand(BrandModel brand, int id);
        Task DeleteBrand(int id);
    }
}
