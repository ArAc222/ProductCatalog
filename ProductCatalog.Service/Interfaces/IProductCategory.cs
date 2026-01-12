using ProductCatalog.Service.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductCatalog.Service.Interfaces
{
    public interface IProductCategory
    {
        public int Id { get; set; }
        string Name { get; set; }
        string? Description { get; set; }
        ICollection<Product> Products { get; set; }
    }
}
