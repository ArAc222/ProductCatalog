using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProductCatalog.Service.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        [Range(0, double.MaxValue)] 
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CategoryId { get; set; }

        public ProductCategory Category { get; set; } = null!;

    }
}
