using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoList.Models;

namespace ToDoList.TestTools
{
    [TestClass]
    public class ItemTests : IDisposable
    {

        [TestMethod]
        public void GetDescription_ReturnsDescription_String()
        {
            //Arrange
            string description = "Walk the dog.";
            Item newItem = new Item(description);

            //Act
            string result = newItem.GetDescription();

            //Assert
            Assert.AreEqual(description, result);
        }

        [TestMethod]
        public void SetDescription_SetDescription_String()
        {
            //Arrange
            string description = "Walk the dog.";
            Item newItem = new Item(description);

            //Act
            string updatedDescription = "Do the dishes";
            newItem.SetDescription(updatedDescription);
            string result = newItem.GetDescription();

            //Assert
            Assert.AreEqual(updatedDescription, result);
        }

        // [TestMethod]
        // public void Save_ItemIsSavedToInstances_Item()
        // {
        //     //Arrange
        //     string description = "Walk the dog.";
        //     Item newItem = new Item(description);

        //     //Act
        //     List<Item> instances = Item.GetAll();
        //     Item savedItem = instances[0];

        //     //Assert
        //     Assert.AreEqual(newItem, savedItem);
        // }

        [TestMethod]
        public void GetAll_ReturnsItems_ItemList()
        {
            //Arrange
            string description01 = "Walk the dog";
            string description02 = "Wash the dishes";
            Item newItem1 = new Item(description01);
            newItem1.Save();
            Item newItem2 = new Item(description02);
            newItem2.Save();
            List<Item> newList = new List<Item> { newItem1, newItem2 };

            //Act
            List<Item> result = Item.GetAll();

            //Assert
            CollectionAssert.AreEqual(newList, result);
        }

        [TestMethod]
        public void GetAll_DbStartsEmpty_0()
        {
            int result = Item.GetAll().Count;
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Equals_ReturnsTrueIfDescriptionsAReTheSame_ITem()
        {
            Item testItem = new Item("Mow the lawn");
            testItem.Save();
            List<Item> result = Item.GetAll();
            List<Item> testList = new List<Item> { testItem };
            CollectionAssert.AreEqual(testList, result);
        }

        [TestMethod]
        public void Save_AssignsIdToObject_Id()
        {
            //Arrange
            Item testItem = new Item("Mow the lawn");

            //Act
            testItem.Save();
            Item savedItem = Item.GetAll() [0];

            int result = savedItem.GetId();
            int testId = testItem.GetId();

            //Assert
            Assert.AreEqual(testId, result);
        }

        [TestMethod]
        public void Find_FindsItemInDatabase_Item()
        {
            //Arrange
            Item testItem = new Item("Mow the lawn");
            testItem.Save();

            //Act
            Item foundItem = Item.Find(testItem.GetId());

            //Assert
            Assert.AreEqual(testItem, foundItem);
        }
        public void Dispose()
        {
            Item.ClearAll();
        }
        public ItemTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=todo_test;";
        }

    }
}