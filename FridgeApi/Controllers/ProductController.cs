using FridgeApi.Models;
using FridgeApi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace FridgeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductRepository products;

        public ProductController()
        {
            this.products = new ProductRepository();
        }

        [HttpGet("{id}")]
        public IEnumerable<ProductInFridge> AllProductsInFridge(Guid id)
        {
            return this.products.GetProductsInFridge(id);
        }

        [HttpGet("Empty/{id}")]
        public IEnumerable<Product> GetEmptyProducts(Guid id)
        {
            return this.products.GetEmptyProducts(id);
        }

        [HttpGet("All")]
        public IEnumerable<Product> AllProductType()
        {
            return this.products.GetProducts();
        }

        [HttpPost]
        public void AddProduct(ProductInFridge product)
        {
            this.products.AddProduct(product);
        }

        [HttpPut]
        public void UpdateProduct(ProductInFridge product)
        {
            this.products.UpdateProduct(product);
        }

        [HttpDelete("{id}")]
        public void RemoveProduct(Guid id)
        {
            this.products.RemoveProduct(id);
        }
    }
}
