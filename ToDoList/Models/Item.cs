using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using ToDoList.Models;

namespace ToDoList.Models
{
  public class Item
  {
    private int _id;
    private string _description;
    public DateTime dueDate { get; set; }

    public Item(string Description, DateTime newDueDate, int Id = 0)
    {
      _description = Description;
      _id = Id;
      dueDate = newDueDate;
    }
    public Item(string itemDescription, int Id = 0)
    {
      _description = itemDescription;
      _id = Id;
      dueDate = DateTime.MinValue;
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
      List<Item> allItems = new List<Item> { };
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT*FROM items ORDER BY date";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        int itemId = rdr.GetInt32(0);
        string itemDescription = rdr.GetString(1);
        DateTime itemDueDate = rdr.GetDateTime(2);
        Item newItem = new Item(itemDescription, itemDueDate, itemId);
        allItems.Add(newItem);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allItems;
    }
    public int GetId()
    {
      return _id;
    }
    public override bool Equals(System.Object otherItem)
    {
      if (!(otherItem is Item))
      {
        return false;
      }
      else
      {
        Item newItem = (Item) otherItem;
        bool idEquality = this.GetId() == newItem.GetId();
        bool descriptionEquality = this.GetDescription() == newItem.GetDescription();
        return (idEquality && descriptionEquality);
      }
    }
    public override int GetHashCode()
    {
      return this.GetDescription().GetHashCode();
    }

    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM items;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO `items` (`description`,`date`) VALUES (@ItemDescription,@ItemDueDate);";
      cmd.Parameters.AddWithValue("@ItemDescription", this._description);
      cmd.Parameters.AddWithValue("@ItemDueDate", this.dueDate);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static Item Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT*FROM items WHERE id = @thisId ORDER BY date;";

      cmd.Parameters.AddWithValue("@thisId", id);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int itemId = 0;
      string itemDescription = "";
      DateTime itemDueDate = DateTime.MinValue;

      rdr.Read();

      itemId = rdr.GetInt32(0);
      itemDescription = rdr.GetString(1);
      itemDueDate = rdr.GetDateTime(2);

      Item foundItem;
      if (rdr.IsDBNull(2))
      {
        foundItem = new Item(itemDescription);
      }
      else
      {
        itemDueDate = rdr.GetDateTime(2);
        foundItem = new Item(itemDescription, itemDueDate);
      }

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundItem;
    }

    public void AddCategory(Category newCategory)
    {

    }
    public List<Category> GetCategories()
    {
      List<Category> categories = new List<Category> {};
      return categories;
    }
  }
}