using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace refactor_me.Models
{
    public class ProductOptionRepository : IProductOptionRepository
    {
        private DatabaseEntities _entities = new DatabaseEntities();

        #region IProductOptionRepository Members
        public void DeleteAllProductOptions(Guid productId)
        {
            var productOptions = from p in _entities.ProductOptions
                                 where p.ProductId == productId
                                 select p;
            _entities.ProductOptions.RemoveRange(productOptions);
            _entities.SaveChanges();
        }

        public IEnumerable<ProductOption> ListAllProductOptions(Guid productId)
        {
            return from p in _entities.ProductOptions
                   where p.ProductId.Equals(productId)
                   select p;
        }

        public ProductOption Get(Guid id)
        {
            return (from p in _entities.ProductOptions
                    where p.Id == id
                    select p).FirstOrDefault();
        }

        public void Create(ProductOption productOptionToCreate)
        {
            _entities.ProductOptions.Add(productOptionToCreate);
            _entities.SaveChanges();
        }

        public void Edit(ProductOption productOptionToEdit)
        {
            var originalProductOption = Get(productOptionToEdit.Id);
            _entities.Entry(originalProductOption).CurrentValues.SetValues(productOptionToEdit);
            _entities.SaveChanges();
        }

        public void Delete(ProductOption productOptionToDelete)
        {
            var originalProductOption = Get(productOptionToDelete.Id);
            _entities.ProductOptions.Remove(originalProductOption);
            _entities.SaveChanges();
        }

        #endregion
    }
}