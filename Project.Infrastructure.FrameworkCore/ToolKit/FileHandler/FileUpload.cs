/*******************************************************
版权所有：
用    途：文件上传

结构组成：

说    明：
作    者：李伟伟

创建日期：2009-12-16
历史记录：

*****************************************************
修改人员：               修改日期： 
修改说明：   
*******************************************************/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using Project.Infrastructure.FrameworkCore.ToolKit.ImageHandler;
using Project.Infrastructure.FrameworkCore.ToolKit.ImageHandler;
using Project.Infrastructure.FrameworkCore.ToolKit.NetWorkHandler;

namespace Project.Infrastructure.FrameworkCore.ToolKit.FileHandler
{
    /// <summary>
    /// 文件上传
    /// </summary>
    public class FileUpload
    {
        /// <summary>
        /// 文件上传保存
        /// </summary>
        /// <param name="postedFile"></param>
        /// <param name="storagePath">上传文件保存路径</param>
        /// <param name="allowFileSuffixs">允许上传的文件格式，如:jpg|gif|bmp</param>
        /// <param name="allowSize">允许上传的文件的大小</param>
        /// <param name="isOpen">上传后是否开放访问</param>
        /// <returns></returns>
        public static FileUploadInfo Save(HttpPostedFile postedFile, string storagePath, string allowFileSuffixs, int allowSize, bool isOpen)
        {
            return Save(postedFile, storagePath, allowFileSuffixs, allowSize, isOpen, null, null, false);
        }

        /// <summary>
        /// 图片文件上传保存
        /// </summary>
        /// <param name="postedFile"></param>
        /// <param name="storagePath">上传文件保存路径</param>
        /// <param name="allowFileSuffixs">允许上传的文件格式，如:jpg|gif|bmp</param>
        /// <param name="allowSize">允许上传的文件的大小</param>
        /// <param name="isOpen">上传后是否开放访问</param>
        /// <param name="listImageSizes">缩略图大小设置</param>
        /// <param name="isSaveSourceFile">是否保存原图片</param>
        /// <returns></returns>
        public static FileUploadInfo Save(HttpPostedFile postedFile, string storagePath, string allowFileSuffixs, int allowSize, bool isOpen, List<ImageSize> listImageSizes, bool isSaveSourceFile)
        {
            return Save(postedFile, storagePath, allowFileSuffixs, allowSize, isOpen, listImageSizes, null, isSaveSourceFile);
        }


