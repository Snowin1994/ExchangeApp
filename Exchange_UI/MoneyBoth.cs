using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange_UI
{
    public class MoneyBoth : ICloneable
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

        public char PosLocation
        {
            get { return posLocation; }
            set { posLocation = value; }
        }
        private int posNum;

        public int PosNum
        {
            get { return posNum; }
            set { posNum = value; }
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

        private int shortState = 0;
        /// <summary>
        /// 短观栏显示状态 0代表常态 1代表非常态
        /// </summary>
        public int ShortState
        {
            get { return shortState; }
            set { shortState = value; }
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

        public void InitPosNum( char key)
        {
            PosLocation = key;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
        
    }
}
