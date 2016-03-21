using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Exchange_UI
{
    public class DataFiler
    {
        public static string basicFormText = "外汇分析系统 Version2016正式版                   ";

        public static Money[] basicMoney = new Money[8];    
        public static MoneyBoth[] basicMoneyBoth = new MoneyBoth[28];
        public static MoneyGroup[] basicMoneyGroup = new MoneyGroup[7];
        public DataFiler()
        {
            basicMoney[0] = new Money("USD");
            basicMoney[1] = new Money("JPY");
            basicMoney[2] = new Money("EUR");
            basicMoney[3] = new Money("GBP");
            basicMoney[4] = new Money("CHF");
            basicMoney[5] = new Money("CAD");
            basicMoney[6] = new Money("AUD");
            basicMoney[7] = new Money("NZD");

            for (int i = 0; i < basicMoneyBoth.Length; i++)
            {
                basicMoneyBoth[i] = new MoneyBoth();
            }
            for (int i = 0; i < basicMoneyGroup.Length; i++)
            {
                basicMoneyGroup[i] = new MoneyGroup(basicMoneyGroup.Length-i);
            }
        }

        public void fenzhu()
        {
            try
            {
                StreamReader sr = new StreamReader(Setup.pathTemp + "/fenzhu.txt", Encoding.Default);
                string line;
                int lineIndex = 0;      //行计数器
                int moneyBothIndex = 0;   //货币对计数器
                string[] Data;
                while ((line = sr.ReadLine()) != null)  //USD1;USDJPY;USDCAD;USDCHF;EURUSD;GBPUSD;AUDUSD;NZDUSD
                {                                //NZD1;NZDUSD.lmx;EURNZD.lmx;GBPNZD.lmx;NZDJPY.lmx;NZDCAD.lmx;AUDNZD.lmx;NZDCHF.lmx
                    Data = line.Split(new char[] { ';' });
                    FindMoney(Data[0].Substring(0, 3)).Order = int.Parse(Data[0].Substring(3, 1));    //为货币的次序赋值
                    for (int i = 1; i < 8 - lineIndex; i++)
                    {
                        string firstPart = Data[i].Substring(0, 3);
                        basicMoneyBoth[moneyBothIndex].moneyA = FindMoney(firstPart);  //解析USDJPY
                        string secondPart = Data[i].Substring(3, 3);
                        basicMoneyBoth[moneyBothIndex].MoneyB = FindMoney(secondPart);  //解析USDJPY

                        basicMoneyGroup[lineIndex].allMoneyBoth[i - 1] = basicMoneyBoth[moneyBothIndex];    //封装到货币对组中
                        moneyBothIndex++;
                    }

                    lineIndex++;
                }
                sr.Close();
            }
            catch(FileNotFoundException ex)
            {
                Setup.isFoundError = true;

                BasicData.mainUI.Invoke(BasicData.mainUI.ShowFormText, new object[] { 
                    basicFormText + "*找不到fenzhu.txt*",
                    BasicData.mainUI });
            }
            catch (Exception ex)
            {
                Setup.isFoundError = true;

                BasicData.mainUI.Invoke(BasicData.mainUI.ShowFormText, new object[] { 
                    basicFormText + "*分组有误*", 
                    BasicData.mainUI });
            }
        }
        public void diancha()
        {
            try
            {
                StreamReader sr = new StreamReader(Setup.pathTemp + "/diancha.txt", Encoding.Default);
                string line;
                //int lineIndex = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    string part = line.Substring(0, 6);
                    FindMoneyBoth(part).Diancha = int.Parse(line.Substring(11));
                    //lineIndex++;
                }
                sr.Close();
            }
            catch (FileNotFoundException ex)
            {
                Setup.isFoundError = true;
                BasicData.mainUI.Invoke(BasicData.mainUI.ShowFormText, new object[] { 
                    basicFormText + "*找不到diancha.txt*",
                    BasicData.mainUI });
            }
            catch (Exception ex)
            {
                Setup.isFoundError = true;
                BasicData.mainUI.Invoke(BasicData.mainUI.ShowFormText, new object[] { 
                    basicFormText + "*点差有误*", 
                    BasicData.mainUI });
            }
        }
        public void SVLV()
        {   //2016.01.24 18:43;USD;7;-16;JPY;-1;25;EUR;-3;-13;GBP;-3;10;CHF;-6;-2;CAD;9;-2;AUD;-4;42;NZD;30;-5
            try
            {
                StreamReader sr = new StreamReader(Setup.pathTemp + "/SVLV.txt", Encoding.Default);
                string line;
                //int lineIndex = 0;
                line = sr.ReadLine();
                string[] Data = line.Split(new char[] { ';' });
                for (int i = 1; i <= (Data.Length - 1) / 3; i++)
                {
                    string name = Data[3 * i - 2];
                    Money money = FindMoney(name);
                    money.ShortM = int.Parse(Data[3 * i - 1]);
                    if (money.ShortM > 0)
                        money.Color = 0;
                    else
                        money.Color = 1;
                    money.LongM = int.Parse(Data[3 * i]);
                }
                sr.Close();
            }
            catch (FileNotFoundException ex)
            {
                Setup.isFoundError = true;

                BasicData.mainUI.Invoke(BasicData.mainUI.ShowFormText, new object[] { 
                    basicFormText + "*找不到SVLV.txt*",
                    BasicData.mainUI });
            }
            catch (Exception ex)
            {
                Setup.isFoundError = true;

                BasicData.mainUI.Invoke(BasicData.mainUI.ShowFormText, new object[] { 
                    basicFormText + "*长短观数据有误*",
                    BasicData.mainUI });
            }
        }
        public void PPP()   //GBPUSD.lmx;L;-12
        {
            try
            {
                foreach(MoneyGroup mgp in DataFiler.basicMoneyGroup)
                {
                    foreach(MoneyBoth mbth in mgp.allMoneyBoth)
                    {
                        mbth.InitPosNum('x');
                    }
                }

                StreamReader sr = new StreamReader("E:/PPP.txt", Encoding.Default);
                string line;
                //int lineIndex = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    //读取每行前六个字符，找到相应的对象
                    string bothName = line.Substring(0, 6);
                    MoneyBoth both = FindMoneyBoth(bothName);
                    //读取part1
                    char pos = char.Parse(line.Substring(11, 1));
                    //读取part2
                    int posNum = int.Parse(line.Substring(13));
                    //将两个部分放到相对应的对象中
                    both.PosLocation = pos;
                    both.PosNum = posNum;
                }
                sr.Close();
            }
            catch(FileNotFoundException ex)
            {
                Setup.isFoundError = true;
                BasicData.mainUI.Invoke(BasicData.mainUI.ShowFormText, new object[] { 
                    basicFormText + "*找不到PPP.txt*",
                    BasicData.mainUI });
            }
            catch (Exception ex)
            {
                Setup.isFoundError = true;
                BasicData.mainUI.Invoke(BasicData.mainUI.ShowFormText, new object[] { 
                    basicFormText + "*盈亏点数有误*", 
                    BasicData.mainUI });
            }
        }
        public void TTT()   // 2016.01.27 02:20;USDJPY.lmx;82S;USDCHF.lmx;2B;USDCAD.lmx;38B;EURUSD.lmx;37S;........
        {
            try
            {
                StreamReader sr = new StreamReader(Setup.pathTemp + "/TTT.txt", Encoding.Default);
                string line;
                int lineNum = 0;
                int hour1, hour2;
                int min1, min2;
                line = sr.ReadLine();
                string[] Data = line.Split(new char[] { ';' });
                hour1 = int.Parse(line.Substring(11, 2));
                min1 = int.Parse(line.Substring(14, 2));
                hour2 = hour1;
                min2 = min1;
                for (int i = 1; i <= (Data.Length - 1) / 2; i++)
                {
                    string name = Data[2 * i - 1].Substring(0, 6);
                    MoneyBoth both = FindMoneyBoth(name);
                    both.BuyOrSellNum = int.Parse(Data[2 * i].Substring(0, Data[2 * i].Length - 1));
                    both.BuyOrSell = char.Parse(Data[2 * i].Substring(Data[2 * i].Length - 1, 1));
                } 
                lineNum++;

                while ((line = sr.ReadLine()) != null)
                {
                    hour2 = int.Parse(line.Substring(11, 2));
                    min2 = int.Parse(line.Substring(14, 2));
                    lineNum++;
                    if (lineNum == 60)
                        break;
                }
                MyTime timeDiff = new MyTime(0, hour1, min1) - new MyTime(0, hour2, min2);
                int planLineNum = timeDiff.Hour * 60 + timeDiff.Min + 1;
                if (planLineNum > lineNum)
                {
                    Setup.isFoundError = true;

                    BasicData.mainUI.Invoke(BasicData.mainUI.ShowFormText, new object[] { 
                    basicFormText + "*进退数据缺失" + (planLineNum - lineNum).ToString() + "条*",
                    BasicData.mainUI });
                }

                sr.Close();
            }
            catch (FileNotFoundException ex)
            {
                Setup.isFoundError = true;

                BasicData.mainUI.Invoke(BasicData.mainUI.ShowFormText, new object[] { 
                    basicFormText + "*找不到TTT.txt*",
                    BasicData.mainUI });
            }
            catch (Exception ex)
            {
                Setup.isFoundError = true;

                BasicData.mainUI.Invoke(BasicData.mainUI.ShowFormText, new object[] { 
                    basicFormText + "*进退数据有误*",
                    BasicData.mainUI });
            }
        }
        public void jjj()
        {   //2016.01.27 02:20;USDJPY.lmx;10;USDCAD.lmx;13;USDCHF.lmx;9;EURUSD.lmx;12;GBPUSD.lmx;16;AUDUSD.lmx;20;NZDUSD.lmx;15;EURJPY.lmx;6;EURCAD.lmx;1;EURCHF.lmx;-2;EURGBP.lmx;-6;EURAUD.lmx;-8;EURNZD.lmx;9;GBPJPY.lmx;7;CADJPY.lmx;-5;CHFJPY.lmx;3;AUDJPY.lmx;15;NZDJPY.lmx;32;GBPAUD.lmx;9;GBPCAD.lmx;3;GBPCHF.lmx;14;GBPNZD.lmx;4;AUDNZD.lmx;-5;AUDCAD.lmx;-9;AUDCHF.lmx;3;NZDCAD.lmx;2;NZDCHF.lmx;9;CADCHF.lmx;10
            try
            {
                StreamReader sr = new StreamReader(Setup.pathTemp + "/jjj.txt", Encoding.Default);
                string line;
                int lineNum = 0;
                int hour1,hour2;
                int min1,min2;
                line = sr.ReadLine();
                string[] Data = line.Split(new char[] { ';' });

                hour1 = int.Parse(line.Substring(11, 2));
                min1 = int.Parse(line.Substring(14, 2));
                hour2 = hour1;
                min2 = min1;
                for (int i = 1; i <= (Data.Length - 1) / 2; i++)
                {
                    string name = Data[2 * i - 1].Substring(0, 6);
                    MoneyBoth both = FindMoneyBoth(name);
                    both.PriceChange = Math.Abs(int.Parse(Data[2 * i]));
                }
                lineNum++;

                while((line = sr.ReadLine()) != null)
                {
                    hour2 = int.Parse(line.Substring(11, 2));
                    min2 = int.Parse(line.Substring(14, 2));
                    lineNum++;
                    if(lineNum == 60)
                        break;
                }

                MyTime timeDiff = new MyTime(0, hour1, min1) - new MyTime(0, hour2, min2);
                int planLineNum = timeDiff.Hour * 60 + timeDiff.Min + 1;
                if(planLineNum > lineNum)
                {
                    Setup.isFoundError = true;

                    BasicData.mainUI.Invoke(BasicData.mainUI.ShowFormText, new object[] { 
                    basicFormText + "*价格行程数据缺失" + (planLineNum - lineNum).ToString() + "条*",
                    BasicData.mainUI });
                }

                sr.Close();
            }
            catch(FileNotFoundException ex)
            {
                Setup.isFoundError = true;

                BasicData.mainUI.Invoke(BasicData.mainUI.ShowFormText, new object[] { 
                    basicFormText + "*找不到jjj.txt*",
                    BasicData.mainUI });
            }
            catch(Exception ex)
            {
                Setup.isFoundError = true;

                BasicData.mainUI.Invoke(BasicData.mainUI.ShowFormText, new object[] { 
                    basicFormText + "*价格行程有误*",
                    BasicData.mainUI });
            }
        }
        public void DDD()       //2016.01.27 02:20;USD；16；7；JPY；25；-1；EUR；13；3；GBP；10；-3；CHF；6；5；CAD；9；1；AUD；42；-7；NZD；30；-5
        {
            try
            {
                int lineNum = 0;
                int hour1, hour2;
                int min1, min2;
                StreamReader sr = new StreamReader(Setup.pathTemp + "/DDD.txt", Encoding.Default);
                string line = sr.ReadLine();
                string[] Data = line.Split(new char[] { ';' });

                hour1 = int.Parse(line.Substring(11, 2));
                min1 = int.Parse(line.Substring(14, 2));
                hour2 = hour1;
                min2 = min1;
                for (int i = 1; i <= (Data.Length - 1) / 3; i++)    //1...8
                {
                    string name = Data[3 * i- 2];
                    Money money = FindMoney(name);
                    money.LengthY = int.Parse(Data[3 * i - 1]);
                    money.ColorX = int.Parse(Data[3 * i]);
                    GetMaxLengthY(money.LengthY);
                    GetYingxianData(money, money.LengthY, money.ColorX);
                }
                lineNum++;
                while((line = sr.ReadLine()) != null)
                {
                    Data = line.Split(new char[] { ';' });
                    for (int i = 1; i <= (Data.Length - 1) / 3; i++)    //1...8
                    {
                        string name = Data[3 * i - 2];
                        Money money = FindMoney(name);
                        int tempLengthY = int.Parse(Data[3 * i - 1]);
                        int tempColorX = int.Parse(Data[3 * i]);
                        GetMaxLengthY(tempLengthY);
                        GetYingxianData(money, tempLengthY, tempColorX);
                    }
                    hour2 = int.Parse(line.Substring(11, 2));
                    min2 = int.Parse(line.Substring(14, 2));
                    lineNum++;
                }
                MyTime timeDiff = new MyTime(0, hour1, min1) - new MyTime(0, hour2, min2);
                int planLineNum = timeDiff.Hour * 60 + timeDiff.Min + 1;
                if (planLineNum > lineNum)
                {
                    Setup.isFoundError = true;

                    BasicData.mainUI.Invoke(BasicData.mainUI.ShowFormText, new object[] { 
                    basicFormText + "*日行图数据缺失" + (planLineNum - lineNum).ToString() + "条*",
                    BasicData.mainUI });
                }

                sr.Close();
            }
            catch (FileNotFoundException ex)
            {
                Setup.isFoundError = true;

                BasicData.mainUI.Invoke(BasicData.mainUI.ShowFormText, new object[] { 
                    basicFormText + "*找不到DDD.txt*",
                    BasicData.mainUI });
            }
            catch (Exception ex)
            {
                Setup.isFoundError = true;

                BasicData.mainUI.Invoke(BasicData.mainUI.ShowFormText, new object[] { 
                    basicFormText + "*日行图数据有误*",
                    BasicData.mainUI });
            }
        }
        public void fenzhong()
        {
            try
            {
                StreamReader sr = new StreamReader(Setup.pathTemp + "/fenzhong.txt", Encoding.Default);
                string line;
                int lineNum = 0;
                int hour1, hour2;
                int min1, min2;
                line = sr.ReadLine();
                string[] Data = line.Split(new char[] { ';' });
                Setup.timeWaihui = Data[0];
                DataShow.zsDownTimeLast = Data[0].Substring(11, 5).ToString();

                hour1 = int.Parse(line.Substring(11, 2));
                min1 = int.Parse(line.Substring(14, 2));
                hour2 = hour1;
                min2 = min1;
                for (int i = 1; i <= (Data.Length - 1) / 2; i++)    //2016.01.27 02:20;USD;56;JPY;65;EUR;73;GBP;60;CHF;69;CAD;90;AUD;84;NZD;90;
                {
                    string name = Data[2 * i - 1];
                    Money money = FindMoney(name);
                    money.OrderLeft = int.Parse(Data[2 * i]);
                    money.fengzhong[119] = money.OrderLeft;
                }
                lineNum++;
                while ((line = sr.ReadLine()) != null)
                {
                    Data = line.Split(new char[] { ';' });
                    for (int i = 1; i <= (Data.Length - 1) / 2; i++)    //2016.01.27 02:20;USD;56;JPY;65;EUR;73;GBP;60;CHF;69;CAD;90;AUD;84;NZD;90;
                    {
                        string name = Data[2 * i - 1];
                        Money money = FindMoney(name);
                        money.fengzhong[119 - lineNum] = int.Parse(Data[2 * i]);
                    }
                    if (lineNum == 60)
                    {
                        DataShow.zsDownTimeMid = Data[0].Substring(11, 5).ToString();
                    }
                    hour2 = int.Parse(line.Substring(11, 2));
                    min2 = int.Parse(line.Substring(14, 2));
                    lineNum++;
                }
                MyTime timeDiff = new MyTime(0, hour1, min1) - new MyTime(0, hour2, min2);
                int planLineNum = timeDiff.Hour * 60 + timeDiff.Min + 1;
                if (planLineNum > lineNum)
                {
                    Setup.isFoundError = true;

                    BasicData.mainUI.Invoke(BasicData.mainUI.ShowFormText, new object[] { 
                    basicFormText + "*走势数据缺失" + (planLineNum - lineNum).ToString() + "条*",
                    BasicData.mainUI });
                }

                sr.Close();
            }
            catch (FileNotFoundException ex)
            {
                Setup.isFoundError = true;

                BasicData.mainUI.Invoke(BasicData.mainUI.ShowFormText, new object[] { 
                    basicFormText + "*找不到fenzhong.txt*",
                    BasicData.mainUI });
            }
            catch (Exception ex)
            {
                Setup.isFoundError = true;

                BasicData.mainUI.Invoke(BasicData.mainUI.ShowFormText, new object[] { 
                    basicFormText + "*走势数据有误*",
                    BasicData.mainUI });
            }
        }

        private void GetMaxLengthY(int p)
        {
            if(p > DataShow.maxLengthY)
            {
                DataShow.maxLengthY = p;
            }
        }

        /// <summary>
        /// 解析绘制影线的数据
        /// </summary>
        /// <param name="money">货币对象</param>
        /// <param name="tempLengthY">解析出的纵坐标</param>
        /// <param name="tempColorX">解析出的横坐标</param>
        private void GetYingxianData(Money money, int tempLengthY, int tempColorX)
        {
            if (tempColorX < money.YColorX[0] || (tempColorX == money.YColorX[0] && tempLengthY > money.YLengthY[0]) )
            {
                money.YColorX[0] = tempColorX;
                money.YLengthY[0] = tempLengthY;
            }

            if (tempColorX > money.YColorX[1] || (tempColorX == money.YColorX[1] && tempLengthY > money.YLengthY[1]) )
            {
                money.YColorX[1] = tempColorX;
                money.YLengthY[1] = tempLengthY;
            }

            if(tempColorX < 0)
            {
                if(tempLengthY > money.YLengthY[2] || (tempLengthY == money.YLengthY[2] && tempColorX < money.YColorX[2]) )
                {
                    money.YColorX[2] = tempColorX;
                    money.YLengthY[2] = tempLengthY;
                }
            }
            else
            {
                if(tempLengthY > money.YLengthY[3] || (tempLengthY == money.YLengthY[3] && tempColorX > money.YColorX[3]) )
                {
                    money.YColorX[3] = tempColorX;
                    money.YLengthY[3] = tempLengthY;
                }
            }
        }
        
        public static Money FindMoney(string name)
        {
            foreach(Money m in basicMoney)
            {
                if (m.Name == name)
                    return m;
            }
            return null;
        }
        public MoneyBoth FindMoneyBoth(string name)
        {
            foreach (MoneyBoth m in basicMoneyBoth)
            {
                if (m.Name == name || m.MoneyB.Name + m.moneyA.Name == name)
                    return m;
            }
            return null;
        }
        public bool IsGetData()
        {
            if(BasicData.indexNum == 0 || BasicData.indexNum == 1)
            {
                return true;
            }
            switch (DateTime.Now.Second)
            {
                case 24: return true;
                case 25: return true;
                case 26: return true;
                case 54: return true;
                case 55: return true;
                case 56: return true;
                default: return false;
            }
        }

        public void SetGroup()
        {
            for(int i = 0;i < 7; i++)
            {
                basicMoneyGroup[i].allMoneyBoth = basicMoneyGroup[i].allMoneyBoth.OrderByDescending(j => j.PriceChange).ToArray();
            }
        }
        public void SetColor()
        {
            //BasicData.mainUI.button3.BackColor = Color.Red;
            //BasicData.mainUI.button5.BackColor = Color.Red;
            //BasicData.mainUI.label19.BackColor = Color.Red;

            #region 中轴

            //if(FindMoney("USD").LengthY )

            #endregion
        }   //空的
        public void GetData()
        {
            fenzhu();
            diancha();
            SVLV();
            PPP();
            TTT();
            jjj();
            DDD();
            fenzhong();
            SetGroup();
            LongSumBSix();
            ShortTrueNum();
            GetPriceChangeName();
        }

        private void GetPriceChangeName()
        {
            foreach(MoneyGroup mgp in basicMoneyGroup)
            {
                foreach(MoneyBoth mBth in mgp.allMoneyBoth)
                {
                    MyPriceChange mpc = new MyPriceChange(mBth.Name,mBth.PriceChange);
                    Setup.priceChangeOrder.Add(mpc);
                }
            }
            Setup.priceChangeOrder.Sort();
        }


        private void ShortTrueNum()
        {
            Money.ShortTrueNum = 0;
            foreach(Money money in basicMoney)
            {
                if(money.Order < 7 && money.ShortM > 0)
                {
                    Money.ShortTrueNum++;
                }
            }
        }

        /// <summary>
        /// 货币中 排序号前六的长观值之和
        /// </summary>
        private void LongSumBSix()
        {
            Money.BeforeSixLongSum = 0;
            foreach(Money money in basicMoney)
            {
                if(money.Order < 7)
                {
                    Money.BeforeSixLongSum += money.LongM;
                }
            }
        }

        internal void ToGGG()
        {
            string source = "";
            int lineNum = 0;

            foreach (MoneyGroup mgp in DataFiler.basicMoneyGroup)
            {
                foreach(MoneyBoth mb in mgp.allMoneyBoth)
                {
                    if (mb.BsState || mb.OutState)
                    {
                        if(lineNum != 0)
                            source += System.Environment.NewLine + mb.Name + ".lmx";
                        else
                            source += mb.Name + ".lmx";
                    
                        if (mb.BsState)
                        {
                            source += mb.BuyOrSell.ToString();
                        }
                        if (mb.OutState)
                        {
                            source += "X";
                        }


                        lineNum++;

                        mb.BsState = false;     //初始化
                        mb.OutState = false;    //初始化

                    }
                }
            }
            try
            {
                //写入GGG文件
                string path = "E:\\GGG.txt";
                FileStream f = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                StreamWriter sw = new StreamWriter(f);
                sw.WriteLine(source);
                sw.Flush();
                sw.Close();
                f.Close();
            }
            catch(System.Security.SecurityException sEx)
            {
                ToGGG();
            }
        }
    }
}
