using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aspose.Words;
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
            textBox1.Text += Thread.CurrentThread.Name + "-----------" + t.UserName;

            SessionFactoryManager.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var t = UserInfoService.GetInstance().GetModel(1);
            textBox1.Text += Thread.CurrentThread.Name + "-----------" + t.UserName;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var dic = new Dictionary<string, string>();
            dic.Add("姓名", "杨幂");
            dic.Add("学历", "本科");
            dic.Add("联系方式", "02759597666");
            dic.Add("邮箱", "304885433@qq.com");
            dic.Add("头像", ".//1.jpg");
            ////使用书签操作
            //Document doc = new Document(".//1.doc");
            //DocumentBuilder builder = new DocumentBuilder(doc);
            //foreach (var key in dic.Keys)
            //{
            //    builder.MoveToBookmark(key);
            //    if (key != "头像")
            //    {
            //        builder.Write(dic[key]);
            //    }
            //    else
            //    {
            //        builder.InsertImage(dic[key]);
            //    }
            //}
            //doc.Save("书签操作.doc");//也可以保存为1.doc 兼容03-07
            //Console.WriteLine("已经完成书签操作");
            //使用特殊字符串替换
            var doc = new Document(".//2.doc");
            foreach (var key in dic.Keys)
            {
                if (key != "头像")
                {
                    var repStr = string.Format("&{0}&", key);
                    doc.Range.Replace(repStr, dic[key], false, false);
                }
                else
                {
                    Regex reg = new Regex("&头像&");
                    doc.Range.Replace(reg, new ReplaceAndInsertImage(".//1.jpg"), false);
                }
            }
            doc.Save("字符串替换操作.doc");//也可以保存为1.doc 兼容03-07
            //Console.WriteLine("已经完成特殊字符串替换操作");
            //Console.ReadKey();
        }

    }

    public class ReplaceAndInsertImage : IReplacingCallback
    {
        /// <summary>
        /// 需要插入的图片路径
        /// </summary>
        public string url { get; set; }

        public ReplaceAndInsertImage(string url)
        {
            this.url = url;
        }

        public ReplaceAction Replacing(ReplacingArgs e)
        {
            //获取当前节点
            var node = e.MatchNode;
            //获取当前文档
            Document doc = node.Document as Document;
            DocumentBuilder builder = new DocumentBuilder(doc);
            //将光标移动到指定节点
            builder.MoveTo(node);
            //插入图片
            builder.InsertImage(url);
            return ReplaceAction.Replace;
        }
    }


}
