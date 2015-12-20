
/*
 使用方法： VerificationImage image = new VerificationImage();
            string authCode = StringHelper.GetRnd(5, true, true, true, false, "");
            image.Width = 100;
            image.Height = 26;
            image.BorderWidth = 1;
            image.BadPiont = 200;
            image.StrukLineCount = 8;
            image.PatternCount = 6;
            image.BorderInsetColor = System.Drawing.Color.LightGray;
            image.BorderOutsetColor = System.Drawing.Color.Empty;
            image.Text = authCode;
            image.CreateImage();
            Session[application] = authCode.ToLower();
            return new ImageResult(image.Stream, "image/jpeg");
 
 
            Utility.VerificationImage dd = new Utility.VerificationImage();
            dd.Text = "r111111";
            dd.CreateImage();
            Response.ClearContent(); 
            Response.ContentType = "image/Jpeg ";
            Response.BinaryWrite(dd.Stream.ToArray()); 
 */

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Security.Cryptography;

namespace  Project.Infrastructure.FrameworkCore.ToolKit.ImageHandler
{
    /// <summary>
    /// 包含验证码图像生成的类。
    /// </summary>
    public class VerificationImage
    {

        #region 私有字段

        private int _Width = 80;
        private int _Height = 22;
        private int _BorderWidth = 1;
        private int _StrukLineCount = 4;
        private int _PatternCount = 4;
        private int _BadPiont = 50;
        private Color _BorderOutsetColor = Color.FromArgb(10, Color.Black);
        private Color _BorderInsetColor = Color.FromArgb(180, Color.LightGray);
        private MemoryStream _Stream;
        private string _Text;
        private Random _Rand;

        #endregion

        #region 构造函数

