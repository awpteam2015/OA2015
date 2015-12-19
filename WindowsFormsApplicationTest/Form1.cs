using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplicationTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        var t=    Thread.CurrentThread.ManagedThreadId.ToString(); 
           // Thread.CurrentThread.Name = "1111";
            textBox1.Text += Thread.CurrentThread.Name + "-----------"+t;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var t = Thread.CurrentThread.ManagedThreadId.ToString();
            textBox1.Text += Thread.CurrentThread.Name + "-----------" + t; ;
        }
    }
}
