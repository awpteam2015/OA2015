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
using Project.Infrastructure.FrameworkCore.DataNhibernate;
using Project.Service.PermissionManager;

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
            var t = UserInfoService.GetInstance().GetModel(1);

            var t2 = UserInfoService.GetInstance().GetModel(1);
           // Thread.CurrentThread.Name = "1111";
            textBox1.Text += Thread.CurrentThread.Name + "-----------"+t.UserName;

            SessionFactoryManager.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var t = UserInfoService.GetInstance().GetModel(1);
            textBox1.Text += Thread.CurrentThread.Name + "-----------" + t.UserName;
        }
    }
}
