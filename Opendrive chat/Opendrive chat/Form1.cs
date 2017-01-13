using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.VisualBasic;
using System.Windows.Forms;

namespace Opendrive_chat
{
    public partial class Form1 : Form
    {

        private string path = Interaction.InputBox("Chat", "Enter chat file path:", "w:\\Open\\Year 9\\james&aron.psd", 0, 0);

        delegate void SetTextCallback(string text);

        private void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.textBox1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.textBox3.Text = text;
            }
        }

        public void refresh() {
            string txt = System.IO.File.ReadAllText(path);
            SetText(txt);
        }

        public Form1()
        {
            InitializeComponent();
            var timer = new System.Threading.Timer((e) =>
            {
                refresh();
            }, null, 0, 1000);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "" && textBox1.Text != "") {
                string text = System.IO.File.ReadAllText(path);
                text = text + System.Environment.NewLine + textBox1.Text + ": " + textBox2.Text;
                System.IO.File.WriteAllText(path, text);
                textBox2.Text = "";
            } else if (textBox1.Text == "") {
                MessageBox.Show("Please enter a name!");
            } else if (textBox2.Text == "") {
                MessageBox.Show("Please enter a message!");
            }
        }
    }
}
