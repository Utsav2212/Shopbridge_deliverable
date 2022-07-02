using Shopbridge_base.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopbridge_base.Data.Repository
{
    public interface IRepository
    {
        Task<List<Product>> GetAllProductAsync(int skip, int take);
        Task<Product> GetProductByIdAsync(long id);
        Task<Product> GetProductByNameAndCompanyNameAsync(string name, string companyName);
        Task<Product> AddProductAsync(Product product);
        Task<Product> UpdateProductAsync( Product product);
        Task<bool> DeleteProductAsync(Product id);
    }
}
