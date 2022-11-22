using FridgeApi.Models;
using FridgeApi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FridgeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FridgeController : ControllerBase
    {
        private readonly FridgeRepository fridges;
        private readonly ProductRepository products;

        public FridgeController()
        {
            this.fridges = new FridgeRepository();
            this.products = new ProductRepository();
        }

        [HttpGet]
        public IEnumerable<Fridge> FridgesList()
        {
            return this.fridges.GetAllFridges();
        }

        [HttpGet("Name/{id}")]
        public string GetFridgeName(Guid id)
        {
            return this.fridges.NameFridge(id);
        }

        [HttpDelete("{id}")]
        public void DeleteFridge(Guid id)
        {
            this.fridges.DeleteFridge(id);
        }

        [HttpPost]
        public void CreateFridge(Fridge fridge)
        {
            this.fridges.AddFridge(fridge);
        }

        [HttpPut]
        public void UpdateFridge(Fridge fridge)
        {
            this.fridges.UpdateFridge(fridge);
        }

        [HttpGet("Models")]
        public IEnumerable<FridgeModel> GetAllModels()
        {
            return this.fridges.GetAllModels();
        }
    }
}
