using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace refactor_me.Models
{
    public interface IProductRepository
    {
        IEnumerable<Product> List();
        IEnumerable<Product> ListByName(string name);
        Product Get(Guid id);
        void Create(Product productToCreate);
        void Edit(Product productToEdit);
        void Delete(Product productToDelete);
    }
}
