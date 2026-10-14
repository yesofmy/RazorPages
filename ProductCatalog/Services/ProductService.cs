using ProductCatalog.Models;

namespace ProductCatalog.Services
{
    public class ProductService : IProductService
    {
        public List<Product> GetProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Wireless Headphones",
                    Price = 89.99m
                },

                new Product
                {
                    Id = 2,
                    Name = "Mechanical Keyboard",
                    Price = 129.99m
                },

                new Product
                {
                    Id = 3,
                    Name = "Gaming Mouse",
                    Price = 59.99m
                },

                new Product
                {
                    Id = 4,
                    Name = "4K Monitor",
                    Price = 349.99m
                }
            };
        }
    }
}