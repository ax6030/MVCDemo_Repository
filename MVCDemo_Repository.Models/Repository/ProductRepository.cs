using MVCDemo_Repository.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCDemo_Repository.Models.Repository
{
    public class ProductRepository : GenericRepository<Products>, IProductRepository
    {
        public IEnumerable<Products> GetByCateogy(int categoryID)
        {
            return GetAll().Where(x => x.CategoryID == categoryID);
        }

        public Products GetByID(int productID)
        {
            return Get(x => x.ProductID == productID);
        }
    }
}