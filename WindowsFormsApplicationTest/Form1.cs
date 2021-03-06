﻿using System;
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
using System.Xml.Linq;
using Project.Infrastructure.FrameworkCore.DataNhibernate;
using Project.Infrastructure.FrameworkCore.Logging;
using Project.Model.PermissionManager;
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

      

        private void button4_Click(object sender, EventArgs e)
        {
            #region 动态调用 webservices地址

            try
            {
                //正式地址 ：http://10.173.64.156:8085/platform/services/HR/getStringHRdept
                // Http Post 请求响应方式
                //string url = m_WebServiceUrl + EMethod.Add.ToString();  //@"http://localhost:25060/testService.asmx/Add";
                // Dictionary<string, string> parameters = new Dictionary<string, string> { { "parameter1", TextBox1.Text }, { "parameter2", TextBox2.Text } };

                // string result = HttpHelper.Helper.GetResponseString("http://10.173.64.156:8085/platform/services/HR/getStringHRdept", "Get", new Dictionary<string, string> { }, Encoding.Default, Encoding.UTF8, 10000);


                var Third_DepartmentWebServiceUrl = "http://10.173.64.156:8085/platform/services/HR/getStringHRdept";


                if (Third_DepartmentWebServiceUrl != "")
                {
                    // UpdateTime = DateTime.Now;

                    string result = HttpHelper.Helper.GetResponseString(Third_DepartmentWebServiceUrl, "Get", new Dictionary<string, string> { }, Encoding.Default, Encoding.UTF8, 10000);

                    //XDocument doc = XDocument.Parse(result);
                     //var path = System.Environment.CurrentDirectory + "/XMLFile1.xml";

                   // XDocument doc1 = XDocument.Load(path);

                    //LoggerHelper.Info("get：" + HTMLDecodeNew(doc1+""));
                    var htmlstr = HTMLDecodeNew(result + "");
                    var start = htmlstr.IndexOf("?xml") - 1;
                    var end = htmlstr.LastIndexOf("/Response") + 11;
                    XDocument doc = XDocument.Parse(htmlstr.Substring(start, end-start));
                    //XDocument doc = XDocument.Load(path);
                    var text = from t in doc.Descendants("dept")                    //定位到节点 
                               select new DepatmentTemp()
                               {
                                   JGDM = t.Element("jgdm").Value,
                                   JGBM = t.Element("jgbm").Value,
                                   JGMC = t.Element("jgmc").Value,
                                   KSMC = t.Element("ksmc").Value,
                                   KSDM = t.Element("ksdm").Value,
                                   KSFJBM = t.Element("ksfjbm").Value,
                                   TYPE = t.Element("type").Value,
                               };
                    var allList = text.ToList();
                    LoggerHelper.Info("getCount：" + allList.Count);
                    var departmentSecondList = new List<DepartmentEntity>();

                    var departmentThirdList = new List<DepartmentEntity>();

                    var secondlist1 = allList.Where(p => p.JGBM == "330110");

                    foreach (var a in secondlist1)
                    {
                        departmentSecondList.Add(new DepartmentEntity()
                        {
                            DepartmentCode = a.KSDM,
                            DepartmentName = a.KSMC,
                            ParentDepartmentCode = "330110"
                        });
                    }

                    var secondlist2 = allList.Where(p => p.JGBM != "330110");
                    foreach (var a in secondlist2)
                    {
                        departmentSecondList.Add(new DepartmentEntity()
                        {
                            DepartmentCode = a.JGDM,
                            DepartmentName = a.JGMC,
                            ParentDepartmentCode = a.JGDM == "330110" ? "0" : "330110"
                        });

                        departmentThirdList.Add(new DepartmentEntity()
                        {
                            DepartmentCode = a.KSDM,
                            DepartmentName = a.KSMC,
                            ParentDepartmentCode = a.JGDM
                        });
                    }
                    LoggerHelper.Info("get2Count：" + departmentSecondList.Count);
                    LoggerHelper.Info("get3Count：" + departmentThirdList.Count);
                    departmentSecondList.ForEach(p =>
                    {
                        DepartmentService.GetInstance().Add(p);
                    });

                    departmentThirdList.ForEach(p =>
                    {
                        DepartmentService.GetInstance().Add(p);
                    });



                }
                //超时时间请设置久点
                //XElement root = XElement.Parse(result);
                //   root.

                //textBox1.Text = root.Value;
            }

            catch (Exception ex)
            {
                textBox1.Text = ex.Message;
                MessageBox.Show(ex.Message);
            }

            #endregion
        }

        public static string HTMLDecodeNew(string sText)
        {
            string stroutput = sText;

            stroutput = stroutput.Replace("&quot;", "\"");
            stroutput = stroutput.Replace("&lt;", "<");
            stroutput = stroutput.Replace("&gt;", ">");
            stroutput = stroutput.Replace("&#146;", "\'");
            stroutput = stroutput.Replace("&nbsp;", " ");
            stroutput = stroutput.Replace("<br>", "\r");
            stroutput = stroutput.Replace("&nbsp;&nbsp;&nbsp;&nbsp;", "\t");


            return stroutput.Replace("&nbsp;", " ").Replace("<br />", "\r\n");
        }
    }
    

    public class DepatmentTemp
    {
        /// <summary>
        /// 机构代码
        /// </summary>
        public string JGDM { get; set; }


        /// <summary>
        /// 医疗机构编码
        /// </summary>
        public string JGBM { get; set; }

        /// <summary>
        /// 机构名称
        /// </summary>
        public string JGMC { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        public string KSMC { get; set; }

        /// <summary>
        /// 科室代码
        /// </summary>
        public string KSDM { get; set; }

        /// <summary>
        /// 科室父级编码
        /// </summary>
        public string KSFJBM { get; set; }

        /// <summary>
        ///  机构/科室状态代码
        /// </summary>
        public string TYPE { get; set; }
    }
}
