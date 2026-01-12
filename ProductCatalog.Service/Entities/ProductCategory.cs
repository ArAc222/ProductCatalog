using ProductCatalog.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductCatalog.Service.Entities
{
    public class ProductCategory : IProductCategory
    {
        public int Id { get; set; }
        public required string Name { get; set; } 

        public string? Description { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();

    }
}
