using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoList.Models;

namespace ToDoList.Tests
{

    [TestClass]
    public class CategoryTest : IDisposable
    {
        public CategoryTest()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=todo_test;";
        }
        public void Dispose()
        {
            Category.ClearAll();
            Item.ClearAll();
        }

        [TestMethod]
        public void GetAll_DatabaseEmptyAtFirst_0()
        {
            //Arrange, Act
            int result = Category.GetAll().Count;

            //Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Equals_TrueForSameDescription_Category()
        {
            //Arrange, Act
            Category firstCategory = new Category("School work");
            Category secondCategory = new Category("School work");

            //Assert
            Assert.AreEqual(firstCategory, secondCategory);
        }

        [TestMethod]
        public void Save_CategorySavesToDatabase_CategoryList()
        {
            //Arrange
            Category testCategory = new Category("Mow the lawn");
            testCategory.Save();

            //Act
            List<Category> result = Category.GetAll();
            List<Category> testList = new List<Category> { testCategory };

            //Assert
            CollectionAssert.AreEqual(testList, result);
        }

        [TestMethod]
        public void Save_AssignsIdToObject_id()
        {
            //Arrange
            Category testCategory = new Category("Mow the lawn");
            testCategory.Save();

            //Act
            Category savedCategory = Category.GetAll() [0];

            int result = savedCategory.GetId();
            int testId = testCategory.GetId();

            //Assert
            Assert.AreEqual(testId, result);
        }

        [TestMethod]
        public void Find_FindsCategoryInDatabase_Category() //Test is failing for no reason?
        {
            //Arrange
            Category testCategory = new Category("Mow the lawn");
            testCategory.Save();

            //Act
            Category result = Category.Find(testCategory.GetId());

            //Assert
            Assert.AreEqual(testCategory, result);
        }

        [TestMethod]
        public void Delete_DeletesCategoryAssociationsFromDatabase_CategoryList()
        {
            Item testItem = new Item("Mow the lawn");
            testItem.Save();

            string testName = "Home stuff";
            Category testCategory = new Category(testName);
            testCategory.Save();

            testCategory.AddItem(testItem);
            testCategory.Delete();

            List<Category> resultItemCategories = testItem.GetCategories();
            List<Category> testItemCategories = new List<Category> { };
            CollectionAssert.AreEqual(testItemCategories, resultItemCategories);
        }

        [TestMethod]
        public void Test_AddItem_AddsItemToCategory()
        {
            Category testCategory = new Category("Household chores");
            testCategory.Save();

            Item testItem = new Item("Mow the lawn");
            testItem.Save();

            Item testItem2 = new Item("Water the garden");
            testItem2.Save();

            //Act
            testCategory.AddItem(testItem);
            testCategory.AddItem(testItem2);
            List<Item> result = testCategory.GetItems();
            List<Item> testList = new List<Item> { testItem, testItem2 };
            Console.WriteLine(result.Count);
            Console.WriteLine(testList.Count);
            //Assert
            CollectionAssert.AreEqual(testList, result);
        }

        [TestMethod]
        public void GetItems_ReturnsAllCategoryItems_ItemList()
        {
            //Arrange
            Category testCategory = new Category("Household chores");
            testCategory.Save();

            Item testItem1 = new Item("Mow the lawn");
            testItem1.Save();

            Item testItem2 = new Item("Buy plane ticket");
            testItem2.Save();

            //Act
            testCategory.AddItem(testItem1);
            List<Item> savedItems = testCategory.GetItems();
            List<Item> testList = new List<Item> { testItem1 };

            //Assert
            CollectionAssert.AreEqual(testList, savedItems);
        }
    }
}