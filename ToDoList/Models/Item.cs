using System;
using System.Collections.Generic;
using ToDoList.Models;
using System.Linq;

namespace ToDoList.Models
{
  public class Item
  {
    private string _description;
    private static List<Item> _instances = new List<Item> {};

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

// class Program
// {
//     public static void Main()
//     {

//         Console.WriteLine("Welcome to the To Do List");
//         Console.WriteLine("Would you like to add an item to your list or view your list? (Add/View/Exit)");
//         string userAction = Console.ReadLine().ToLower();
//         while (userAction == "add")
//         {
//             Console.WriteLine("Please enter the description for the new item.");
//             string description = Console.ReadLine();
//             Item myItem = new Item(description);
//             myItem.Save();
//             Console.WriteLine("'{0}' has been added to your list. Would you like to add an item to your list or view your list? (Add/View/Exit)", myItem.GetDescription());
//             userAction = Console.ReadLine().ToLower();
//         }
//         if (userAction == "view")
//         {
//             List<Item> instances = Item.GetAll();
//             if (!instances.Any())
//             {
//                 Console.WriteLine("You have no item to view! Would you like to add an item? (Add/Exit)");
//                 userAction = Console.ReadLine().ToLower();
//             }
//             else
//             {
//                 Console.WriteLine("Your To Do List: ");
//                 foreach (Item thisItem in instances)
//                 {
//                     Console.WriteLine(thisItem.GetDescription());
//                 }
//             }

//         }
//         else if (userAction == "exit")
//         {
//             Console.WriteLine("Goodbye!");
//         }
//     }
// }