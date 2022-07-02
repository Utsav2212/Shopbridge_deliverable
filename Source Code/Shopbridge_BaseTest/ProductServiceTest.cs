using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shopbridge_base.Data.Repository;
using Shopbridge_base.Domain.Models;
using Shopbridge_base.Domain.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopbridge_BaseTest
{
    [TestClass]
    public class ProductServiceTest
    {
        private readonly Mock<IRepository> _repositoryMock;
        private readonly ProductService _productService;
        private Product product;
        private List<Product> productList;        
        public ProductServiceTest()
        {
            _repositoryMock = new Mock<IRepository>();
            _productService = new ProductService(_repositoryMock.Object);
            product = new Product()
            {
                Product_Id = 1,
                ProductName = "earphones",
                Amount = 300,
                ComapnyName = "Boat",
                Description = "Wired earphones",
                IsActive = true
            };            
            productList = new List<Product>();
            productList.Add(product);

        }

        [TestMethod]
        public async Task GetProduct_ValidGetPRoduct_ReturnProduct()
        {
            int skip = 0, take = 10;
            _repositoryMock.Setup(d => d.GetAllProductAsync(skip, take)).ReturnsAsync(productList);
            var response = await _productService.GetAllProductAsync(skip, take);
            Assert.AreEqual(response,productList);
        }

        [TestMethod]
        public async Task AddProduct_ValidAddPRoduct_ReturnProduct()
        {
            _repositoryMock.Setup(d => d.AddProductAsync(product)).ReturnsAsync(product);
            _repositoryMock.Setup(d => d.GetProductByNameAndCompanyNameAsync(product.ProductName, product.ComapnyName));
            var response = await _productService.AddProductAsync(product);
            Assert.AreEqual(response,product);
        }

        [TestMethod]
        public async Task AddProduct_DuplicateAddPRoduct_ReturnProduct()
        {
             _repositoryMock.Setup(d => d.AddProductAsync(product)).ReturnsAsync(product);
            _repositoryMock.Setup(d => d.GetProductByNameAndCompanyNameAsync(product.ProductName,product.ComapnyName)).ReturnsAsync(product);           
            _ = await Assert.ThrowsExceptionAsync<Exception>(async () => await _productService.AddProductAsync(product));
        }

        [TestMethod]
        public async Task UpdateProduct_ValidUpdateProduct_ReturnProduct()
        {
            _repositoryMock.Setup(d => d.UpdateProductAsync(product)).ReturnsAsync(product);
            _repositoryMock.Setup(d => d.GetProductByNameAndCompanyNameAsync(product.ProductName, product.ComapnyName));
            var response = await _productService.UpdateProductAsync(product);
            Assert.AreEqual(response, product);
        }

        [TestMethod]
        public async Task UpdateProduct_DuplicateUpdatePRoduct_ReturnProduct()
        {
            _repositoryMock.Setup(d => d.AddProductAsync(product)).ReturnsAsync(product);
            _repositoryMock.Setup(d => d.GetProductByNameAndCompanyNameAsync(product.ProductName, product.ComapnyName)).ReturnsAsync(product);
            _ = await Assert.ThrowsExceptionAsync<Exception>(async () => await _productService.UpdateProductAsync(product));
        }

        [TestMethod]
        public async Task DeleteProduct_ValidDeleteProduct_ReturnTrue()
        {
            long id = 1;
            _repositoryMock.Setup(d => d.DeleteProductAsync(product)).ReturnsAsync(true);
            _repositoryMock.Setup(d => d.GetProductByIdAsync(id)).ReturnsAsync(product);
            var response = await _productService.DeleteProductAsync(id);
            Assert.AreEqual(response, true);
        }

        [TestMethod]
        public async Task DeleteProduct_NotFoundDeletePRoduct_ReturnFalse()
        {
            long id = 1;
            _repositoryMock.Setup(d => d.DeleteProductAsync(product));
            _repositoryMock.Setup(d => d.GetProductByIdAsync(id));
            _ = await Assert.ThrowsExceptionAsync<Exception>(async () => await _productService.DeleteProductAsync(id));
        }
    }
}
