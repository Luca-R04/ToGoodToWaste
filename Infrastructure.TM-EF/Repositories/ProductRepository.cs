using Core.Domain;
using Core.DomainServices;
using Infrastructure.TGTW_EF.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Infrastructure.TGTW_EF.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ToGoodToWasteDbContext _dbContext;
        public IEnumerable<Product> Products => _dbContext.Products
            .Include("Packages")
            .ToList();

        public ProductRepository(ToGoodToWasteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Product? CreateProduct(Product product)
        {
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();

            return product;
        }

        public Product? ReadProduct(int productId)
        {
            return _dbContext.Products.FirstOrDefault(c => c.ProductId == productId);
        }

        public Product? UpdateProduct(Product product)
        {
            var entityToUpdate = _dbContext.Products.FirstOrDefault(c => c.ProductId == product.ProductId);
            if (entityToUpdate != null)
            {
                entityToUpdate.ProductId = product.ProductId;
                entityToUpdate.Name = product.Name;
                entityToUpdate.HasAlcohol = product.HasAlcohol;
                entityToUpdate.Image = product.Image;

                _dbContext.SaveChanges();
            }

            return entityToUpdate;
        }
        public Product? DeleteProduct(Product product)
        {
            var entityToRemove = _dbContext.Products.FirstOrDefault(c => c.ProductId == product.ProductId);
            if (entityToRemove != null)
            {
                _dbContext.Products.Remove(entityToRemove);
                _dbContext.SaveChanges();
            }

            return entityToRemove;
        }

        public IEnumerable<Product>? GetList(int id)
        {
            IEnumerable<Product>? selectedProducts;
            if (id == 1)
            {
                selectedProducts = Products.Where(p => p.ProductId <= 3).ToList();
                return selectedProducts;
            }
            else if (id == 2)
            {
                selectedProducts = Products.Where(p => p.ProductId > 3 && p.ProductId <= 5).ToList();
                return selectedProducts;
            }
            else
            {
                selectedProducts = Products.Where(p => p.ProductId > 5 && p.ProductId <= 7).ToList();
                return selectedProducts;
            }
        }
    }
}
