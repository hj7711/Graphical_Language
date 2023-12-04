using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graphical_Language
{
    public class CommandParser
    {
        private static CommandParser instance;
        public int CurrentPenX { get; private set; } = 0;
        public int CurrentPenY { get; private set; } = 0;
        public Color PenColor { get; set; } = Color.Black;
        public PictureBox pictureBox{ get; set;}

        public bool FillEnabled { get; set; }

        //filepath change it according to requirement
        public string filepath = @"C:\Users\hp\Desktop\Graphical Language\Graphical_Language\Graphical_Language\Program.txt";

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
            // Split the command into words
            string[] words = command.Split(' ');

            // Extract the command keyword (first word)
            string keyword = words[0].ToLower();

            // Execute the corresponding method based on the command keyword
            switch (keyword)
            {
                case "position":
                    ExecutePositionCommand(words);
                    break;
                case "pen":      
                    ExecutePenColorCommand(words);
                    break;
                case "draw":
                    //ExecuteDrawCommand(words);
                    break;
                case "clear":
                    //ExecuteClearCommand();
                    break;
                case "reset":
                    //ExecuteResetCommand();
                    break;
                case "rectangle":
                    //ExecuteRectangleCommand(words);
                    break;
                case "circle":
                   // ExecuteCircleCommand(words);
                    break;
                case "triangle":
                    //ExecuteTriangleCommand(words);
                    break;
                case "fill":
                    //ExecuteFillCommand(words);
                    break;
                default:
                    throw new ArgumentException("invalidcommand");
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
                    ParseAndExecute(line.Trim());
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


        #region Position Command

        /// <summary>
        /// Executes the 'position' command, updating the pen position and drawing it on the PictureBox.
        /// </summary>
        /// <param name="words">An array of words containing the command and its parameters.</param>
        /// <exception cref="ArgumentException">Thrown when the command is invalid or has incorrect parameters.</exception>
        private void ExecutePositionCommand(string[] words)
        {
            if (words.Length < 3)
            {
                throw new ArgumentException("Invalid 'position' command. Usage: position <x> <y>");
            }

            if (int.TryParse(words[1], out int x) && int.TryParse(words[2], out int y))
            {
                // Update the pen position
                CurrentPenX = x;
                CurrentPenY = y;

                // Draw the updated position in the PictureBox
                DrawPenPosition();
            }
            else
            {
                throw new ArgumentException("Invalid 'position' command. Coordinates must be integers.");
            }
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
            PenColor = Color.FromName(colorName);

            DisplayMessage($"Pen color set to {colorName}");
        }

        #endregion


        private void DisplayMessage(string message)
        {
            // Show the message in a MessageBox
            MessageBox.Show(message);

        }

    }
}
