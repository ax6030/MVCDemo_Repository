﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCDemo_Repository.Models;
using MVCDemo_Repository.Models.Interface;
using MVCDemo_Repository.Models.Repository;
using MVCDemo_Repository.Service;
using MVCDemo_Repository.Service.Interface;

namespace MVCDemo_Repository.Web.Controllers
{
    public class ProductController : Controller
    {
        private IProductService productService;
        private ICategoryService categoryService;

        public IEnumerable<Categories> Categories
        {
            get
            {
                return categoryService.GetAll();
            }
        }

        public ProductController()
        {
            this.productService = new ProductService();
            this.categoryService = new CategoryService();
        }

        public ActionResult Index(string category = "all")
        {
            int categoryID = 1;

            int selectedCategoryID = 0;
            ViewBag.CategorySelectList = int.TryParse(category, out selectedCategoryID)
                ? this.CategorySelectList(selectedCategoryID.ToString())
                : this.CategorySelectList("all");

            var result = category.Equals("all", StringComparison.OrdinalIgnoreCase)
                ? productService.GetAll()
                : productService.GetByCategory(categoryID);

            var products = result.OrderByDescending(x => x.ProductID).ToList();

            ViewBag.Category = category;

            return View(products);
        }
        

        [HttpPost]
        public ActionResult ProductsOfCategory(string category)
        {
            return RedirectToAction("Index", new { category = category });
        }

        public List<SelectListItem> CategorySelectList(string selectedValue = "all")
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem()
            {
                Text = "All Category",
                Value = "all",
                Selected = selectedValue.Equals("all", StringComparison.OrdinalIgnoreCase)
            });

            var categories = categoryService.GetAll().OrderBy(x => x.CategoryID);

            foreach (var c in categories)
            {
                items.Add(new SelectListItem()
                {
                    Text = c.CategoryName,
                    Value = c.CategoryID.ToString(),
                    Selected = selectedValue.Equals(c.CategoryID.ToString())
                });
            }
            
            return items;
        }

        public ActionResult Details(int? id, string category)
        {
            if (!id.HasValue) return RedirectToAction("index");

            Products product = productService.GetByID(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }

            ViewBag.Category = string.IsNullOrWhiteSpace(category) ? "all" : category;

            return View(product);
        }

        public ActionResult Create(string category)
        {
            ViewBag.CategoryID = new SelectList(this.Categories, "CategoryID", "CategoryName");
            ViewBag.Category = string.IsNullOrWhiteSpace(category) ? "all" : category;

            return View();
        }

        [HttpPost]
        public ActionResult Create(Products products, string category)
        {
            if (ModelState.IsValid)
            {
                this.productService.Create(products);
                return RedirectToAction("Index", new { category = category });
            }

            ViewBag.CategoryID = new SelectList(this.Categories, "CategoryID", "CategoryName", products.CategoryID);

            return View(products);
        }

        public ActionResult Edit(int? id, string category)
        {
            if (!id.HasValue) return RedirectToAction("index");

            Products product = this.productService.GetByID(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }

            ViewBag.CategoryID = new SelectList(this.Categories, "CategoryID", "CategoryName", product.CategoryID);
            ViewBag.Category = string.IsNullOrWhiteSpace(category) ? "all" : category;

            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Products products, string category)
        {
            if (ModelState.IsValid)
            {
                this.productService.Update(products);
                return RedirectToAction("Index", new { category = category });
            }

            ViewBag.CategoryID = new SelectList(this.Categories, "CategoryID", "CategoryName", products.CategoryID);

            return View(products);
        }

        public ActionResult Delete(int? id, string category)
        {
            if (!id.HasValue) return RedirectToAction("index");

            Products product = this.productService.GetByID(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }

            ViewBag.Category = string.IsNullOrWhiteSpace(category) ? "all" : category;

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id, string category)
        {
            this.productService.Delete(id);

            return RedirectToAction("Index", new { category = category });
        }
    }
}
