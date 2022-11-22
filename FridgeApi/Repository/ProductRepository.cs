using Dapper;
using FridgeApi.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FridgeApi.Repository
{
    public class ProductRepository
    {
        private readonly string connectionString = Startup.ConfString.GetConnectionString("DefaultConnection");

        public IEnumerable<Product> GetProducts()
        {
            IEnumerable<Product> products;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                products = db.Query<Product>("SELECT * FROM PRODUCTS");
            }

            return products;
        }

        public IEnumerable<ProductInFridge> GetProductsInFridge(Guid id)
        {
            IEnumerable<ProductInFridge> products;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                products = db.Query<ProductInFridge>(
                    @"SELECT Fridge_Products.Id, Fridge_Products.Quantity, Products.Name, Fridge_Products.Fridge_id, Products.Id as Product_id 
	                  FROM Fridge 
	                  INNER JOIN Fridge_Products ON Fridge_Products.Fridge_id = Fridge.Id
	                  INNER JOIN Products ON Products.Id = Fridge_Products.Product_id
	                  WHERE Fridge.Id = @id", new { id });
            }

            return products;
        }

        public void AddProduct(ProductInFridge product)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                db.Execute("INSERT INTO[Fridge_Products]([Id],[Quantity],[Product_id],[Fridge_id]) VALUES(@Id,@Quantity,@Product_id,@Fridge_id)", product);
            }
        }

        public void DeleteProduct(Guid idDeleted)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                db.Execute("DELETE FROM Fridge_Products WHERE Id = @idDeleted", new { idDeleted });
            }
        }

        public void UpdateProduct(ProductInFridge product)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                db.Execute("UPDATE Fridge_Products SET Quantity = @Quantity, Product_id = @Product_id WHERE Id = @Id", product);
            }
        }

        public IEnumerable<Product> GetEmptyProducts(Guid fridgeId)
        {
            IEnumerable<Product> result;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                result = db.Query<Product>("EXEC FindEmptyProducts @fridgeId", new { fridgeId });
            }

            return result;
        }

        public void RemoveProduct(Guid id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                db.Execute("DELETE FROM Fridge_Products WHERE Id = @id", new { id });
            }
        }
    }
}
