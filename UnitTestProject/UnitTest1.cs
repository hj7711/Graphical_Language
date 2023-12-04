using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Graphical_Language;
using System.Drawing;
using System.Windows.Forms;

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



        /// <summary>
        /// Test whether the 'draw' command draws a line and updates the pen position correctly.
        /// </summary>
        [TestMethod]
        public void ExecuteCommand_DrawCommand_ShouldDrawLineAndUpdatePenPosition()
        {
            // Arrange
            CommandParser commandParser = CommandParser.Instance;

            // Act
            commandParser.ParseAndExecute("draw 20 30");

            // Assert
            Assert.AreEqual(20, commandParser.CurrentPenX);
            Assert.AreEqual(30, commandParser.CurrentPenY);
            // You might need additional assertions based on your application's behavior
        }





        /// <summary>
        /// Test whether the 'draw' command throws an ArgumentException with invalid parameters.
        /// </summary>
        [TestMethod]
        public void ExecuteCommand_DrawCommand_WithInvalidParameters_ShouldThrowArgumentException()
        {
            // Arrange
            CommandParser commandParser = CommandParser.Instance;

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() =>
            {
                commandParser.ParseAndExecute("draw invalidParameter");
            });
        }






        /// <summary>
        /// Unit test for the ExecuteClearCommand method.
        /// </summary>
        [TestMethod]
        public void ExecuteCommand_ClearCommand_ShouldClearDrawingArea()
        {
            CommandParser commandParser = CommandParser.Instance;
            commandParser.pictureBox = new PictureBox(); // Initialize the PictureBox

            // Act
            commandParser.ParseAndExecute("clear");

            // Assert - Check if the drawing area is cleared
            Assert.IsTrue(commandParser.pictureBox.Image == null, "The drawing area is not cleared.");

        }






        /// <summary>
        /// Test method to ensure that executing the 'fill on' command sets the fill state to true.
        /// </summary>
        [TestMethod]
        public void ExecuteCommand_FillCommand_TurnOnFill_ShouldSetFillStateToTrue()
        {
            // Arrange
            CommandParser commandParser = CommandParser.Instance;

            // Act
            commandParser.ParseAndExecute("fill on");

            // Assert
            Assert.IsTrue(commandParser.FillEnabled, "Fill state should be true after 'fill on' command.");
        }

        /// <summary>
        /// Test method to ensure that executing the 'fill off' command sets the fill state to false.
        /// </summary>
        [TestMethod]
        public void ExecuteCommand_FillCommand_TurnOffFill_ShouldSetFillStateToFalse()
        {
            // Arrange
            CommandParser commandParser = CommandParser.Instance;

            // Act
            commandParser.ParseAndExecute("fill off");

            // Assert
            Assert.IsFalse(commandParser.FillEnabled, "Fill state should be false after 'fill off' command.");
        }

        /// <summary>
        /// Test method to ensure that executing the 'fill' command with an invalid fill state throws an ArgumentException.
        /// </summary>
        [TestMethod]
        public void ExecuteCommand_FillCommand_InvalidFillState_ShouldThrowArgumentException()
        {
            // Arrange
            CommandParser commandParser = CommandParser.Instance;

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() =>
            {
                commandParser.ParseAndExecute("fill invalidState");
            });
        }




    }






}
