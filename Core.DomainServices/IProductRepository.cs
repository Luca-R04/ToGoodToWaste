using Core.Domain;

namespace Core.DomainServices
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }

        Product? CreateProduct(Product product);
        Product? ReadProduct(int productId);
        Product? UpdateProduct(Product product);
        Product? DeleteProduct(Product product);
        IEnumerable<Product>? GetList(int id);
    }
}