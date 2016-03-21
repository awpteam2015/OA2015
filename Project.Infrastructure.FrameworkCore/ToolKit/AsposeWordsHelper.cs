using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Aspose.Words;
using Aspose.Words.Saving;

namespace Project.Infrastructure.FrameworkCore.ToolKit
{

    public class AsposeWordsHelper
    {
        public static Tuple<bool, string> SaveWordByTemplate(string templatePath, Dictionary<string, string> dictRepalce, string savePath, double imgWidth = 100, double imgHeight = 100)
        {
            try
            {
                var doc = new Document(templatePath);
                foreach (var key in dictRepalce.Keys)
                {
                    if (!key.ToUpper().Contains("IMG"))
                    {
                        var repStr = string.Format("&{0}&", key);
                        var repacleStr = dictRepalce[key];
                        if (string.IsNullOrEmpty(dictRepalce[key]))
                            repacleStr = "";
                        // doc.Range.Replace(repStr, repacleStr, false, false);

                        Regex reg = new Regex($"&{key}&");
                        doc.Range.Replace(reg, new ReplaceList(repacleStr), false);
                    }
                    else
                    {
                        Regex reg = new Regex($"&{key}&");
                        doc.Range.Replace(reg, new ReplaceAndInsertImage(dictRepalce[key], imgWidth, imgHeight), false);
                    }
                }
                SaveOutputParameters ret = doc.Save(savePath);
                return new Tuple<bool, string>(true, savePath);
            }
            catch (Exception ex)
            {
                return new Tuple<bool, string>(false, ex.Message);
            }
        }
    }

    public class ReplaceAndInsertImage : IReplacingCallback
    {
        /// <summary>
        /// 需要插入的图片路径
        /// </summary>
        public string url { get; set; }

        public double imgWidth { get; set; } = 100;
        public double imgHeight { get; set; } = 100;

        public ReplaceAndInsertImage(string url)
        {
            this.url = url;
        }
        public ReplaceAndInsertImage(string url, double width, double height)
        {
            this.url = url;
            this.imgWidth = width;
            this.imgHeight = height;
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
            builder.InsertImage(url, imgWidth, imgHeight);
            return ReplaceAction.Replace;
        }
    }

    public class ReplaceList : IReplacingCallback
    {
        public string content { get; set; }
        public ReplaceList(string content)
        {
            this.content = content;
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
            builder.InsertHtml(content);
            return ReplaceAction.Replace;
        }
    }
}
