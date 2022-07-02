using Microsoft.EntityFrameworkCore;
using Shopbridge_base.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Shopbridge_base.Data.Repository
{
    public class Repository : IRepository
    {
        private readonly Shopbridge_Context _shopbridgeDBCcontext;

        public Repository(Shopbridge_Context _dbcontext)
        {
            this._shopbridgeDBCcontext = _dbcontext;
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            _shopbridgeDBCcontext.Add(product);
            await _shopbridgeDBCcontext.SaveChangesAsync();
            return product;
        }

        public async Task<List<Product>> GetAllProductAsync(int skip, int take)
        {
            return await _shopbridgeDBCcontext.Product.Where(a => a.IsActive == true).Skip(skip).Take(take).ToListAsync();
        }        

        public async Task<Product> GetProductByIdAsync(long id)
        {
            return await _shopbridgeDBCcontext.Product.FirstOrDefaultAsync(a => a.Product_Id == id && a.IsActive == true);
        }

        public async Task<bool> DeleteProductAsync(Product product)
        {
            product.IsActive = false;
            _shopbridgeDBCcontext.Update(product);
            await _shopbridgeDBCcontext.SaveChangesAsync();
            return true;
        }

        public async Task<Product> GetProductByNameAndCompanyNameAsync(string name, string companyName)
        {
            return await _shopbridgeDBCcontext.Product.FirstOrDefaultAsync(a => a.ProductName == name && a.ComapnyName== companyName && a.IsActive == true);
        }

        public async Task<Product> UpdateProductAsync( Product product)
        {
            _shopbridgeDBCcontext.Update(product);
            await _shopbridgeDBCcontext.SaveChangesAsync();
            return product;
        }
    }
}
