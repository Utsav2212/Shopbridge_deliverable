using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shopbridge_base.Controllers;
using Shopbridge_base.Domain.Services.Interfaces;
using Moq;
using Shopbridge_base.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Shopbridge_BaseTest
{
    [TestClass]
    public class ProductsControllerTests
    {
        private readonly Mock<IProductService> _productServiceMock;
        private readonly ProductsController _productsController;
        private Product product;
        private List<Product> productList;
        public ProductsControllerTests()
        {
            _productServiceMock = new Mock<IProductService>();
            _productsController = new ProductsController(_productServiceMock.Object);
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
            int skip=0,take=10;
            _productServiceMock.Setup(d => d.GetAllProductAsync(skip,take)).ReturnsAsync(productList);
            ActionResult<Product> response = await _productsController.GetProduct(skip, take);
            Assert.AreEqual(200, (response.Result as ObjectResult)?.StatusCode);            
        }

        [TestMethod]
        public async Task AddProduct_ValidAddPRoduct_ReturnProduct()
        {
            _productServiceMock.Setup(d => d.AddProductAsync(product)).ReturnsAsync(product);
            var response = await _productsController.AddProduct(product);
            Assert.AreEqual(200, (response.Result as ObjectResult)?.StatusCode);
        }

        [TestMethod]
        public async Task UpdateProduct_ValidUpdateProduct_ReturnProduct()
        {
             _productServiceMock.Setup(d => d.UpdateProductAsync(product)).ReturnsAsync(product);
            var response = await _productsController.UpdateProduct(product);
            Assert.AreEqual(200, (response.Result as ObjectResult)?.StatusCode);
        }

        [TestMethod]
        public async Task DeleteProduct_ValidDeleteProduct_ReturnTrueStatus()
        {
            long id = 1;
            _productServiceMock.Setup(d => d.DeleteProductAsync(id)).ReturnsAsync(true);
            var response = await _productsController.AddProduct(product);
            Assert.AreEqual(200, (response.Result as ObjectResult)?.StatusCode);
        }       
    }
}
