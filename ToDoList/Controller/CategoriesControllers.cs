using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class CategoriesController : Controller
    {

        [HttpGet("/categories")]
        public ActionResult Index()
        {
            List<Category> allCategories = Category.GetAll();
            return View("Index",allCategories);
        }

        [HttpGet("/categories/new")]
        public ActionResult CreateForm()
        {
            return View();
        }

        [HttpPost("/categories")]
        public ActionResult Create(string categoryName)
        {
            Category newCategory = new Category(categoryName);
            newCategory.Save();
            List<Category> allCategories = Category.GetAll();
            return View("Index", allCategories);
        }

        [HttpGet("/categories/{categoryId}")]
        public ActionResult Details(int categoryId)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Category selectedCategory = Category.Find(categoryId);
            
            List<Item> categoryItems = selectedCategory.GetItems();
            model.Add("category", selectedCategory);
            model.Add("items", categoryItems);
            
            return View("Details",model);
        }
    }
}