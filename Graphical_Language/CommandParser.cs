using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graphical_Language
{
    public class CommandParser
    {
        private static CommandParser instance;
        public int CurrentPenX { get;  set; } = 0;
        public int CurrentPenY { get;  set; } = 0;
        public Color PenColor { get; set; } = Color.Black;
        public PictureBox pictureBox{ get; set;}

        public bool FillEnabled { get; set; }

        //filepath change it according to requirement
        public string filepath = @"C:\Users\hp\Desktop\Graphical Language\Graphical_Language\Graphical_Language\SaveProgram.txt";

        private Dictionary<string, Variable> variables = new Dictionary<string, Variable>();

        private Stack<string> IfStack = new Stack<string>();

        bool IfConditionMet;
        public static CommandParser Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CommandParser();
                }
                return instance;
            }
        }

        public void ParseAndExecute(string command)
        {
            if (!command.Contains("==") && !command.Contains("!=") && command.Contains("="))
            {
                HandleVariableAssignment(command);
            }
            else
            {
                // Split the command into words
                string[] words = command.Split(' ');

                // Extract the command keyword (first word)
                string keyword = words[0].ToLower();

                // Execute the corresponding method based on the command keyword
                switch (keyword)
                {
                    case "moveto":
                        ExecutePositionCommand(words);
                        break;
                    case "pen":
                        ExecutePenColorCommand(words);
                        break;
                    case "drawto":
                        ExecuteDrawCommand(words);
                        break;
                    case "clear":
                        ExecuteClearCommand();
                        break;
                    case "reset":
                        ExecuteResetCommand();
                        break;
                    case "rectangle":
                        ExecuteRectangleCommand(words);
                        break;
                    case "circle":
                        ExecuteCircleCommand(words);
                        break;
                    case "triangle":
                        ExecuteTriangleCommand(words);
                        break;
                    case "fill":
                        ExecuteFillCommand(words);
                        break;
                    case "if":
                        ExecuteIfCommand(words);
                        break;
                    default:
                        throw new ArgumentException("invalidcommand");
                        break;
                }
            }
        }


        public void CheckSyntex(string command)
        {
            // Split the command into words
            string[] words = command.Split(' ');

            // Extract the command keyword (first word)
            string keyword = words[0].ToLower();

            // Execute the corresponding method based on the command keyword
            switch (keyword)
            {
                case "position":
                    if (int.TryParse(words[1], out int x) && int.TryParse(words[2], out int y) && words.Length == 3)
                    {
                        DisplayMessage("Correct Syntex");
                    }
                    else
                        DisplayMessage("Invalid Syntex");
                    break;
                case "pen":
                    string colorName = words[1].ToLower();
                    if (IsKnownColor(colorName))
                        DisplayMessage("Correct Syntex");
                    else
                        DisplayMessage("Invalid Syntex");
                    break;
                case "draw":
                    if (int.TryParse(words[1], out int x1) && int.TryParse(words[2], out int y1) && words.Length == 3)
                    {
                        DisplayMessage("Correct Syntex");
                    }
                    else
                        DisplayMessage("Invalid Syntex");
                    break;
                case "clear":
                    if (words.Length == 1)
                        DisplayMessage("Correct Syntex");
                    else
                        DisplayMessage("Invalid Syntex");
                    break;
                case "reset":
                    if (words.Length == 1)
                        DisplayMessage("Correct Syntex");
                    else
                        DisplayMessage("Invalid Syntex");
                    break;
                case "rectangle":
                    if (int.TryParse(words[1], out int x2) && int.TryParse(words[2], out int y2) && words.Length == 3)
                    {
                        DisplayMessage("Correct Syntex");
                    }
                    else
                        DisplayMessage("Invalid Syntex");
                    break;
                case "circle":
                    if (int.TryParse(words[1], out int c) &&words.Length == 2)
                    {
                        DisplayMessage("Correct Syntex");
                    }
                    else
                        DisplayMessage("Invalid Syntex");
                    break;
                case "triangle":
                    if (int.TryParse(words[1], out int x3) && int.TryParse(words[2], out int y3) && words.Length == 3)
                    {
                        DisplayMessage("Correct Syntex");
                    }
                    else
                        DisplayMessage("Invalid Syntex");
                    break;
                case "fill":
                    if(words[1]=="on" || words[1] == "off")
                        DisplayMessage("Correct Syntex");
                    else
                        DisplayMessage("Invalid Syntex");
                    break;
                default:
                    DisplayMessage("Invalid Syntex");
                    break;
            }
        }


        public void RunProgram(string program)
        {
            try
            {
                string[] lines = program.Split('\n');


                foreach (string line in lines)
                {
                    string trimmedLine = line.Trim();

                    if (trimmedLine.StartsWith("if"))
                    {
                        ParseAndExecute(trimmedLine);
                        IfStack.Push(trimmedLine);
                    }
                    else if(trimmedLine.StartsWith("endif"))
                    {
                        IfStack.Pop();
                    }
                    else if(IfConditionMet)
                    {
                        ParseAndExecute(trimmedLine);
                    }
                    else if(IfStack.Count == 0)
                    {
                        ParseAndExecute(trimmedLine);
                    }
                }

                if(IfStack.Count > 0)
                {
                    DisplayMessage($"Error executing program: no endif found");
                }
            }
            catch (Exception ex)
            {
                DisplayMessage($"Error executing program: {ex.Message}");
            }
        }



        public void SaveProgramToFile(string filePath, string program)
        {
            try
            {
                System.IO.File.WriteAllText(filePath, program);
            }
            catch (Exception ex)
            {
                DisplayMessage($"Error saving program to file: {ex.Message}");
            }
        }

        public string LoadProgramFromFile(string filePath)
        {
            try
            {
                return System.IO.File.ReadAllText(filePath);
            }
            catch (Exception ex)
            {
                DisplayMessage($"Error loading program from file: {ex.Message}");
                return string.Empty;
            }
        }

        #region Variables


        public void AssignVariable(string name, int value)
        {
            if (variables.ContainsKey(name))
            {
                variables[name].Value = value;
            }
            else
            {
                variables.Add(name, new Variable { Name = name, Value = value });
            }
        }

        public int GetVariableValue(string name)
        {
            if (variables.ContainsKey(name))
            {
                return variables[name].Value;
            }
            throw new ArgumentException($"Variable '{name}' not found.");
        }
        #endregion


        #region Position Command

        /// <summary>
        /// Executes the 'position' command, updating the pen position and drawing it on the PictureBox.
        /// </summary>
        /// <param name="words">An array of words containing the command and its parameters.</param>
        /// <exception cref="ArgumentException">Thrown when the command is invalid or has incorrect parameters.</exception>
        private void ExecutePositionCommand(string[] words)
        {
            if (words.Length < 2)
            {
                throw new ArgumentException("Invalid 'moveTo' command. Usage: moveTo <x>,<y>");
            }

            // Extract the coordinates part of the command (after the first word 'position')
            string coordinatesPart = string.Join(" ", words.Skip(1));

            // Split the coordinates by comma and trim each part to remove spaces
            string[] coordinates = coordinatesPart.Split(',').Select(c => c.Trim()).ToArray();

            // Ensure there are two parts (x and y) after splitting
            if (coordinates.Length != 2)
            {
                throw new ArgumentException("Invalid 'moveTo' command. Usage: moveTo <x>,<y>");
            }

            // Parse x coordinate
            int x;
            if (!int.TryParse(coordinates[0], out x))
            {
                // Attempt to retrieve value of the variable if it's not an integer
                x = GetVariableValue(coordinates[0]);
            }

            // Parse y coordinate
            int y;
            if (!int.TryParse(coordinates[1], out y))
            {
                // Attempt to retrieve value of the variable if it's not an integer
                y = GetVariableValue(coordinates[1]);
            }

            // Update the pen position
            CurrentPenX = x;
            CurrentPenY = y;

            // Draw the updated position in the PictureBox
            DrawPenPosition();
        }


        /// <summary>
        /// Draws the current pen position on the PictureBox.
        /// </summary>
        private void DrawPenPosition()
        {
            if (pictureBox != null)
            {
                using (Graphics g = pictureBox.CreateGraphics())
                using (Pen pen = new Pen(PenColor)) // You can set the pen color based on your needs
                {
                    // Clear the previous drawings in the PictureBox (optional)
                    // g.Clear(pictureBox.BackColor);

                    // Draw a point or small circle to represent the pen position
                    int penSize = 5;
                    g.DrawEllipse(pen, CurrentPenX - penSize / 2, CurrentPenY - penSize / 2, penSize, penSize);
                }
            }
        }

        #endregion

        #region PenColor Command

        /// <summary>
        /// Executes the 'pen' command, setting the pen color based on the specified color name.
        /// </summary>
        /// <param name="words">An array of words containing the command and its parameters.</param>
        /// <exception cref="ArgumentException">Thrown when the command is invalid or has incorrect parameters.</exception>
        private void ExecutePenColorCommand(string[] words)
        {
            if (words.Length < 2)
            {
                throw new ArgumentException("Invalid 'pen' command. Usage: pen <colour>");
            }

            // Parse color from the second word (assuming it's a valid color name)
            string colorName = words[1].ToLower();
            if (IsKnownColor(colorName))
            {
                PenColor = Color.FromName(colorName);
                DisplayMessage($"Pen color set to {colorName}");
            }
            else
            {
                throw new ArgumentException($"Invalid 'pen' command. '{colorName}' is not a valid color.");
            }
        }

        private bool IsKnownColor(string colorName)
        {
            foreach (var knownColor in Enum.GetValues(typeof(KnownColor)))
            {
                if (Color.FromKnownColor((KnownColor)knownColor).Name.ToLower() == colorName)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region Draw Command

        /// <summary>
        /// Executes the 'draw' command, drawing a line from the current pen position to the specified coordinates.
        /// </summary>
        /// <param name="words">An array of words containing the command and its parameters.</param>
        /// <exception cref="ArgumentException">Thrown when the command is invalid or has incorrect parameters.</exception>
        private void ExecuteDrawCommand(string[] words)
        {
            if (words.Length < 2)
            {
                throw new ArgumentException("Invalid 'drawTo' command. Usage: drawTo <x>,<y>");
            }

            // Extract the coordinates part of the command (after the first word 'draw')
            string coordinatesPart = string.Join(" ", words.Skip(1));

            // Split the coordinates by comma and trim each part to remove spaces
            string[] coordinates = coordinatesPart.Split(',').Select(c => c.Trim()).ToArray();

            // Ensure there are two parts (x and y) after splitting
            if (coordinates.Length != 2)
            {
                throw new ArgumentException("Invalid 'drawTo' command. Usage: drawTo <x>,<y>");
            }

            // Parse x coordinate
            int x;
            if (!int.TryParse(coordinates[0], out x))
            {
                // Attempt to retrieve value of the variable if it's not an integer
                x = GetVariableValue(coordinates[0]);
            }

            // Parse y coordinate
            int y;
            if (!int.TryParse(coordinates[1], out y))
            {
                // Attempt to retrieve value of the variable if it's not an integer
                y = GetVariableValue(coordinates[1]);
            }

            if (pictureBox != null)
            {
                // Draw a line from the current pen position to the specified coordinates
                using (Graphics g = pictureBox.CreateGraphics())
                {
                    g.DrawLine(new Pen(PenColor), CurrentPenX, CurrentPenY, x, y);
                }
            }

            // Update pen position
            CurrentPenX = x;
            CurrentPenY = y;
        }


        #endregion

        #region Clear Command

        /// <summary>
        /// Clears the drawing area in the graphical environment.
        /// </summary>
        /// <remarks>
        /// This command removes all drawn elements from the drawing area, providing a clean slate for further drawing.
        /// </remarks>
        private void ExecuteClearCommand()
        {
            
            if(pictureBox != null)
            {
                // Use the Refresh method to clear the drawing area
                pictureBox.Refresh();

                // Display a message indicating that the drawing area has been cleared
                DisplayMessage("Drawing area cleared");
            }
        }

        #endregion

        #region Fill Command

        /// <summary>
        /// Executes the 'fill' command, toggling the fill state for subsequent shape operations.
        /// </summary>
        /// <param name="words">An array of words containing the command and its parameters.</param>
        /// <exception cref="ArgumentException">Thrown when the command is invalid or has incorrect parameters.</exception>
        private void ExecuteFillCommand(string[] words)
        {
            if (words.Length < 2)
            {
                throw new ArgumentException("Invalid 'fill' command. Usage: fill <on/off>");
            }

            string fillState = words[1].ToLower();

            if (fillState == "on")
            {
                FillEnabled = true;
                DisplayMessage("Fill turned on");
            }
            else if (fillState == "off")
            {
                FillEnabled = false;
                DisplayMessage("Fill turned off");
            }
            else
            {
                throw new ArgumentException("Invalid 'fill' command. Use 'on' or 'off'.");
            }
        }

        #endregion


        #region Rectangle Command

        /// <summary>
        /// Executes the 'rectangle' command, drawing a rectangle at the current pen position.
        /// </summary>
        /// <param name="words">An array of words containing the command and its parameters.</param>
        /// <exception cref="ArgumentException">Thrown when the command is invalid or has incorrect parameters.</exception>

        private void ExecuteRectangleCommand(string[] words)
        {
            if (words.Length < 2)
            {
                throw new ArgumentException("Invalid 'rectangle' command. Usage: rectangle <width>,<height>");
            }

            // Extract the dimensions part of the command (after the first word 'rectangle')
            string dimensionsPart = string.Join(" ", words.Skip(1));

            // Split the dimensions by comma and trim each part to remove spaces
            string[] dimensions = dimensionsPart.Split(',').Select(d => d.Trim()).ToArray();

            // Ensure there are two parts (width and height) after splitting
            if (dimensions.Length != 2)
            {
                throw new ArgumentException("Invalid 'rectangle' command. Usage: rectangle <width>,<height>");
            }

            // Parse width
            int width;
            if (!int.TryParse(dimensions[0], out width))
            {
                // Attempt to retrieve value of the variable if it's not an integer
                width = GetVariableValue(dimensions[0]);
            }

            // Parse height
            int height;
            if (!int.TryParse(dimensions[1], out height))
            {
                // Attempt to retrieve value of the variable if it's not an integer
                height = GetVariableValue(dimensions[1]);
            }

            if (pictureBox != null)
            {
                // Draw a rectangle at the current pen position
                using (Graphics g = pictureBox.CreateGraphics())
                {
                    if (FillEnabled)
                    {
                        // Draw filled rectangle at the specified position
                        g.FillRectangle(new SolidBrush(PenColor), CurrentPenX, CurrentPenY, width, height);
                    }
                    else
                    {
                        // Draw rectangle outline at the specified position
                        g.DrawRectangle(new Pen(PenColor), CurrentPenX, CurrentPenY, width, height);
                    }
                }
            }
        }




        #endregion

        #region Reset Command

        /// <summary>
        /// Executes the 'reset' command, moving the pen to the initial position at the top left of the screen.
        /// </summary>
        private void ExecuteResetCommand()
        {
            // Set the pen position to the initial position (e.g., top left corner)
            CurrentPenX = 0;
            CurrentPenY = 0;

            DisplayMessage("Pen reset to initial position");
        }

        #endregion

        #region Circle Command

        /// <summary>
        /// Executes the 'circle' command, drawing a circle at the current pen position with the specified radius.
        /// </summary>
        /// <param name="words">An array of words containing the command and its parameters.</param>
        /// <exception cref="ArgumentException">Thrown when the command is invalid or has incorrect parameters.</exception>
        private void ExecuteCircleCommand(string[] words)
        {
            if (words.Length < 2)
            {
                throw new ArgumentException("Invalid 'circle' command. Usage: circle <radius>");
            }

            // Parse radius
            int radius;
            if (!int.TryParse(words[1], out radius))
            {
                // Attempt to retrieve value of the variable if it's not an integer
                radius = GetVariableValue(words[1]);
            }

            if (pictureBox != null)
            {
                // Draw a circle at the current pen position
                using (Graphics g = pictureBox.CreateGraphics())
                {
                    if (FillEnabled)
                    {
                        // Draw filled circle at the specified position
                        g.FillEllipse(new SolidBrush(PenColor), CurrentPenX - radius, CurrentPenY - radius, 2 * radius, 2 * radius);
                    }
                    else
                    {
                        // Draw circle outline at the specified position
                        g.DrawEllipse(new Pen(PenColor), CurrentPenX - radius, CurrentPenY - radius, 2 * radius, 2 * radius);
                    }
                }
            }
            else
            {
                throw new ArgumentException("Invalid 'circle' command. Radius must be an integer.");
            }
        }

        #endregion

        #region trianglecommand

        /// <summary>
        /// Executes the 'triangle' command, drawing a triangle at the current pen position.
        /// </summary>
        /// <param name="words">An array of words containing the command and its parameters.</param>
        /// <exception cref="ArgumentException">Thrown when the command is invalid or has incorrect parameters.</exception>
        /// <remarks>
        /// The 'triangle' command requires the base and height of the triangle.
        /// The command format is: triangle <base> <height>
        /// </remarks>
        private void ExecuteTriangleCommand(string[] words)
        {
            if (words.Length < 2)
            {
                throw new ArgumentException("Invalid 'triangle' command. Usage: triangle <base>,<height>");
            }

            // Extract the dimensions part of the command (after the first word 'triangle')
            string dimensionsPart = string.Join(" ", words.Skip(1));

            // Split the dimensions by comma and trim each part to remove spaces
            string[] dimensions = dimensionsPart.Split(',').Select(d => d.Trim()).ToArray();

            // Ensure there are two parts (base and height) after splitting
            if (dimensions.Length != 2)
            {
                throw new ArgumentException("Invalid 'triangle' command. Usage: triangle <base>,<height>");
            }

            // Parse base length
            int baseLength;
            if (!int.TryParse(dimensions[0], out baseLength))
            {
                // Attempt to retrieve value of the variable if it's not an integer
                baseLength = GetVariableValue(dimensions[0]);
            }

            // Parse height
            int height;
            if (!int.TryParse(dimensions[1], out height))
            {
                // Attempt to retrieve value of the variable if it's not an integer
                height = GetVariableValue(dimensions[1]);
            }

            // Calculate the coordinates of the vertices based on the base and height
            int x1 = CurrentPenX;
            int y1 = CurrentPenY;
            int x2 = x1 + baseLength;
            int y2 = y1;
            int x3 = x1 + baseLength / 2;
            int y3 = y1 - height;

            if (pictureBox != null)
            {
                // Draw a triangle at the current pen position
                using (Graphics g = pictureBox.CreateGraphics())
                {
                    if (FillEnabled)
                    {
                        // Draw filled triangle at the specified vertices
                        Point[] triangleVertices = { new Point(x1, y1), new Point(x2, y2), new Point(x3, y3) };
                        g.FillPolygon(new SolidBrush(PenColor), triangleVertices);
                    }
                    else
                    {
                        // Draw triangle outline at the specified vertices
                        Point[] triangleVertices = { new Point(x1, y1), new Point(x2, y2), new Point(x3, y3) };
                        g.DrawPolygon(new Pen(PenColor), triangleVertices);
                    }
                }

            }
            else
            {
                throw new ArgumentException("Invalid 'triangle' command. Base and height must be integers.");
            }
        }

        #endregion

        #region Variable Assignment Command

        /// <summary>
        /// Handles the assignment of variables in the graphical language.
        /// </summary>
        /// <param name="command">The command string containing the variable assignment.</param>
        /// <exception cref="ArgumentException">
        /// Thrown when the variable assignment syntax is invalid or the value provided for the variable is not a valid integer.
        /// </exception>
        private void HandleVariableAssignment(string command)
        {
            string[] parts = command.Split('=');
            if (parts.Length == 2)
            {
                string variableName = parts[0].Trim();
                int value;
                if (int.TryParse(parts[1], out value))
                {
                    AssignVariable(variableName, value);
                }
                else
                {
                    throw new ArgumentException($"Invalid value for variable '{variableName}'.");
                }
            }
            else
            {
                throw new ArgumentException("Invalid variable assignment syntax.");
            }
        }

        #endregion

        #region if statement
        /// <summary>
        /// Executes an 'if' command with the specified condition.
        /// </summary>
        /// <param name="words">An array of words containing the 'if' command and its condition.</param>
        /// <exception cref="ArgumentException">Thrown when the condition syntax is invalid or when an operand is not a valid variable or constant.</exception>

        private void ExecuteIfCommand(string[] words)
        {
            string condition = string.Join(" ", words.Skip(1));

            string[] conditionParts = Regex.Split(condition, @"\s*(==|!=|<=|>=|<|>)\s*");

            if (conditionParts.Length != 3)
            {
                throw new ArgumentException("Invalid condition syntax.");
            }


            string leftOperand = conditionParts[0].Trim();
            string op = conditionParts[1].Trim();
            string rightOperand = conditionParts[2].Trim();

            int leftValue;
            int rightValue;

            // Check if the left operand is a variable
            if (int.TryParse(leftOperand, out leftValue))
            {
                // Left operand is a constant, try to parse the right operand as a variable
                if (!int.TryParse(rightOperand, out rightValue))
                {
                    rightValue = GetVariableValue(rightOperand);
                }
            }
            else
            {
                // Left operand is a variable, parse its value
                leftValue = GetVariableValue(leftOperand);

                // Try to parse the right operand as a variable or constant
                if (!int.TryParse(rightOperand, out rightValue))
                {
                    rightValue = GetVariableValue(rightOperand);
                }
            }

            IfConditionMet = EvaluateCondition(leftValue, op, rightValue);
        }

        private bool EvaluateCondition(int value1, string op, int value2)
        {
            switch (op)
            {
                case "==":
                    return value1 == value2;
                case "!=":
                    return value1 != value2;
                case "<":
                    return value1 < value2;
                case ">":
                    return value1 > value2;
                case "<=":
                    return value1 <= value2;
                case ">=":
                    return value1 >= value2;
                default:
                    throw new ArgumentException($"Invalid operator: {op}");
            }
        }


        #endregion


        private void DisplayMessage(string message)
        {
            // Show the message in a MessageBox
            MessageBox.Show(message);

        }

    }
}
