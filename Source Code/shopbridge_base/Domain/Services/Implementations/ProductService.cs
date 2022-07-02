using Microsoft.Extensions.Logging;
using Shopbridge_base.Data.Repository;
using Shopbridge_base.Domain.Models;
using Shopbridge_base.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopbridge_base.Domain.Services
{
    public class ProductService : IProductService
    {        
        private readonly IRepository repository; 
        public ProductService(IRepository repository)
        {            
            this.repository = repository;
        }

        public async Task<Product> AddProductAsync(Product product)
        {          
            _ = product ?? throw new ArgumentNullException(nameof(product));
            var existingProdutc = await repository.GetProductByNameAndCompanyNameAsync(product.ProductName,product.ComapnyName);
            if (existingProdutc != null)
                throw new Exception(message: "Duplicate product found");
            return await repository.AddProductAsync(product);           
        }

        public async Task<bool> DeleteProductAsync(long id)
        {
            var deleteProduct = await repository.GetProductByIdAsync(id);
            if (deleteProduct == null)
                throw new Exception(message: "Product not found");            
            return await repository.DeleteProductAsync(deleteProduct);           
        }

        public async Task<List<Product>> GetAllProductAsync(int skip, int take)
        {
            return await repository.GetAllProductAsync(skip, take);
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            var isExisting = await repository.GetProductByNameAndCompanyNameAsync(product.ProductName,product.ComapnyName);
            if (isExisting != null)
                throw new Exception(message: "Duplicate product found");
            return await repository.UpdateProductAsync(product);
        }
    }
}
