using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shopbridge_base.Data;
using Shopbridge_base.Domain.Models;
using Shopbridge_base.Domain.Services.Interfaces;

namespace Shopbridge_base.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;
        public ProductsController(IProductService _productService)
        {
            this.productService = _productService;
        }
       
        [HttpGet("GetProducts/{skip}/{take}")]
        //This method will return list of the products by pagination.
        public async Task<ActionResult<Product>> GetProduct(int skip, int take)
        {
            return Ok(new ApiResponse<List<Product>>(await productService.GetAllProductAsync(skip, take), true, "The product list has been fetched successfully."));
        }

        [HttpPost("AddProduct")]
        //This action mathod will add a new product
        public async Task<ActionResult<Product>> AddProduct(Product product)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                return Ok(new ApiResponse<Product>(await productService.AddProductAsync(product), true, "The product has been added successfully."));
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Duplicate product found"))
                    return Ok(new ApiResponse<Product>(product, false, "The product alreay exists!"));
                throw;
            }
        }

        [HttpPut("UpdateProduct/{id}")]
        //This action mathod will update the product details
        public async Task<ActionResult<Product>> UpdateProduct(Product product)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                return Ok(new ApiResponse<Product>(await productService.UpdateProductAsync(product), true, "The product has been updated successfully."));
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Duplicate product found"))
                    return Ok(new ApiResponse<Product>(product, false, "The product alreay exists!"));
                throw;
            }
        }        

        [HttpDelete("DeleteProduct/{id}")]
        //This action mathod will delete product
        public async Task<IActionResult> DeleteProduct(long id)
        {
            try
            {
                return Ok(new ApiResponse<bool>(await productService.DeleteProductAsync(id), true, "The product has been deleted successfully."));
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Product not found"))
                    return NotFound(string.Format("The product not found for the product id {0}", id));
                throw ;
            }
            
        }

    }
}
