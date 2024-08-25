﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly string _connectionString;

        public ProductController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet("products")]
        public IActionResult GetProducts()
        {
            List<Product> products = GetProductsFromDatabase();

            // Конвертируем список товаров в CSV файл
            StringBuilder csv = new StringBuilder();
            csv.AppendLine("Id,Name,Price");
            foreach (var product in products)
            {
                csv.AppendLine($"{product.Id},{product.Name},{product.Price}");
            }

            return File(Encoding.UTF8.GetBytes(csv.ToString()), "text/csv", "products.csv");
        }

        private List<Product> GetProductsFromDatabase()
        {
             _connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=usersdb;Integrated Security=True";
        }

        [HttpGet("statistics")]
        public IActionResult GetStatistics()
        {
           
            string cacheStats = System.IO.File.ReadAllText("cache_statistics.txt");
            return Content(cacheStats, "text/plain");
        }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
