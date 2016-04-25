using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange_UI
{
    public class MyTime : IComparable, ICloneable
    {
        private int hour;

        public int Hour
        {
            get { return hour; }
            set { hour = value; }
        }
        private int min;

        public int Min
        {
            get { return min; }
            set { min = value; }
        }

        private int day;

        public int Day
        {
            get { return day; }
            set { day = value; }
        }

        /// <summary>
        /// 当前时间
        /// </summary>
        public static MyTime nowTime;
        public static DateTime lastClosedAppTime;

        public static MyTime operator- (MyTime m1,MyTime m2)
        {
            int dayT, hourT, minT;
            if(m1.Hour >= m2.Hour)
            {
                if (m1.Min < m2.Min)
                {
                    minT = m1.Min + 60 - m2.Min;
                    hourT = m1.Hour - 1 - m2.Hour;
                }
                else
                {
                    minT = m1.Min - m2.Min;
                    hourT = m1.Hour - m2.Hour;
                }
                dayT = 0;
            }
            else
            {
                if (m1.Min < m2.Min)
                {
                    minT = m1.Min + 60 - m2.Min;
                    hourT = m1.Hour + 24 - 1 - m2.Hour;
                }
                else
                {
                    minT = m1.Min - m2.Min;
                    hourT = m1.Hour + 24 - m2.Hour;
                }
                dayT = 1;
            }

            return new MyTime(dayT, hourT, minT);
        }
        
        public static MyTime operator< (MyTime m,MyTime n)
        {
            
            if( m.Day < n.Day)
            {
                return m;
            }
            else if(m.Day > n.Day)
            {
                return n;
            }
            else
            {
                if(m.Hour < n.Hour)
                {
                    return m;
                }
                else if(m.Hour > n.Hour)
                {
                    return n;
                }
                else
                {
                    if (m.Min < n.Min)
                    {
                        return m;
                    }
                    else if (m.Min > n.Min)
                    {
                        return n;
                    }
                    else
                    {
                        return m;
                    }
                }
            }
        }
        public static MyTime operator> (MyTime m, MyTime n)
        {
            //if (m.Day < n.Day)
            //{
            //    return m;
            //}
            //else if (m.Day > n.Day)
            //{
            //    return n;
            //}
            //else
            //{
            //    if (m.Hour < n.Hour)
            //    {
            //        return m;
            //    }
            //    else if (m.Hour > n.Hour)
            //    {
            //        return n;
            //    }
            //    else
            //    {
            //        if (m.Min < n.Min)
            //        {
            //            return m;
            //        }
            //        else if (m.Min > n.Min)
            //        {
            //            return n;
            //        }
            //        else
            //        {
            //            return m;
            //        }
            //    }
            //}
            return nowTime;
        }

        public static bool operator== (MyTime m,MyTime n)
        {
            if(m.Day == n.Day && m.Hour == n.Hour && m.Min == n.Min)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool operator!= (MyTime m,MyTime n)
        {
            if(m == n)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 转成时间形式的字符串
        /// </summary>
        /// <returns></returns>
        public string ToStr()
        {
            string hourStr = this.Hour.ToString();
            string minStr = this.Min.ToString();

            if(this.Hour < 10)
            {
                hourStr = "0" + hourStr;
            }
            if(this.Min < 10)
            {
                minStr = "0" + minStr;
            }


            return hourStr + ":" + minStr;
        }

        public int CompareTo(Object obj)
        {
            int flg = 0;  
            try  
            {  
                MyTime sObj = (MyTime)obj;  
                flg = this.Day.CompareTo(sObj.Day); //先按照“天”进行排序    
                if (flg == 0)  //如果 天 相同，再按照“小时“的大小进行排序   
                {  
                    if (this.Hour > sObj.Hour)  
                    {  
                        flg = 1;  
                    }  
                    else if (this.Hour < sObj.Hour)  
                    {  
                        flg = -1;  
                    }
                    else
                    {
                        if (this.Min > sObj.Min)
                        {
                            flg = 1;
                        }
                        else if (this.Min < sObj.Min)
                        {
                            flg = -1;
                        }
                    }
                }  
            }
            catch (Exception ex)  
            {  
                throw new Exception("比较异常", ex.InnerException);  
            }  
            return flg;  
        }
        public static int GetTimeDiff(MyTime startTime)
        {
            MyTime timeDiff = MyTime.nowTime - startTime;

            return timeDiff.Min;
        }

        public MyTime(int d,int h,int m)
        {
            hour = h;
            min = m;
            day = d;
        }

        public MyTime(string source)
        {
            day = DateTime.Now.Day;
            hour = int.Parse(source.Substring(0, 2));
            min = int.Parse(source.Substring(3, 2));
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
