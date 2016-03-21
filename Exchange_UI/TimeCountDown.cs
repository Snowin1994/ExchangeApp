using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange_UI
{
    public class TimeCountDown : IComparable
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private MyTime countDown;

        public MyTime CountDown
        {
            get { return countDown; }
            set { countDown = value; }
        }

        public TimeCountDown(string n,MyTime c)
        {
            name = n;
            countDown = c;
        }

        public TimeCountDown(string source)
        {
            Name = source.Substring(0, 3);
            CountDown = new MyTime(source.Substring(3));
        }

        public TimeCountDown()
        {

        }

        public string ToStrForSave()
        {
            return Name + CountDown.ToStr();
        }

        public int CompareTo(Object obj)
        {
            int flg = 0;
            try
            {
                TimeCountDown sObj = (TimeCountDown)obj;
                flg = this.CountDown.CompareTo(sObj.CountDown); //先按照“天”进行排序    
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
