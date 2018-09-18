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
    public string dueDate { get; set; }

    public Item(string Description, int Id = 0)
    {
      _description = Description;
      _id = Id;
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
      cmd.CommandText = @"SELECT*FROM items";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        int itemId = rdr.GetInt32(0);
        string itemDescription = rdr.GetString(1);
        Item newItem = new Item(itemDescription, itemId);
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
    public override bool Equals(System.Object otherItem)
    {
      if (!(otherItem is Item))
      {
        return false;
      }
      else
      {
        Item newItem = (Item) otherItem;
        bool descriptionEquality = (this.GetDescription() == newItem.GetDescription());
        return (descriptionEquality);
      }
    }
    public override int GetHashCode()
    {
      return this.GetDescription().GetHashCode();
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      Console.WriteLine("Connection is open");
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO `items` (`description`) VALUES (@ItemDescription);";
      MySqlParameter description = new MySqlParameter();
      description.ParameterName = "@ItemDescription";
      description.Value = this._description;
      cmd.Parameters.Add(description);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
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
      cmd.CommandText = @"SELECT*FROM items WHERE ID = @thisId;";
      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int itemId = 0;
      string itemDescription = "";

      while (rdr.Read())
      {
        itemId = rdr.GetInt32(0);
        itemDescription = rdr.GetString(1);
      }
      Item foundItem = new Item(itemDescription, itemId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundItem;
    }
  }
}