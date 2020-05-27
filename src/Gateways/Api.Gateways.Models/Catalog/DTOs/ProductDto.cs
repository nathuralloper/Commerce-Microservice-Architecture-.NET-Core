using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Gateways.Models.Catalog.DTOs
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public int Name { get; set; }
        public int Description { get; set; }
        public decimal Price { get; set; }
    }
}
