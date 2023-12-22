using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVCDemo_Repository.Models;
using MVCDemo_Repository.Models.Interface;
using MVCDemo_Repository.Models.Repository;
using MVCDemo_Repository.Service.Interface;


namespace MVCDemo_Repository.Service
{
    public class CategoryService : ICategoryService
    {
        private IRepository<Categories> repository;

        public CategoryService() 
        {
            repository = new GenericRepository<Categories>();
        }
        public IResult Create(Categories instance)
        {
            if(instance == null)
            {
                throw new ArgumentNullException();
            }
            IResult result = new Result(false);
            try
            {
                repository.Create(instance);
                result.Success = true;
            }
            catch(Exception ex)
            {
                result.Exception = ex;
            }
            return result;
        }

        public IResult Delete(int categoryID)
        {
            Result result = new Result(false);
            if (!this.IsExists(categoryID))
            {
                result.Message = "找不到資料";
            }
            try
            {
                var instance = GetByID(categoryID);
                repository.Delete(instance);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Exception = ex;
            }
            return result;
        }

        public IEnumerable<Categories> GetAll()
        {
            return repository.GetAll();
        }

        public Categories GetByID(int categoryID)
        {
            return repository.Get(x => x.CategoryID == categoryID);
        }

        public bool IsExists(int categoryID)
        {
            return repository.GetAll().Any(x => x.CategoryID == categoryID);
        }

        public IResult Update(Categories instance)
        {
            if(instance == null)
            {
                throw new ArgumentNullException();
            }
            Result result = new Result(false);
            try
            {
                repository.Update(instance);
                result.Success = true;
            }
            catch(Exception ex)
            {
                result.Exception = ex;
            }
            return result;
        }
    }
}
