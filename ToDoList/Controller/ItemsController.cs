using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class ItemsController : Controller
    {

        [HttpGet("/items")]
        public ActionResult Index()
        {
            List<Item> allItems = Item.GetAll();
            return View(allItems);
        }

        [HttpGet("/categories/{categoryId}/items/new")]
        public ActionResult CreateForm(int categoryId)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Category category = Category.Find(categoryId);
            return View(category);
        }

        [HttpPost("/items/delete")]
        public ActionResult DeleteAll()
        {
            Item.ClearAll();
            return View();
        }

        [HttpGet("/categories/{categoryId}/items/{itemId}")]
        public ActionResult Details(int categoryId, int itemId)
        {
            Item item = Item.Find(itemId);
            Dictionary<string, object> model = new Dictionary<string, object>();
            Category category = Category.Find(categoryId);
            model.Add("item", item);
            model.Add("category", category);
            return View(model);
        }
          [HttpPost("/items")]
        public ActionResult CreateItem(int categoryId, string itemDescription,string itemDue) //pulling from form.
        {
          Dictionary<string, object> model = new Dictionary<string, object>();
          Category foundCategory = Category.Find(categoryId);
          Item newItem = new Item(itemDescription,itemDue,categoryId);
          newItem.Save();
        //   foundCategory.AddItem(newItem);
        //   List<Item> categoryItems = foundCategory.GetItems();
          model.Add("items", newItem);
          model.Add("category", foundCategory);
          return View("Details", model);
        }
    }
}