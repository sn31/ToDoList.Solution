// using System;
// using ToDoList.Models;
// using System.Collections.Generic;


// class Program
// {
//     public static void Main()
//     {
        
//         Console.WriteLine("Welcome to the To do");
//         Console.WriteLine("Would you like to add an item to your list or view your list? (Add/View)");
//         string userAction = Console.ReadLine().ToLower();
//         if (userAction == "add")
//         {
//             Console.WriteLine("Please enter the description for the new item.");
//             string description = Console.ReadLine();
//             Item myItem = new Item(description);
//             myItem.Save();
//             Console.WriteLine("'{}' has been added to your list. Would you like to add an item to your list or view your list? (Add/View)",myItem.GetDescription());
//         }
//         else if (userAction == "view")
//         {
//             List<Item> instances = Item.GetAll();
//         }
//     }
// }

