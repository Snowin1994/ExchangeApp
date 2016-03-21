using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Exchange_UI
{
    public partial class FMWarnTime : Form
    {
        public Label[] lblWPTime = new Label[20] ;
        public Label[] lblWPTimeName = new Label[20];

        //public static TimeCountDown[] timeCountDown = new TimeCountDown[35];
        public static List<TimeCountDown> timeCountDown = new List<TimeCountDown>();
        /// <summary>
        /// 倒计时保存在设置文件中的存储结构
        /// </summary>
        public static string[] timeCDownSetting = { 
                                                      AllSettings.Default.timeCountDown0, AllSettings.Default.timeCountDown1,
                                                      AllSettings.Default.timeCountDown2, AllSettings.Default.timeCountDown3,
                                                      AllSettings.Default.timeCountDown4, AllSettings.Default.timeCountDown5,
                                                      AllSettings.Default.timeCountDown6, AllSettings.Default.timeCountDown7,
                                                      AllSettings.Default.timeCountDown8, AllSettings.Default.timeCountDown9,
                                                      AllSettings.Default.timeCountDown10, AllSettings.Default.timeCountDown11,
                                                      AllSettings.Default.timeCountDown12, AllSettings.Default.timeCountDown13,
                                                      AllSettings.Default.timeCountDown14, AllSettings.Default.timeCountDown15,
                                                      AllSettings.Default.timeCountDown16, AllSettings.Default.timeCountDown17,
                                                      AllSettings.Default.timeCountDown18, AllSettings.Default.timeCountDown19,
                                                      AllSettings.Default.timeCountDown20, AllSettings.Default.timeCountDown21,
                                                      AllSettings.Default.timeCountDown22, AllSettings.Default.timeCountDown23,
                                                      AllSettings.Default.timeCountDown24, AllSettings.Default.timeCountDown25,
                                                      AllSettings.Default.timeCountDown26, AllSettings.Default.timeCountDown27,
                                                      AllSettings.Default.timeCountDown28, AllSettings.Default.timeCountDown29,
                                                      AllSettings.Default.timeCountDown30, AllSettings.Default.timeCountDown31,
                                                      AllSettings.Default.timeCountDown32, AllSettings.Default.timeCountDown33,
                                                      AllSettings.Default.timeCountDown34
                                                  };

        public enum DesktopLayer
        {
            Progman =0,
            SHELLDLL =1,
            FolderView =2
        }

        IntPtr hDesktop;
        public const int GW_CHILD = 5;

        [DllImport("user32 ")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32 ")]
        private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);


        public FMWarnTime()
        {
            InitializeComponent();

            Label[] LB = {   
                             lblT1, lblT2, lblT3, lblT4, lblT5, lblT6, lblT7, lblT8, lblT9, lblT10, 
                             lblT11, lblT12, lblT13, lblT14, lblT15, lblT16, lblT17, lblT18, lblT19, lblT20, 
                             lblT21, lblT22, lblT23, lblT24,lblT25, lblT26, lblT27, lblT28, lblT29, lblT30,
                             lblT31, lblT32, lblT33, lblT34, lblT35
                         };
            lblWPTime = LB;

            Label[] LBName = {   
                                 lblM1, lblM2, lblM3, lblM4, lblM5, lblM6, lblM7, lblM8, lblM9, lblM10, 
                                 lblM11, lblM12, lblM13, lblM14, lblM15, lblM16, lblM17, lblM18, lblM19, lblM20, 
                                 lblM21, lblM22, lblM23, lblM24, lblM25, lblM26, lblM27, lblM28, lblM29, lblM30,
                                 lblM31, lblM32, lblM33, lblM34, lblM35
                             };
            lblWPTimeName = LBName;


            this.hDesktop = GetDesktopHandle(DesktopLayer.Progman);
            EmbedDesktop(this, this.Handle, this.hDesktop);
            //isMouseDown = false; 

            for(int i = 0; i < FMWarnTime.timeCountDown.Count; i++)
            {
                if ((FMWarnTime.timeCountDown[i].CountDown < MyTime.nowTime) == FMWarnTime.timeCountDown[i].CountDown)
                {
                    FMWarnTime.timeCountDown.Remove(FMWarnTime.timeCountDown[i]);
                }
            }
        }

        private void FMWarnTime_Load(object sender, EventArgs e)
        {
            IntPtr hDeskTop = FindWindow("Progman ", "Program Manager ");
            SetParent(this.Handle, hDeskTop);
            showWPTimeToTanle();
            //this.reportViewer1.RefreshReport();
        }

        public IntPtr GetDesktopHandle(DesktopLayer layer)
        { //hWnd = new HandleRef();
            HandleRef hWnd;
            IntPtr hDesktop = new IntPtr();
            switch (layer)
            {
                case DesktopLayer.Progman:
                    hDesktop = Win32Support.FindWindow("Progman", null);//第一层桌面
                    break;
                case DesktopLayer.SHELLDLL:
                    hDesktop = Win32Support.FindWindow("Progman", null);//第一层桌面
                    hWnd = new HandleRef(this, hDesktop);
                    hDesktop = Win32Support.GetWindow(hWnd, GW_CHILD);//第2层桌面
                    break;
                case DesktopLayer.FolderView:
                    hDesktop = Win32Support.FindWindow("Progman", null);//第一层桌面
                    hWnd = new HandleRef(this, hDesktop);
                    hDesktop = Win32Support.GetWindow(hWnd, GW_CHILD);//第2层桌面
                    hWnd = new HandleRef(this, hDesktop);
                    hDesktop = Win32Support.GetWindow(hWnd, GW_CHILD);//第3层桌面
                    break;
            }
            return hDesktop;
        }

        public void EmbedDesktop(Object embeddedWindow, IntPtr childWindow, IntPtr parentWindow)
        {
            Form window = (Form)embeddedWindow;
            HandleRef HWND_BOTTOM = new HandleRef(embeddedWindow, new IntPtr(1));
            const int SWP_FRAMECHANGED = 0x0020;//发送窗口大小改变消息
            Win32Support.SetParent(childWindow, parentWindow);
            Win32Support.SetWindowPos(new HandleRef(window, childWindow), HWND_BOTTOM, 300, 300, window.Width, window.Height, SWP_FRAMECHANGED);


        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                int hour = int.Parse(tbxHour.Text);
                int min = int.Parse(tbxMin.Text);
            
                Money money = DataFiler.FindMoney(tbxMoney.Text);

                if (hour > DateTime.Now.Hour || ( hour == DateTime.Now.Hour && min - 1 > DateTime.Now.Minute))
                {
                    money.wakeupList.Add(new MyTime(0, hour, min));
                    timeCountDown.Add(new TimeCountDown(money.Name, new MyTime(0, hour, min)));

                    money.wakeupList.Sort();
                    timeCountDown.Sort();

                    showWPTimeToTanle();
                }
                //else if(hour < DateTime.Now.Hour || (hour == DateTime.Now.Hour && min < DateTime.Now.Minute))
                //{
                //    money.wakeupList.Add(new MyTime(1, hour, min));
                //    timeCountDown.Add(new TimeCountDown(money.Name, new MyTime(1, hour, min)));
                //}
            }
            catch(Exception ex)
            {
                MessageBox.Show("内容非法！");
            }
        }

        /// <summary>
        /// 刷新倒计时列表
        /// </summary>
        private void showWPTimeToTanle()
        {
            #region 清空所有label文本
            foreach (Label lb in lblWPTime)
            {
                lb.Text = "";
            }
            foreach(Label lb in lblWPTimeName)
            {
                lb.Text = "";
            }
            #endregion

            #region 添加新文本
            int lineNum = 0;          // 倒计时的总行数

            lineNum = 0;
            foreach(TimeCountDown tcd in timeCountDown)
            {
                lblWPTimeName[lineNum].Text = tcd.Name;
                lblWPTime[lineNum].Text = tcd.CountDown.ToStr();
                lineNum++;
            }
            #endregion
        }

        #region 右键删除一行倒计时
        private void lblM1_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                foreach (Money m in DataFiler.basicMoney)
                {
                    if(m.wakeupList.Count > 0)
                    {
                        m.wakeupList.Remove(m.wakeupList[0]);
                        break;
                    }
                }
                if(FMWarnTime.timeCountDown.Count >= 1)
                    FMWarnTime.timeCountDown.Remove(FMWarnTime.timeCountDown[0]);
                showWPTimeToTanle();
            }
        }

        private void lblM2_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                int lineNum = 2;
                int leftNum = lineNum;
                foreach(Money m in DataFiler.basicMoney)
                {
                    if(m.wakeupList.Count >= leftNum)
                    {
                        m.wakeupList.Remove(m.wakeupList[leftNum - 1]);
                        break;
                    }
                    else
                    {
                        leftNum -= m.wakeupList.Count;
                    }
                }
                if (FMWarnTime.timeCountDown.Count >= 2)
                    FMWarnTime.timeCountDown.Remove(FMWarnTime.timeCountDown[1]);
                showWPTimeToTanle();
            }
        }

        private void lblM3_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int lineNum = 3;
                int leftNum = lineNum;
                foreach (Money m in DataFiler.basicMoney)
                {
                    if (m.wakeupList.Count >= leftNum)
                    {
                        m.wakeupList.Remove(m.wakeupList[leftNum - 1]);
                        break;
                    }
                    else
                    {
                        leftNum -= m.wakeupList.Count;
                    }
                }
                if (FMWarnTime.timeCountDown.Count >= 3)
                    FMWarnTime.timeCountDown.Remove(FMWarnTime.timeCountDown[2]);
                showWPTimeToTanle();
            }
        }

        private void lblM4_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int lineNum = 4;
                int leftNum = lineNum;
                foreach (Money m in DataFiler.basicMoney)
                {
                    if (m.wakeupList.Count >= leftNum)
                    {
                        m.wakeupList.Remove(m.wakeupList[leftNum - 1]);
                        break;
                    }
                    else
                    {
                        leftNum -= m.wakeupList.Count;
                    }
                }
                if (FMWarnTime.timeCountDown.Count >= 4)
                    FMWarnTime.timeCountDown.Remove(FMWarnTime.timeCountDown[3]);
                showWPTimeToTanle();
            }
        }

        private void lblM5_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int lineNum = 5;
                int leftNum = lineNum;
                foreach (Money m in DataFiler.basicMoney)
                {
                    if (m.wakeupList.Count >= leftNum)
                    {
                        m.wakeupList.Remove(m.wakeupList[leftNum - 1]);
                        break;
                    }
                    else
                    {
                        leftNum -= m.wakeupList.Count;
                    }
                }
                if (FMWarnTime.timeCountDown.Count >= 5)
                    FMWarnTime.timeCountDown.Remove(FMWarnTime.timeCountDown[4]);
                showWPTimeToTanle();
            }
        }

        private void lblM6_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int lineNum = 6;
                int leftNum = lineNum;
                foreach (Money m in DataFiler.basicMoney)
                {
                    if (m.wakeupList.Count >= leftNum)
                    {
                        m.wakeupList.Remove(m.wakeupList[leftNum - 1]);
                        break;
                    }
                    else
                    {
                        leftNum -= m.wakeupList.Count;
                    }
                }
                if (FMWarnTime.timeCountDown.Count >= 6)
                    FMWarnTime.timeCountDown.Remove(FMWarnTime.timeCountDown[5]);
                showWPTimeToTanle();
            }
        }

        private void lblM7_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int lineNum = 7;
                int leftNum = lineNum;
                foreach (Money m in DataFiler.basicMoney)
                {
                    if (m.wakeupList.Count >= leftNum)
                    {
                        m.wakeupList.Remove(m.wakeupList[leftNum - 1]);
                        break;
                    }
                    else
                    {
                        leftNum -= m.wakeupList.Count;
                    }
                }
                if (FMWarnTime.timeCountDown.Count >= 7)
                    FMWarnTime.timeCountDown.Remove(FMWarnTime.timeCountDown[6]);
                showWPTimeToTanle();
            }
        }

        private void lblM8_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int lineNum = 8;
                int leftNum = lineNum;
                foreach (Money m in DataFiler.basicMoney)
                {
                    if (m.wakeupList.Count >= leftNum)
                    {
                        m.wakeupList.Remove(m.wakeupList[leftNum - 1]);
                        break;
                    }
                    else
                    {
                        leftNum -= m.wakeupList.Count;
                    }
                }
                if (FMWarnTime.timeCountDown.Count >= 8)
                    FMWarnTime.timeCountDown.Remove(FMWarnTime.timeCountDown[7]);
                showWPTimeToTanle();
            }
        }

        private void lblM9_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int lineNum = 9;
                int leftNum = lineNum;
                foreach (Money m in DataFiler.basicMoney)
                {
                    if (m.wakeupList.Count >= leftNum)
                    {
                        m.wakeupList.Remove(m.wakeupList[leftNum - 1]);
                        break;
                    }
                    else
                    {
                        leftNum -= m.wakeupList.Count;
                    }
                }
                if (FMWarnTime.timeCountDown.Count >= 9)
                    FMWarnTime.timeCountDown.Remove(FMWarnTime.timeCountDown[8]);
                showWPTimeToTanle();
            }
        }

        private void lblM10_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int lineNum = 10;
                int leftNum = lineNum;
                foreach (Money m in DataFiler.basicMoney)
                {
                    if (m.wakeupList.Count >= leftNum)
                    {
                        m.wakeupList.Remove(m.wakeupList[leftNum - 1]);
                        break;
                    }
                    else
                    {
                        leftNum -= m.wakeupList.Count;
                    }
                }
                if (FMWarnTime.timeCountDown.Count >= 10)
                    FMWarnTime.timeCountDown.Remove(FMWarnTime.timeCountDown[9]);
                showWPTimeToTanle();
            }
        }

        private void lblM11_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int lineNum = 11;
                int leftNum = lineNum;
                foreach (Money m in DataFiler.basicMoney)
                {
                    if (m.wakeupList.Count >= leftNum)
                    {
                        m.wakeupList.Remove(m.wakeupList[leftNum - 1]);
                        break;
                    }
                    else
                    {
                        leftNum -= m.wakeupList.Count;
                    }
                }
                if (FMWarnTime.timeCountDown.Count >= 11)
                    FMWarnTime.timeCountDown.Remove(FMWarnTime.timeCountDown[10]);
                showWPTimeToTanle();
            }
        }

        private void lblM12_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int lineNum = 12;
                int leftNum = lineNum;
                foreach (Money m in DataFiler.basicMoney)
                {
                    if (m.wakeupList.Count >= leftNum)
                    {
                        m.wakeupList.Remove(m.wakeupList[leftNum - 1]);
                        break;
                    }
                    else
                    {
                        leftNum -= m.wakeupList.Count;
                    }
                }

                if (FMWarnTime.timeCountDown.Count >= 12)
                    FMWarnTime.timeCountDown.Remove(FMWarnTime.timeCountDown[11]);
                showWPTimeToTanle();
            }
        }

        private void lblM13_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int lineNum = 13;
                int leftNum = lineNum;
                foreach (Money m in DataFiler.basicMoney)
                {
                    if (m.wakeupList.Count >= leftNum)
                    {
                        m.wakeupList.Remove(m.wakeupList[leftNum - 1]);
                        break;
                    }
                    else
                    {
                        leftNum -= m.wakeupList.Count;
                    }
                }

                if (FMWarnTime.timeCountDown.Count >= 13)
                    FMWarnTime.timeCountDown.Remove(FMWarnTime.timeCountDown[12]);
                showWPTimeToTanle();
            }
        }

        private void lblM14_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int lineNum = 14;
                int leftNum = lineNum;
                foreach (Money m in DataFiler.basicMoney)
                {
                    if (m.wakeupList.Count >= leftNum)
                    {
                        m.wakeupList.Remove(m.wakeupList[leftNum - 1]);
                        break;
                    }
                    else
                    {
                        leftNum -= m.wakeupList.Count;
                    }
                }

                if (FMWarnTime.timeCountDown.Count >= 14)
                    FMWarnTime.timeCountDown.Remove(FMWarnTime.timeCountDown[13]);
                showWPTimeToTanle();
            }
        }

        private void lblM15_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int lineNum = 15;
                int leftNum = lineNum;
                foreach (Money m in DataFiler.basicMoney)
                {
                    if (m.wakeupList.Count >= leftNum)
                    {
                        m.wakeupList.Remove(m.wakeupList[leftNum - 1]);
                        break;
                    }
                    else
                    {
                        leftNum -= m.wakeupList.Count;
                    }
                }

                if (FMWarnTime.timeCountDown.Count >= 15)
                    FMWarnTime.timeCountDown.Remove(FMWarnTime.timeCountDown[14]);
                showWPTimeToTanle();
            }
        }

        private void lblM16_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int lineNum = 16;
                int leftNum = lineNum;
                foreach (Money m in DataFiler.basicMoney)
                {
                    if (m.wakeupList.Count >= leftNum)
                    {
                        m.wakeupList.Remove(m.wakeupList[leftNum - 1]);
                        break;
                    }
                    else
                    {
                        leftNum -= m.wakeupList.Count;
                    }
                }

                if (FMWarnTime.timeCountDown.Count >= 16)
                    FMWarnTime.timeCountDown.Remove(FMWarnTime.timeCountDown[15]);
                showWPTimeToTanle();
            }
        }

        private void lblM17_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int lineNum = 17;
                int leftNum = lineNum;
                foreach (Money m in DataFiler.basicMoney)
                {
                    if (m.wakeupList.Count >= leftNum)
                    {
                        m.wakeupList.Remove(m.wakeupList[leftNum - 1]);
                        break;
                    }
                    else
                    {
                        leftNum -= m.wakeupList.Count;
                    }
                }

                if (FMWarnTime.timeCountDown.Count >= 17)
                    FMWarnTime.timeCountDown.Remove(FMWarnTime.timeCountDown[16]);
                showWPTimeToTanle();
            }
        }

        private void lblM18_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int lineNum = 18;
                int leftNum = lineNum;
                foreach (Money m in DataFiler.basicMoney)
                {
                    if (m.wakeupList.Count >= leftNum)
                    {
                        m.wakeupList.Remove(m.wakeupList[leftNum - 1]);
                        break;
                    }
                    else
                    {
                        leftNum -= m.wakeupList.Count;
                    }
                }

                if (FMWarnTime.timeCountDown.Count >= 18)
                    FMWarnTime.timeCountDown.Remove(FMWarnTime.timeCountDown[17]);
                showWPTimeToTanle();
            }
        }

        private void lblM19_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int lineNum = 19;
                int leftNum = lineNum;
                foreach (Money m in DataFiler.basicMoney)
                {
                    if (m.wakeupList.Count >= leftNum)
                    {
                        m.wakeupList.Remove(m.wakeupList[leftNum - 1]);
                        break;
                    }
                    else
                    {
                        leftNum -= m.wakeupList.Count;
                    }
                }

                if (FMWarnTime.timeCountDown.Count >= 19)
                    FMWarnTime.timeCountDown.Remove(FMWarnTime.timeCountDown[18]);
                showWPTimeToTanle();
            }
        }

        private void lblM20_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int lineNum = 20;
                int leftNum = lineNum;
                foreach (Money m in DataFiler.basicMoney)
                {
                    if (m.wakeupList.Count >= leftNum)
                    {
                        m.wakeupList.Remove(m.wakeupList[leftNum - 1]);
                        break;
                    }
                    else
                    {
                        leftNum -= m.wakeupList.Count;
                    }
                }

                if (FMWarnTime.timeCountDown.Count >= 20)
                    FMWarnTime.timeCountDown.Remove(FMWarnTime.timeCountDown[19]);
                showWPTimeToTanle();
            }
        }

        private void lblM21_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int lineNum = 21;
                int leftNum = lineNum;
                foreach (Money m in DataFiler.basicMoney)
                {
                    if (m.wakeupList.Count >= leftNum)
                    {
                        m.wakeupList.Remove(m.wakeupList[leftNum - 1]);
                        break;
                    }
                    else
                    {
                        leftNum -= m.wakeupList.Count;
                    }
                }

                if (FMWarnTime.timeCountDown.Count >= 21)
                    FMWarnTime.timeCountDown.Remove(FMWarnTime.timeCountDown[20]);
                showWPTimeToTanle();
            }
        }

        private void lblM22_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int lineNum = 22;
                int leftNum = lineNum;
                foreach (Money m in DataFiler.basicMoney)
                {
                    if (m.wakeupList.Count >= leftNum)
                    {
                        m.wakeupList.Remove(m.wakeupList[leftNum - 1]);
                        break;
                    }
                    else
                    {
                        leftNum -= m.wakeupList.Count;
                    }
                }

                if (FMWarnTime.timeCountDown.Count >= 22)
                    FMWarnTime.timeCountDown.Remove(FMWarnTime.timeCountDown[21]);
                showWPTimeToTanle();
            }
        }

        private void lblM23_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int lineNum = 23;
                int leftNum = lineNum;
                foreach (Money m in DataFiler.basicMoney)
                {
                    if (m.wakeupList.Count >= leftNum)
                    {
                        m.wakeupList.Remove(m.wakeupList[leftNum - 1]);
                        break;
                    }
                    else
                    {
                        leftNum -= m.wakeupList.Count;
                    }
                }

                if (FMWarnTime.timeCountDown.Count >= 23)
                    FMWarnTime.timeCountDown.Remove(FMWarnTime.timeCountDown[22]);
                showWPTimeToTanle();
            }
        }

        private void lblM24_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int lineNum = 24;
                int leftNum = lineNum;
                foreach (Money m in DataFiler.basicMoney)
                {
                    if (m.wakeupList.Count >= leftNum)
                    {
                        m.wakeupList.Remove(m.wakeupList[leftNum - 1]);
                        break;
                    }
                    else
                    {
                        leftNum -= m.wakeupList.Count;
                    }
                }

                if (FMWarnTime.timeCountDown.Count >= 24)
                    FMWarnTime.timeCountDown.Remove(FMWarnTime.timeCountDown[23]);
                showWPTimeToTanle();
            }
        }
        private void lblM25_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int lineNum = 25;
                int leftNum = lineNum;
                foreach (Money m in DataFiler.basicMoney)
                {
                    if (m.wakeupList.Count >= leftNum)
                    {
                        m.wakeupList.Remove(m.wakeupList[leftNum - 1]);
                        break;
                    }
                    else
                    {
                        leftNum -= m.wakeupList.Count;
                    }
                }

                if (FMWarnTime.timeCountDown.Count >= 25)
                    FMWarnTime.timeCountDown.Remove(FMWarnTime.timeCountDown[24]);
                showWPTimeToTanle();
            }
        }

        private void lblM26_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int lineNum = 26;
                int leftNum = lineNum;
                foreach (Money m in DataFiler.basicMoney)
                {
                    if (m.wakeupList.Count >= leftNum)
                    {
                        m.wakeupList.Remove(m.wakeupList[leftNum - 1]);
                        break;
                    }
                    else
                    {
                        leftNum -= m.wakeupList.Count;
                    }
                }

                if (FMWarnTime.timeCountDown.Count >= 26)
                    FMWarnTime.timeCountDown.Remove(FMWarnTime.timeCountDown[25]);
                showWPTimeToTanle();
            }
        }

        private void lblM27_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int lineNum = 27;
                int leftNum = lineNum;
                foreach (Money m in DataFiler.basicMoney)
                {
                    if (m.wakeupList.Count >= leftNum)
                    {
                        m.wakeupList.Remove(m.wakeupList[leftNum - 1]);
                        break;
                    }
                    else
                    {
                        leftNum -= m.wakeupList.Count;
                    }
                }

                if (FMWarnTime.timeCountDown.Count >= 27)
                    FMWarnTime.timeCountDown.Remove(FMWarnTime.timeCountDown[26]);
                showWPTimeToTanle();
            }
        }

        private void lblM28_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int lineNum = 28;
                int leftNum = lineNum;
                foreach (Money m in DataFiler.basicMoney)
                {
                    if (m.wakeupList.Count >= leftNum)
                    {
                        m.wakeupList.Remove(m.wakeupList[leftNum - 1]);
                        break;
                    }
                    else
                    {
                        leftNum -= m.wakeupList.Count;
                    }
                }

                if (FMWarnTime.timeCountDown.Count >= 28)
                    FMWarnTime.timeCountDown.Remove(FMWarnTime.timeCountDown[27]);
                showWPTimeToTanle();
            }
        }

        private void lblM29_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int lineNum = 29;
                int leftNum = lineNum;
                foreach (Money m in DataFiler.basicMoney)
                {
                    if (m.wakeupList.Count >= leftNum)
                    {
                        m.wakeupList.Remove(m.wakeupList[leftNum - 1]);
                        break;
                    }
                    else
                    {
                        leftNum -= m.wakeupList.Count;
                    }
                }

                if (FMWarnTime.timeCountDown.Count >= 29)
                    FMWarnTime.timeCountDown.Remove(FMWarnTime.timeCountDown[28]);
                showWPTimeToTanle();
            }
        }

        private void lblM30_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int lineNum = 30;
                int leftNum = lineNum;
                foreach (Money m in DataFiler.basicMoney)
                {
                    if (m.wakeupList.Count >= leftNum)
                    {
                        m.wakeupList.Remove(m.wakeupList[leftNum - 1]);
                        break;
                    }
                    else
                    {
                        leftNum -= m.wakeupList.Count;
                    }
                }

                if (FMWarnTime.timeCountDown.Count >= 30)
                    FMWarnTime.timeCountDown.Remove(FMWarnTime.timeCountDown[29]);
                showWPTimeToTanle();
            }
        }

        private void lblM31_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int lineNum = 31;
                int leftNum = lineNum;
                foreach (Money m in DataFiler.basicMoney)
                {
                    if (m.wakeupList.Count >= leftNum)
                    {
                        m.wakeupList.Remove(m.wakeupList[leftNum - 1]);
                        break;
                    }
                    else
                    {
                        leftNum -= m.wakeupList.Count;
                    }
                }

                if (FMWarnTime.timeCountDown.Count >= 31)
                    FMWarnTime.timeCountDown.Remove(FMWarnTime.timeCountDown[30]);
                showWPTimeToTanle();
            }
        }

        private void lblM32_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int lineNum = 32;
                int leftNum = lineNum;
                foreach (Money m in DataFiler.basicMoney)
                {
                    if (m.wakeupList.Count >= leftNum)
                    {
                        m.wakeupList.Remove(m.wakeupList[leftNum - 1]);
                        break;
                    }
                    else
                    {
                        leftNum -= m.wakeupList.Count;
                    }
                }

                if (FMWarnTime.timeCountDown.Count >= 32)
                    FMWarnTime.timeCountDown.Remove(FMWarnTime.timeCountDown[31]);
                showWPTimeToTanle();
            }
        }

        private void lblM33_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int lineNum = 33;
                int leftNum = lineNum;
                foreach (Money m in DataFiler.basicMoney)
                {
                    if (m.wakeupList.Count >= leftNum)
                    {
                        m.wakeupList.Remove(m.wakeupList[leftNum - 1]);
                        break;
                    }
                    else
                    {
                        leftNum -= m.wakeupList.Count;
                    }
                }

                if (FMWarnTime.timeCountDown.Count >= 33)
                    FMWarnTime.timeCountDown.Remove(FMWarnTime.timeCountDown[32]);
                showWPTimeToTanle();
            }
        }

        private void lblM34_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int lineNum = 34;
                int leftNum = lineNum;
                foreach (Money m in DataFiler.basicMoney)
                {
                    if (m.wakeupList.Count >= leftNum)
                    {
                        m.wakeupList.Remove(m.wakeupList[leftNum - 1]);
                        break;
                    }
                    else
                    {
                        leftNum -= m.wakeupList.Count;
                    }
                }

                if (FMWarnTime.timeCountDown.Count >= 34)
                    FMWarnTime.timeCountDown.Remove(FMWarnTime.timeCountDown[33]);
                showWPTimeToTanle();
            }
        }

        private void lblM35_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int lineNum = 35;
                int leftNum = lineNum;
                foreach (Money m in DataFiler.basicMoney)
                {
                    if (m.wakeupList.Count >= leftNum)
                    {
                        m.wakeupList.Remove(m.wakeupList[leftNum - 1]);
                        break;
                    }
                    else
                    {
                        leftNum -= m.wakeupList.Count;
                    }
                }

                if (FMWarnTime.timeCountDown.Count >= 35)
                    FMWarnTime.timeCountDown.Remove(FMWarnTime.timeCountDown[34]);
                showWPTimeToTanle();
            }
        }
        #endregion

        #region 设置倒计时时间
        //货币对
        private void lblUSD_MouseClick(object sender, MouseEventArgs e)
        {
            tbxMoney.Text = lblUSD.Text;
        }

        private void lblEUR_MouseClick(object sender, MouseEventArgs e)
        {
            tbxMoney.Text = lblEUR.Text;
        }

        private void lblCHF_MouseClick(object sender, MouseEventArgs e)
        {
            tbxMoney.Text = lblCHF.Text;
        }

        private void lblAUD_MouseClick(object sender, MouseEventArgs e)
        {
            tbxMoney.Text = lblAUD.Text;
        }

        private void lblJPY_MouseClick(object sender, MouseEventArgs e)
        {
            tbxMoney.Text = lblJPY.Text;
        }

        private void lblGBP_MouseClick(object sender, MouseEventArgs e)
        {
            tbxMoney.Text = lblGBP.Text;
        }

        private void lblCAD_MouseClick(object sender, MouseEventArgs e)
        {
            tbxMoney.Text = lblCAD.Text;
        }

        private void lblNZD_MouseClick(object sender, MouseEventArgs e)
        {
            tbxMoney.Text = lblNZD.Text;
        }
        //分钟
        private void lblMin1_MouseClick(object sender, MouseEventArgs e)
        {
            tbxMin.Text = lblMin1.Text;
        }

        private void lblMin2_MouseClick(object sender, MouseEventArgs e)
        {
            tbxMin.Text = lblMin2.Text;
        }

        private void lblMin3_MouseClick(object sender, MouseEventArgs e)
        {
            tbxMin.Text = lblMin3.Text;
        }

        private void lblMin4_MouseClick(object sender, MouseEventArgs e)
        {
            tbxMin.Text = lblMin4.Text;
        }

        private void lblMin5_MouseClick(object sender, MouseEventArgs e)
        {
            tbxMin.Text = lblMin5.Text;
        }

        private void lblMin6_MouseClick(object sender, MouseEventArgs e)
        {
            tbxMin.Text = lblMin6.Text;
        }

        private void lblHour1_MouseClick(object sender, MouseEventArgs e)
        {
            tbxHour.Text = lblHour1.Text;
        }

        private void lblHour2_MouseClick(object sender, MouseEventArgs e)
        {
            tbxHour.Text = lblHour2.Text;
        }

        private void lblHour3_MouseClick(object sender, MouseEventArgs e)
        {
            tbxHour.Text = lblHour3.Text;
        }

        private void lblHour4_MouseClick(object sender, MouseEventArgs e)
        {
            tbxHour.Text = lblHour4.Text;
        }

        private void lblHour5_MouseClick(object sender, MouseEventArgs e)
        {
            tbxHour.Text = lblHour5.Text;
        }

        private void lblHour6_MouseClick(object sender, MouseEventArgs e)
        {
            tbxHour.Text = lblHour6.Text;
        }
        #endregion

        #region 按钮控制 倒计时设置
        private void btnHourUp_Click(object sender, EventArgs e)
        {
            int hour = int.Parse(tbxHour.Text);
            if(hour < 23)
            {
                string hourStr = (++hour).ToString();
                showToControl(tbxHour, hourStr);
            }
        }

        private void btnHourDown_Click(object sender, EventArgs e)
        {
            int hour = int.Parse(tbxHour.Text);
            if (hour > 0)
            {
                string hourStr = (--hour).ToString();
                showToControl(tbxHour, hourStr);
            }
        }

        private void showToControl(Control con,string source)
        {
            if(source.Length == 1)
            {
                con.Text = "0" + source;
            }
            else
            {
                con.Text = source;
            }
        }

        private void btnMinUp_Click(object sender, EventArgs e)
        {
            int min = int.Parse(tbxMin.Text);
            if(min < 59)
            {
                string minStr = (++min).ToString();
                showToControl(tbxMin, minStr);
            }
        }

        private void btnMinDown_Click(object sender, EventArgs e)
        {
            int min = int.Parse(tbxMin.Text);
            if (min > 0)
            {
                string minStr = (--min).ToString();
                showToControl(tbxMin, minStr);
            }
        }
        #endregion

    }
}
