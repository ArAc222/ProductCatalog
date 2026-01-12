using ProductCatalog.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProductCatalog.Service.Entities
{
    public class Product : IProduct
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public double Price { get; set; }
        public int StockQuantity { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CategoryId { get; set; }

        public ProductCategory Category { get; set; } = null!;

    }
}
