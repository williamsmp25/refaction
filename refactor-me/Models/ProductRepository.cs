using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace refactor_me.Models
{
    public class ProductRepository : IProductRepository
    {
        private DatabaseEntities _entities = new DatabaseEntities();

        #region IproductRepository Members
        public IEnumerable<Product> List()
        {
            return _entities.Products.ToList();
        }

        public IEnumerable<Product> ListByName(string name)
        {
            return from p in _entities.Products
                   where p.Name.Contains(name)
                   select p;
        }

        public Product Get(Guid id)
        {
            return (from p in _entities.Products
                    where p.Id == id
                    select p).FirstOrDefault();
        }

        public void Create(Product productToCreate)
        {
            _entities.Products.Add(productToCreate);
            _entities.SaveChanges();
        }

        public void Edit(Product productToEdit)
        {
            var originalProduct = Get(productToEdit.Id);
            _entities.Entry(originalProduct).CurrentValues.SetValues(productToEdit);
            _entities.SaveChanges();
        }

        public void Delete(Product productToDelete)
        {
            var originalProduct = Get(productToDelete.Id);
            _entities.Products.Remove(originalProduct);
            _entities.SaveChanges();
        }

        #endregion
    }
}