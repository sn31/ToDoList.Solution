using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using ToDoList.Models;

namespace ToDoList.Tests
{

[TestClass]
public class ItemTest : IDisposable
 {
    public ItemTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=88896;database=todo_test;";
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
      List<Item> testList = new List<Item>{testItem};

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
      Item savedItem = Item.GetAll()[0];

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
      Item result = Item.Find(testItem.GetId());

      //Assert
      Assert.AreEqual(testItem, result);
    }

  }
}