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
            commandParser.ParseAndExecute("moveTo 10, 20");

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
            commandParser.ParseAndExecute("drawTo 20, 30");


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


        /// <summary>
        /// Tests whether variables are assigned and retrieved correctly.
        /// </summary>
        [TestMethod]
        public void TestVariableAssignmentAndRetrieval()
        {
            // Arrange
            CommandParser commandParser = CommandParser.Instance;

            // Act
            commandParser.ParseAndExecute("x = 10");
            commandParser.ParseAndExecute("y = 20");

            // Assert
            // Check if variables are assigned correctly
            Assert.AreEqual(10, commandParser.GetVariableValue("x"), "Variable 'x' should be assigned the value 10.");
            Assert.AreEqual(20, commandParser.GetVariableValue("y"), "Variable 'y' should be assigned the value 20.");

            // Attempt to retrieve an undefined variable, should throw ArgumentException
            Assert.ThrowsException<ArgumentException>(() => commandParser.GetVariableValue("z"));
        }


        /// <summary>
        /// Tests if the ParseAndExecute method correctly sets IfConditionMet to true when the condition is met.
        /// </summary>
        [TestMethod]
        public void ParseAndExecute_IfStatementWithValidCondition_ShouldSetIfConditionMetToTrue()
        {
            // Arrange
            CommandParser commandParser = CommandParser.Instance;
            commandParser.AssignVariable("x", 10); // Assign a value to variable x
            string ifStatement = "if x > 5";

            // Act
            commandParser.ParseAndExecute(ifStatement);

            // Assert
            Assert.IsTrue(commandParser.IfConditionMet, "If condition should be met when x > 5.");
        }



        /// <summary>
        /// Tests the <see cref="CommandParser"/> class for setting the loop condition met flag to true with a valid loop condition.
        /// </summary>
        [TestMethod]
        public void ExecuteLoopCommand_ValidCondition_ShouldSetLoopConditionMetToTrue()
        {
            // Arrange
            CommandParser commandParser = CommandParser.Instance;
            commandParser.AssignVariable("x", 5);
            string loopcondition = "while x < 10";// Example loop condition: while x < 10

            // Act
            commandParser.ParseAndExecute(loopcondition);

            // Assert
            Assert.IsTrue(commandParser.LoopConditionMet, "Loop condition should be met when x < 10.");
        }


        /// <summary>
        /// Tests the <see cref="CommandParser"/> class for throwing an exception with an invalid loop condition.
        /// </summary>
        [TestMethod]
        public void ExecuteLoopCommand_InvalidCondition_ShouldThrowArgumentException()
        {
            // Arrange
            CommandParser commandParser = CommandParser.Instance;
            string loopcondition = "while invalid"; // Example invalid loop condition: while invalid

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() =>
            {
                commandParser.ParseAndExecute(loopcondition);
            });
        }




        /// <summary>
        /// Test method to ensure that executing the 'rectangle' command with invalid parameters throws an ArgumentException.
        /// </summary>
        [TestMethod]
        public void ExecuteCommand_RectangleCommand_WithInvalidParameters_ShouldThrowArgumentException()
        {
            // Arrange
            CommandParser commandParser = CommandParser.Instance;

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() =>
            {
                commandParser.ParseAndExecute("rectangle invalidParameter");
            });
        }


        /// <summary>
        /// Tests the 'reset' command, ensuring that the pen is moved to the initial position.
        /// </summary>
        [TestMethod]
        public void ExecuteCommand_ResetCommand_ShouldMovePenToInitialPosition()
        {
            // Arrange
            CommandParser commandParser = CommandParser.Instance;

            // Set the current pen position to a non-initial position
            commandParser.CurrentPenX = 50;
            commandParser.CurrentPenY = 60;

            // Act
            commandParser.ParseAndExecute("reset");

            // Assert
            Assert.AreEqual(0, commandParser.CurrentPenX);
            Assert.AreEqual(0, commandParser.CurrentPenY);
        }



        /// <summary>
        /// Test method to ensure that executing the 'circle' command with invalid parameters throws an ArgumentException.
        /// </summary>
        [TestMethod]
        public void ExecuteCommand_CircleCommand_WithInvalidParameters_ShouldThrowArgumentException()
        {
            // Arrange
            CommandParser commandParser = CommandParser.Instance;

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() =>
            {
                commandParser.ParseAndExecute("circle invalidParameter");
            });
        }


        /// <summary>
        /// Tests whether the 'circle' command is executed correctly with a radius of 30.
        /// </summary>
        [TestMethod]
        public void ExecuteCommand_CircleCommand_WithRadiusThirty_ShouldExecuteCorrectly()
        {
            // Arrange
            CommandParser commandParser = CommandParser.Instance;
            PictureBox pictureBox = new PictureBox();
            commandParser.pictureBox = pictureBox;

            // Act
            commandParser.ParseAndExecute("circle 30");

            // Assert
            // Check if the current pen position is updated after executing the command
            Assert.AreEqual(0, commandParser.CurrentPenX, "CurrentPenX should be updated to 0.");
            Assert.AreEqual(0, commandParser.CurrentPenY, "CurrentPenY should be updated to 0.");

            // Check if the circle command was executed correctly
            Assert.AreEqual(100, pictureBox.Width, "The circle width should be set to 30.");
        }




        /// <summary>
        /// Test method to ensure that executing the 'triangle' command with invalid parameters throws an ArgumentException.
        /// </summary>
        [TestMethod]
        public void ExecuteCommand_TriangleCommand_WithInvalidParameters_ShouldThrowArgumentException()
        {
            // Arrange
            CommandParser commandParser = CommandParser.Instance;

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() =>
            {
                commandParser.ParseAndExecute("triangle invalidParameter");
            });
        }
    }

}
