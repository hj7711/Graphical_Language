﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Graphical_Language;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestPositionCommand_ValidCommand_ShouldSetPenPosition()
        {
            // Arrange
            CommandParser commandParser = CommandParser.Instance;

            // Act
            commandParser.ParseAndExecute("position 10 20");

            // Assert - Check the state of CommandParser or other relevant objects
            Assert.AreEqual(10, commandParser.CurrentPenX);
            Assert.AreEqual(20, commandParser.CurrentPenY);
        }

        [TestMethod]
        public void TestExecuteCommand_InvalidCommand_ShouldThrowArgumentException()
        {
            // Arrange
            CommandParser commandParser = CommandParser.Instance;

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() =>
            {
                commandParser.ParseAndExecute("invalidcommand");
            });
        }
    }

       
}