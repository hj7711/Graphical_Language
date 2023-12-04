using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Graphical_Language;
using System.Drawing;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// Tests the <see cref="CommandParser"/> class for the 'position' command with valid parameters.
        /// </summary>
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






        /// <summary>
        /// Tests the <see cref="CommandParser"/> class for executing an invalid command, which should throw an <see cref="ArgumentException"/>.
        /// </summary>
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






        /// <summary>
        /// Tests the <see cref="YourClass"/> class for the 'pen' command with valid parameters.
        /// </summary>
        [TestMethod]
        public void TestExecutePenColorCommand_ValidCommand_ShouldSetPenColor()
        {

            // Arrange
            CommandParser commandParser = CommandParser.Instance;
            string validCommand = "pen red";

            // Act
            commandParser.ParseAndExecute(validCommand);

            // Assert - Check the state of YourClass or other relevant objects
            Assert.AreEqual(Color.Red, commandParser.PenColor);
        }





        /// <summary>
        /// Tests the <see cref="CommandParser"/> class for the 'pen' command with an invalid color name.
        /// </summary>
        [TestMethod]
        public void ExecuteCommand_PenCommand_InvalidColor_ShouldThrowArgumentException()
        {
            // Arrange
            CommandParser commandParser = CommandParser.Instance;

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() =>
            {
                commandParser.ParseAndExecute("pen invalidcolor");
            });
        }

    }






}