        /// <summary>
        /// 初始化验证码图像生成器。
        /// </summary>
        public VerificationImage()
        {
            byte[] b = new byte[4];
            new RNGCryptoServiceProvider().GetBytes(b);
            _Rand = new Random(BitConverter.ToInt32(b, 0));
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 从指定颜色获取一个随机笔刷。
        /// </summary>
        /// <param name="color">指定一种颜色。</param>
        /// <returns>Brush</returns>
        private Brush _GetBrush(Color color)
        {
            Color bgcolor = Color.Empty;
            Brush[] brushes = new Brush[] {
			new LinearGradientBrush(new Rectangle(1, 2, 3, 4), color, _BackColor, _Rand.Next(0, 360)),
			new LinearGradientBrush(new Rectangle(2, 2, 4, 3), color, _BackColor, _Rand.Next(0, 360)),
			new LinearGradientBrush(new Rectangle(1, 1, 2, 2), color, _BackColor, _Rand.Next(0, 360)),
			new LinearGradientBrush(new Rectangle(2, 2, 1, 1), color, _BackColor, _Rand.Next(0, 360)),
			new LinearGradientBrush(new Rectangle(3, 4, 1, 2), color, _BackColor, _Rand.Next(0, 360)),
			new LinearGradientBrush(new Rectangle(5, 5, 10, 10), color, _BackColor, _Rand.Next(0, 360)),
			new LinearGradientBrush(new Rectangle(5, 5, 20, 20), color, _BackColor, _Rand.Next(0, 360)),
			new LinearGradientBrush(new Rectangle(10, 10, 5, 5), color, _BackColor, _Rand.Next(0, 360)),
			new HatchBrush(HatchStyle.BackwardDiagonal, color, bgcolor),
			new HatchBrush(HatchStyle.Cross, color, bgcolor),
			new HatchBrush(HatchStyle.DarkHorizontal, color, bgcolor),
			new HatchBrush(HatchStyle.DarkVertical, color, bgcolor),
			new HatchBrush(HatchStyle.DashedDownwardDiagonal, color, bgcolor),
			new HatchBrush(HatchStyle.DashedHorizontal, color, bgcolor),
			new HatchBrush(HatchStyle.DashedVertical, color, bgcolor),
			new HatchBrush(HatchStyle.DiagonalBrick, color, bgcolor),
			new HatchBrush(HatchStyle.Divot, color, bgcolor),
			new HatchBrush(HatchStyle.DottedGrid, color, bgcolor),
			new HatchBrush(HatchStyle.LargeCheckerBoard, color, bgcolor),
			new HatchBrush(HatchStyle.LargeConfetti, color, bgcolor),
			new HatchBrush(HatchStyle.Plaid, color, bgcolor),
			new HatchBrush(HatchStyle.Shingle, color, bgcolor),
			new HatchBrush(HatchStyle.Sphere, color, bgcolor),
			new HatchBrush(HatchStyle.Weave, color, bgcolor),
		};
            return brushes[_Rand.Next(0, brushes.Length)];
        }



        /// <summary>
        /// 获取不超过指定大小的特定文本格式的实例。
        /// </summary>
        /// <param name="maxsize">文本格式的最大尺寸。</param>
        /// <returns>Font 	"Comic Sans MS",</returns>
        private Font _GetFont(int maxsize)
        {
            string[] fonts = new string[] { "Verdana", "Arial" };

            FontStyle[] styles = new FontStyle[] {
			FontStyle.Bold,
			FontStyle.Bold | FontStyle.Italic,
            };

            FontFamily ff = new FontFamily(fonts[_Rand.Next(0, fonts.Length)]);
            FontStyle fs = styles[_Rand.Next(0, styles.Length)];
            int size = maxsize;
            if (size > 12)
                size = _Rand.Next(6 + size / 2, size);
            else
                size = _Rand.Next(10, 12);

            Font f = new Font(ff, maxsize, fs);
            return f;
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 创建验证码图像。
        /// </summary>
        public void CreateImage()
        {
            if (_Text == null || _Text.Length == 0) throw new ArgumentNullException("Text", "没有指定要显示的验证码文本信息。");
            if (_BorderWidth > _Width / 2 || _BorderWidth > _Height / 2) throw new ArgumentNullException("BorderWidth", "边框设定值过大。");

            int charcount = _Text.Length;
            Bitmap image = new Bitmap(_Width, _Height);
            Graphics g = Graphics.FromImage(image);
            Pen pen;
            g.Clear(Color.White);
            int w = _Width - _BorderWidth;
            int h = _Height - _BorderWidth;

            // 背景
            g.FillRectangle(_GetBrush(_BackColor), 0, 0, _Width, _Height);

            // 图案
            for (int i = 1; i < _PatternCount; i++)
                g.FillPolygon(_GetBrush(_BackColor), new Point[] {
				new Point(_Rand.Next(_BorderWidth, w), _Rand.Next(_BorderWidth, h)),
				new Point(_Rand.Next(_BorderWidth, w), _Rand.Next(_BorderWidth, h)),
				new Point(_Rand.Next(_BorderWidth, w), _Rand.Next(_BorderWidth, h)),
				new Point(_Rand.Next(_BorderWidth, w), _Rand.Next(_BorderWidth, h)),
				new Point(_Rand.Next(_BorderWidth, w), _Rand.Next(_BorderWidth, h)),
				new Point(_Rand.Next(_BorderWidth, w), _Rand.Next(_BorderWidth, h)),
			}, FillMode.Winding);

            // 噪点
            pen = new Pen(Color.LightGray, 1);
            for (int i = 0; i < _BadPiont; i++)
            {
                int x = _Rand.Next(_BorderWidth, w);
                int y = _Rand.Next(_BorderWidth, h);
                g.DrawRectangle(pen, x, y, 1, 1);
            }

            w -= _BorderWidth;
            h -= _BorderWidth;
            int colwidth = w / charcount;

            // 输出不同字体和颜色的验证码字符
            for (int i = 0; i < _Text.Length; i++)
            {
                string text = _Text.Substring(i, 1);
                int fw = colwidth - colwidth / 3;
                int fh = 0;
                Font f = _GetFont(fw);

                fw = (int)Math.Ceiling(g.MeasureString(text, f).Width);
                fh = (int)Math.Ceiling(g.MeasureString(text, f).Height);

                int x = _BorderWidth + (i * colwidth + (colwidth - fw) / 2);
                int y = (h - fh) / 2;

                if (y > _BorderWidth)
                    y = _Rand.Next(_BorderWidth, y);
                else
                    y = _Rand.Next(_BorderWidth, _BorderWidth + 2);

                Matrix mat = new Matrix();
                mat.Shear((float)(0.3 - _Rand.Next(10) / 11), 0, MatrixOrder.Append);
                mat.RotateAt(18 - _Rand.Next(30), new PointF(x, y), MatrixOrder.Append);
                g.Transform = mat;

                g.DrawString(text, f, new SolidBrush(_BorderOutsetColor), x + 2, y + 2);
                g.DrawString(text, f, new SolidBrush(_BorderInsetColor), x + 1, y + 1);
                g.DrawString(text, f, new SolidBrush(_ForeColor), x, y);

                mat.Reset();
                g.Transform = mat;
                mat.Dispose();


                //g.DrawLine(Pens.Black, i * colwidth, 0, i * colwidth, _Height);
                //g.DrawLine(Pens.Red, i * colwidth, y, i * colwidth + colwidth, y);
            }



            // 干扰线
            pen = new Pen(_GetBrush(_BorderColor), 1);
            int n = _StrukLineCount / 2;
            for (int i = 1; i < n; i++)
            {
                g.DrawLine(pen, _Rand.Next(_BorderWidth, w - w / charcount), _Rand.Next(_BorderWidth, h / 4), _Rand.Next(_BorderWidth, w), _Rand.Next(_BorderWidth, h));
                g.DrawBezier(pen, _Rand.Next(_BorderWidth, w), _Rand.Next(_BorderWidth, h), _Rand.Next(_BorderWidth, w), _Rand.Next(_BorderWidth, h), _Rand.Next(_BorderWidth, w), _Rand.Next(_BorderWidth, h), _Rand.Next(_BorderWidth, w), _Rand.Next(_BorderWidth, h));
            }

            // 边框
            if (_BorderWidth > 0)
                g.DrawRectangle(new Pen(_BorderColor, _BorderWidth), _BorderWidth / 2, _BorderWidth / 2, image.Width - _BorderWidth, image.Height - _BorderWidth);

            // 输出
            _Stream = new MemoryStream();
            image.Save(_Stream, ImageFormat.Png);
            g.Dispose();
            image.Dispose();
        }

        #endregion

        #region 私有属性

        /// <summary>
        /// 设置或获取验证码前景颜色（随机）。
        /// </summary>
        private Color _ForeColor
        {
            get
            {
                Color[] colors = new Color[] {
				    Color.Black,
				    Color.Red,
				    Color.DarkBlue,
				    Color.DarkGreen,
				    Color.DarkMagenta,
				    Color.DarkRed,
				    Color.DarkViolet,
			    };
                return Color.FromArgb(_Rand.Next(192, 220), colors[_Rand.Next(0, colors.Length)]);
            }
        }


        /// <summary>
        /// 设置或获取验证码边框颜色（随机）。
        /// </summary>
        private Color _BorderColor
        {
            get
            {
                Color[] colors = new Color[] {
				    Color.Black,
				    Color.Red,
				    Color.DarkBlue,
				    Color.DarkCyan,
				    Color.DarkCyan,
				    Color.DarkGreen,
				    Color.DarkMagenta,
				    Color.DarkRed
			    };
                return Color.FromArgb(_Rand.Next(50, 80), colors[_Rand.Next(0, colors.Length)]);
            }
        }



        /// <summary>
        /// 设置或获取验证码背景颜色（随机）。
        /// </summary>
        private Color _BackColor
        {
            get
            {
                Color[] colors = new Color[] {
				    Color.FromArgb(255, 255, 255),
				    Color.FromArgb(192, 255, 255),
				    Color.FromArgb(255, 255, 192),
				    Color.FromArgb(153, 255, 255),
				    Color.FromArgb(192, 255, 153),
				    Color.FromArgb(255, 192, 255),
				    Color.FromArgb(255, 153, 255),
				    Color.FromArgb(192, 153, 255),
				    Color.FromArgb(51, 153, 255),
				    Color.FromArgb(255, 102, 255),
				    Color.FromArgb(51, 102, 0),
				    Color.FromArgb(51, 153, 0),
				    Color.FromArgb(255, 0, 0),
			    };
                return Color.FromArgb(_Rand.Next(10, 35), colors[_Rand.Next(0, colors.Length)]);
            }
        }

        #endregion

        #region 公有属性

        /// <summary>
        /// 设置或获取验证码图像的宽度。以像素为单位。
        /// </summary>
        public int Width
        {
            get { return _Width; }
            set { _Width = value; }
        }

        /// <summary>
        /// 设置或获取验证码图像的高度。以像素为单位。
        /// </summary>
        public int Height
        {
            get { return _Height; }
            set { _Height = value; }
        }


        /// <summary>
        /// 设置或获取验证码图像的边框大小。以像素为单位。
        /// </summary>
        public int BorderWidth
        {
            get { return _BorderWidth; }
            set { _BorderWidth = value; }
        }



        /// <summary>
        /// 设置或获取验证码图像的干扰线数量。干扰线将绘制于文字上方。
        /// </summary>
        public int StrukLineCount
        {
            get { return _StrukLineCount; }
            set { _StrukLineCount = value; }
        }

        /// <summary>
        /// 设置或获取验证码图像的干扰色块的数量。干扰色块将绘制于文字下方。
        /// </summary>
        public int PatternCount
        {
            get { return _PatternCount; }
            set { _PatternCount = value; }
        }


        /// <summary>
        /// 设置或获取验证码图像的干扰噪点的数量。干扰噪点将绘制于文字下方、干扰色块的上方。
        /// </summary>
        public int BadPiont
        {
            get { return _BadPiont; }
            set { _BadPiont = value; }
        }



        /// <summary>
        /// 设置或获取验证码文本信息外部阴影颜色。
        /// </summary>
        public Color BorderOutsetColor
        {
            get { return _BorderOutsetColor; }
            set { _BorderOutsetColor = value; }
        }

        /// <summary>
        /// 设置或获取验证码文本信息内部阴影颜色。
        /// </summary>
        public Color BorderInsetColor
        {
            get { return _BorderInsetColor; }
            set { _BorderInsetColor = value; }
        }

        /// <summary>
        /// 设置或获取包含验证码图像信息的二进制流。
        /// </summary>
        public MemoryStream Stream
        {
            get { return _Stream; }
            set { _Stream = value; }
        }

        /// <summary>
        /// 设置或获取验证码图像要显示的文本信息。
        /// </summary>
        public string Text
        {
            get { return _Text; }
            set { _Text = value; }
        }

        #endregion
    }
}
