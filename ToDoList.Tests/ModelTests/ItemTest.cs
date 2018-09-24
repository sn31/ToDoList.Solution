using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoList.Models;

namespace ToDoList.Tests
{

    [TestClass]
    public class ItemTest : IDisposable
    {
        public ItemTest()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=todo_test;";
        }
        public void Dispose()
        {
            Item.ClearAll();
            Category.ClearAll();
        }

        [TestMethod]
        public void GetAll_DatabaseEmptyAtFirst_0()
        {
            //Arrange, Act
            int result = Item.GetAll().Count;

            //Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Equals_TrueForSameDescription_Item()
        {
            //Arrange, Act
            Item firstItem = new Item("Mow the lawn");
            Item secondItem = new Item("Mow the lawn");

            //Assert
            Assert.AreEqual(firstItem, secondItem);
        }

        [TestMethod]
        public void Save_ItemSavesToDatabase_ItemList()
        {
            //Arrange
            Item testItem = new Item("Mow the lawn");
            testItem.Save();

            //Act
            List<Item> result = Item.GetAll();
            List<Item> testList = new List<Item> { testItem };

            //Assert
            CollectionAssert.AreEqual(testList, result);
        }

        [TestMethod]
        public void Save_AssignsIdToObject_id()
        {
            //Arrange
            Item testItem = new Item("Mow the lawn");
            testItem.Save();

            //Act
            Item savedItem = Item.GetAll() [0];

            int result = savedItem.GetId();
            int testId = testItem.GetId();

            //Assert
            Assert.AreEqual(testId, result);
        }

        [TestMethod]
        public void Find_FindsItemInDatabase_Item() //Test is failing for no reason?
        {
            //Arrange
            Item testItem = new Item("Mow the lawn");
            testItem.Save();

            //Act
            Item result = Item.Find(testItem.GetId());

            //Assert
            Assert.AreEqual(testItem, result);
        }

        [TestMethod]
        public void AddCategory_AddsCategoryToItem_CategoryList()
        {
            Item testItem = new Item("Mow the lawn");
            testItem.Save();

            Category testCategory = new Category("Home stuff");
            testCategory.Save();

            testItem.AddCategory(testCategory);
            List<Category> result = testItem.GetCategories();
            List<Category> testList = new List<Category> { testCategory };

            CollectionAssert.AreEqual(testList, result);
        }

        [TestMethod]
        public void GetCategories_ReturnsAllItemCategories_CategoryList()
        {
            Item testItem = new Item("Mow the lawn");
            testItem.Save();

            Category testCategory = new Category("Home stuff");
            testCategory.Save();

            testItem.AddCategory(testCategory);
            List<Category> result = testItem.GetCategories();
            List<Category> testList = new List<Category> { testCategory };
            CollectionAssert.AreEqual(testList, result);
        }

        [TestMethod]
        public void Delete_DeletesItemAssociationsFromDatabase_ItemList()
        {
            Category testCategory = new Category("Home stuff");
            testCategory.Save();

            string testDescription = "Mow the lawn";
            Item testItem = new Item(testDescription);
            testItem.Save();
            testItem.AddCategory(testCategory);
            testItem.Delete();

            List<Item> resultCategoryItems = testCategory.GetItems();
            List<Item> testCategoryItems = new List<Item> { };

            //Assert
            CollectionAssert.AreEqual(testCategoryItems, resultCategoryItems);
        }
    }
}