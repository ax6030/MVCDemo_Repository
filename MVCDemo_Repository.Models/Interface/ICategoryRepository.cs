using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCDemo_Repository.Models.Interface
{
    public interface ICategoryRepository : IRepository<Categories>
    {
        Categories GetByID(int categoryID);
    }
}
