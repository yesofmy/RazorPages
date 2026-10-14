using ProductCatalog.Models;

namespace ProductCatalog.Services
{
    public interface IProductService
    {
        List<Product> GetProducts();
    }
}