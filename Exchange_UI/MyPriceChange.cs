using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange_UI
{
    public class MyPriceChange : IComparable
    {
        string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        int priceChange;

        public int PriceChange
        {
            get { return priceChange; }
            set { priceChange = value; }
        }

        public MyPriceChange(string n,int p)
        {
            name = n;
            priceChange = p;
        }

        public int CompareTo(Object obj)
        {
            int flg = 0;
            try
            {
                MyPriceChange sObj = (MyPriceChange)obj;
                flg = this.PriceChange.CompareTo(sObj.PriceChange); //先按照“天”进行排序    
                if (flg == 0)  //如果 天 相同，再按照“小时“的大小进行排序   
                {

                }
            }
            catch (Exception ex)
            {
                throw new Exception("比较异常", ex.InnerException);
            }
            return flg;
        }
    }
}
