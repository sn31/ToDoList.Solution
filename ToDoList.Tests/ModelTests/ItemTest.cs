using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoList.Models;

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
    }
}