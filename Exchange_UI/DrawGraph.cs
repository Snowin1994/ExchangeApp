using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Exchange_UI
{ 
    public class DrawGraph
    {
        private Graphics graph;
        private Pen pen;
        public const int xPercent = 14;
        public const int rectX = 655;
        public const int rectY = 23;
        public static Color lineColor = Color.FromArgb(80, 80, 80);        //线颜色

        public void PaintClear(Control con)
        {
            PaintRectangle(con,con.BackColor, new Point(0, 0), con.Size.Width, con.Size.Height);
        }
        /// <summary>
        /// 例子
        /// </summary>
        /// <param name="con"></param>
        /// <param name="color"></param>
        /// <param name="xStart"></param>
        /// <param name="yStart"></param>
        public void PaintTemp(Control con,Color color,int xStart,int yStart)
        {
            //创建一个Graphics对象
            graph = con.CreateGraphics();
            //定义画笔，里面的参数为画笔的颜色   
            pen = new Pen(color);   
            //画线的方法，第一个参数为起始点X的坐标，第二个参数为起始点Y的坐标；第三个参数为终点X的坐标，第四个参数为终点Y的坐标；
            graph.DrawLine(pen, xStart, yStart,10 ,11 );       
        }
        /// <summary>
        /// Draw a star where you want
        /// </summary>
        /// <param name="con">The location for drawing</param>
        /// <param name="color">What color</param>
        /// <param name="start">The point for start</param>
        public void PaintStar(Control con,Point start,Color color)
        {
            //Point start = new Point(48, 3);
            Point[] starLoc = new Point[10];
            starLoc[0] = start;
            starLoc[1].X = start.X - 2;
            starLoc[1].Y = start.Y + 6;
            starLoc[2].X = start.X - 8;
            starLoc[2].Y = start.Y + 6;
            starLoc[3].X = start.X - 4;
            starLoc[3].Y = start.Y + 10;
            starLoc[4].X = start.X - 6;
            starLoc[4].Y = start.Y + 16;
            starLoc[5].X = start.X;
            starLoc[5].Y = start.Y + 12;
            starLoc[6].X = start.X + 6;
            starLoc[6].Y = start.Y + 16;
            starLoc[7].X = start.X + 4;
            starLoc[7].Y = start.Y + 10;
            starLoc[8].X = start.X + 8;
            starLoc[8].Y = start.Y + 6;
            starLoc[9].X = start.X + 2;
            starLoc[9].Y = start.Y + 6;

            //创建一个Graphics对象
            graph = con.CreateGraphics();
            //定义画笔，里面的参数为画笔的颜色   
            pen = new Pen(color);
            //画线的方法，第一个参数为起始点X的坐标，第二个参数为起始点Y的坐标；第三个参数为终点X的坐标，第四个参数为终点Y的坐标；
            for (int i = 0; i < starLoc.Length - 1; i++)
            {
                graph.DrawLine(pen, starLoc[i].X, starLoc[i].Y, starLoc[i + 1].X, starLoc[i + 1].Y);
            }
            graph.DrawLine(pen, starLoc[0].X, starLoc[0].Y, starLoc[9].X, starLoc[9].Y);
            graph.FillPolygon(new SolidBrush(color), starLoc, FillMode.Winding);

            graph.Dispose();
        }
        /// <summary>
        /// 画圆点
        /// </summary>
        /// <param name="con"></param>
        /// <param name="color"></param>
        /// <param name="centerP"></param>
        /// <param name="radius"></param>
        public void PaintCir(Control con, Color color, Point centerP, int radius)
        {
            graph = con.CreateGraphics();
            pen = new Pen(color);
            graph.DrawEllipse(pen, centerP.X - radius, centerP.Y - radius, radius, radius);

            Brush bColor = new SolidBrush(color);
            graph.FillEllipse(bColor, centerP.X - radius, centerP.Y - radius, radius, radius);

            graph.Dispose();
        }
        /// <summary>
        /// 信号灯专用
        /// </summary>
        /// <param name="con"></param>
        /// <param name="color"></param>
        /// <param name="centerP"></param>
        /// <param name="radius"></param>
        public void PaintCir(Control con, Color color, Point centerP, int radius, int lightMark)
        {
            graph = con.CreateGraphics();
            //画白底（白边）
            //pen = new Pen(color);
            //graph.DrawEllipse(pen, centerP.X - radius, centerP.Y - radius, radius, radius);

            //Brush bColor = new SolidBrush(color);
            //graph.FillEllipse(bColor, centerP.X - radius, centerP.Y - radius, radius, radius);
            
            //画颜色
            pen = new Pen(Color.FromArgb(234, 234, 234),16);
            Pen pen1 = new Pen(con.BackColor, 15);
            graph.DrawEllipse(pen, centerP.X - radius, centerP.Y - radius, radius, radius);
            graph.DrawEllipse(pen1, centerP.X - radius - 10, centerP.Y - radius - 10, radius + 20, radius + 20);

            Brush bColor = new SolidBrush(color);
            graph.FillEllipse(bColor, centerP.X - radius, centerP.Y - radius, radius, radius);

            //graph.DrawEllipse(new Pen(con.BackColor,16), centerP.X - radius, centerP.Y - radius, radius + 10, radius + 10);

            graph.Dispose();
        }
        /// <summary>
        /// 画实心矩形
        /// </summary>
        /// <param name="con">画板</param>
        /// <param name="color">颜色</param>
        /// <param name="startPoint">起点坐标</param>
        /// <param name="lengthX">高度</param>
        /// <param name="widthY">宽度</param>
        public void PaintRectangle(Control con, Color color, Point startPoint, int lengthX, int widthY)
        {
            Point[] recLocation = new Point[4];
            recLocation[0] = startPoint;
            recLocation[1].X = startPoint.X;
            recLocation[1].Y = startPoint.Y + widthY;
            recLocation[2].X = startPoint.X + lengthX;
            recLocation[2].Y = startPoint.Y + widthY;
            recLocation[3].X = startPoint.X + lengthX;
            recLocation[3].Y = startPoint.Y;
            graph = con.CreateGraphics();
            pen = new Pen(color);
            graph.DrawLines(pen, recLocation);
            //Brush rColor = ChangeToBrush(color);
            graph.FillRectangle(new SolidBrush(color), startPoint.X, startPoint.Y, lengthX, widthY);

            graph.Dispose();
        }

        /// <summary>
        /// 画空心矩形 画线框专用
        /// </summary>
        /// <param name="con"></param>
        /// <param name="color"></param>
        /// <param name="startPoint"></param>
        /// <param name="lengthX"></param>
        /// <param name="widthY"></param>
        /// <param name="mark"></param>
        public void PaintRectangle(Control con, Color color, Point startPoint, int lengthX, int widthY,int mark)
        {
            Point[] recLocation = new Point[5];
            recLocation[0] = startPoint;
            recLocation[1].X = startPoint.X;
            recLocation[1].Y = startPoint.Y + widthY;
            recLocation[2].X = startPoint.X + lengthX;
            recLocation[2].Y = startPoint.Y + widthY;
            recLocation[3].X = startPoint.X + lengthX;
            recLocation[3].Y = startPoint.Y;
            recLocation[4] = recLocation[0];
            graph = con.CreateGraphics();
            pen = new Pen(color,2);
            graph.DrawLines(pen, recLocation);

            graph.Dispose();
        }
        /// <summary>
        /// 画空心矩形 影线专用
        /// </summary>
        /// <param name="con"></param>
        /// <param name="color"></param>
        /// <param name="startPoint"></param>
        /// <param name="lengthX"></param>
        /// <param name="widthY"></param>
        /// <param name="mark"></param>
        public void PaintRectangle(Control con, Color color, Point startPoint, int lengthX, int widthY, char key)
        {
            Point[] recLocation = new Point[5];
            recLocation[0] = startPoint;
            recLocation[1].X = startPoint.X;
            recLocation[1].Y = startPoint.Y + widthY;
            recLocation[2].X = startPoint.X + lengthX;
            recLocation[2].Y = startPoint.Y + widthY;
            recLocation[3].X = startPoint.X + lengthX;
            recLocation[3].Y = startPoint.Y;
            recLocation[4] = recLocation[0];
            graph = con.CreateGraphics();
            pen = new Pen(color, 1);
            graph.DrawLines(pen, recLocation);

            graph.Dispose();
        }
        /// <summary>
        /// 画实心矩形 不需画笔，重绘专用
        /// </summary>
        /// <param name="g"></param>
        /// <param name="con"></param>
        /// <param name="color"></param>
        /// <param name="startPoint"></param>
        /// <param name="lengthX"></param>
        /// <param name="widthY"></param>
        public void PaintRectangle(Graphics g, Control con, Color color, Point startPoint, int lengthX, int widthY)
        {
            Point[] recLocation = new Point[4];
            recLocation[0] = startPoint;
            recLocation[1].X = startPoint.X;
            recLocation[1].Y = startPoint.Y + widthY;
            recLocation[2].X = startPoint.X + lengthX;
            recLocation[2].Y = startPoint.Y + widthY;
            recLocation[3].X = startPoint.X + lengthX;
            recLocation[3].Y = startPoint.Y;
            pen = new Pen(color);
            g.DrawLines(pen, recLocation);
            Brush rColor = ChangeToBrush(color);
            g.FillRectangle(rColor, startPoint.X, startPoint.Y, lengthX, widthY);
        }
        /// <summary>
        /// 画弧
        /// </summary>
        /// <param name="con">画板</param>
        /// <param name="color">颜色</param>
        /// <param name="startP">起始坐标</param>
        /// <param name="location">左括号 or 右括号</param>
        public void PaintArc(Control con, Color color, Point startP, int location)
        {
            graph = con.CreateGraphics();
            pen = new Pen(color);

            PaintRectangle(con,color,startP,4,15);

            //if (location == 0)
            //    graph.DrawString("|", new Font("Kartika", 20, FontStyle.Bold), Brushes.DarkGray, startP);
            ////graph.DrawArc(pen, startP.X, startP.Y, 10, 20, 90, 180);    //zuo
            //else
            //    graph.DrawString("|", new Font("Kartika", 20, FontStyle.Bold), Brushes.DarkGray, startP);
                //graph.DrawArc(pen, startP.X, startP.Y, 13, 20, 270, 180);   //you

            graph.Dispose();
        }
        /// <summary>
        /// 画离场符号
        /// </summary>
        /// <param name="con"></param>
        /// <param name="startPoint"></param>
        public void PaintMark(Control con,Point startPoint,Color color)
        {
            if (color == con.BackColor)
                return;
            graph = con.CreateGraphics();
            pen = new Pen(color, 3);
            graph.DrawLine(pen, startPoint.X, startPoint.Y, startPoint.X + 12, startPoint.Y + 12);
            graph.DrawLine(pen, startPoint.X + 12, startPoint.Y, startPoint.X, startPoint.Y + 12);

            graph.Dispose();

        }
        /// <summary>
        /// 【画线】 日行图 分割线 
        /// </summary>
        /// <param name="con"></param>
        /// <param name="color"></param>
        /// <param name="startP"></param>
        /// <param name="endP"></param>
        public void PaintLine(Control con, Color color, Point startP, Point endP ,int mark)
        {
            float penWidth = 2f;      //画笔宽度

            graph = con.CreateGraphics();
            pen = new Pen(color, penWidth);
            graph.DrawLine(pen, startP, endP);

            graph.Dispose();
        }
        /// <summary>
        /// 【画线】画走势图坐标线，极暗细线
        /// </summary>
        /// <param name="con"></param>
        /// <param name="color"></param>
        /// <param name="startP"></param>
        /// <param name="endP"></param>
        public void PaintLine(Control con,Color color, Point startP,Point endP)
        {
            float penWidth = 0.1f;      //画笔宽度

            graph = con.CreateGraphics();
            pen = new Pen(color, penWidth);
            graph.DrawLine(pen, startP, endP);

            graph.Dispose();
        }
        public Brush ChangeToBrush(Color color) //Color [Red]
        {
            string c = color.ToString();
            switch(c)
            {
                case "Color [Red]": return Brushes.Red;
                case "Color [Green]": return Brushes.Green;
                case "Color [Gray]": return Brushes.Gray;
                case "Color [Orange]": return Brushes.Orange;
                case "Color [Black]": return Brushes.Black;
                case "Color [Yellow]": return Brushes.Yellow;   //Color [A=255, R=0, G=0, B=64]
                //case "Color [A=255, R=0, G=0, B=64]": return Brushes
                default: return Brushes.Green;
            }
        }

        /// <summary>
        /// 画字符串
        /// </summary>
        /// <param name="con"></param>
        /// <param name="p"></param>
        /// <param name="start"></param>
        internal void PaintStr(Control con, string p, Point start)
        {
            Brush lightGreen = new SolidBrush(Color.FromArgb(0, 252, 0));

            graph = con.CreateGraphics();
            if (p == "B")
            {
                graph.DrawString(p, new Font("Kartika", 10), lightGreen, start);
            }
            else
            {
                graph.DrawString(p, new Font("Kartika", 10), Brushes.Red, start);
            }
        }
        /// <summary>
        /// 擦除字符串
        /// </summary>
        /// <param name="con"></param>
        /// <param name="p"></param>
        /// <param name="start"></param>
        /// <param name="Mark">擦除标记</param>
        internal void PaintStr(Control con, string p, Point start, int Mark)
        {
            //graph = con.CreateGraphics();
            
            //graph.DrawString(p, new Font("Kartika", 10), new SolidBrush(con.BackColor), start);
            this.PaintRectangle(con, con.BackColor, start, 10, 15);

        }
    }
}