// using System.Collections.Generic;
// using Microsoft.VisualStudio.TestTools.UnitTesting;
// using UnityEngine;
//
// [TestClass]
// public class InteractionManagerLogicTests
// {
//     [TestMethod]
//     public void CheckInteractions_ItemInRange_ShowsUI()
//     {
//         // Arrange
//         var logic = new InteractionManagerLogic();
//         var items = new List<IInteractable>();
//         var mockItem = new MockInteractable(new Vector3(0, 0, 0));
//         items.Add(mockItem);
//         
//         // Act
//         logic.CheckInteractions(items, new Vector3(0, 0, 2), 3f);
//         
//         // Assert
//         Assert.IsTrue(mockItem.UIVisible);
//     }
// } 