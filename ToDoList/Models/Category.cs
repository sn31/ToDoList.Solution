using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace ToDoList.Models
{
    public class Category
    {
        private List<Item> _foundItems = new List<Item> { };
        private string _name;
        private int _id;

        public Category(string categoryName, int Id = 0)
        {
            _name = categoryName;
            _id = Id;
        }

        public string GetName()
        {
            return _name;
        }
        public int GetId()
        {
            return _id;
        }
        public static List<Category> GetAll()
        {
            List<Category> allCategories = new List<Category> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT*FROM categories";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int categoryId = rdr.GetInt32(0);
                string categoryDescription = rdr.GetString(1);
                Category newCategory = new Category(categoryDescription, categoryId);
                allCategories.Add(newCategory);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allCategories;
        }
        public override bool Equals(System.Object otherCategory)
        {
            if (!(otherCategory is Category))
            {
                return false;
            }
            else
            {
                Category newCategory = (Category) otherCategory;
                bool descriptionEquality = (this.GetName() == newCategory.GetName());
                return (descriptionEquality);
            }
        }
        public override int GetHashCode()
        {
            return this.GetName().GetHashCode();
        }
        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO `categories` (`name`) VALUES (@categoryName);";
            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@categoryName";
            name.Value = this._name;
            cmd.Parameters.Add(name);

            cmd.ExecuteNonQuery();

            _id = (int) cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public static Category Find(int Id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM `categories` WHERE `categoryId` = @thisId;";
            cmd.Parameters.AddWithValue("@thisId", Id);

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            int categoryId = 0;
            string categoryName = "";
            while (rdr.Read())
            {
                categoryId = rdr.GetInt32(0);
                categoryName = rdr.GetString(1);
            }
            Category foundCategory = new Category(categoryName, categoryId);

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return foundCategory;
        }
        public List<Item> GetItems()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT items.* FROM categories
                JOIN categories_items ON (categories.categoryId = categories_items.category_id)
                JOIN items ON (categories_items.item_id = items.id)
                WHERE categories.categoryId = @CategoryId;";
            cmd.Parameters.AddWithValue("@CategoryId", _id);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;

            List<Item> items = new List<Item> { };
            while (rdr.Read())
            {
                int itemId = rdr.GetInt32(0);
                string itemDescription = rdr.GetString(1);
                Item newItem = new Item(itemDescription, itemId);
                items.Add(newItem);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return items;
        }
    
    public static void ClearAll()
    {
        MySqlConnection conn = DB.Connection();
        conn.Open();

        MySqlCommand cmd = conn.CreateCommand();
        cmd.CommandText = @"DELETE FROM categories;";
        cmd.ExecuteNonQuery();
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
    }
    public void AddItem(Item newItem)
    {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand();
        cmd.CommandText = @"INSERT INTO categories_items (category_id, item_id) VALUES (@CategoryId, @ItemId);";
        cmd.Parameters.AddWithValue("@CategoryId", _id);
        cmd.Parameters.AddWithValue("@ItemId", newItem.GetId());
        cmd.ExecuteNonQuery();
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
    }

    public void Delete()
    {
        MySqlConnection conn = DB.Connection();
        conn.Open();

        MySqlCommand cmd = new MySqlCommand("DELETE FROM categories WHERE categoryId=@CategoryId; DELETE FROM categories_items WHERE category_id = @CategoryId;", conn);
        cmd.Parameters.AddWithValue("@CategoryId", this.GetId());
        cmd.ExecuteNonQuery();
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
    }

}
}