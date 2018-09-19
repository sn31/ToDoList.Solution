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
        public static void Clear()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM categories;";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

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
            cmd.CommandText = @"SELECT*FROM categories WHERE categoryId = @thisId;";
            MySqlParameter thisId = new MySqlParameter();
            thisId.ParameterName = "@thisId";
            thisId.Value = Id;
            cmd.Parameters.Add(thisId);

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            rdr.Read();
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
            cmd.CommandText = @"SELECT*FROM items WHERE categoryId =@thisId ORDER BY date;";
            MySqlParameter thisId = new MySqlParameter();
            thisId.ParameterName = "@thisId";
            thisId.Value = this._id;
            cmd.Parameters.Add(thisId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int itemId = 0;
            string itemDescription = "";
            string itemDueDate = "";

            while (rdr.Read())
            {
                itemId = rdr.GetInt32(0);
                itemDescription = rdr.GetString(1);

                Item foundItem;
                if (rdr.IsDBNull(2))
                {
                    //if current row's duedate column's value is null
                    foundItem = new Item(itemDescription, this._id);
                }
                else
                {
                    //if current row's duedate column's value is not null and has specific value
                    itemDueDate = rdr.GetDateTime(2).ToString();
                    foundItem = new Item(itemDescription, itemDueDate, this._id);
                }
                _foundItems.Add(foundItem);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return _foundItems;
        }

    }
}