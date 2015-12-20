using System.Data;
using System.Xml;
using Project.Infrastructure.FrameworkCore.ToolKit.NetWorkHandler;

namespace  Project.Infrastructure.FrameworkCore.ToolKit
{
    /// <summary>
    /// XmlHelper类提供对XML数据库的读写
    /// </summary>
    public class XmlHelper
    {
        //声明一个XmlDocument空对象
        protected XmlDocument XmlDoc = new XmlDocument();
        /// <summary>
        /// 构造函数，导入Xml文件
        /// </summary>
        /// <param name="xmlFile">文件虚拟路径</param>
        public XmlHelper(string xmlFile)
        {       
                XmlDoc.Load(GetXmlFilePath(xmlFile));   //载入Xml文档 
        }
        /// <summary>
        /// 析构函数
        /// </summary>
        ~XmlHelper()
        {
            XmlDoc = null;  //释放XmlDocument对象
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="filePath">文件虚拟路径</param>
        public void Save(string filePath)
        {
            try
            {
                XmlDoc.Save(GetXmlFilePath(filePath));
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 返回Xml文件实际路径
        /// </summary>
        /// <param name="xmlFile">文件虚拟路径</param>
        /// <returns></returns>
        public string GetXmlFilePath(string xmlFile)
        {
            return AppDomainUtil.MapPath(xmlFile);
        }

        /// <summary>
        /// 根据Xml文件的节点路径，返回一个DataSet数据集
        /// </summary>
        /// <param name="XmlPathNode">Xml文件的某个节点</param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(string XmlPathNode)
        {
            DataSet ds = new DataSet();
            try
            {
                System.IO.StringReader read = new System.IO.StringReader(XmlDoc.SelectSingleNode(XmlPathNode).OuterXml);
                ds.ReadXml(read);   //利用DataSet的ReadXml方法读取StringReader文件流
                read.Close();
            }
            catch
            { }
            return ds;
        }

        /// <summary>
        /// 属性查询，返回属性值
        /// </summary>
        /// <param name="XmlPathNode">属性所在的节点</param>
        /// <param name="Attribute">属性</param>
        /// <returns></returns>
        public string SelectAttribute(string XmlPathNode, string Attribute)
        {
            string _strNode = "";
            try
            {
                _strNode = XmlDoc.SelectSingleNode(XmlPathNode).Attributes[Attribute].Value;
            }
            catch
            {
                _strNode = null;
            }
            return _strNode;
        }
        /// <summary>
        /// 属性查询，返回属性值
        /// </summary>
        /// <param name="XmlPathNode">属性所在的节点</param>
        /// <param name="Attribute">属性</param>
        /// <returns></returns>
        public string SelectID(string XmlPathNode, int Number)
        {
            string _strNode = "";
            try
            {
                XmlNode root = XmlDoc.SelectSingleNode(XmlPathNode);
                _strNode = root.LastChild.ChildNodes[0].InnerText;
            }
            catch
            {
                _strNode = "";
            }
            return _strNode;
        }
        /// <summary>
        /// 节点查询，返回节点值
        /// </summary>
        /// <param name="XmlPathNode">节点的路径</param>
        /// <returns></returns>
        public string SelectNodeText(string XmlPathNode)
        {
            try
            {
                XmlNode node = XmlDoc.SelectSingleNode(XmlPathNode);
                return node.InnerText;
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 节点值查询判断
        /// </summary>
        /// <param name="XmlPathNode">父节点</param>
        /// <param name="index">节点索引</param>
        /// <param name="NodeText">节点值</param>
        /// <returns></returns>
        public int SelectNode(string XmlPathNode, int index, string NodeText)
        {
            try
            {
                XmlNodeList _NodeList = XmlDoc.SelectNodes(XmlPathNode);
                //循环遍历节点，查询是否存在该节点
                for (int i = 0; i < _NodeList.Count; i++)
                {
                    if (_NodeList[i].ChildNodes[index].InnerText == NodeText)
                    {
                        return 1;
                    }
                }
            }
            catch
            {
                return 0;
            }
            return 0;
        }

        /// <summary>
        /// 获取子节点个数
        /// </summary>
        /// <param name="XmlPathNode">父节点</param>
        /// <returns></returns>
        public int NodeCount(string XmlPathNode)
        {
            int i = 0;
            try
            {
                i = XmlDoc.SelectSingleNode(XmlPathNode).ChildNodes.Count;
            }
            catch
            {
                i = 0;
            }
            return i;
        }

        /// <summary>
        /// 更新一个节点的内容
        /// </summary>
        /// <param name="XmlPathNode">节点的路径</param>
        /// <param name="Content">新的节点值</param>
        /// <returns></returns>
        public int UpdateNode(string XmlPathNode, string NodeContent)
        {
            try
            {
                XmlDoc.SelectSingleNode(XmlPathNode).InnerText = NodeContent;
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 更新N个节点值
        /// </summary>
        /// <param name="XmlParentNode">父节点</param>
        /// <param name="XmlNode">子节点</param>
        /// <param name="NodeContent">子节点内容</param>
        /// <returns></returns>
        public int UpdateNode(string XmlParentNode, string[] XmlNode, string[] NodeContent)
        {
            try
            {
                //根据节点数组循环修改节点值
                for (int i = 0; i < XmlNode.Length; i++)
                {
                    XmlDoc.SelectSingleNode(XmlParentNode + "/" + XmlNode[i]).InnerText = NodeContent[i];
                }
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 修改属性
        /// </summary>
        /// <param name="XmlPathNode">属性所在的节点</param>
        /// <param name="Attribute">属性名</param>
        /// <param name="Content">属性值</param>
        /// <returns></returns>
        public int UpdateAttribute(string XmlPathNode, string Attribute, string AttributeContent)
        {
            try
            {
                //修改属性值
                XmlDoc.SelectSingleNode(XmlPathNode).Attributes[Attribute].Value = AttributeContent;
                return 1;
            }
            catch
            {
                return 0;
            }
        }


        /// <summary>
        /// 添加属性
        /// </summary>
        /// <param name="MainNode">属性所在节点</param>
        /// <param name="Attribute">属性名</param>
        /// <param name="AttributeContent">属性值</param>
        /// <returns></returns>
        public int InsertAttribute(string MainNode, string Attribute, string AttributeContent)
        {
            try
            {
                XmlElement objNode = (XmlElement)XmlDoc.SelectSingleNode(MainNode); //强制转化成XmlElement对象
                objNode.SetAttribute(Attribute, AttributeContent);    //XmlElement对象添加属性方法    
                return 1;
            }
            catch
            {
                return 0;
            }
        }


        /// <summary>
        /// 插入一个节点，带N个子节点
        /// </summary>
        /// <param name="MainNode">插入节点的父节点</param>
        /// <param name="ChildNode">插入节点的元素名</param>
        /// <param name="Element">插入节点的子节点名数组</param>
        /// <param name="Content">插入节点的子节点内容数组</param>
        /// <returns></returns>
        public int InsertNode(string MainNode, string ChildNode, string[] Element, string[] Content)
        {
            try
            {
                XmlNode objRootNode = XmlDoc.SelectSingleNode(MainNode);    //声明XmlNode对象
                XmlElement objChildNode = XmlDoc.CreateElement(ChildNode);  //创建XmlElement对象
                objRootNode.AppendChild(objChildNode);
                for (int i = 0; i < Element.Length; i++)    //循环插入节点元素
                {
                    XmlElement objElement = XmlDoc.CreateElement(Element[i]);
                    objElement.InnerText = Content[i];
                    objChildNode.AppendChild(objElement);
                }
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 删除一个节点
        /// </summary>
        /// <param name="Node">要删除的节点</param>
        /// <returns></returns>
        public int DeleteNode(string Node)
        {
            try
            {
                //XmlNode的RemoveChild方法来删除节点及其所有子节点
                XmlDoc.SelectSingleNode(Node).ParentNode.RemoveChild(XmlDoc.SelectSingleNode(Node));
                return 1;
            }
            catch
            {
                return 0;
            }
        }
    }
}
