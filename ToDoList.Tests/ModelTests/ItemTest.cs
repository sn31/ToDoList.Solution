using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoList.Models;
using System.Collections.Generic;

namespace ToDoList.TestTools
{
    [TestClass]
    public class ItemTest
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
            Assert.AreEqual(description,result);
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
            Assert.AreEqual(updatedDescription,result);
        }
        [TestMethod]
        public void Save_ItemIsSavedToInstances_Item()
        {
            //Arrange
            string description = "Walk the dog.";
            Item newItem = new Item(description);
            newItem.Save();

            //Act
            List<Item> instances = Item.GetAll();
            Item savedItem = instances[0];

            //Assert
            Assert.AreEqual(newItem,savedItem);
        }
    }
}