using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.IO;
using Microsoft.VisualBasic;
using System.Windows.Forms;
using System.Threading;

namespace Opendrive_chat
{
    public partial class Form1 : Form
    {

        private string path;

        delegate void SetTextCallback(string text);

        System.Threading.Timer timer;

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
            timer.Change(1000, Timeout.Infinite);
        }

        public Form1()
        {
            string defaultpath;
            if (!File.Exists("defaultpath.txt"))
            {
                File.Create("defaultpath.txt").Dispose();
                defaultpath = "w:\\Open\\Year 9\\james&aron.psd";
            }
            else {
                defaultpath = File.ReadAllText("defaultpath.txt");
            }
            path = Interaction.InputBox("Chat", "Enter chat file path:", defaultpath, 0, 0);
            if (!File.Exists(path))
            {
                File.Create(path).Dispose();
            }

            File.WriteAllText("defaultpath.txt", path);

            InitializeComponent();

            if (File.Exists("defaultname.txt"))
            {
                string text = File.ReadAllText("defaultname.txt");
                textBox1.Text = text;
            }
            else
            {
                File.Create("defaultname.txt").Dispose();
            }

            timer = new System.Threading.Timer((e) =>
            {
                refresh();
            }, null, 5000, Timeout.Infinite);
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            File.WriteAllText("defaultname.txt", textBox1.Text);
        }
    }
}
