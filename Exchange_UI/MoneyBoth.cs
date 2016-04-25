using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange_UI
{
    public class MoneyBoth : ICloneable,IComparable
    {
        public Money moneyA;
        private Money moneyB;

        public Money MoneyB
        {
            get { return moneyB; }
            set { moneyB = value; UpdateName(); }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private int diancha;

        public int Diancha
        {
            get { return diancha; }
            set { diancha = value; }
        }
        private char posLocation;
        /// <summary>
        /// 左侧的位置栏位置标记
        /// </summary>
        public char PosLocation
        {
            get { return posLocation; }
            set { posLocation = value; }
        }
        private char posLocationR;
        /// <summary>
        /// 右侧的位置栏位置标记
        /// </summary>
        public char PosLocationR
        {
            get { return posLocationR; }
            set { posLocationR = value; }
        }

        private int posNum;
        /// <summary>
        /// 左侧的数字
        /// </summary>
        public int PosNum
        {
            get { return posNum; }
            set { posNum = value; }
        }
        private int posNumR;
        /// <summary>
        /// 右侧的数字
        /// </summary>
        public int PosNumR
        {
            get { return posNumR; }
            set { posNumR = value; }
        }
        private char buyOrSell;

        public char BuyOrSell
        {
            get { return buyOrSell; }
            set { buyOrSell = value; }
        }
        private int buyOrSellNum;

        public int BuyOrSellNum
        {
            get { return buyOrSellNum; }
            set { buyOrSellNum = value; }
        }
        private int priceChange;

        public int PriceChange
        {
            get { return priceChange; }
            set { priceChange = value; }
        }
        private int hxcNum;
        /// <summary>
        /// 读取到的hxc.txt文件数据
        /// </summary>
        public int HxcNum
        {
            get { return hxcNum; }
            set { hxcNum = value; }
        }
        private int ordersum = 0;
        /// <summary>
        /// 两个货币的排序号之和
        /// </summary>
        public int Ordersum
        {
            get { return ordersum; }
            set { ordersum = value; }
        }

        private int shortState = 0;
        /// <summary>
        /// 短观栏显示状态 0代表常态 1代表非常态
        /// </summary>
        public int ShortState
        {
            get { return shortState; }
            set { shortState = value; }
        }

        private bool cirMarkState = false;

        /// <summary>
        /// 绿色提示符的是否出现
        /// </summary>
        public bool CirMarkState
        {
            get { return cirMarkState; }
            set { cirMarkState = value; }
        }

        private int cirMarkPlayNum = 0;
        /// <summary>
        /// 绿色提示符 提示音播放次数
        /// </summary>
        public int CirMarkPlayNum
        {
            get { return cirMarkPlayNum; }
            set { cirMarkPlayNum = value; }
        }

        private bool bsState = false;
        /// <summary>
        /// 标记是否出现买卖符号
        /// </summary>
        public bool BsState
        {
            get { return bsState; }
            set { bsState = value; }
        }

        private bool outState = false;
        /// <summary>
        /// 标记是否出现离场符号
        /// </summary>
        public bool OutState
        {
            get { return outState; }
            set { outState = value; }
        }

        private bool doubleGreen = false;
        /// <summary>
        /// 标记是否为非常态显示
        /// </summary>
        public bool DoubleGreen
        {
            get { return doubleGreen; }
            set { doubleGreen = value; }
        }

        private int redLineLoc = 0;

        /// <summary>
        /// 叠加走势图中红线的位置
        /// </summary>
        public int RedLineLoc
        {
            get { return redLineLoc; }
            set { redLineLoc = value; }
        }

        private int redLineOldLoc = 0;
        /// <summary>
        /// 叠加走势图中红线的上一次的位置
        /// </summary>
        public int RedLineOldLoc
        {
            get { return redLineOldLoc; }
            set { redLineOldLoc = value; }
        }

        public MoneyBoth()
        {
            moneyA = new Money("AAA");
            moneyB = new Money("BBB");
            this.Name = moneyA.Name + moneyB.Name;

        }
        public void UpdateName()
        {
            this.Name = moneyA.Name + MoneyB.Name;
        }

        public void InitPosNum( char key, int num)
        {
            PosLocation = key;
            PosLocationR = key;
            PosNum = num;
            PosNumR = num;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
        public int CompareTo(object obj)
        {
            int result;
            try
            {
                 MoneyBoth info = obj as MoneyBoth;
                 if (this.Ordersum < info.Ordersum)
                 {
                     result = 0;
                 }
                 else
                     result = 1;
                 return result;
             }
             catch (Exception ex) { throw new Exception(ex.Message); }
         }
    }
}
