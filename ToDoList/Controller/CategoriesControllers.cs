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
            List<Category> allCategories = Category.GetAll();
            return View("Index", allCategories);
        }

        [HttpGet("/categories/{id}")]
        public ActionResult Details(int id)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Category selectedCategory = Category.Find(id);
            List<Item> categoryItems = selectedCategory.GetItems();
            model.Add("category", selectedCategory);
            model.Add("items", categoryItems);
            return View(model);
        }

        [HttpPost("/items")]
        public ActionResult CreateItem(int categoryId, string itemDescription,string duedate)
        {
          Dictionary<string, object> model = new Dictionary<string, object>();
          Category foundCategory = Category.Find(categoryId);
          Item newItem = new Item(itemDescription,duedate);
          newItem.Save();
          foundCategory.AddItem(newItem);
          List<Item> categoryItems = foundCategory.GetItems();
          model.Add("items", categoryItems);
          model.Add("category", foundCategory);
          return View("Details", model);
        }
    }
}