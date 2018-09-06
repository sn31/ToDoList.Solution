using System;
using System.Collections.Generic;
using ToDoList.Models;

namespace ToDoList.Models
{
    public class Item
    {
        private string _description;
        private static List<Item> _instances = new List<Item>{};
        public Item (string description)
        {
            _description = description;
        }
        public string GetDescription()
        {
            return _description;
        }
        public void SetDescription(string newDescription)
        {
            _description = newDescription;
        }
        public static List<Item> GetAll()
        {
            return _instances;
        }
        public void Save()
        {
            _instances.Add(this);
        }
        public static void ClearAll()
        {
            _instances.Clear();
        }
    }
}

class Program
{
    public static void Main()
    {
        
        Console.WriteLine("Welcome to the To do");
        Console.WriteLine("Would you like to add an item to your list or view your list? (Add/View)");
        string userAction = Console.ReadLine().ToLower();
        while (userAction == "add")
        {
            Console.WriteLine("Please enter the description for the new item.");
            string description = Console.ReadLine();
            Item myItem = new Item(description);
            myItem.Save();
            Console.WriteLine("'{0}' has been added to your list. Would you like to add an item to your list or view your list? (Add/View)",myItem.GetDescription());
            userAction = Console.ReadLine().ToLower();
        }
        if (userAction == "view")
        {
            Console.WriteLine("Your To Do List: ");
            List<Item> instances = Item.GetAll();
            foreach (Item thisItem in instances)
            {
                Console.WriteLine(thisItem.GetDescription());
            }
        }
    }
}