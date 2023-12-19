using MVCDemo_Repository.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCDemo_Repository.Models.Repository
{
    public class CategoryRepository : GenericRepository<Categories>, ICategoryRepository
    {
        public Categories GetByID(int categoryID)
        {
            return Get(x => x.CategoryID == categoryID);
        }
    }
}