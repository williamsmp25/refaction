using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace refactor_me.Models
{
    public interface IProductOptionRepository
    {
        void DeleteAllProductOptions(Guid productId);
        IEnumerable<ProductOption> ListAllProductOptions(Guid productId);
        ProductOption Get(Guid id);
        void Create(ProductOption productOptionToCreate);
        void Edit(ProductOption productOptionToEdit);
        void Delete(ProductOption productOptionToDelete);
    }
}
