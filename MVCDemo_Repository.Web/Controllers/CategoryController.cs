using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCDemo_Repository.Models;
using MVCDemo_Repository.Service;
using MVCDemo_Repository.Service.Interface;

namespace MVCDemo_Repository.Web.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryService _categoryService;

        public CategoryController()
        {
            this._categoryService = new CategoryService();
        }


        public ActionResult Index()
        {
            var categories = this._categoryService.GetAll()
                .OrderByDescending(x => x.CategoryID)
                .ToList();

            return View(categories);
        }
        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("index");
            }
            else
            {
                var category = this._categoryService.GetByID(id.Value);
                return View(category);

            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Categories category)
        {
            if (category != null && ModelState.IsValid)
            {
                this._categoryService.Create(category);
                return RedirectToAction("index");
            }
            else
            {
                return View(category);
            }
        }

        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("index");
            }
            else
            {
                var category = this._categoryService.GetByID(id.Value);
                return View(category);
            }
        }

        [HttpPost]
        public ActionResult Edit(Categories category)
        {
            if (category != null && ModelState.IsValid)
            {
                this._categoryService.Update(category);
                return View(category);
            }
            else
            {
                return RedirectToAction("index");
            }
        }

        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("index");
            }
            else
            {
                var category = this._categoryService.GetByID(id.Value);
                return View(category);
            }
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {   
            try
            {
                this._categoryService.Delete(id);
            }
            catch (DataException)
            {
                return RedirectToAction("Delete", new { id = id });
            }
            return RedirectToAction("index");
        }
    }
}
