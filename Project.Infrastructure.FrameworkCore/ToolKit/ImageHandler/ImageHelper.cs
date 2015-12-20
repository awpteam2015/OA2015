using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Project.Infrastructure.FrameworkCore.ToolKit.ImageHandler
{
    public static class ImageHelper
    {
        
        [DllImport("Kernel32.dll", EntryPoint = "RtlMoveMemory", SetLastError = true)]
        private static extern void CopyMemory(IntPtr Dest, IntPtr src, int Length);                 // Marshal.Copy 居然没有从一个内存地址直接复制到另外一个内存的重载函数        

        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <param name="sizeConfig">尺寸：" 100_100|200|200 "</param>
        /// <param name="isCoverFile">当缩略图存在时，是否覆盖文件, 默认是True</param>
        /// <param name="isUsmSharpen">是否进行锐化效果</param>
        public static void GenerateThumbImg(string filename, string sizeConfig, bool isCoverFile = true, bool isUsmSharpen = false)
        {
            if (string.IsNullOrEmpty(sizeConfig)) return;
            var arr = sizeConfig.Split('|').ToList();
            if (arr.Count == 0) return;
            arr.ForEach(a =>
            {
                var size = a.Split('_');
                int width = int.Parse(size[0]);
                int height = int.Parse(size[1]);
                GenerateThumbImg(filename, width, height, isCoverFile, isUsmSharpen);
            });
        }

        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        /// <param name="isCoverFile">当缩略图存在时，是否覆盖文件, 默认是True</param>
        /// <param name="isUsmSharpen">是否进行锐化效果</param>
        public static void GenerateThumbImg(string filename, int width, int height, bool isCoverFile = true, bool isUsmSharpen = false)
        {
            // 原文件不存在,Return!
            if (!File.Exists(filename)) return;
            var newFileDir = Path.GetDirectoryName(filename) + "\\" + width + "_" + height + "\\";
            var newFileName = newFileDir + Path.GetFileName(filename);
            // 新文件已存在,Return!
            if (isCoverFile == false && File.Exists(newFileName)) return;
            // 新文件目录不存在，创建!
            if (!Directory.Exists(newFileDir)) Directory.CreateDirectory(newFileDir);

            // 生成缩略图
            using (Image img = Image.FromFile(filename))
            {
                if (img.Width == width && img.Height == height) { File.Delete(newFileName); File.Copy(filename, newFileName); return; }
                if (width > 0 && height == 0)
                    height = (int)Math.Round(((float)width / img.Width) * img.Height, 0, MidpointRounding.AwayFromZero);
                if (width == 0 && height > 0)
                    width = (int)Math.Round(((float)height / img.Height) * img.Width, 0, MidpointRounding.AwayFromZero);
                if (width > 0 && height > 0)
                {
                    Bitmap hb = new System.Drawing.Bitmap(width, height);//创建图片对象
                    System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(hb);//创建画板并加载空白图像
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;//设置保真模式为高度保真
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    Rectangle Rect = new Rectangle(0, 0, width, height);
                    g.DrawImage(img, Rect, 0, 0, img.Width, img.Height, GraphicsUnit.Pixel);//开始画图                                        
                    if (isUsmSharpen)
                        hb.UsmSharpen(ref Rect, 10, 50);
                    KiSaveAsJPEG(hb, newFileName, 100);
                    g.Dispose();
                    hb.Dispose();
                }
            }
        }

        /// <summary>
        /// 保存JPG时用
        /// </summary>
        /// <param name="mimeType"></param>
        /// <returns>得到指定mimeType的ImageCodecInfo</returns>
        private static ImageCodecInfo GetCodecInfo(string mimeType)
        {
            ImageCodecInfo[] CodecInfo = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo ici in CodecInfo)
            {
                if (ici.MimeType == mimeType) return ici;
            }
            return null;
        }

        /**/
        /// <summary>
        /// 保存为JPEG格式，支持压缩质量选项
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="FileName"></param>
        /// <param name="Qty"></param>
        /// <returns></returns>
        private static bool KiSaveAsJPEG(Bitmap bmp, string FileName, int Qty)
        {
            try
            {
                EncoderParameter p;
                EncoderParameters ps;
                ps = new EncoderParameters(1);
                p = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, Qty);
                ps.Param[0] = p;
                bmp.Save(FileName, GetCodecInfo("image/jpeg"), ps);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    // ***************** GDI+ Effect函数的示例代码 *********************
    // 作者     ： laviewpbt 
    // 作者简介 ： 对图像处理（非识别）有着较深程度的理解
    // 使用语言 ： VB6.0/C#/VB.NET
    // 联系方式 ： QQ-33184777  E-Mail:laviewpbt@sina.com
    // 开发时间 ： 2012.12.10-2012.12.12
    // 致谢     ： Aaron Lee Murgatroyd
    // 版权声明 ： 复制或转载请保留以上个人信息
    // *****************************************************************
    public static class GdipEffect
    {

        private static Guid BlurEffectGuid = new Guid("{633C80A4-1843-482B-9EF2-BE2834C5FDD4}");
        private static Guid UsmSharpenEffectGuid = new Guid("{63CBF3EE-C526-402C-8F71-62C540BF5142}");

        [StructLayout(LayoutKind.Sequential)]
        private struct BlurParameters
        {
            internal float Radius;
            internal bool ExpandEdges;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct SharpenParams
        {
            internal float Radius;
            internal float Amount;
        }

        internal enum PaletteType               // GDI+1.1还可以针对一副图像获取某种特殊的调色
        {
            PaletteTypeCustom = 0,
            PaletteTypeOptimal = 1,
            PaletteTypeFixedBW = 2,
            PaletteTypeFixedHalftone8 = 3,
            PaletteTypeFixedHalftone27 = 4,
            PaletteTypeFixedHalftone64 = 5,
            PaletteTypeFixedHalftone125 = 6,
            PaletteTypeFixedHalftone216 = 7,
            PaletteTypeFixedHalftone252 = 8,
            PaletteTypeFixedHalftone256 = 9
        };

        internal enum DitherType                    // 这个主要用于将真彩色图像转换为索引图像，并尽量减低颜色损失
        {
            DitherTypeNone = 0,
            DitherTypeSolid = 1,
            DitherTypeOrdered4x4 = 2,
            DitherTypeOrdered8x8 = 3,
            DitherTypeOrdered16x16 = 4,
            DitherTypeOrdered91x91 = 5,
            DitherTypeSpiral4x4 = 6,
            DitherTypeSpiral8x8 = 7,
            DitherTypeDualSpiral4x4 = 8,
            DitherTypeDualSpiral8x8 = 9,
            DitherTypeErrorDiffusion = 10
        }


        [DllImport("gdiplus.dll", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Unicode)]
        private static extern int GdipCreateEffect(Guid guid, out IntPtr effect);

        [DllImport("gdiplus.dll", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Unicode)]
        private static extern int GdipDeleteEffect(IntPtr effect);

        [DllImport("gdiplus.dll", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Unicode)]
        private static extern int GdipGetEffectParameterSize(IntPtr effect, out uint size);

        [DllImport("gdiplus.dll", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Unicode)]
        private static extern int GdipSetEffectParameters(IntPtr effect, IntPtr parameters, uint size);

        [DllImport("gdiplus.dll", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Unicode)]
        private static extern int GdipGetEffectParameters(IntPtr effect, ref uint size, IntPtr parameters);

        [DllImport("gdiplus.dll", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Unicode)]
        private static extern int GdipBitmapApplyEffect(IntPtr bitmap, IntPtr effect, ref Rectangle rectOfInterest, bool useAuxData, IntPtr auxData, int auxDataSize);

        [DllImport("gdiplus.dll", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Unicode)]
        private static extern int GdipBitmapCreateApplyEffect(ref IntPtr SrcBitmap, int numInputs, IntPtr effect, ref Rectangle rectOfInterest, ref Rectangle outputRect, out IntPtr outputBitmap, bool useAuxData, IntPtr auxData, int auxDataSize);


        // 这个函数我在C#下已经调用成功
        [DllImport("gdiplus.dll", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Unicode)]
        private static extern int GdipInitializePalette(IntPtr palette, int palettetype, int optimalColors, int useTransparentColor, int bitmap);

        // 该函数一致不成功，不过我在VB6下调用很简单，也很成功，主要是结构体的问题。
        [DllImport("gdiplus.dll", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Unicode)]
        private static extern int GdipBitmapConvertFormat(IntPtr bitmap, int pixelFormat, int dithertype, int palettetype, IntPtr palette, float alphaThresholdPercent);

        /// <summary>
        /// 获取对象的私有字段的值，感谢Aaron Lee Murgatroyd
        /// </summary>
        /// <typeparam name="TResult">字段的类型</typeparam>
        /// <param name="obj">要从其中获取字段值的对象</param>
        /// <param name="fieldName">字段的名称.</param>
        /// <returns>字段的值</returns>
        /// <exception cref="System.InvalidOperationException">无法找到该字段.</exception>
        /// 
        internal static TResult GetPrivateField<TResult>(this object obj, string fieldName)
        {
            if (obj == null) return default(TResult);
            Type ltType = obj.GetType();
            FieldInfo lfiFieldInfo = ltType.GetField(fieldName, System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            if (lfiFieldInfo != null)
                return (TResult)lfiFieldInfo.GetValue(obj);
            else
                throw new InvalidOperationException(string.Format("Instance field '{0}' could not be located in object of type '{1}'.", fieldName, obj.GetType().FullName));
        }

        public static IntPtr NativeHandle(this Bitmap Bmp)
        {
            return Bmp.GetPrivateField<IntPtr>("nativeImage");
            /*  用Reflector反编译System.Drawing.Dll可以看到Image类有如下的私有字段
                internal IntPtr nativeImage;
                private byte[] rawData;
                private object userData;
                然后还有一个 SetNativeImage函数
                internal void SetNativeImage(IntPtr handle)
                {
                    if (handle == IntPtr.Zero)
                    {
                        throw new ArgumentException(SR.GetString("NativeHandle0"), "handle");
                    }
                    this.nativeImage = handle;
                }
                这里在看看FromFile等等函数，其实也就是调用一些例如GdipLoadImageFromFile之类的GDIP函数，并把返回的GDIP图像句柄
                通过调用SetNativeImage赋值给变量nativeImage，因此如果我们能获得该值，就可以调用VS2010暂时还没有封装的GDIP函数
                进行相关处理了，并且由于.NET肯定已经初始化过了GDI+，我们也就无需在调用GdipStartup初始化他了。
             */
        }

        /// <summary>
        /// 对图像进行高斯模糊,参考：http://msdn.microsoft.com/en-us/library/ms534057(v=vs.85).aspx
        /// </summary>
        /// <param name="Rect">需要模糊的区域，会对该值进行边界的修正并返回.</param>
        /// <param name="Radius">指定高斯卷积核的半径，有效范围[0，255],半径越大，图像变得越模糊.</param>
        /// <param name="ExpandEdge">指定是否对边界进行扩展，设置为True，在边缘处可获得较为柔和的效果. </param>

        public static void GaussianBlur(this Bitmap Bmp, ref Rectangle Rect, float Radius = 10, bool ExpandEdge = false)
        {
            int Result;
            IntPtr BlurEffect;
            BlurParameters BlurPara;
            if ((Radius < 0) || (Radius > 255))
            {
                throw new ArgumentOutOfRangeException("半径必须在[0,255]范围内");
            }
            BlurPara.Radius = Radius;
            BlurPara.ExpandEdges = ExpandEdge;
            Result = GdipCreateEffect(BlurEffectGuid, out BlurEffect);
            if (Result == 0)
            {
                IntPtr Handle = Marshal.AllocHGlobal(Marshal.SizeOf(BlurPara));
                Marshal.StructureToPtr(BlurPara, Handle, true);
                GdipSetEffectParameters(BlurEffect, Handle, (uint)Marshal.SizeOf(BlurPara));
                GdipBitmapApplyEffect(Bmp.NativeHandle(), BlurEffect, ref Rect, false, IntPtr.Zero, 0);
                // 使用GdipBitmapCreateApplyEffect函数可以不改变原始的图像，而把模糊的结果写入到一个新的图像中
                GdipDeleteEffect(BlurEffect);
                Marshal.FreeHGlobal(Handle);
            }
            else
            {
                throw new ExternalException("不支持的GDI+版本，必须为GDI+1.1及以上版本，且操作系统要求为Win Vista及之后版本.");
            }
        }


        /// <summary>
        /// 对图像进行锐化,参考：http://msdn.microsoft.com/en-us/library/ms534073(v=vs.85).aspx
        /// </summary>
        /// <param name="Rect">需要锐化的区域，会对该值进行边界的修正并返回.</param>
        /// <param name="Radius">指定高斯卷积核的半径，有效范围[0，255],因为这个锐化算法是以高斯模糊为基础的，所以他的速度肯定比高斯模糊妈妈</param>
        /// <param name="ExpandEdge">指定锐化的程度，0表示不锐化。有效范围[0,255]. </param>
        /// 
        public static void UsmSharpen(this Bitmap Bmp, ref Rectangle Rect, float Radius = 10, float Amount = 50f)
        {
            int Result;
            IntPtr UnSharpMaskEffect;
            SharpenParams sharpenParams;
            if ((Radius < 0) || (Radius > 255))
            {
                throw new ArgumentOutOfRangeException("参数Radius必须在[0,255]范围内");
            }
            if ((Amount < 0) || (Amount > 100))
            {
                throw new ArgumentOutOfRangeException("参数Amount必须在[0,255]范围内");
            }
            sharpenParams.Radius = Radius;
            sharpenParams.Amount = Amount;
            Result = GdipCreateEffect(UsmSharpenEffectGuid, out UnSharpMaskEffect);
            if (Result == 0)
            {
                IntPtr Handle = Marshal.AllocHGlobal(Marshal.SizeOf(sharpenParams));
                Marshal.StructureToPtr(sharpenParams, Handle, true);
                GdipSetEffectParameters(UnSharpMaskEffect, Handle, (uint)Marshal.SizeOf(sharpenParams));
                GdipBitmapApplyEffect(Bmp.NativeHandle(), UnSharpMaskEffect, ref Rect, false, IntPtr.Zero, 0);
                GdipDeleteEffect(UnSharpMaskEffect);
                Marshal.FreeHGlobal(Handle);
            }
            else
            {
                throw new ExternalException("不支持的GDI+版本，必须为GDI+1.1及以上版本，且操作系统要求为Win Vista及之后版本.");
            }
        }
    }
}
