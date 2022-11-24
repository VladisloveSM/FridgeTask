using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using FridgeApi.Models;
using System.Data.SqlClient;

namespace FridgeApi.Repository
{
    public class FridgeRepository
    {
        private readonly string connectionString = Startup.ConfString.GetConnectionString("DefaultConnection");

        public IEnumerable<Fridge> GetAllFridges()
        {
            IEnumerable<Fridge> fridges;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                fridges = db.Query<Fridge>(@"SELECT FRIDGE.Id, FRIDGE.Name, FRIDGE.Owner_Name, Fridge_Model.Name AS Model FROM FRIDGE 
                    INNER JOIN Fridge_Model ON Fridge_Model.Id = FRIDGE.Model_Id");
            }

            return fridges;
        }

        public string NameFridge(Guid id)
        {
            IEnumerable<string> result;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                result = db.Query<string>(@"SELECT NAME FROM FRIDGE WHERE Id = @id", new { id });
            }

            return result.ElementAt(0);
        }

        public void DeleteFridge(Guid id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                db.Execute(@"DELETE FROM Fridge_Products WHERE Fridge_id = @id", new { id });
                db.Execute(@"DELETE FROM FRIDGE WHERE Id = @id", new { id });
            }
        }

        public IEnumerable<FridgeModel> GetAllModels()
        {
            IEnumerable<FridgeModel> models;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                models = db.Query<FridgeModel>("SELECT * FROM Fridge_Model");
            }

            return models;
        }

        public void AddFridge(Fridge fridge)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                db.Execute("INSERT INTO[Fridge]([Id],[Name],[Owner_Name],[Model_id]) VALUES(@Id,@Name,@Owner_Name,@Model_id)", fridge);
            }
        }

        public void UpdateFridge(Fridge fridge)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                db.Execute("UPDATE FRIDGE SET NAME = @Name, Owner_Name = @Owner_Name, Model_id = @Model_id WHERE Id = @Id", fridge);
            }
        }
    }
}
