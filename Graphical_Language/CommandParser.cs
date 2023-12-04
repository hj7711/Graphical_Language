using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graphical_Language
{
    class CommandParser
    {
        private static CommandParser instance;
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
                    //ExecutePositionCommand(words);
                    break;
                case "pen":
                   // ExecutePenCommand(words);
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
                   // ExecuteRectangleCommand(words);
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
                    Console.WriteLine("Invalid command");
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

        private void DisplayMessage(string message)
        {
            // Show the message in a MessageBox
            MessageBox.Show(message);

        }

    }
}