        /// <summary>
        /// 图片文件上传保存
        /// </summary>
        /// <param name="postedFile"></param>
        /// <param name="storagePath">上传文件保存路径</param>
        /// <param name="allowFileSuffixs">允许上传的文件格式，如:jpg|gif|bmp</param>
        /// <param name="allowSize">允许上传的文件的大小</param>
        /// <param name="listImageSizes">缩略图大小</param>
        /// <param name="isSaveSourceFile">是否保存原来的上传的图片文件</param>
        /// <param name="isOpen">上传后是否开放访问</param>
        /// <param name="listImageSizes">缩略图大小设置</param>
        /// <param name="watermark">水印设置</param>
        /// <param name="isSaveSourceFile">是否保存原图片</param>
        /// <returns></returns>
        public static FileUploadInfo Save(HttpPostedFile postedFile, string storagePath, string allowFileSuffixs, int allowSize, bool isOpen, List<ImageSize> listImageSizes, Watermark watermark, bool isSaveSourceFile)
        {
            FileUploadInfo uploadFileInfo = new FileUploadInfo();

            #region 上传文件验证

            if (string.IsNullOrEmpty(postedFile.FileName))
            {
                uploadFileInfo.IsSucceed = false;
                uploadFileInfo.Message = "请选择要上传的文件！";
                return uploadFileInfo;
            }

            if (!Regex.IsMatch(postedFile.FileName, @"\.(?i:" + allowFileSuffixs + ")$"))
            {
                uploadFileInfo.IsSucceed = false;
                uploadFileInfo.Message = "上传的文件格式不符合要求！";
                return uploadFileInfo;
            }

            float fileSize = (float)postedFile.ContentLength / (float)1024;
            if (fileSize > allowSize)
            {
                uploadFileInfo.IsSucceed = false;
                uploadFileInfo.Message = "上传的文件大小不能大于" + allowSize + "KB！";
                return uploadFileInfo;
            }

            #endregion

            #region 构建上传文件路径和文件名称

            //判断 路径最后是否有 "/" ，如果没有就把它加上
            if (storagePath.LastIndexOf("/") != (storagePath.Length - 1))
            {
                storagePath = storagePath + "/";
            }

            //DateTime dt = DateTime.Today;
            //GregorianCalendar gc = new GregorianCalendar();
            //int weekOfYear = gc.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday);  //获取今天是今年的第几周
            //Convert36 digit36Date = Decimal.ToInt64(Decimal.Parse(dt.ToString("yyyyMM") + weekOfYear.ToString()));
            //storagePath = storagePath + digit36Date.ToString() + "/";

            string relativePath = storagePath;      //相对路径
            storagePath = AppDomainUtil.MapPath(storagePath);   //服务器绝对路径
            FileHelper.FolderCreate(storagePath);  //创建保存文件夹路径

            string filename = Path.GetFileName(postedFile.FileName);    //获得上传的文件名
            int position = filename.LastIndexOf(".");
            int length = filename.Length;
            string fileSuffix = filename.Substring(position, length - position);
            filename = filename.Substring(0, position);

            uploadFileInfo.FileName = filename;
            uploadFileInfo.FileSuffix = fileSuffix;
            uploadFileInfo.Size = fileSize;

            string guidFileName = Guid.NewGuid().ToString().Replace("-", "");
            //保存文件的名称
            string saveFileName = string.Empty;
            if (isOpen)
            {
                saveFileName = guidFileName + fileSuffix;
                uploadFileInfo.IsOpen = true;
            }
            else
            {
                saveFileName = guidFileName + ".config";
                uploadFileInfo.IsOpen = false;
            }
            //绝对物理路径
            string storagePathAndFile = storagePath + saveFileName;
            //相对路径
            string relativePathAndFile = relativePath + saveFileName;
            uploadFileInfo.Path = relativePathAndFile;
            #endregion

            #region 图片处理
            if (listImageSizes != null && listImageSizes.Count > 0)
            {
                Stream stream = postedFile.InputStream;

                if (isSaveSourceFile == true && watermark == null)
                {
                    //保存原文件
                    postedFile.SaveAs(storagePathAndFile);
                }

                #region 水印处理
                if (watermark != null)
                {
                    switch (watermark.Type)
                    {
                        case MarkType.Text:
                            if (isSaveSourceFile)
                            {
                                //都保存成JPG格式图片
                                storagePathAndFile = storagePath + guidFileName + ".jpg";
                                ImageUtil.AddTextWatermarkAsJPG(stream, watermark.MarkInfo, storagePathAndFile, watermark.Position);
                                FileStream fileStream = new FileStream(storagePathAndFile, FileMode.Open);
                                stream = fileStream;
                            }
                            else
                            {
                                stream = ImageUtil.AddTextWatermarkAsJPG(stream, watermark.MarkInfo, watermark.Position);
                            }
                            break;
                        case MarkType.Picture:
                            if (isSaveSourceFile)
                            {
                                //都保存成JPG格式图片
                                storagePathAndFile = storagePath + guidFileName + ".jpg";
                                ImageUtil.AddPicWatermarkAsJPG(stream, watermark.MarkInfo, storagePathAndFile, watermark.Position);
                                FileStream fileStream = new FileStream(storagePathAndFile, FileMode.Open);
                                stream = fileStream;
                            }
                            else
                            {
                                stream = ImageUtil.AddPicWatermarkAsJPG(stream, watermark.MarkInfo, watermark.Position);
                            }
                            break;
                    }
                }
                #endregion

                #region 图片缩略

                Image image = Image.FromStream(postedFile.InputStream);
                List<ImagePath> listImagePathAndFile = new List<ImagePath>();
                //图片文件名称
                string saveImageFile = string.Empty;
                //绝对物理路径
                string storagePathAndImageFile = string.Empty;
                //相对路径
                string relativePathAndImageFile = string.Empty;

                int imageSizeCount = listImageSizes.Count;
                if (imageSizeCount > 1)
                {
                    ImagePath imagePath = new ImagePath();
                    imagePath.Specification = "o";
                    imagePath.Path = relativePathAndFile;
                    listImagePathAndFile.Add(imagePath);
                }

                foreach (ImageSize imageSize in listImageSizes)
                {
                    if (image.Width > imageSize.Width || image.Height > imageSize.Height)
                    {
                        saveImageFile = guidFileName + "_" + imageSize.Width + "X" + imageSize.Height + ".jpg";
                        storagePathAndImageFile = storagePath + saveImageFile;
                        relativePathAndImageFile = relativePath + saveImageFile;
                        ImageUtil.ThumbAsJPG(stream, storagePathAndImageFile, imageSize.Width, imageSize.Height);
                    }
                    else
                    {
                        relativePathAndImageFile = relativePathAndFile;
                    }

                    if (imageSizeCount > 1)
                    {
                        ImagePath imagePath = new ImagePath();
                        imagePath.Specification = imageSize.Specification;
                        imagePath.Path = relativePathAndImageFile;
                        listImagePathAndFile.Add(imagePath);
                    }

                    if (imageSize.IsCurrent)
                    {
                        uploadFileInfo.Path = relativePathAndImageFile;
                    }
                }
                uploadFileInfo.ListImagePath = listImagePathAndFile;

                #endregion

                stream.Close();
                stream.Dispose();
                image.Dispose();

            }
            else
            {
                postedFile.SaveAs(storagePathAndFile);
            }
            #endregion

            uploadFileInfo.IsSucceed = true;
            return uploadFileInfo;
        }
    }

