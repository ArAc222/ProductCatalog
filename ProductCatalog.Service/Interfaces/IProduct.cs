using ProductCatalog.Service.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductCatalog.Service.Interfaces
{
    public interface IProduct
    {
        int Id { get; set; }
        string Name { get; set; }
        double Price { get; set; }
        int StockQuantity { get; set; }
        bool IsActive { get; set; }
        DateTime CreatedAt { get; set; }
        int CategoryId { get; set; }
        ProductCategory Category { get; set; }

    }
}
