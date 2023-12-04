using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graphical_Language
{
    public partial class Graphical_Language : Form
    {
        string input_text = "";
        public Graphical_Language()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(input_text == textBox2.Text && input_text != "")
            {
                CommandParser.Instance.RunProgram(input_text);
                CommandParser.Instance.SaveProgramToFile(CommandParser.Instance.filepath, input_text);
                input_text = "";

            }

            else if (input_text == textBox1.Text && input_text != "")
            {
                CommandParser.Instance.ParseAndExecute(input_text);
                input_text = "";
            }

            textBox1.Text = "";
            textBox2.Text = "";

        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            input_text = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            input_text = textBox2.Text;
        }

        private void SYNTEX_Click(object sender, EventArgs e)
        {
            if (input_text == textBox1.Text && input_text != "")
            {
                CommandParser.Instance.CheckSyntex(input_text);
            }

        }
    }
}
