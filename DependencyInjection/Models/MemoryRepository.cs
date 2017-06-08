using System.Collections.Generic;

namespace DependencyInjection.Models
{
    public class MemoryRepository : IRepository
    {
        private IModelStorage storage;

        private string guid = System.Guid.NewGuid().ToString();


        //private Dictionary<string, Product> products;

        public MemoryRepository(IModelStorage modelStore){
            storage = modelStore;

            new List<Product> {
                new Product { Name = "Bate", Price = 15.50M },
                new Product { Name = "Helmet", Price = 22.15M },
                new Product { Name = "Soccer ball", Price = 16.50M }
            }.ForEach(p => AddProduct(p));
        }

        public IEnumerable<Product> Products => storage.Items;

        public Product this[string name] => storage[name];

        public void AddProduct(Product product) => storage[product.Name] = product;

        public void DeleteProduct(Product product) => storage.RemoveItem(product.Name);

        public override string ToString() {
            return guid;
        }  
    }
}