using TechPhone.Models;

namespace TechPhone.Contracts
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductModel>> GetAll();
        Task<ProductModel> GetById(int id);        
        Task InsertProduct(ProductModel product);
        Task UpdateProduct(ProductModel product, int id);
        Task DeleteProduct(int id);
    }
}
