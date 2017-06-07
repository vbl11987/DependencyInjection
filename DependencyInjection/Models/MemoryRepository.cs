using System.Collections.Generic;

namespace DependencyInjection.Models
{
    public class MemoryRepository : IRepository
    {
        private Dictionary<string, Product> products;

        public MemoryRepository(){
            products = new Dictionary<string, Product>();

            new List<Product> {
                new Product { Name = "Bate", Price = 15.50M },
                new Product { Name = "Helmet", Price = 22.15M },
                new Product { Name = "Soccer ball", Price = 16.50M }
            }.ForEach(p => AddProduct(p));
        }

        public IEnumerable<Product> Products => products.Values;

        public Product this[string name] => products[name];

        public void AddProduct(Product product) => products[product.Name] = product;

        public void DeleteProduct(Product product) => products.Remove(product.Name);  
    }
}