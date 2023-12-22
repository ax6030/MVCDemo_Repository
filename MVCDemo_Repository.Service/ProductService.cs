using MVCDemo_Repository.Models;
using MVCDemo_Repository.Models.Interface;
using MVCDemo_Repository.Models.Repository;
using MVCDemo_Repository.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCDemo_Repository.Service
{
    public class ProductService : IProductService
    {
        private IRepository<Products> repository;
        public ProductService()
        {
            repository = new GenericRepository<Products>();
        }
        public IResult Create(Products instance)
        {
            if (instance == null) 
                throw new ArgumentNullException();
            Result result = new Result(false);
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

        public IResult Delete(int productID)
        {
            Result result = new Result(false);
            if (!this.IsExists(productID))
            {
                result.Message = "找不到資料";
            }
            try
            {
                var instance = this.GetByID(productID);
                repository.Delete(instance);
                result.Success = true;
            }
            catch(Exception ex)
            {
                result.Exception = ex;
            }
            return result;
        }

        public IEnumerable<Products> GetAll()
        {
            return repository.GetAll();
        }

        public IEnumerable<Products> GetByCategory(int categoryID)
        {
            return repository.GetAll().Where(x => x.CategoryID == categoryID);
        }

        public Products GetByID(int productID)
        {
            return repository.Get(x => x.ProductID == productID);
        }

        public bool IsExists(int productID)
        {
            return repository.GetAll().Any(x => x.ProductID == productID);
        }

        public IResult Update(Products instance)
        {
            if(instance == null)
                throw new ArgumentNullException();
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
