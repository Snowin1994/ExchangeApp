using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Exchange_UI
{
    public class Money : ICloneable
    {
        private string name;    //货币名称

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private int order;      //货币排序 fenzhu.txt
        /// <summary>
        /// 货币排序值
        /// </summary>
        public int Order
        {
            get { return order; }
            set { order = value; }
        }

        public int[] fengzhong = new int[120];

        #region 倒计时相关
        //public DateTime[] wakeupTime = new DateTime[10];        //倒计时上限 10个
        public List<MyTime> wakeupList = new List<MyTime>();
        /// <summary>
        /// 货币倒计时的数目
        /// </summary>
        public int wpTimeNum = 0;

        #endregion

        private int shortM;     //短观值

        public int ShortM
        {
            get { return shortM; }
            set { shortM = value; }
        }
        private int longM;      //长观值

        public int LongM
        {
            get { return longM; }
            set { longM = value; }
        }
        private int lengthY;     //日行图 货币色块高度值

        public int LengthY
        {
            get { return lengthY; }
            set { lengthY = value; }
        }
        public int[] YLengthY = new int[4];

        private int colorX;      //日行图 货币色块宽度值及颜色

        public int ColorX
        {
            get { return colorX; }
            set { colorX = value; }
        }
        public int[] YColorX = new int[4];

        private int heightX;     //风向图 高度

        public int HeightX
        {
            get { return heightX; }
            set { heightX = value; }
        }
        private int orderLeft;      //风向图中 货币左侧的数字
        /// <summary>
        /// 风向图左侧数字的颜色的分界值
        /// </summary>
        public int OrderLeft
        {
            get { return orderLeft; }
            set { orderLeft = value; }
        }

        private int color = 0;
        /// <summary>
        /// 代表表格中短观值的底色 0代表绿色 1代表红色
        /// </summary>
        public int Color
        {
            get { return color; }
            set { color = value; }
        }

        private Color moneyColor;

        /// <summary>
        /// 表格中货币符号，走势图中该货币的线柱图，中轴的货币按钮的颜色
        /// </summary>
        public Color MoneyColor
        {
            get { return moneyColor; }
            set { moneyColor = value; }
        }

        /// <summary>
        /// 货币中 排序号前六的长观值之和
        /// </summary>
        public static int BeforeSixLongSum = 0;
        /// <summary>
        /// 货币中 排序号前六的短观值为正数的数目
        /// </summary>
        public static int ShortTrueNum = 0;

        public Money(string toName)
        {
            name = toName;
            MoneyColor = System.Drawing.Color.Gray;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        internal static void CheckCountDownLose()
        {
            foreach (Money m in DataFiler.basicMoney)
            {
                if (m.wakeupList.Count > 0)
                {
                    if ((m.wakeupList[0] < MyTime.nowTime) == m.wakeupList[0])
                    {
                        m.wakeupList.Remove(m.wakeupList[0]);
                    }
                }
            }
            
        }
    }
}