    #region 图片缩略尺寸类
    /// <summary>
    /// 图片缩略尺寸类
    /// </summary>
    public class ImageSize
    {
        /// <summary>
        /// 图片规格名称
        /// </summary>
        public string Specification { get; set; }

        /// <summary>
        /// 图片宽度
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// 图片高度
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// 是否是当前需要的尺寸规格
        /// </summary>
        public bool IsCurrent { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="width">图片宽度</param>
        /// <param name="height">图片高度</param>
        /// <param name="isCurrent">是否是当前需要的尺寸规格</param>
        public ImageSize(int width, int height, bool isCurrent)
        {
            Width = width;
            Height = height;
            IsCurrent = isCurrent;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="specification">图片规格名称</param>
        /// <param name="width">图片宽度</param>
        /// <param name="height">图片高度</param>
        /// <param name="isCurrent">是否是当前需要的尺寸规格</param>
        public ImageSize(string specification, int width, int height, bool isCurrent)
        {
            Specification = specification;
            Width = width;
            Height = height;
            IsCurrent = isCurrent;
        }

    }

    #endregion

    #region 图片水印信息
    /// <summary>
    /// 图片水印信息
    /// </summary>
    public class Watermark
    {
        /// <summary>
        /// 水印类型
        /// </summary>
        public MarkType Type { get; set; }

        /// <summary>
        /// 水印位置
        /// </summary>
        public MarkPosition Position { get; set; }

        /// <summary>
        /// 水印信息
        /// </summary>
        public string MarkInfo { get; set; }


        public Watermark(string markInfo, MarkType type, MarkPosition position)
        {
            MarkInfo = markInfo;
            Type = type;
            Position = position;
        }
    }

    #endregion

    #region 文件上传后返回的文件信息
    /// <summary>
    /// 文件上传后返回的文件信息
    /// </summary>
    public class FileUploadInfo
    {
        /// <summary>
        /// 是否上传成功
        /// </summary>
        public bool IsSucceed { get; set; }

        /// <summary>
        /// 上传验证出错时提示的信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 文件扩展名：用于授权下载（.config  -- .doc）
        /// </summary>
        public string FileSuffix { get; set; }

        /// <summary>
        /// 是否开放访问
        /// </summary>
        public bool IsOpen { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public float Size { get; set; }

        /// <summary>
        /// 返回当前需要的文件路径
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 所有规格图片的路径JSON格式的内容
        /// </summary>
        public List<ImagePath> ListImagePath { get; set; }

        /// <summary>
        /// 获取指定规格的图片名称的保存路径信息
        /// </summary>
        /// <param name="fileName">原图文件名</param>
        /// <param name="sizeSpecification">指定规格：55X55</param>
        /// <returns></returns>
        public string GetImagePath(string fileName, string sizeSpecification)
        {
            string resultPathFile = string.Empty;
            string imageName = fileName + "_" + sizeSpecification + ".jpg";
            foreach (ImagePath imagePath in ListImagePath)
            {
                int startIndex = imagePath.Path.LastIndexOf('/');
                string name = imagePath.Path.Substring(startIndex);
                if (string.Compare(imageName, name, true) == 0)
                    resultPathFile = imagePath.Path;
            }
            return resultPathFile;
        }
    }
    #endregion

    #region 图片存储路径
    public class ImagePath
    {
        /// <summary>
        /// 图片规格名称
        /// </summary>
        public string Specification { get; set; }

        /// <summary>
        /// 图片路径
        /// </summary>
        public string Path { get; set; }
    }
    #endregion

}
