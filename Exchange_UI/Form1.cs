using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Media;

namespace Exchange_UI
{
    public partial class MainUI : Form
    {
        /// <summary>
        /// 工作线程
        /// </summary>
        private Thread workThread;
        /// <summary>
        /// 提示音线程
        /// </summary>
        private Thread musicThread;
        public static int watchNum = 0;
        private static SoundPlayer sp = new SoundPlayer();
        private static bool isEnabled = false;
        private static MyTime pauseStartTime;
        private bool isPause = false;
        private Point grayPointLoc = new Point(44, 26);
        private int grayR = 12;

        public delegate void showText(string s, System.Windows.Forms.Label LB );
        public delegate void showButtonText(string s, System.Windows.Forms.Button BT);
        public delegate void showFormText(string s, System.Windows.Forms.Form FM);
        public delegate void showLabelFont(Font f, System.Windows.Forms.Label LB);
        public showText ShowText;
        public showButtonText ShowButtonText;
        public showFormText ShowFormText;
        public showLabelFont ShowLabelFont;

        public DrawGraph draw;

        private int[] colorRNum = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        public MainUI()
        {
            InitializeComponent();
            SetupInit();
            CountDownInit();
        }

        private void CountDownInit()
        {

            MyTime.lastClosedAppTime = AllSettings.Default.lastClosedAppTime;
            if(DateTime.Now.Date > MyTime.lastClosedAppTime.Date)
            {
                FMWarnTime.timeCountDown.Clear();
            }
        }

        private void SetupInit()
        {
            Setup.countDownLess = AllSettings.Default.countDownLess;
            Setup.countDownAgain = AllSettings.Default.countDownAgain;
            Setup.timeHourDiff = AllSettings.Default.timeHourDiff;

            #region 长观栏显示条件是否显示
            Setup.isUseCondition1 = AllSettings.Default.isUseCondition1;
            Setup.isUseCondition2 = AllSettings.Default.isUseCondition2;
            Setup.isUseCondition3 = AllSettings.Default.isUseCondition3;
            #endregion

            #region 中轴 - 货币颜色
            Setup.sMHeightLimit = AllSettings.Default.sMHeightLimit;   //高度分界线
            Setup.sMGrayFix1 = AllSettings.Default.sMGrayFix1;      //灰色特值1
            Setup.sMGrayFix2 = AllSettings.Default.sMGrayFix2;      //灰色特值2
            Setup.sMGreen = AllSettings.Default.sMGreen;  //颜色值大于此值时为绿色
            Setup.sMRed = AllSettings.Default.sMRed;   //颜色值小于此值时为红色
            #endregion

            #region 风向图相关设置
            Setup.shortNumMax = AllSettings.Default.shortNumMax;
            Setup.longNumMax = AllSettings.Default.longNumMax;
            Setup.sFxtLeftNum = AllSettings.Default.sFxtLeftNum;
            #endregion

            #region 点差栏提示符设置
            Setup.gDayNumMore = AllSettings.Default.gDayNumMore;
            Setup.gJinTuiMore = AllSettings.Default.gJinTuiMore;
            Setup.gIsColorDiff = AllSettings.Default.gIsColorDiff;
            Setup.gWithoutOrder1 = AllSettings.Default.gWithoutOrder1;
            Setup.gWithoutOrder2 = AllSettings.Default.gWithoutOrder2;
            Setup.gOrderSumLess = AllSettings.Default.gOrderSumLess;
            Setup.gShortBothMore = AllSettings.Default.gShortBothMore;
            Setup.gLongSumMore = AllSettings.Default.gLongSumMore;
            Setup.gFengzhongSumMore = AllSettings.Default.gFengzhongSumMore;

            Setup.gPChangeOrderNum = AllSettings.Default.gPChangeOrderNum;
            Setup.gIsUseSound = AllSettings.Default.gIsUseSound;
            #endregion

            #region 短观值非常态设置
            Setup.sIsGreenAndRed = AllSettings.Default.sIsGreenAndRed;
            Setup.sWithoutNum1 = AllSettings.Default.sWithoutNum1;
            Setup.sWithoutNum2 = AllSettings.Default.sWithoutNum2;
            Setup.sOrderSumLess = AllSettings.Default.sOrderSumLess;
            Setup.sShortAllMore = AllSettings.Default.sShortAllMore;
            Setup.sLongSumMore = AllSettings.Default.sLongSumMore;
            #endregion

            Setup.pathTemp = AllSettings.Default.pathTemp;

            #region 走势图 信号灯设置
            Setup.sZSRed[0] = AllSettings.Default.sZSRed0;
            Setup.sZSRed[1] = AllSettings.Default.sZSRed1;
            Setup.sZSRed[2] = AllSettings.Default.sZSRed2;
            Setup.sZSRed[3] = AllSettings.Default.sZSRed3;
            Setup.sZSRed[4] = AllSettings.Default.sZSRed4;
            Setup.sZSRed[5] = AllSettings.Default.sZSRed5;
            Setup.sZSRed[6] = AllSettings.Default.sZSRed6;
            Setup.sZSRedLess = AllSettings.Default.sZSRedLess;
            Setup.sZIsUpRed = AllSettings.Default.sZIsUpRed;


            Setup.sZSYellow[0] = AllSettings.Default.sZSYellow0;
            Setup.sZSYellow[1] = AllSettings.Default.sZSYellow1;
            Setup.sZSYellow[2] = AllSettings.Default.sZSYellow2;
            Setup.sZSYellow[3] = AllSettings.Default.sZSYellow3;
            Setup.sZSYellow[4] = AllSettings.Default.sZSYellow4;
            Setup.sZSYellow[5] = AllSettings.Default.sZSYellow5;
            Setup.sZSYellow[6] = AllSettings.Default.sZSYellow6;
            Setup.sZSYellowMore = AllSettings.Default.sZSYellowMore;

            Setup.sZSGreen[0] = AllSettings.Default.sZSGreen0;
            Setup.sZSGreen[1] = AllSettings.Default.sZSGreen1;
            Setup.sZSGreen[2] = AllSettings.Default.sZSGreen2;
            Setup.sZSGreen[3] = AllSettings.Default.sZSGreen3;
            Setup.sZSGreen[4] = AllSettings.Default.sZSGreen4;
            Setup.sZSGreen[5] = AllSettings.Default.sZSGreen5;
            Setup.sZSGreen[6] = AllSettings.Default.sZSGreen6;
            Setup.sZSGreenMore = AllSettings.Default.sZSGreenMore;
            Setup.sZIsUpGreen = AllSettings.Default.sZIsUpGreen;

            #endregion

            #region 离场符号设置
            Setup.sOutLongSum = AllSettings.Default.sOutLongSum;
            Setup.sOutOrderSum = AllSettings.Default.sOutOrderSum;
            Setup.sAfterMinNum = AllSettings.Default.sAfterMinNum;
            #endregion

            Setup.zoushiGreen = AllSettings.Default.zoushiGreen;
            Setup.zoushiRed = AllSettings.Default.zoushiRed;

            #region 报错相关
            Setup.stopSec = AllSettings.Default.stopSec;
            #endregion

            #region GGG文件生成设置
            Setup.sBuyOrSell = AllSettings.Default.sBuyOrSell;
            Setup.sOut = AllSettings.Default.sOut;
            #endregion

            #region 读取上一次倒计时设置

            FMWarnTime.timeCDownSetting[0]  = AllSettings.Default.timeCountDown0  ;
            FMWarnTime.timeCDownSetting[1]  = AllSettings.Default.timeCountDown1  ;
            FMWarnTime.timeCDownSetting[2]  = AllSettings.Default.timeCountDown2  ;
            FMWarnTime.timeCDownSetting[3]  = AllSettings.Default.timeCountDown3  ;
            FMWarnTime.timeCDownSetting[4]  = AllSettings.Default.timeCountDown4  ;
            FMWarnTime.timeCDownSetting[5]  = AllSettings.Default.timeCountDown5  ;
                                                                                  
            FMWarnTime.timeCDownSetting[6]  = AllSettings.Default.timeCountDown6  ;
            FMWarnTime.timeCDownSetting[7]  = AllSettings.Default.timeCountDown7  ;
            FMWarnTime.timeCDownSetting[8]  = AllSettings.Default.timeCountDown8  ;
            FMWarnTime.timeCDownSetting[9]  = AllSettings.Default.timeCountDown9  ;
            FMWarnTime.timeCDownSetting[10] = AllSettings.Default.timeCountDown10 ;
            FMWarnTime.timeCDownSetting[11] = AllSettings.Default.timeCountDown11 ;

            FMWarnTime.timeCDownSetting[12] = AllSettings.Default.timeCountDown12 ;
            FMWarnTime.timeCDownSetting[13] = AllSettings.Default.timeCountDown13 ;
            FMWarnTime.timeCDownSetting[14] = AllSettings.Default.timeCountDown14 ;
            FMWarnTime.timeCDownSetting[15] = AllSettings.Default.timeCountDown15 ;
            FMWarnTime.timeCDownSetting[16] = AllSettings.Default.timeCountDown16 ;
            FMWarnTime.timeCDownSetting[17] = AllSettings.Default.timeCountDown17 ;

            FMWarnTime.timeCDownSetting[18] = AllSettings.Default.timeCountDown18 ;
            FMWarnTime.timeCDownSetting[19] = AllSettings.Default.timeCountDown19 ;
            FMWarnTime.timeCDownSetting[20] = AllSettings.Default.timeCountDown20 ;
            FMWarnTime.timeCDownSetting[21] = AllSettings.Default.timeCountDown21 ;
            FMWarnTime.timeCDownSetting[22] = AllSettings.Default.timeCountDown22 ;
            FMWarnTime.timeCDownSetting[23] = AllSettings.Default.timeCountDown23 ;

            FMWarnTime.timeCDownSetting[24] = AllSettings.Default.timeCountDown24 ;
            FMWarnTime.timeCDownSetting[25] = AllSettings.Default.timeCountDown25 ;
            FMWarnTime.timeCDownSetting[26] = AllSettings.Default.timeCountDown26 ;
            FMWarnTime.timeCDownSetting[27] = AllSettings.Default.timeCountDown27 ;
            FMWarnTime.timeCDownSetting[28] = AllSettings.Default.timeCountDown28 ;
            FMWarnTime.timeCDownSetting[29] = AllSettings.Default.timeCountDown29 ;

            FMWarnTime.timeCDownSetting[30] = AllSettings.Default.timeCountDown30 ;
            FMWarnTime.timeCDownSetting[31] = AllSettings.Default.timeCountDown31 ;
            FMWarnTime.timeCDownSetting[32] = AllSettings.Default.timeCountDown32 ;
            FMWarnTime.timeCDownSetting[33] = AllSettings.Default.timeCountDown33 ;
            FMWarnTime.timeCDownSetting[34] = AllSettings.Default.timeCountDown34 ;

            for (int i = 0; i < FMWarnTime.timeCDownSetting.Length; i++)
            {
                if (FMWarnTime.timeCDownSetting[i] != "0")
                {
                    FMWarnTime.timeCountDown.Add(new TimeCountDown(FMWarnTime.timeCDownSetting[i]));
                }
            }
            FMWarnTime.timeCountDown.Sort();

            #endregion

        }

        public void setText(string s, System.Windows.Forms.Label LB)
        {
            LB.Text = s;
        }

        public void setButtonText(string s,System.Windows.Forms.Button BT)
        {
            BT.Text = s;
        }
        public void setFormText(string s,System.Windows.Forms.Form FM)
        {
            try
            {
                FM.Text = s;
                //sp.SoundLocation = @".\jingbao.wav";
                //sp.Play();
                //Thread.Sleep(2000);
                //sp.Stop();
            }
            catch(Exception ex)
            {
                //BasicData.mainUI.Invoke(BasicData.mainUI.ShowFormText, new object[] { 
                //    DataFiler.basicFormText + "*未找到音效文件*",
                //    BasicData.mainUI });
            }
        }
        public void setLabelFont(Font f, System.Windows.Forms.Label LB)
        {
            LB.Font = f;
        }
        private void MainUI_Load(object sender, EventArgs e)
        {

            ShowText = new showText(setText);
            ShowButtonText = new showButtonText(setButtonText);
            ShowFormText = new showFormText(setFormText);
            ShowLabelFont = new showLabelFont(setLabelFont);

            MyTime.nowTime = new MyTime(0, DateTime.Now.Hour, DateTime.Now.Minute);     //系统时间初始化
            BasicData.mainUI = this;
            draw = new DrawGraph();

            workThread = new Thread(()=>
            {
                BasicData.Run();
            });
            workThread.Start();

            musicThread = new Thread(() =>
            {
                BasicData.PlayMusic();
            });
            musicThread.Start();
        }

        private void MainUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            SettingSave();

            if(workThread.IsAlive)
            {
                workThread.Abort();
            }
            System.Environment.Exit(0);
        }

        /// <summary>
        /// 保存当前设置到设置文件
        /// </summary>
        private void SettingSave()
        {
            AllSettings.Default.countDownLess = Setup.countDownLess;
            AllSettings.Default.countDownAgain = Setup.countDownAgain;
            AllSettings.Default.timeHourDiff = Setup.timeHourDiff;

            #region 中轴 - 货币颜色
            AllSettings.Default.sMHeightLimit = Setup.sMHeightLimit;   //高度分界线
            AllSettings.Default.sMGrayFix1 = Setup.sMGrayFix1;      //灰色特值1
            AllSettings.Default.sMGrayFix2 = Setup.sMGrayFix2;      //灰色特值2
            AllSettings.Default.sMGreen = Setup.sMGreen;  //颜色值大于此值时为绿色
            AllSettings.Default.sMRed = Setup.sMRed;   //颜色值小于此值时为红色
            #endregion

            #region 风向图相关设置
            AllSettings.Default.shortNumMax = Setup.shortNumMax;
            AllSettings.Default.longNumMax = Setup.longNumMax;
            AllSettings.Default.sFxtLeftNum = Setup.sFxtLeftNum;
            #endregion

            #region 点差栏提示符设置
            AllSettings.Default.gDayNumMore = Setup.gDayNumMore;
            AllSettings.Default.gJinTuiMore= Setup.gJinTuiMore;
            AllSettings.Default.gIsColorDiff= Setup.gIsColorDiff;
            AllSettings.Default.gWithoutOrder1= Setup.gWithoutOrder1;  
            AllSettings.Default.gWithoutOrder2= Setup.gWithoutOrder2;   
            AllSettings.Default.gOrderSumLess= Setup.gOrderSumLess;
            AllSettings.Default.gShortBothMore= Setup.gShortBothMore;
            AllSettings.Default.gLongSumMore= Setup.gLongSumMore;
            AllSettings.Default.gFengzhongSumMore= Setup.gFengzhongSumMore;

            AllSettings.Default.gPChangeOrderNum = Setup.gPChangeOrderNum;
            AllSettings.Default.gIsUseSound = Setup.gIsUseSound;
            #endregion

            #region 短观值非常态设置
            AllSettings.Default.sIsGreenAndRed = Setup.sIsGreenAndRed;
            AllSettings.Default.sWithoutNum1 = Setup.sWithoutNum1;
            AllSettings.Default.sWithoutNum2 = Setup.sWithoutNum2;
            AllSettings.Default.sOrderSumLess = Setup.sOrderSumLess;
            AllSettings.Default.sShortAllMore = Setup.sShortAllMore;
            AllSettings.Default.sLongSumMore = Setup.sLongSumMore;
            #endregion

            #region 长观栏显示条件是否显示
            AllSettings.Default.isUseCondition1 = Setup.isUseCondition1;
            AllSettings.Default.isUseCondition2 = Setup.isUseCondition2;
            AllSettings.Default.isUseCondition3 = Setup.isUseCondition3;
            #endregion

            AllSettings.Default.pathTemp = Setup.pathTemp;

            #region 走势图 信号灯设置
            AllSettings.Default.sZSRed0 = Setup.sZSRed[0];
            AllSettings.Default.sZSRed1 = Setup.sZSRed[1];
            AllSettings.Default.sZSRed2 = Setup.sZSRed[2];
            AllSettings.Default.sZSRed3 = Setup.sZSRed[3];
            AllSettings.Default.sZSRed4 = Setup.sZSRed[4];
            AllSettings.Default.sZSRed5 = Setup.sZSRed[5];
            AllSettings.Default.sZSRed6 = Setup.sZSRed[6];
            AllSettings.Default.sZSRedLess = Setup.sZSRedLess;
            AllSettings.Default.sZIsUpRed = Setup.sZIsUpRed;

            AllSettings.Default.sZSYellow0 = Setup.sZSYellow[0];
            AllSettings.Default.sZSYellow1 = Setup.sZSYellow[1];
            AllSettings.Default.sZSYellow2 = Setup.sZSYellow[2];
            AllSettings.Default.sZSYellow3 = Setup.sZSYellow[3];
            AllSettings.Default.sZSYellow4 = Setup.sZSYellow[4];
            AllSettings.Default.sZSYellow5 = Setup.sZSYellow[5];
            AllSettings.Default.sZSYellow6 = Setup.sZSYellow[6];
            AllSettings.Default.sZSYellowMore = Setup.sZSYellowMore;

            AllSettings.Default.sZSGreen0 = Setup.sZSGreen[0];
            AllSettings.Default.sZSGreen1 = Setup.sZSGreen[1];
            AllSettings.Default.sZSGreen2 = Setup.sZSGreen[2];
            AllSettings.Default.sZSGreen3 = Setup.sZSGreen[3];
            AllSettings.Default.sZSGreen4 = Setup.sZSGreen[4];
            AllSettings.Default.sZSGreen5 = Setup.sZSGreen[5];
            AllSettings.Default.sZSGreen6 = Setup.sZSGreen[6];
            AllSettings.Default.sZSGreenMore = Setup.sZSGreenMore;
            AllSettings.Default.sZIsUpGreen = Setup.sZIsUpGreen;
            #endregion

            #region 离场符号设置
            AllSettings.Default.sOutLongSum = Setup.sOutLongSum;
            AllSettings.Default.sOutOrderSum = Setup.sOutOrderSum;
            AllSettings.Default.sAfterMinNum = Setup.sAfterMinNum;
            #endregion

            AllSettings.Default.zoushiGreen = Setup.zoushiGreen;
            AllSettings.Default.zoushiRed = Setup.zoushiRed;

            #region 报错相关
            AllSettings.Default.stopSec = Setup.stopSec;
            #endregion

            #region GGG文件生成设置
            AllSettings.Default.sBuyOrSell = Setup.sBuyOrSell;
            AllSettings.Default.sOut = Setup.sOut;
            #endregion

            #region 倒计时保存

            for (int i = 0; i < FMWarnTime.timeCountDown.Count; i++)
            {
                FMWarnTime.timeCDownSetting[i] = FMWarnTime.timeCountDown[i].ToStrForSave();
            }
            for (int i = FMWarnTime.timeCountDown.Count; i < FMWarnTime.timeCDownSetting.Length; i++)
            {
                FMWarnTime.timeCDownSetting[i] = "0";
            }
            AllSettings.Default.timeCountDown0 = FMWarnTime.timeCDownSetting[0];
            AllSettings.Default.timeCountDown1 = FMWarnTime.timeCDownSetting[1];
            AllSettings.Default.timeCountDown2 = FMWarnTime.timeCDownSetting[2];
            AllSettings.Default.timeCountDown3 = FMWarnTime.timeCDownSetting[3];
            AllSettings.Default.timeCountDown4 = FMWarnTime.timeCDownSetting[4];
            AllSettings.Default.timeCountDown5 = FMWarnTime.timeCDownSetting[5];

            AllSettings.Default.timeCountDown6 = FMWarnTime.timeCDownSetting[6];
            AllSettings.Default.timeCountDown7 = FMWarnTime.timeCDownSetting[7];
            AllSettings.Default.timeCountDown8 = FMWarnTime.timeCDownSetting[8];
            AllSettings.Default.timeCountDown9 = FMWarnTime.timeCDownSetting[9];
            AllSettings.Default.timeCountDown10 = FMWarnTime.timeCDownSetting[10];
            AllSettings.Default.timeCountDown11 = FMWarnTime.timeCDownSetting[11];

            AllSettings.Default.timeCountDown12 = FMWarnTime.timeCDownSetting[12];
            AllSettings.Default.timeCountDown13 = FMWarnTime.timeCDownSetting[13];
            AllSettings.Default.timeCountDown14 = FMWarnTime.timeCDownSetting[14];
            AllSettings.Default.timeCountDown15 = FMWarnTime.timeCDownSetting[15];
            AllSettings.Default.timeCountDown16 = FMWarnTime.timeCDownSetting[16];
            AllSettings.Default.timeCountDown17 = FMWarnTime.timeCDownSetting[17];

            AllSettings.Default.timeCountDown18 = FMWarnTime.timeCDownSetting[18];
            AllSettings.Default.timeCountDown19 = FMWarnTime.timeCDownSetting[19];
            AllSettings.Default.timeCountDown20 = FMWarnTime.timeCDownSetting[20];
            AllSettings.Default.timeCountDown21 = FMWarnTime.timeCDownSetting[21];
            AllSettings.Default.timeCountDown22 = FMWarnTime.timeCDownSetting[22];
            AllSettings.Default.timeCountDown23 = FMWarnTime.timeCDownSetting[23];

            AllSettings.Default.timeCountDown24 = FMWarnTime.timeCDownSetting[24];
            AllSettings.Default.timeCountDown25 = FMWarnTime.timeCDownSetting[25];
            AllSettings.Default.timeCountDown26 = FMWarnTime.timeCDownSetting[26];
            AllSettings.Default.timeCountDown27 = FMWarnTime.timeCDownSetting[27];
            AllSettings.Default.timeCountDown28 = FMWarnTime.timeCDownSetting[28];
            AllSettings.Default.timeCountDown29 = FMWarnTime.timeCDownSetting[29];

            AllSettings.Default.timeCountDown30 = FMWarnTime.timeCDownSetting[30];
            AllSettings.Default.timeCountDown31 = FMWarnTime.timeCDownSetting[31];
            AllSettings.Default.timeCountDown32 = FMWarnTime.timeCDownSetting[32];
            AllSettings.Default.timeCountDown33 = FMWarnTime.timeCDownSetting[33];
            AllSettings.Default.timeCountDown34 = FMWarnTime.timeCDownSetting[34];


            #endregion

            MyTime.lastClosedAppTime = DateTime.Now;
            AllSettings.Default.lastClosedAppTime = MyTime.lastClosedAppTime;

            AllSettings.Default.Save();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblDateTime.Text = DateTime.Now.ToString();

            MyTime.nowTime = new MyTime(0,DateTime.Now.Hour, DateTime.Now.Minute);


            #region 倒计时闪烁
            if(isEnabled)
            {
                foreach(Label lb in DataShow.lblTableTime)
                {
                    lb.Enabled = true;
                }
                isEnabled = false;
            }
            else
            {
                foreach (Label lb in DataShow.lblTableTime)
                {
                    if (lb.Text != "" && IsStartEnable(lb.Text) && IsPauseEnable())
                        lb.Enabled = false;
                }
                isEnabled = true;
            }
            #endregion

        }

        /// <summary>
        /// 检查是否小于规定的开始闪烁的时间
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private bool IsStartEnable(string p)
        {
            if (p != "" && p != null)
            {
                int hour = int.Parse(p.Substring(0, 2));
                int min = int.Parse(p.Substring(3, 2));
                if ((hour * 60 + min) < Setup.countDownLess)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 是否被暂停闪烁
        /// </summary>
        /// <returns></returns>
        private bool IsPauseEnable()
        {
            if (isPause)
            {
                MyTime diffTime = MyTime.nowTime - pauseStartTime;
                if ((diffTime.Hour * 60 + diffTime.Min) < Setup.countDownAgain)
                {
                    return false;
                }
                else
                {
                    isPause = false;
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        private void timerCountDown_Tick(object sender, EventArgs e)
        {
            DataShow drawS = new DataShow();
            drawS.UpLine();
            drawS.ToLong();
            drawS.ToOutMark();
            drawS.IsLocHaveData();
            drawS.UpFXTMoneyNameColor();

            if(watchNum == 0)
            {
                watchNum = DataShow.linePos;
            }

            if(DataShow.linePosLast == watchNum)
            {
                watchNum = DataShow.linePos;
                drawS.ToZoushitu();
                drawS.ToFengxiangtu();
            }

            #region 检查是否发现错误
            if (Setup.isFoundError)
            {
                //Thread.Sleep(Setup.stopSec * 1000);
                this.Text = DataFiler.basicFormText;

                Setup.isFoundError = false;
            }
            #endregion

        }



        private void btnSetup_Click(object sender, EventArgs e)
        {
            FMSetup setup = new FMSetup();
            setup.Show();
        }

        private void lblDateTime_Click(object sender, EventArgs e)
        {
            FMSetup setup = new FMSetup();
            setup.Show();
        }


        private void btnUpdata_Click(object sender, EventArgs e)
        {
            //DataFiler dataFiler = new DataFiler();
            //dataFiler.GetData();

            DataShow dataShow = new DataShow();
            dataShow.ToTable();
            dataShow.ToZhongzhou();
            dataShow.UpStar();
            dataShow.UpCirMark();
            dataShow.ToRixingtu();
            dataShow.ToFengxiangtu();
            dataShow.ToZoushitu();
            //dataShow.ToTime();

            //dataFiler.PPP();
            //dataFiler.ToGGG();
        }

        private void MainUI_Shown(object sender, EventArgs e)
        {
            
        }
        private void MainUI_Paint(object sender, PaintEventArgs e)
        {
            DataShow dataShow = new DataShow();
            dataShow.ToTable();
            dataShow.ToTime();
            dataShow.ToZhongzhou();
            dataShow.ToRixingtu();
            dataShow.ToFengxiangtu();
            dataShow.ToZoushitu();
            dataShow.ToSignLight();
            dataShow.UpCirMark();
            dataShow.ToOutMark();
        }

        private void MainUI_TextChanged(object sender, EventArgs e)
        {
            //if (Setup.isFoundError)
            //{
            //    Thread.Sleep(Setup.stopSec * 1000);
            //    this.Text = DataFiler.basicFormText;

            //    Setup.isFoundError = false;
            //}
        }

        #region 中轴按钮色块变化
        private void button4_Click(object sender, EventArgs e)
        {
            if (colorRNum[0] < 5)
            {
                colorRNum[0]++;
            }
        }

        private void button4_Paint(object sender, PaintEventArgs e)
        {
            if (colorRNum[0] <= 5)
            {
                draw.PaintRectangle(e.Graphics, button4, Color.Green, new Point(0, 2), 10 * colorRNum[0], 12);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(colorRNum[0] > 0)
            {
                colorRNum[0]--;
            }
            button4.Invalidate();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (colorRNum[1] < 5)
            {
                colorRNum[1]++;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (colorRNum[1] > 0)
            {
                colorRNum[1]--;
            }
            button6.Invalidate();
        }

        private void button6_Paint(object sender, PaintEventArgs e)
        {
            if (colorRNum[1] <= 5)
            {
                draw.PaintRectangle(e.Graphics, button6, Color.Red, new Point(0, 18), 10 * colorRNum[1], 12);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (colorRNum[2] < 5)
            {
                colorRNum[2]++;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (colorRNum[2] > 0)
            {
                colorRNum[2]--;
            }
            button9.Invalidate();
        }

        private void button9_Paint(object sender, PaintEventArgs e)
        {
            if (colorRNum[2] <= 5)
            {
                draw.PaintRectangle(e.Graphics, button9, Color.Green, new Point(0, 2), 10 * colorRNum[2], 12);
                //button4.Invalidate();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (colorRNum[3] < 5)
            {
                colorRNum[3]++;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (colorRNum[3] > 0)
            {
                colorRNum[3]--;
            }
            button7.Invalidate();
        }

        private void button7_Paint(object sender, PaintEventArgs e)
        {
            if (colorRNum[3] <= 5)
            {
                draw.PaintRectangle(e.Graphics, button7, Color.Red, new Point(0, 18), 10 * colorRNum[3], 12);
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (colorRNum[4] < 5)
            {
                colorRNum[4]++;
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (colorRNum[4] > 0)
            {
                colorRNum[4]--;
            }
            button17.Invalidate();
        }

        private void button17_Paint(object sender, PaintEventArgs e)
        {
            if (colorRNum[4] <= 5)
            {
                draw.PaintRectangle(e.Graphics, button17, Color.Green, new Point(-1, 2), 10 * colorRNum[4], 12);
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (colorRNum[5] < 5)
            {
                colorRNum[5]++;
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (colorRNum[5] > 0)
            {
                colorRNum[5]--;
            }
            button15.Invalidate();
        }

        private void button15_Paint(object sender, PaintEventArgs e)
        {
            if (colorRNum[5] <= 5)
            {
                draw.PaintRectangle(e.Graphics, button15, Color.Red, new Point(-1, 18), 10 * colorRNum[5], 12);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (colorRNum[6] < 5)
            {
                colorRNum[6]++;
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (colorRNum[6] > 0)
            {
                colorRNum[6]--;
            }
            button13.Invalidate();
        }

        private void button13_Paint(object sender, PaintEventArgs e)
        {
            if (colorRNum[6] <= 5)
            {
                draw.PaintRectangle(e.Graphics, button13, Color.Green, new Point(-1, 2), 10 * colorRNum[6], 12);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (colorRNum[7] < 5)
            {
                colorRNum[7]++;
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (colorRNum[7] > 0)
            {
                colorRNum[7]--;
            }
            button11.Invalidate();
        }

        private void button11_Paint(object sender, PaintEventArgs e)
        {
            if (colorRNum[7] <= 5)
            {
                draw.PaintRectangle(e.Graphics, button11, Color.Red, new Point(-1, 18), 10 * colorRNum[7], 12);
            }
        }

        private void button33_Click(object sender, EventArgs e)
        {
            if (colorRNum[8] < 5)
            {
                colorRNum[8]++;
            }
        }

        private void button34_Click(object sender, EventArgs e)
        {
            if (colorRNum[8] > 0)
            {
                colorRNum[8]--;
            }
            button33.Invalidate();
        }

        private void button33_Paint(object sender, PaintEventArgs e)
        {
            if (colorRNum[8] <= 5)
            {
                draw.PaintRectangle(e.Graphics, button33, Color.Green, new Point(0, 2), 10 * colorRNum[8], 12);
            }
        }

        private void button31_Click(object sender, EventArgs e)
        {
            if (colorRNum[9] < 5)
            {
                colorRNum[9]++;
            }
        }

        private void button32_Click(object sender, EventArgs e)
        {
            if (colorRNum[9] > 0)
            {
                colorRNum[9]--;
            }
            button31.Invalidate();
        }

        private void button31_Paint(object sender, PaintEventArgs e)
        {
            if (colorRNum[9] <= 5)
            {
                draw.PaintRectangle(e.Graphics, button31, Color.Red, new Point(0, 18), 10 * colorRNum[9], 12);
            }
        }

        private void button29_Click(object sender, EventArgs e)
        {
            if (colorRNum[10] < 5)
            {
                colorRNum[10]++;
            }
        }

        private void button30_Click(object sender, EventArgs e)
        {
            if (colorRNum[10] > 0)
            {
                colorRNum[10]--;
            }
            button29.Invalidate();
        }

        private void button29_Paint(object sender, PaintEventArgs e)
        {
            if (colorRNum[10] <= 5)
            {
                draw.PaintRectangle(e.Graphics, button29, Color.Green, new Point(0, 2), 10 * colorRNum[10], 12);
            }
        }

        private void button27_Click(object sender, EventArgs e)
        {
            if (colorRNum[11] < 5)
            {
                colorRNum[11]++;
            }
        }

        private void button28_Click(object sender, EventArgs e)
        {
            if (colorRNum[11] > 0)
            {
                colorRNum[11]--;
            }
            button27.Invalidate();
        }

        private void button27_Paint(object sender, PaintEventArgs e)
        {
            if (colorRNum[11] <= 5)
            {
                draw.PaintRectangle(e.Graphics, button27, Color.Red, new Point(-1, 18), 10 * colorRNum[11], 12);
            }
        }

        private void button25_Click(object sender, EventArgs e)
        {
            if (colorRNum[12] < 5)
            {
                colorRNum[12]++;
            }
        }

        private void button26_Click(object sender, EventArgs e)
        {
            if (colorRNum[12] > 0)
            {
                colorRNum[12]--;
            }
            button25.Invalidate();
        }

        private void button25_Paint(object sender, PaintEventArgs e)
        {
            if (colorRNum[12] <= 5)
            {
                draw.PaintRectangle(e.Graphics, button25, Color.Green, new Point(-1, 2), 10 * colorRNum[12], 12);
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            if (colorRNum[13] < 5)
            {
                colorRNum[13]++;
            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            if (colorRNum[13] > 0)
            {
                colorRNum[13]--;
            }
            button23.Invalidate();
        }

        private void button23_Paint(object sender, PaintEventArgs e)
        {
            if (colorRNum[13] <= 5)
            {
                draw.PaintRectangle(e.Graphics, button23, Color.Red, new Point(-1, 18), 10 * colorRNum[13], 12);
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            if (colorRNum[14] < 5)
            {
                colorRNum[14]++;
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            if (colorRNum[14] > 0)
            {
                colorRNum[14]--;
            }
            button21.Invalidate();
        }

        private void button21_Paint(object sender, PaintEventArgs e)
        {
            if (colorRNum[14] <= 5)
            {
                draw.PaintRectangle(e.Graphics, button21, Color.Green, new Point(-1, 2), 10 * colorRNum[14], 12);
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (colorRNum[15] < 5)
            {
                colorRNum[15]++;
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (colorRNum[15] > 0)
            {
                colorRNum[15]--;
            }
            button19.Invalidate();
        }

        private void button19_Paint(object sender, PaintEventArgs e)
        {
            if (colorRNum[15] <= 5)
            {
                draw.PaintRectangle(e.Graphics, button19, Color.Red, new Point(-1, 18), 10 * colorRNum[15], 12);
            }
        }
        #endregion

        #region 【删除】 - 表格 短观栏 颜色与字体变化

        ///// <summary>
        ///// 判断货币对的短观值的状态 常态与非常态
        ///// </summary>
        ///// <param name="mBoth">货币对 对象</param>
        ///// <returns>是与否</returns>
        //private bool IsSuperState( MoneyBoth mBoth )
        //{
        //    if(Setup.sIsGreenAndRed)
        //    {
        //        if (mBoth.moneyA.Color == mBoth.MoneyB.Color)
        //            return false;
        //    }
        //    if (mBoth.moneyA.Order == Setup.sWithoutNum1 ||
        //        mBoth.moneyA.Order == Setup.sWithoutNum2 ||
        //        mBoth.MoneyB.Order == Setup.sWithoutNum1 ||
        //        mBoth.MoneyB.Order == Setup.sWithoutNum2
        //        )
        //        return false;
        //    if (mBoth.moneyA.Order + mBoth.MoneyB.Order >= Setup.sOrderSumLess)
        //        return false;
        //    if (mBoth.moneyA.ShortM <= Setup.sShortAllMore ||
        //        mBoth.MoneyB.ShortM <= Setup.sShortAllMore
        //        )
        //        return false;
        //    if (mBoth.moneyA.LongM + mBoth.MoneyB.LongM <= Setup.sLongSumMore)
        //        return false;
        //    mBoth.ShortState = 1;
        //    DataShow.doubleSuperNum++;
        //    mBoth.DoubleGreen = true;
        //    return true;
        //}

        //private void shortA1_TextChanged(object sender, EventArgs e)
        //{
        //    if (IsSuperState(DataFiler.basicMoneyGroup[0].allMoneyBoth[0]))
        //    {
        //        shortA1.Font = new Font("黑体", 12, FontStyle.Regular);
        //        shortA1.BackColor = Color.Green;
        //        shortA1.ForeColor = Color.Black;
        //        shortB1.Font = new Font("黑体", 12, FontStyle.Regular);
        //        shortB1.BackColor = Color.Green;
        //        shortB1.ForeColor = Color.Black;
        //    }
        //    else
        //    {
        //        shortA1.Font = new Font("Arial", 10, FontStyle.Bold);
        //        shortA1.BackColor = tlpTable1.BackColor;
        //        shortB1.Font = new Font("Arial", 10, FontStyle.Bold);
        //        shortB1.BackColor = tlpTable1.BackColor;
        //        if (DataFiler.basicMoneyGroup[0].allMoneyBoth[0].moneyA.ShortM > 0)
        //        {
        //            shortA1.ForeColor = Color.Green;
        //        }
        //        else
        //        {
        //            shortA1.ForeColor = Color.Red;
        //        }
        //        if (DataFiler.basicMoneyGroup[0].allMoneyBoth[0].MoneyB.ShortM > 0)
        //        {
        //            shortB1.ForeColor = Color.Green;
        //        }
        //        else
        //        {
        //            shortB1.ForeColor = Color.Red;
        //        }
        //    }
        //}
        //private void shortA2_TextChanged(object sender, EventArgs e)
        //{
        //    if (IsSuperState(DataFiler.basicMoneyGroup[0].allMoneyBoth[1]))
        //    {
        //        shortA2.Font = new Font("黑体", 12, FontStyle.Regular);
        //        shortA2.BackColor = Color.Green;
        //        shortA2.ForeColor = Color.Black;
        //        shortB2.Font = new Font("黑体", 12, FontStyle.Regular);
        //        shortB2.BackColor = Color.Green;
        //        shortB2.ForeColor = Color.Black;
        //    }
        //    else
        //    {
        //        shortA2.Font = new Font("Arial", 10, FontStyle.Bold);
        //        shortA2.BackColor = tlpTable1.BackColor;
        //        shortB2.Font = new Font("Arial", 10, FontStyle.Bold);
        //        shortB2.BackColor = tlpTable1.BackColor;
        //        if (DataFiler.basicMoneyGroup[0].allMoneyBoth[1].moneyA.ShortM > 0)
        //        {
        //            shortA2.ForeColor = Color.Green;
        //        }
        //        else
        //        {
        //            shortA2.ForeColor = Color.Red;
        //        }
        //        if (DataFiler.basicMoneyGroup[0].allMoneyBoth[1].MoneyB.ShortM > 0)
        //        {
        //            shortB2.ForeColor = Color.Green;
        //        }
        //        else
        //        {
        //            shortB2.ForeColor = Color.Red;
        //        }
        //    }
        //}

        //private void shortA3_TextChanged(object sender, EventArgs e)
        //{
        //    if (IsSuperState(DataFiler.basicMoneyGroup[0].allMoneyBoth[2]))
        //    {
        //        shortA3.Font = new Font("黑体", 12, FontStyle.Regular);
        //        shortA3.BackColor = Color.Green;
        //        shortA3.ForeColor = Color.Black;
        //        shortB3.Font = new Font("黑体", 12, FontStyle.Regular);
        //        shortB3.BackColor = Color.Green;
        //        shortB3.ForeColor = Color.Black;
        //    }
        //    else
        //    {
        //        shortA3.Font = new Font("Arial", 10, FontStyle.Bold);
        //        shortA3.BackColor = tlpTable1.BackColor;
        //        shortB3.Font = new Font("Arial", 10, FontStyle.Bold);
        //        shortB3.BackColor = tlpTable1.BackColor;
        //        if (DataFiler.basicMoneyGroup[0].allMoneyBoth[2].moneyA.ShortM > 0)
        //        {
        //            shortA3.ForeColor = Color.Green;
        //        }
        //        else
        //        {
        //            shortA3.ForeColor = Color.Red;
        //        }
        //        if (DataFiler.basicMoneyGroup[0].allMoneyBoth[2].MoneyB.ShortM > 0)
        //        {
        //            shortB3.ForeColor = Color.Green;
        //        }
        //        else
        //        {
        //            shortB3.ForeColor = Color.Red;
        //        }
        //    }
        //}

        //private void shortA4_TextChanged(object sender, EventArgs e)
        //{
        //    if (IsSuperState(DataFiler.basicMoneyGroup[0].allMoneyBoth[3]))
        //    {
        //        shortA4.Font = new Font("黑体", 12, FontStyle.Regular);
        //        shortA4.BackColor = Color.Green;
        //        shortA4.ForeColor = Color.Black;
        //        shortB4.Font = new Font("黑体", 12, FontStyle.Regular);
        //        shortB4.BackColor = Color.Green;
        //        shortB4.ForeColor = Color.Black;
        //    }
        //    else
        //    {
        //        shortA4.Font = new Font("Arial", 10, FontStyle.Bold);
        //        shortA4.BackColor = tlpTable1.BackColor;
        //        shortB4.Font = new Font("Arial", 10, FontStyle.Bold);
        //        shortB4.BackColor = tlpTable1.BackColor;
        //        if (DataFiler.basicMoneyGroup[0].allMoneyBoth[3].moneyA.ShortM > 0)
        //        {
        //            shortA4.ForeColor = Color.Green;
        //        }
        //        else
        //        {
        //            shortA4.ForeColor = Color.Red;
        //        }
        //        if (DataFiler.basicMoneyGroup[0].allMoneyBoth[3].MoneyB.ShortM > 0)
        //        {
        //            shortB4.ForeColor = Color.Green;
        //        }
        //        else
        //        {
        //            shortB4.ForeColor = Color.Red;
        //        }
        //    }
        //}

        //private void shortA5_TextChanged(object sender, EventArgs e)
        //{
        //    if (IsSuperState(DataFiler.basicMoneyGroup[0].allMoneyBoth[4]))
        //    {
        //        shortA5.Font = new Font("黑体", 12, FontStyle.Regular);
        //        shortA5.BackColor = Color.Green;
        //        shortA5.ForeColor = Color.Black;
        //        shortB5.Font = new Font("黑体", 12, FontStyle.Regular);
        //        shortB5.BackColor = Color.Green;
        //        shortB5.ForeColor = Color.Black;
        //    }
        //    else
        //    {
        //        shortA5.Font = new Font("Arial", 10, FontStyle.Bold);
        //        shortA5.BackColor = tlpTable1.BackColor;
        //        shortB5.Font = new Font("Arial", 10, FontStyle.Bold);
        //        shortB5.BackColor = tlpTable1.BackColor;
        //        if (DataFiler.basicMoneyGroup[0].allMoneyBoth[4].moneyA.ShortM > 0)
        //        {
        //            shortA5.ForeColor = Color.Green;
        //        }
        //        else
        //        {
        //            shortA5.ForeColor = Color.Red;
        //        }
        //        if (DataFiler.basicMoneyGroup[0].allMoneyBoth[4].MoneyB.ShortM > 0)
        //        {
        //            shortB5.ForeColor = Color.Green;
        //        }
        //        else
        //        {
        //            shortB5.ForeColor = Color.Red;
        //        }
        //    }
        //}

        //private void shortA6_TextChanged(object sender, EventArgs e)
        //{
        //    if (IsSuperState(DataFiler.basicMoneyGroup[0].allMoneyBoth[5]))
        //    {
        //        shortA6.Font = new Font("黑体", 12, FontStyle.Regular);
        //        shortA6.BackColor = Color.Green;
        //        shortA6.ForeColor = Color.Black;
        //        shortB6.Font = new Font("黑体", 12, FontStyle.Regular);
        //        shortB6.BackColor = Color.Green;
        //        shortB6.ForeColor = Color.Black;
        //    }
        //    else
        //    {
        //        shortA6.Font = new Font("Arial", 10, FontStyle.Bold);
        //        shortA6.BackColor = tlpTable1.BackColor;
        //        shortB6.Font = new Font("Arial", 10, FontStyle.Bold);
        //        shortB6.BackColor = tlpTable1.BackColor;
        //        if (DataFiler.basicMoneyGroup[0].allMoneyBoth[5].moneyA.ShortM > 0)
        //        {
        //            shortA6.ForeColor = Color.Green;
        //        }
        //        else
        //        {
        //            shortA6.ForeColor = Color.Red;
        //        }
        //        if (DataFiler.basicMoneyGroup[0].allMoneyBoth[5].MoneyB.ShortM > 0)
        //        {
        //            shortB6.ForeColor = Color.Green;
        //        }
        //        else
        //        {
        //            shortB6.ForeColor = Color.Red;
        //        }
        //    }
        //}

        //private void shortA7_TextChanged(object sender, EventArgs e)
        //{
        //    if (IsSuperState(DataFiler.basicMoneyGroup[0].allMoneyBoth[6]))
        //    {
        //        shortA7.Font = new Font("黑体", 12, FontStyle.Regular);
        //        shortA7.BackColor = Color.Green;
        //        shortA7.ForeColor = Color.Black;
        //        shortB7.Font = new Font("黑体", 12, FontStyle.Regular);
        //        shortB7.BackColor = Color.Green;
        //        shortB7.ForeColor = Color.Black;
        //    }
        //    else
        //    {
        //        shortA7.Font = new Font("Arial", 10, FontStyle.Bold);
        //        shortA7.BackColor = tlpTable1.BackColor;
        //        shortB7.Font = new Font("Arial", 10, FontStyle.Bold);
        //        shortB7.BackColor = tlpTable1.BackColor;
        //        if (DataFiler.basicMoneyGroup[0].allMoneyBoth[6].moneyA.ShortM > 0)
        //        {
        //            shortA7.ForeColor = Color.Green;
        //        }
        //        else
        //        {
        //            shortA7.ForeColor = Color.Red;
        //        }
        //        if (DataFiler.basicMoneyGroup[0].allMoneyBoth[6].MoneyB.ShortM > 0)
        //        {
        //            shortB7.ForeColor = Color.Green;
        //        }
        //        else
        //        {
        //            shortB7.ForeColor = Color.Red;
        //        }
        //    }
        //}

        //private void shortA8_TextChanged(object sender, EventArgs e)
        //{
        //    if (IsSuperState(DataFiler.basicMoneyGroup[1].allMoneyBoth[0]))
        //    {
        //        shortA8.Font = new Font("黑体", 12, FontStyle.Regular);
        //        shortA8.BackColor = Color.Green;
        //        shortA8.ForeColor = Color.Black;
        //        shortB8.Font = new Font("黑体", 12, FontStyle.Regular);
        //        shortB8.BackColor = Color.Green;
        //        shortB8.ForeColor = Color.Black;
        //    }
        //    else
        //    {
        //        shortA8.Font = new Font("Arial", 10, FontStyle.Bold);
        //        shortA8.BackColor = tlpTable2.BackColor;
        //        shortB8.Font = new Font("Arial", 10, FontStyle.Bold);
        //        shortB8.BackColor = tlpTable2.BackColor;
        //        if (DataFiler.basicMoneyGroup[1].allMoneyBoth[0].moneyA.ShortM > 0)
        //        {
        //            shortA8.ForeColor = Color.Green;
        //        }
        //        else
        //        {
        //            shortA8.ForeColor = Color.Red;
        //        }
        //        if (DataFiler.basicMoneyGroup[1].allMoneyBoth[0].MoneyB.ShortM > 0)
        //        {
        //            shortB8.ForeColor = Color.Green;
        //        }
        //        else
        //        {
        //            shortB8.ForeColor = Color.Red;
        //        }
        //    }
        //}

        //private void shortA9_TextChanged(object sender, EventArgs e)
        //{
        //    if (IsSuperState(DataFiler.basicMoneyGroup[1].allMoneyBoth[1]))
        //    {
        //        shortA9.Font = new Font("黑体", 12, FontStyle.Regular);
        //        shortA9.BackColor = Color.Green;
        //        shortA9.ForeColor = Color.Black;
        //        shortB9.Font = new Font("黑体", 12, FontStyle.Regular);
        //        shortB9.BackColor = Color.Green;
        //        shortB9.ForeColor = Color.Black;
        //    }
        //    else
        //    {
        //        shortA9.Font = new Font("Arial", 10, FontStyle.Bold);
        //        shortA9.BackColor = tlpTable2.BackColor;
        //        shortB9.Font = new Font("Arial", 10, FontStyle.Bold);
        //        shortB9.BackColor = tlpTable2.BackColor;
        //        if (DataFiler.basicMoneyGroup[1].allMoneyBoth[1].moneyA.ShortM > 0)
        //        {
        //            shortA9.ForeColor = Color.Green;
        //        }
        //        else
        //        {
        //            shortA9.ForeColor = Color.Red;
        //        }
        //        if (DataFiler.basicMoneyGroup[1].allMoneyBoth[1].MoneyB.ShortM > 0)
        //        {
        //            shortB9.ForeColor = Color.Green;
        //        }
        //        else
        //        {
        //            shortB9.ForeColor = Color.Red;
        //        }
        //    }
        //}

        //private void shortA10_TextChanged(object sender, EventArgs e)
        //{
        //    if (IsSuperState(DataFiler.basicMoneyGroup[1].allMoneyBoth[2]))
        //    {
        //        shortA10.Font = new Font("黑体", 12, FontStyle.Regular);
        //        shortA10.BackColor = Color.Green;
        //        shortA10.ForeColor = Color.Black;
        //        shortB10.Font = new Font("黑体", 12, FontStyle.Regular);
        //        shortB10.BackColor = Color.Green;
        //        shortB10.ForeColor = Color.Black;
        //    }
        //    else
        //    {
        //        shortA10.Font = new Font("Arial", 10, FontStyle.Bold);
        //        shortA10.BackColor = tlpTable2.BackColor;
        //        shortB10.Font = new Font("Arial", 10, FontStyle.Bold);
        //        shortB10.BackColor = tlpTable2.BackColor;
        //        if (DataFiler.basicMoneyGroup[1].allMoneyBoth[2].moneyA.ShortM > 0)
        //        {
        //            shortA10.ForeColor = Color.Green;
        //        }
        //        else
        //        {
        //            shortA10.ForeColor = Color.Red;
        //        }
        //        if (DataFiler.basicMoneyGroup[1].allMoneyBoth[2].MoneyB.ShortM > 0)
        //        {
        //            shortB10.ForeColor = Color.Green;
        //        }
        //        else
        //        {
        //            shortB10.ForeColor = Color.Red;
        //        }
        //    }
        //}

        //private void shortA11_TextChanged(object sender, EventArgs e)
        //{
        //    if (IsSuperState(DataFiler.basicMoneyGroup[1].allMoneyBoth[3]))
        //    {
        //        shortA11.Font = new Font("黑体", 12, FontStyle.Regular);
        //        shortA11.BackColor = Color.Green;
        //        shortA11.ForeColor = Color.Black;
        //        shortB11.Font = new Font("黑体", 12, FontStyle.Regular);
        //        shortB11.BackColor = Color.Green;
        //        shortB11.ForeColor = Color.Black;
        //    }
        //    else
        //    {
        //        shortA11.Font = new Font("Arial", 10, FontStyle.Bold);
        //        shortA11.BackColor = tlpTable2.BackColor;
        //        shortB11.Font = new Font("Arial", 10, FontStyle.Bold);
        //        shortB11.BackColor = tlpTable2.BackColor;
        //        if (DataFiler.basicMoneyGroup[1].allMoneyBoth[3].moneyA.ShortM > 0)
        //        {
        //            shortA11.ForeColor = Color.Green;
        //        }
        //        else
        //        {
        //            shortA11.ForeColor = Color.Red;
        //        }
        //        if (DataFiler.basicMoneyGroup[1].allMoneyBoth[3].MoneyB.ShortM > 0)
        //        {
        //            shortB11.ForeColor = Color.Green;
        //        }
        //        else
        //        {
        //            shortB11.ForeColor = Color.Red;
        //        }
        //    }
        //}

        //private void shortA12_TextChanged(object sender, EventArgs e)
        //{
        //    if (IsSuperState(DataFiler.basicMoneyGroup[1].allMoneyBoth[4]))
        //    {
        //        shortA12.Font = new Font("黑体", 12, FontStyle.Regular);
        //        shortA12.BackColor = Color.Green;
        //        shortA12.ForeColor = Color.Black;
        //        shortB12.Font = new Font("黑体", 12, FontStyle.Regular);
        //        shortB12.BackColor = Color.Green;
        //        shortB12.ForeColor = Color.Black;
        //    }
        //    else
        //    {
        //        shortA12.Font = new Font("Arial", 10, FontStyle.Bold);
        //        shortA12.BackColor = tlpTable2.BackColor;
        //        shortB12.Font = new Font("Arial", 10, FontStyle.Bold);
        //        shortB12.BackColor = tlpTable2.BackColor;
        //        if (DataFiler.basicMoneyGroup[1].allMoneyBoth[4].moneyA.ShortM > 0)
        //        {
        //            shortA12.ForeColor = Color.Green;
        //        }
        //        else
        //        {
        //            shortA12.ForeColor = Color.Red;
        //        }
        //        if (DataFiler.basicMoneyGroup[1].allMoneyBoth[4].MoneyB.ShortM > 0)
        //        {
        //            shortB12.ForeColor = Color.Green;
        //        }
        //        else
        //        {
        //            shortB12.ForeColor = Color.Red;
        //        }
        //    }
        //}

        //private void shortA13_TextChanged(object sender, EventArgs e)
        //{
        //    if (IsSuperState(DataFiler.basicMoneyGroup[1].allMoneyBoth[5]))
        //    {
        //        shortA13.Font = new Font("黑体", 12, FontStyle.Regular);
        //        shortA13.BackColor = Color.Green;
        //        shortA13.ForeColor = Color.Black;
        //        shortB13.Font = new Font("黑体", 12, FontStyle.Regular);
        //        shortB13.BackColor = Color.Green;
        //        shortB13.ForeColor = Color.Black;
        //    }
        //    else
        //    {
        //        shortA13.Font = new Font("Arial", 10, FontStyle.Bold);
        //        shortA13.BackColor = tlpTable2.BackColor;
        //        shortB13.Font = new Font("Arial", 10, FontStyle.Bold);
        //        shortB13.BackColor = tlpTable2.BackColor;
        //        if (DataFiler.basicMoneyGroup[1].allMoneyBoth[5].moneyA.ShortM > 0)
        //        {
        //            shortA13.ForeColor = Color.Green;
        //        }
        //        else
        //        {
        //            shortA13.ForeColor = Color.Red;
        //        }
        //        if (DataFiler.basicMoneyGroup[1].allMoneyBoth[5].MoneyB.ShortM > 0)
        //        {
        //            shortB13.ForeColor = Color.Green;
        //        }
        //        else
        //        {
        //            shortB13.ForeColor = Color.Red;
        //        }
        //    }
        //}

        ////3-1
        //private void shortA14_TextChanged(object sender, EventArgs e)
        //{
        //    if (IsSuperState(DataFiler.basicMoneyGroup[2].allMoneyBoth[0]))
        //    {
        //        shortA14.Font = new Font("Arial", 12, FontStyle.Regular);
        //        shortA14.BackColor = Color.Green;
        //        shortA14.ForeColor = Color.Black;
        //        shortB14.Font = new Font("Arial", 12, FontStyle.Regular);
        //        shortB14.BackColor = Color.Green;
        //        shortB14.ForeColor = Color.Black;
        //    }
        //    else
        //    {
        //        shortA14.Font = new Font("Arial", 10, FontStyle.Bold);
        //        shortA14.BackColor = tlpTable3.BackColor;
        //        shortB14.Font = new Font("Arial", 10, FontStyle.Bold);
        //        shortB14.BackColor = tlpTable3.BackColor;
        //        if (DataFiler.basicMoneyGroup[2].allMoneyBoth[0].moneyA.ShortM > 0)
        //        {
        //            shortA14.ForeColor = Color.Green;
        //        }
        //        else
        //        {
        //            shortA14.ForeColor = Color.Red;
        //        }
        //        if (DataFiler.basicMoneyGroup[2].allMoneyBoth[0].MoneyB.ShortM > 0)
        //        {
        //            shortB14.ForeColor = Color.Green;
        //        }
        //        else
        //        {
        //            shortB14.ForeColor = Color.Red;
        //        }
        //    }
        //}

        //private void shortA15_TextChanged(object sender, EventArgs e)
        //{
        //    if (IsSuperState(DataFiler.basicMoneyGroup[2].allMoneyBoth[1]))
        //    {
        //        shortA15.Font = new Font("黑体", 12, FontStyle.Regular);
        //        shortA15.BackColor = Color.Green;
        //        shortA15.ForeColor = Color.Black;
        //        shortB15.Font = new Font("黑体", 12, FontStyle.Regular);
        //        shortB15.BackColor = Color.Green;
        //        shortB15.ForeColor = Color.Black;
        //    }
        //    else
        //    {
        //        shortA15.Font = new Font("Arial", 10, FontStyle.Bold);
        //        shortA15.BackColor = tlpTable3.BackColor;
        //        shortB15.Font = new Font("Arial", 10, FontStyle.Bold);
        //        shortB15.BackColor = tlpTable3.BackColor;
        //        if (DataFiler.basicMoneyGroup[2].allMoneyBoth[1].moneyA.ShortM > 0)
        //        {
        //            shortA15.ForeColor = Color.Green;
        //        }
        //        else
        //        {
        //            shortA15.ForeColor = Color.Red;
        //        }
        //        if (DataFiler.basicMoneyGroup[2].allMoneyBoth[1].MoneyB.ShortM > 0)
        //        {
        //            shortB15.ForeColor = Color.Green;
        //        }
        //        else
        //        {
        //            shortB15.ForeColor = Color.Red;
        //        }
        //    }
        //}

        //private void shortA16_TextChanged(object sender, EventArgs e)
        //{
        //    if (IsSuperState(DataFiler.basicMoneyGroup[2].allMoneyBoth[2]))
        //    {
        //        shortA16.Font = new Font("黑体", 12, FontStyle.Regular);
        //        shortA16.BackColor = Color.Green;
        //        shortA16.ForeColor = Color.Black;
        //        shortB16.Font = new Font("黑体", 12, FontStyle.Regular);
        //        shortB16.BackColor = Color.Green;
        //        shortB16.ForeColor = Color.Black;
        //    }
        //    else
        //    {
        //        shortA16.Font = new Font("Arial", 10, FontStyle.Bold);
        //        shortA16.BackColor = tlpTable3.BackColor;
        //        shortB16.Font = new Font("Arial", 10, FontStyle.Bold);
        //        shortB16.BackColor = tlpTable3.BackColor;
        //        if (DataFiler.basicMoneyGroup[2].allMoneyBoth[2].moneyA.ShortM > 0)
        //        {
        //            shortA16.ForeColor = Color.Green;
        //        }
        //        else
        //        {
        //            shortA16.ForeColor = Color.Red;
        //        }
        //        if (DataFiler.basicMoneyGroup[2].allMoneyBoth[2].MoneyB.ShortM > 0)
        //        {
        //            shortB16.ForeColor = Color.Green;
        //        }
        //        else
        //        {
        //            shortB16.ForeColor = Color.Red;
        //        }
        //    }
        //}

        //private void shortA17_TextChanged(object sender, EventArgs e)
        //{
        //    if (IsSuperState(DataFiler.basicMoneyGroup[2].allMoneyBoth[3]))
        //    {
        //        shortA17.Font = new Font("黑体", 12, FontStyle.Regular);
        //        shortA17.BackColor = Color.Green;
        //        shortA17.ForeColor = Color.Black;
        //        shortB17.Font = new Font("黑体", 12, FontStyle.Regular);
        //        shortB17.BackColor = Color.Green;
        //        shortB17.ForeColor = Color.Black;
        //    }
        //    else
        //    {
        //        shortA17.Font = new Font("Arial", 10, FontStyle.Bold);
        //        shortA17.BackColor = tlpTable3.BackColor;
        //        shortB17.Font = new Font("Arial", 10, FontStyle.Bold);
        //        shortB17.BackColor = tlpTable3.BackColor;
        //        if (DataFiler.basicMoneyGroup[2].allMoneyBoth[3].moneyA.ShortM > 0)
        //        {
        //            shortA17.ForeColor = Color.Green;
        //        }
        //        else
        //        {
        //            shortA17.ForeColor = Color.Red;
        //        }
        //        if (DataFiler.basicMoneyGroup[2].allMoneyBoth[3].MoneyB.ShortM > 0)
        //        {
        //            shortB17.ForeColor = Color.Green;
        //        }
        //        else
        //        {
        //            shortB17.ForeColor = Color.Red;
        //        }
        //    }
        //}

        //private void shortA18_TextChanged(object sender, EventArgs e)
        //{
        //    if (IsSuperState(DataFiler.basicMoneyGroup[2].allMoneyBoth[4]))
        //    {
        //        shortA18.Font = new Font("黑体", 12, FontStyle.Regular);
        //        shortA18.BackColor = Color.Green;
        //        shortA18.ForeColor = Color.Black;
        //        shortB18.Font = new Font("黑体", 12, FontStyle.Regular);
        //        shortB18.BackColor = Color.Green;
        //        shortB18.ForeColor = Color.Black;
        //    }
        //    else
        //    {
        //        shortA18.Font = new Font("Arial", 10, FontStyle.Bold);
        //        shortA18.BackColor = tlpTable3.BackColor;
        //        shortB18.Font = new Font("Arial", 10, FontStyle.Bold);
        //        shortB18.BackColor = tlpTable3.BackColor;
        //        if (DataFiler.basicMoneyGroup[2].allMoneyBoth[4].moneyA.ShortM > 0)
        //        {
        //            shortA18.ForeColor = Color.Green;
        //        }
        //        else
        //        {
        //            shortA18.ForeColor = Color.Red;
        //        }
        //        if (DataFiler.basicMoneyGroup[2].allMoneyBoth[4].MoneyB.ShortM > 0)
        //        {
        //            shortB18.ForeColor = Color.Green;
        //        }
        //        else
        //        {
        //            shortB18.ForeColor = Color.Red;
        //        }
        //    }
        //}

        //private void shortA19_TextChanged(object sender, EventArgs e)
        //{
        //    if (IsSuperState(DataFiler.basicMoneyGroup[3].allMoneyBoth[0]))
        //    {
        //        shortA19.Font = new Font("黑体", 12, FontStyle.Regular);
        //        shortA19.BackColor = Color.Green;
        //        shortA19.ForeColor = Color.Black;
        //        shortB19.Font = new Font("黑体", 12, FontStyle.Regular);
        //        shortB19.BackColor = Color.Green;
        //        shortB19.ForeColor = Color.Black;
        //    }
        //    else
        //    {
        //        shortA19.Font = new Font("Arial", 10, FontStyle.Bold);
        //        shortA19.BackColor = tlpTable4.BackColor;
        //        shortB19.Font = new Font("Arial", 10, FontStyle.Bold);
        //        shortB19.BackColor = tlpTable4.BackColor;
        //        if (DataFiler.basicMoneyGroup[3].allMoneyBoth[0].moneyA.ShortM > 0)
        //        {
        //            shortA19.ForeColor = Color.Green;
        //        }
        //        else
        //        {
        //            shortA19.ForeColor = Color.Red;
        //        }
        //        if (DataFiler.basicMoneyGroup[3].allMoneyBoth[0].MoneyB.ShortM > 0)
        //        {
        //            shortB19.ForeColor = Color.Green;
        //        }
        //        else
        //        {
        //            shortB19.ForeColor = Color.Red;
        //        }
        //    }
        //}

        //private void shortA20_TextChanged(object sender, EventArgs e)
        //{
        //    if (IsSuperState(DataFiler.basicMoneyGroup[3].allMoneyBoth[1]))
        //    {
        //        shortA20.Font = new Font("黑体", 12, FontStyle.Regular);
        //        shortA20.BackColor = Color.Green;
        //        shortA20.ForeColor = Color.Black;
        //        shortB20.Font = new Font("黑体", 12, FontStyle.Regular);
        //        shortB20.BackColor = Color.Green;
        //        shortB20.ForeColor = Color.Black;
        //    }
        //    else
        //    {
        //        shortA20.Font = new Font("Arial", 10, FontStyle.Bold);
        //        shortA20.BackColor = tlpTable4.BackColor;
        //        shortB20.Font = new Font("Arial", 10, FontStyle.Bold);
        //        shortB20.BackColor = tlpTable4.BackColor;
        //        if (DataFiler.basicMoneyGroup[3].allMoneyBoth[1].moneyA.ShortM > 0)
        //        {
        //            shortA20.ForeColor = Color.Green;
        //        }
        //        else
        //        {
        //            shortA20.ForeColor = Color.Red;
        //        }
        //        if (DataFiler.basicMoneyGroup[3].allMoneyBoth[1].MoneyB.ShortM > 0)
        //        {
        //            shortB20.ForeColor = Color.Green;
        //        }
        //        else
        //        {
        //            shortB20.ForeColor = Color.Red;
        //        }
        //    }
        //}

        //private void shortA21_TextChanged(object sender, EventArgs e)
        //{
        //    if (IsSuperState(DataFiler.basicMoneyGroup[3].allMoneyBoth[2]))
        //    {
        //        shortA21.Font = new Font("黑体", 12, FontStyle.Regular);
        //        shortA21.BackColor = Color.Green;
        //        shortA21.ForeColor = Color.Black;
        //        shortB21.Font = new Font("黑体", 12, FontStyle.Regular);
        //        shortB21.BackColor = Color.Green;
        //        shortB21.ForeColor = Color.Black;
        //    }
        //    else
        //    {
        //        shortA21.Font = new Font("Arial", 10, FontStyle.Bold);
        //        shortA21.BackColor = tlpTable4.BackColor;
        //        shortB21.Font = new Font("Arial", 10, FontStyle.Bold);
        //        shortB21.BackColor = tlpTable4.BackColor;
        //        if (DataFiler.basicMoneyGroup[3].allMoneyBoth[2].moneyA.ShortM > 0)
        //        {
        //            shortA21.ForeColor = Color.Green;
        //        }
        //        else
        //        {
        //            shortA21.ForeColor = Color.Red;
        //        }
        //        if (DataFiler.basicMoneyGroup[3].allMoneyBoth[2].MoneyB.ShortM > 0)
        //        {
        //            shortB21.ForeColor = Color.Green;
        //        }
        //        else
        //        {
        //            shortB21.ForeColor = Color.Red;
        //        }
        //    }
        //}

        //private void shortA22_TextChanged(object sender, EventArgs e)
        //{
        //    if (IsSuperState(DataFiler.basicMoneyGroup[3].allMoneyBoth[3]))
        //    {
        //        shortA22.Font = new Font("黑体", 12, FontStyle.Regular);
        //        shortA22.BackColor = Color.Green;
        //        shortA22.ForeColor = Color.Black;
        //        shortB22.Font = new Font("黑体", 12, FontStyle.Regular);
        //        shortB22.BackColor = Color.Green;
        //        shortB22.ForeColor = Color.Black;
        //    }
        //    else
        //    {
        //        shortA22.Font = new Font("Arial", 10, FontStyle.Bold);
        //        shortA22.BackColor = tlpTable4.BackColor;
        //        shortB22.Font = new Font("Arial", 10, FontStyle.Bold);
        //        shortB22.BackColor = tlpTable4.BackColor;
        //        if (DataFiler.basicMoneyGroup[3].allMoneyBoth[3].moneyA.ShortM > 0)
        //        {
        //            shortA22.ForeColor = Color.Green;
        //        }
        //        else
        //        {
        //            shortA22.ForeColor = Color.Red;
        //        }
        //        if (DataFiler.basicMoneyGroup[3].allMoneyBoth[3].MoneyB.ShortM > 0)
        //        {
        //            shortB22.ForeColor = Color.Green;
        //        }
        //        else
        //        {
        //            shortB22.ForeColor = Color.Red;
        //        }
        //    }
        //}
        //// -1
        //private void shortA23_TextChanged(object sender, EventArgs e)
        //{
        //    if (IsSuperState(DataFiler.basicMoneyGroup[4].allMoneyBoth[0]))
        //    {
        //        shortA23.Font = new Font("黑体", 12, FontStyle.Regular);
        //        shortA23.BackColor = Color.Green;
        //        shortA23.ForeColor = Color.Black;
        //        shortB23.Font = new Font("黑体", 12, FontStyle.Regular);
        //        shortB23.BackColor = Color.Green;
        //        shortB23.ForeColor = Color.Black;
        //    }
        //    else
        //    {
        //        shortA23.Font = new Font("Arial", 10, FontStyle.Bold);
        //        shortA23.BackColor = tlpTable5.BackColor;
        //        shortB23.Font = new Font("Arial", 10, FontStyle.Bold);
        //        shortB23.BackColor = tlpTable5.BackColor;
        //        if (DataFiler.basicMoneyGroup[4].allMoneyBoth[0].moneyA.ShortM > 0)
        //        {
        //            shortA23.ForeColor = Color.Green;
        //        }
        //        else
        //        {
        //            shortA23.ForeColor = Color.Red;
        //        }
        //        if (DataFiler.basicMoneyGroup[4].allMoneyBoth[0].MoneyB.ShortM > 0)
        //        {
        //            shortB23.ForeColor = Color.Green;
        //        }
        //        else
        //        {
        //            shortB23.ForeColor = Color.Red;
        //        }
        //    }
        //}

        //private void shortA24_TextChanged(object sender, EventArgs e)
        //{
        //    if (IsSuperState(DataFiler.basicMoneyGroup[4].allMoneyBoth[1]))
        //    {
        //        shortA24.Font = new Font("黑体", 12, FontStyle.Regular);
        //        shortA24.BackColor = Color.Green;
        //        shortA24.ForeColor = Color.Black;
        //        shortB24.Font = new Font("黑体", 12, FontStyle.Regular);
        //        shortB24.BackColor = Color.Green;
        //        shortB24.ForeColor = Color.Black;
        //    }
        //    else
        //    {
        //        shortA24.Font = new Font("Arial", 10, FontStyle.Bold);
        //        shortA24.BackColor = tlpTable5.BackColor;
        //        shortB24.Font = new Font("Arial", 10, FontStyle.Bold);
        //        shortB24.BackColor = tlpTable5.BackColor;
        //        if (DataFiler.basicMoneyGroup[4].allMoneyBoth[1].moneyA.ShortM > 0)
        //        {
        //            shortA24.ForeColor = Color.Green;
        //        }
        //        else
        //        {
        //            shortA24.ForeColor = Color.Red;
        //        }
        //        if (DataFiler.basicMoneyGroup[4].allMoneyBoth[1].MoneyB.ShortM > 0)
        //        {
        //            shortB24.ForeColor = Color.Green;
        //        }
        //        else
        //        {
        //            shortB24.ForeColor = Color.Red;
        //        }
        //    }
        //}

        //private void shortA25_TextChanged(object sender, EventArgs e)
        //{
        //    if (IsSuperState(DataFiler.basicMoneyGroup[4].allMoneyBoth[2]))
        //    {
        //        shortA25.Font = new Font("黑体", 12, FontStyle.Regular);
        //        shortA25.BackColor = Color.Green;
        //        shortA25.ForeColor = Color.Black;
        //        shortB25.Font = new Font("黑体", 12, FontStyle.Regular);
        //        shortB25.BackColor = Color.Green;
        //        shortB25.ForeColor = Color.Black;
        //    }
        //    else
        //    {
        //        shortA25.Font = new Font("Arial", 10, FontStyle.Bold);
        //        shortA25.BackColor = tlpTable5.BackColor;
        //        shortB25.Font = new Font("Arial", 10, FontStyle.Bold);
        //        shortB25.BackColor = tlpTable5.BackColor;
        //        if (DataFiler.basicMoneyGroup[4].allMoneyBoth[2].moneyA.ShortM > 0)
        //        {
        //            shortA25.ForeColor = Color.Green;
        //        }
        //        else
        //        {
        //            shortA25.ForeColor = Color.Red;
        //        }
        //        if (DataFiler.basicMoneyGroup[4].allMoneyBoth[2].MoneyB.ShortM > 0)
        //        {
        //            shortB25.ForeColor = Color.Green;
        //        }
        //        else
        //        {
        //            shortB25.ForeColor = Color.Red;
        //        }
        //    }
        //}

        //private void shortA26_TextChanged(object sender, EventArgs e)
        //{
        //    if (IsSuperState(DataFiler.basicMoneyGroup[5].allMoneyBoth[0]))
        //    {
        //        shortA26.Font = new Font("黑体", 12, FontStyle.Regular);
        //        shortA26.BackColor = Color.Green;
        //        shortA26.ForeColor = Color.Black;
        //        shortB26.Font = new Font("黑体", 12, FontStyle.Regular);
        //        shortB26.BackColor = Color.Green;
        //        shortB26.ForeColor = Color.Black;
        //    }
        //    else
        //    {
        //        shortA26.Font = new Font("Arial", 10, FontStyle.Bold);
        //        shortA26.BackColor = tlpTable6.BackColor;
        //        shortB26.Font = new Font("Arial", 10, FontStyle.Bold);
        //        shortB26.BackColor = tlpTable6.BackColor;
        //        if (DataFiler.basicMoneyGroup[5].allMoneyBoth[0].moneyA.ShortM > 0)
        //        {
        //            shortA26.ForeColor = Color.Green;
        //        }
        //        else
        //        {
        //            shortA26.ForeColor = Color.Red;
        //        }
        //        if (DataFiler.basicMoneyGroup[5].allMoneyBoth[0].MoneyB.ShortM > 0)
        //        {
        //            shortB26.ForeColor = Color.Green;
        //        }
        //        else
        //        {
        //            shortB26.ForeColor = Color.Red;
        //        }
        //    }
        //}

        //private void shortA27_TextChanged(object sender, EventArgs e)
        //{
        //    if (IsSuperState(DataFiler.basicMoneyGroup[5].allMoneyBoth[1]))
        //    {
        //        shortA27.Font = new Font("黑体", 12, FontStyle.Regular);
        //        shortA27.BackColor = Color.Green;
        //        shortA27.ForeColor = Color.Black;
        //        shortB27.Font = new Font("黑体", 12, FontStyle.Regular);
        //        shortB27.BackColor = Color.Green;
        //        shortB27.ForeColor = Color.Black;
        //    }
        //    else
        //    {
        //        shortA27.Font = new Font("Arial", 10, FontStyle.Bold);
        //        shortA27.BackColor = tlpTable6.BackColor;
        //        shortB27.Font = new Font("Arial", 10, FontStyle.Bold);
        //        shortB27.BackColor = tlpTable6.BackColor;
        //        if (DataFiler.basicMoneyGroup[5].allMoneyBoth[1].moneyA.ShortM > 0)
        //        {
        //            shortA27.ForeColor = Color.Green;
        //        }
        //        else
        //        {
        //            shortA27.ForeColor = Color.Red;
        //        }
        //        if (DataFiler.basicMoneyGroup[5].allMoneyBoth[1].MoneyB.ShortM > 0)
        //        {
        //            shortB27.ForeColor = Color.Green;
        //        }
        //        else
        //        {
        //            shortB27.ForeColor = Color.Red;
        //        }
        //    }
        //}

        //private void shortA28_TextChanged(object sender, EventArgs e)
        //{
        //    if (IsSuperState(DataFiler.basicMoneyGroup[6].allMoneyBoth[0]))
        //    {
        //        shortA28.Font = new Font("黑体", 12, FontStyle.Regular);
        //        shortA28.BackColor = Color.Green;
        //        shortA28.ForeColor = Color.Black;
        //        shortB28.Font = new Font("黑体", 12, FontStyle.Regular);
        //        shortB28.BackColor = Color.Green;
        //        shortB28.ForeColor = Color.Black;
        //    }
        //    else
        //    {
        //        shortA28.Font = new Font("Arial", 10, FontStyle.Bold);
        //        shortA28.BackColor = tlpTable7.BackColor;
        //        shortB28.Font = new Font("Arial", 10, FontStyle.Bold);
        //        shortB28.BackColor = tlpTable7.BackColor;
        //        if (DataFiler.basicMoneyGroup[6].allMoneyBoth[0].moneyA.ShortM > 0)
        //        {
        //            shortA28.ForeColor = Color.Green;
        //        }
        //        else
        //        {
        //            shortA28.ForeColor = Color.Red;
        //        }
        //        if (DataFiler.basicMoneyGroup[6].allMoneyBoth[0].MoneyB.ShortM > 0)
        //        {
        //            shortB28.ForeColor = Color.Green;
        //        }
        //        else
        //        {
        //            shortB28.ForeColor = Color.Red;
        //        }
        //    }
        //}

        #endregion

        #region 点击货币对触发事件

        //1.1-7
        private void mbName1_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 1;
            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[0].allMoneyBoth[0].Clone();

            //Date


        }
        private void lblMB1_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 1;
            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[0].allMoneyBoth[0].Clone();
        }
        private void mbName2_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 2;
            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[0].allMoneyBoth[1].Clone();
        }
        private void lblMB2_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 2;
            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[0].allMoneyBoth[1].Clone();
        }
        private void mbName3_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 3;
            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[0].allMoneyBoth[2].Clone();
        }
        private void lblMB3_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 3;
            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[0].allMoneyBoth[2].Clone();
        }
        private void mbName4_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 4;
            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[0].allMoneyBoth[3].Clone();
        }
        private void lblMB4_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 4;
            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[0].allMoneyBoth[3].Clone();
        }
        private void mbName5_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 5;
            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[0].allMoneyBoth[4].Clone();
        }
        private void lblMB5_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 5;
            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[0].allMoneyBoth[4].Clone();
        }
        private void mbName6_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 6;
            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[0].allMoneyBoth[5].Clone();
        }
        private void lblMB6_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 6;
            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[0].allMoneyBoth[5].Clone();
        }
        private void mbName7_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 7;
            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[0].allMoneyBoth[6].Clone();
        }
        private void lblMB7_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 7;
            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[0].allMoneyBoth[6].Clone();
        }

        //2.1-6
        private void mbName8_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 8;
            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[1].allMoneyBoth[0].Clone();
        }
        private void lblMB8_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 8;
            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[1].allMoneyBoth[0].Clone();
        }
        private void mbName9_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 9;
            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[1].allMoneyBoth[1].Clone();
        }
        private void lblMB9_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 9;
            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[1].allMoneyBoth[1].Clone();
        }
        private void mbName10_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 10;
            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[1].allMoneyBoth[2].Clone();
        }
        private void lblMB10_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 10;
            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[1].allMoneyBoth[2].Clone();
        }
        private void mbName11_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 11;
            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[1].allMoneyBoth[3].Clone();
        }
        private void lblMB11_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 11;
            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[1].allMoneyBoth[3].Clone();
        }
        private void mbName12_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 12;
            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[1].allMoneyBoth[4].Clone();
        }
        private void lblMB12_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 12;
            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[1].allMoneyBoth[4].Clone();
        }
        private void mbName13_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 13;
            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[1].allMoneyBoth[5].Clone();
        }
        private void lblMB13_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 13;
            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[1].allMoneyBoth[5].Clone();
        }

        //3.1-5
        private void mbName14_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 14;
            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[2].allMoneyBoth[0].Clone();
        }
        private void lblMB14_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 14;
            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[2].allMoneyBoth[0].Clone();
        }
        private void mbName15_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 15;
            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[2].allMoneyBoth[1].Clone();
        }
        private void lblMB15_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 15;
            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[2].allMoneyBoth[1].Clone();
        }
        private void mbName16_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 16;
            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[2].allMoneyBoth[2].Clone();
        }
        private void lblMB16_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 16;
            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[2].allMoneyBoth[2].Clone();
        }
        private void mbName17_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 17;
            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[2].allMoneyBoth[3].Clone();
        }
        private void lblMB17_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 17;
            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[2].allMoneyBoth[3].Clone();
        }
        private void mbName18_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 18;
            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[2].allMoneyBoth[4].Clone();
        }
        private void lblMB18_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 18;
            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[2].allMoneyBoth[4].Clone();
        }

        //4.1-4
        private void mbName19_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 19;
            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[3].allMoneyBoth[0].Clone();
        }
        private void lblMB19_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 19;
            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[3].allMoneyBoth[0].Clone();
        }
        private void mbName20_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 20;
            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[3].allMoneyBoth[1].Clone();
        }
        private void lblMB20_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 20;
            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[3].allMoneyBoth[1].Clone();
        }
        private void mbName21_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 21;
            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[3].allMoneyBoth[2].Clone();
        }
        private void lblMB21_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 21;
            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[3].allMoneyBoth[2].Clone();
        }
        private void mbName22_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 22;
            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[3].allMoneyBoth[3].Clone();
        }
        private void lblMB22_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 22;
            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[3].allMoneyBoth[3].Clone();
        }

        //5.1-3
        private void mbName23_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 23;
            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[4].allMoneyBoth[0].Clone();
        }
        private void lblMB23_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 23;
            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[4].allMoneyBoth[0].Clone();
        }
        private void mbName24_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 24;
            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[4].allMoneyBoth[1].Clone();
        }
        private void lblMB24_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 24;
            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[4].allMoneyBoth[1].Clone();
        }
        private void mbName25_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 25;
            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[4].allMoneyBoth[2].Clone();
        }
        private void lblMB25_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 25;
            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[4].allMoneyBoth[2].Clone();
        }

        //6.1-2
        private void mbName26_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 26;
            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[5].allMoneyBoth[0].Clone();
        }
        private void lblMB26_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 26;
            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[5].allMoneyBoth[0].Clone();
        }
        private void mbName27_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 27;
            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[5].allMoneyBoth[1].Clone();
        }
        private void lblMB27_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 27;
            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[5].allMoneyBoth[1].Clone();
        }

        //7.1-1
        private void mbName28_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 28;
            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[6].allMoneyBoth[0].Clone();
        }
        private void lblMB28_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 28;
            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[6].allMoneyBoth[0].Clone();
        }

        #endregion

        #region 风向图货币名称选择
        private void lblOne_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                if (Setup.fengxiangCNum == 0)
                {
                    draw.PaintCir(lblOneT, Color.Gray, grayPointLoc, grayR);
                    Setup.chooseMoneyBoth[0] = (Money)DataFiler.FindMoney(lblOne.Text).Clone();
                    Setup.fengxiangCNum++;
                }
                else if (Setup.fengxiangCNum == 1 && Setup.chooseMoneyBoth[0].Name != lblOne.Text)
                {
                    Setup.chooseMoneyBoth[1] = (Money)DataFiler.FindMoney(lblOne.Text).Clone();

                    DataShow.linePosLast = DataShow.linePos;
                    DataShow.linePos = FindMBoth(Setup.chooseMoneyBoth[0].Name, Setup.chooseMoneyBoth[1].Name);
                }
            }
            else if (e.Button == MouseButtons.Right &&
                     e.Clicks == 1 &&
                     lblOne.Text == Setup.chooseMoneyBoth[0].Name)
            {
                draw.PaintCir(lblOneT, Color.Black, grayPointLoc, grayR);
                Setup.fengxiangCNum = 0;
            }
        }


        private void lblTwo_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                if (Setup.fengxiangCNum == 0)
                {
                    draw.PaintCir(lblTwoT, Color.Gray, grayPointLoc, grayR);
                    Setup.chooseMoneyBoth[0] = (Money)DataFiler.FindMoney(lblTwo.Text).Clone();
                    Setup.fengxiangCNum++;
                }
                else if (Setup.fengxiangCNum == 1 && Setup.chooseMoneyBoth[0].Name != lblTwo.Text)
                {
                    Setup.chooseMoneyBoth[1] = (Money)DataFiler.FindMoney(lblTwo.Text).Clone();

                    DataShow.linePosLast = DataShow.linePos;
                    DataShow.linePos = FindMBoth(Setup.chooseMoneyBoth[0].Name, Setup.chooseMoneyBoth[1].Name);
                }
            }
            else if (e.Button == MouseButtons.Right &&
                     e.Clicks == 1 && 
                     lblTwo.Text == Setup.chooseMoneyBoth[0].Name)
            {
                draw.PaintCir(lblTwoT, Color.Black, grayPointLoc, grayR);
                Setup.fengxiangCNum = 0;
            }
        }

        private void lblThree_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                if (Setup.fengxiangCNum == 0)
                {
                    draw.PaintCir(lblThreeT, Color.Gray, grayPointLoc, grayR);
                    Setup.chooseMoneyBoth[0] = (Money)DataFiler.FindMoney(lblThree.Text).Clone();
                    Setup.fengxiangCNum++;
                }
                else if (Setup.fengxiangCNum == 1 && Setup.chooseMoneyBoth[0].Name != lblThree.Text)
                {
                    Setup.chooseMoneyBoth[1] = (Money)DataFiler.FindMoney(lblThree.Text).Clone();

                    DataShow.linePosLast = DataShow.linePos;
                    DataShow.linePos = FindMBoth(Setup.chooseMoneyBoth[0].Name, Setup.chooseMoneyBoth[1].Name);
                }
            }
            else if (e.Button == MouseButtons.Right &&
                     e.Clicks == 1 &&
                     lblThree.Text == Setup.chooseMoneyBoth[0].Name)
            {
                draw.PaintCir(lblThreeT, Color.Black, grayPointLoc, grayR);
                Setup.fengxiangCNum = 0;
            }
        }

        private void lblFour_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                if (Setup.fengxiangCNum == 0)
                {
                    draw.PaintCir(lblFourT, Color.Gray, grayPointLoc, grayR);
                    Setup.chooseMoneyBoth[0] = (Money)DataFiler.FindMoney(lblFour.Text).Clone();
                    Setup.fengxiangCNum++;
                }
                else if (Setup.fengxiangCNum == 1 && Setup.chooseMoneyBoth[0].Name != lblFour.Text)
                {
                    Setup.chooseMoneyBoth[1] = (Money)DataFiler.FindMoney(lblFour.Text).Clone();

                    DataShow.linePosLast = DataShow.linePos;
                    DataShow.linePos = FindMBoth(Setup.chooseMoneyBoth[0].Name, Setup.chooseMoneyBoth[1].Name);
                }
            }
            else if (e.Button == MouseButtons.Right &&
                     e.Clicks == 1 &&
                     lblFour.Text == Setup.chooseMoneyBoth[0].Name)
            {
                draw.PaintCir(lblFourT, Color.Black, grayPointLoc, grayR);
                Setup.fengxiangCNum = 0;
            }
        }

        private void lblFive_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                if (Setup.fengxiangCNum == 0)
                {
                    draw.PaintCir(lblFiveT, Color.Gray, grayPointLoc, grayR);
                    Setup.chooseMoneyBoth[0] = (Money)DataFiler.FindMoney(lblFive.Text).Clone();
                    Setup.fengxiangCNum++;
                }
                else if (Setup.fengxiangCNum == 1 && Setup.chooseMoneyBoth[0].Name != lblFive.Text)
                {
                    Setup.chooseMoneyBoth[1] = (Money)DataFiler.FindMoney(lblFive.Text).Clone();

                    DataShow.linePosLast = DataShow.linePos;
                    DataShow.linePos = FindMBoth(Setup.chooseMoneyBoth[0].Name, Setup.chooseMoneyBoth[1].Name);
                }
            }
            else if (e.Button == MouseButtons.Right &&
                     e.Clicks == 1 &&
                     lblFive.Text == Setup.chooseMoneyBoth[0].Name)
            {
                draw.PaintCir(lblFiveT, Color.Black, grayPointLoc, grayR);
                Setup.fengxiangCNum = 0;
            }
        }

        private void lblSix_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                if (Setup.fengxiangCNum == 0)
                {
                    draw.PaintCir(lblSixT, Color.Gray, grayPointLoc, grayR);
                    Setup.chooseMoneyBoth[0] = (Money)DataFiler.FindMoney(lblSix.Text).Clone();
                    Setup.fengxiangCNum++;
                }
                else if (Setup.fengxiangCNum == 1 && Setup.chooseMoneyBoth[0].Name != lblSix.Text)
                {
                    Setup.chooseMoneyBoth[1] = (Money)DataFiler.FindMoney(lblSix.Text).Clone();

                    DataShow.linePosLast = DataShow.linePos;
                    DataShow.linePos = FindMBoth(Setup.chooseMoneyBoth[0].Name, Setup.chooseMoneyBoth[1].Name);
                }
            }
            else if (e.Button == MouseButtons.Right &&
                     e.Clicks == 1 &&
                     lblSix.Text == Setup.chooseMoneyBoth[0].Name)
            {
                draw.PaintCir(lblSixT, Color.Black, grayPointLoc, grayR);
                Setup.fengxiangCNum = 0;
            }
        }
        #endregion

        #region 线框在双绿色货币对间滚动

        private void dwName1_MouseClick(object sender, MouseEventArgs e)
        {
            if (DataShow.doubleSuperNum > 1)
            {
                for (int i = DataFiler.basicMoneyGroup.Length - 1; i >= 0 ; i--)
                {
                    for (int j = DataFiler.basicMoneyGroup[i].allMoneyBoth.Length - 1; j >= 0; j--)
                    {
                        if (DataFiler.basicMoneyGroup[i].allMoneyBoth[j].DoubleGreen && (GetLine(i, j) < DataShow.linePos))
                        {
                            DataShow.linePosLast = DataShow.linePos;
                            DataShow.linePos = GetLine(i, j);
                            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[i].allMoneyBoth[j].Clone();

                            return;
                        }
                    }
                }
            }
        }
        private void dwName2_MouseClick(object sender, MouseEventArgs e)
        {
            if (DataShow.doubleSuperNum > 1)
            {
                for (int i = DataFiler.basicMoneyGroup.Length - 1; i >= 0; i--)
                {
                    for (int j = DataFiler.basicMoneyGroup[i].allMoneyBoth.Length - 1; j >= 0; j--)
                    {
                        if (DataFiler.basicMoneyGroup[i].allMoneyBoth[j].DoubleGreen && (GetLine(i, j) < DataShow.linePos))
                        {
                            DataShow.linePosLast = DataShow.linePos;
                            DataShow.linePos = GetLine(i, j);
                            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[i].allMoneyBoth[j].Clone();

                            return;
                        }
                    }
                }
            }
        }

        private void dwName3_MouseClick(object sender, MouseEventArgs e)
        {
            if (DataShow.doubleSuperNum > 1)
            {
                for (int i = DataFiler.basicMoneyGroup.Length - 1; i >= 0; i--)
                {
                    for (int j = DataFiler.basicMoneyGroup[i].allMoneyBoth.Length - 1; j >= 0; j--)
                    {
                        if (DataFiler.basicMoneyGroup[i].allMoneyBoth[j].DoubleGreen && (GetLine(i, j) < DataShow.linePos))
                        {
                            DataShow.linePosLast = DataShow.linePos;
                            DataShow.linePos = GetLine(i, j);
                            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[i].allMoneyBoth[j].Clone();

                            return;
                        }
                    }
                }
            }
        }

        private void dwName4_MouseClick(object sender, MouseEventArgs e)
        {
            if (DataShow.doubleSuperNum > 1)
            {
                for (int i = DataFiler.basicMoneyGroup.Length - 1; i >= 0; i--)
                {
                    for (int j = DataFiler.basicMoneyGroup[i].allMoneyBoth.Length - 1; j >= 0; j--)
                    {
                        if (DataFiler.basicMoneyGroup[i].allMoneyBoth[j].DoubleGreen && (GetLine(i, j) < DataShow.linePos))
                        {
                            DataShow.linePosLast = DataShow.linePos;
                            DataShow.linePos = GetLine(i, j);
                            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[i].allMoneyBoth[j].Clone();

                            return;
                        }
                    }
                }
            }
        }

        private void dwName5_MouseClick(object sender, MouseEventArgs e)
        {
            if (DataShow.doubleSuperNum > 1)
            {
                for (int i = DataFiler.basicMoneyGroup.Length - 1; i >= 0; i--)
                {
                    for (int j = DataFiler.basicMoneyGroup[i].allMoneyBoth.Length - 1; j >= 0; j--)
                    {
                        if (DataFiler.basicMoneyGroup[i].allMoneyBoth[j].DoubleGreen && (GetLine(i, j) < DataShow.linePos))
                        {
                            DataShow.linePosLast = DataShow.linePos;
                            DataShow.linePos = GetLine(i, j);
                            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[i].allMoneyBoth[j].Clone();

                            return;
                        }
                    }
                }
            }
        }

        private void dwName6_MouseClick(object sender, MouseEventArgs e)
        {
            if (DataShow.doubleSuperNum > 1)
            {
                for (int i = DataFiler.basicMoneyGroup.Length - 1; i >= 0; i--)
                {
                    for (int j = DataFiler.basicMoneyGroup[i].allMoneyBoth.Length - 1; j >= 0; j--)
                    {
                        if (DataFiler.basicMoneyGroup[i].allMoneyBoth[j].DoubleGreen && (GetLine(i, j) < DataShow.linePos))
                        {
                            DataShow.linePosLast = DataShow.linePos;
                            DataShow.linePos = GetLine(i, j);
                            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[i].allMoneyBoth[j].Clone();

                            return;
                        }
                    }
                }
            }
        }

        private void dwName7_MouseClick(object sender, MouseEventArgs e)
        {
            if (DataShow.doubleSuperNum > 1)
            {
                for (int i = DataFiler.basicMoneyGroup.Length - 1; i >= 0; i--)
                {
                    for (int j = DataFiler.basicMoneyGroup[i].allMoneyBoth.Length - 1; j >= 0; j--)
                    {
                        if (DataFiler.basicMoneyGroup[i].allMoneyBoth[j].DoubleGreen && (GetLine(i, j) < DataShow.linePos))
                        {
                            DataShow.linePosLast = DataShow.linePos;
                            DataShow.linePos = GetLine(i, j);
                            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[i].allMoneyBoth[j].Clone();

                            return;
                        }
                    }
                }
            }
        }

        private void dwName8_MouseClick(object sender, MouseEventArgs e)
        {
            if (DataShow.doubleSuperNum > 1)
            {
                for (int i = DataFiler.basicMoneyGroup.Length - 1; i >= 0; i--)
                {
                    for (int j = DataFiler.basicMoneyGroup[i].allMoneyBoth.Length - 1; j >= 0; j--)
                    {
                        if (DataFiler.basicMoneyGroup[i].allMoneyBoth[j].DoubleGreen && (GetLine(i, j) < DataShow.linePos))
                        {
                            DataShow.linePosLast = DataShow.linePos;
                            DataShow.linePos = GetLine(i, j);
                            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[i].allMoneyBoth[j].Clone();

                            return;
                        }
                    }
                }
            }
        }

        private void dwName9_MouseClick(object sender, MouseEventArgs e)
        {
            if (DataShow.doubleSuperNum > 1)
            {
                for (int i = DataFiler.basicMoneyGroup.Length - 1; i >= 0; i--)
                {
                    for (int j = DataFiler.basicMoneyGroup[i].allMoneyBoth.Length - 1; j >= 0; j--)
                    {
                        if (DataFiler.basicMoneyGroup[i].allMoneyBoth[j].DoubleGreen && (GetLine(i, j) < DataShow.linePos))
                        {
                            DataShow.linePosLast = DataShow.linePos;
                            DataShow.linePos = GetLine(i, j);
                            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[i].allMoneyBoth[j].Clone();

                            return;
                        }
                    }
                }
            }
        }

        private void dwName10_MouseClick(object sender, MouseEventArgs e)
        {
            if (DataShow.doubleSuperNum > 1)
            {
                for (int i = DataFiler.basicMoneyGroup.Length - 1; i >= 0; i--)
                {
                    for (int j = DataFiler.basicMoneyGroup[i].allMoneyBoth.Length - 1; j >= 0; j--)
                    {
                        if (DataFiler.basicMoneyGroup[i].allMoneyBoth[j].DoubleGreen && (GetLine(i, j) < DataShow.linePos))
                        {
                            DataShow.linePosLast = DataShow.linePos;
                            DataShow.linePos = GetLine(i, j);
                            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[i].allMoneyBoth[j].Clone();

                            return;
                        }
                    }
                }
            }
        }

        private void dwName11_MouseClick(object sender, MouseEventArgs e)
        {
            if (DataShow.doubleSuperNum > 1)
            {
                for (int i = DataFiler.basicMoneyGroup.Length - 1; i >= 0; i--)
                {
                    for (int j = DataFiler.basicMoneyGroup[i].allMoneyBoth.Length - 1; j >= 0; j--)
                    {
                        if (DataFiler.basicMoneyGroup[i].allMoneyBoth[j].DoubleGreen && (GetLine(i, j) < DataShow.linePos))
                        {
                            DataShow.linePosLast = DataShow.linePos;
                            DataShow.linePos = GetLine(i, j);
                            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[i].allMoneyBoth[j].Clone();

                            return;
                        }
                    }
                }
            }
        }

        private void dwName12_MouseClick(object sender, MouseEventArgs e)
        {
            if (DataShow.doubleSuperNum > 1)
            {
                for (int i = DataFiler.basicMoneyGroup.Length - 1; i >= 0; i--)
                {
                    for (int j = DataFiler.basicMoneyGroup[i].allMoneyBoth.Length - 1; j >= 0; j--)
                    {
                        if (DataFiler.basicMoneyGroup[i].allMoneyBoth[j].DoubleGreen && (GetLine(i, j) < DataShow.linePos))
                        {
                            DataShow.linePosLast = DataShow.linePos;
                            DataShow.linePos = GetLine(i, j);
                            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[i].allMoneyBoth[j].Clone();

                            return;
                        }
                    }
                }
            }
        }

        private void dwName13_MouseClick(object sender, MouseEventArgs e)
        {
            if (DataShow.doubleSuperNum > 1)
            {
                for (int i = DataFiler.basicMoneyGroup.Length - 1; i >= 0; i--)
                {
                    for (int j = DataFiler.basicMoneyGroup[i].allMoneyBoth.Length - 1; j >= 0; j--)
                    {
                        if (DataFiler.basicMoneyGroup[i].allMoneyBoth[j].DoubleGreen && (GetLine(i, j) < DataShow.linePos))
                        {
                            DataShow.linePosLast = DataShow.linePos;
                            DataShow.linePos = GetLine(i, j);
                            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[i].allMoneyBoth[j].Clone();

                            return;
                        }
                    }
                }
            }
        }
        private void dwName14_MouseClick(object sender, MouseEventArgs e)
        {
            if(DataShow.doubleSuperNum > 1)
            {
                for(int i = 0; i < DataFiler.basicMoneyGroup.Length; i++)
                {
                    for(int j = 0; j < DataFiler.basicMoneyGroup[i].allMoneyBoth.Length; j++)
                    {
                        if(DataFiler.basicMoneyGroup[i].allMoneyBoth[j].DoubleGreen && (GetLine(i, j) > DataShow.linePos))
                        {
                            DataShow.linePosLast = DataShow.linePos;
                            DataShow.linePos = GetLine(i,j);
                            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[i].allMoneyBoth[j].Clone();
                            return ;
                        }
                    }
                }
            }
        }

        private void dwName15_MouseClick(object sender, MouseEventArgs e)
        {
            if (DataShow.doubleSuperNum > 1)
            {
                for (int i = 0; i < DataFiler.basicMoneyGroup.Length; i++)
                {
                    for (int j = 0; j < DataFiler.basicMoneyGroup[i].allMoneyBoth.Length; j++)
                    {
                        if (DataFiler.basicMoneyGroup[i].allMoneyBoth[j].DoubleGreen && (GetLine(i, j) > DataShow.linePos))
                        {
                            DataShow.linePosLast = DataShow.linePos;
                            DataShow.linePos = GetLine(i, j);
                            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[i].allMoneyBoth[j].Clone();

                            return;
                        }
                    }
                }
            }
        }

        private void dwName16_MouseClick(object sender, MouseEventArgs e)
        {
            if (DataShow.doubleSuperNum > 1)
            {
                for (int i = 0; i < DataFiler.basicMoneyGroup.Length; i++)
                {
                    for (int j = 0; j < DataFiler.basicMoneyGroup[i].allMoneyBoth.Length; j++)
                    {
                        if (DataFiler.basicMoneyGroup[i].allMoneyBoth[j].DoubleGreen && (GetLine(i, j) > DataShow.linePos))
                        {
                            DataShow.linePosLast = DataShow.linePos;
                            DataShow.linePos = GetLine(i, j);
                            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[i].allMoneyBoth[j].Clone();

                            return;
                        }
                    }
                }
            }
        }

        private void dwName17_MouseClick(object sender, MouseEventArgs e)
        {
            if (DataShow.doubleSuperNum > 1)
            {
                for (int i = 0; i < DataFiler.basicMoneyGroup.Length; i++)
                {
                    for (int j = 0; j < DataFiler.basicMoneyGroup[i].allMoneyBoth.Length; j++)
                    {
                        if (DataFiler.basicMoneyGroup[i].allMoneyBoth[j].DoubleGreen && (GetLine(i, j) > DataShow.linePos))
                        {
                            DataShow.linePosLast = DataShow.linePos;
                            DataShow.linePos = GetLine(i, j);
                            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[i].allMoneyBoth[j].Clone();

                            return;
                        }
                    }
                }
            }
        }

        private void dwName18_MouseClick(object sender, MouseEventArgs e)
        {
            if (DataShow.doubleSuperNum > 1)
            {
                for (int i = 0; i < DataFiler.basicMoneyGroup.Length; i++)
                {
                    for (int j = 0; j < DataFiler.basicMoneyGroup[i].allMoneyBoth.Length; j++)
                    {
                        if (DataFiler.basicMoneyGroup[i].allMoneyBoth[j].DoubleGreen && (GetLine(i, j) > DataShow.linePos))
                        {
                            DataShow.linePosLast = DataShow.linePos;
                            DataShow.linePos = GetLine(i, j);
                            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[i].allMoneyBoth[j].Clone();

                            return;
                        }
                    }
                }
            }
        }

        private void dwName19_MouseClick(object sender, MouseEventArgs e)
        {
            if (DataShow.doubleSuperNum > 1)
            {
                for (int i = 0; i < DataFiler.basicMoneyGroup.Length; i++)
                {
                    for (int j = 0; j < DataFiler.basicMoneyGroup[i].allMoneyBoth.Length; j++)
                    {
                        if (DataFiler.basicMoneyGroup[i].allMoneyBoth[j].DoubleGreen && (GetLine(i, j) > DataShow.linePos))
                        {
                            DataShow.linePosLast = DataShow.linePos;
                            DataShow.linePos = GetLine(i, j);
                            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[i].allMoneyBoth[j].Clone();

                            return;
                        }
                    }
                }
            }
        }

        private void dwName20_MouseClick(object sender, MouseEventArgs e)
        {
            if (DataShow.doubleSuperNum > 1)
            {
                for (int i = 0; i < DataFiler.basicMoneyGroup.Length; i++)
                {
                    for (int j = 0; j < DataFiler.basicMoneyGroup[i].allMoneyBoth.Length; j++)
                    {
                        if (DataFiler.basicMoneyGroup[i].allMoneyBoth[j].DoubleGreen && (GetLine(i, j) > DataShow.linePos))
                        {
                            DataShow.linePosLast = DataShow.linePos;
                            DataShow.linePos = GetLine(i, j);
                            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[i].allMoneyBoth[j].Clone();

                            return;
                        }
                    }
                }
            }
        }

        private void dwName21_MouseClick(object sender, MouseEventArgs e)
        {
            if (DataShow.doubleSuperNum > 1)
            {
                for (int i = 0; i < DataFiler.basicMoneyGroup.Length; i++)
                {
                    for (int j = 0; j < DataFiler.basicMoneyGroup[i].allMoneyBoth.Length; j++)
                    {
                        if (DataFiler.basicMoneyGroup[i].allMoneyBoth[j].DoubleGreen && (GetLine(i, j) > DataShow.linePos))
                        {
                            DataShow.linePosLast = DataShow.linePos;
                            DataShow.linePos = GetLine(i, j);
                            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[i].allMoneyBoth[j].Clone();

                            return;
                        }
                    }
                }
            }
        }

        private void dwName22_MouseClick(object sender, MouseEventArgs e)
        {
            if (DataShow.doubleSuperNum > 1)
            {
                for (int i = 0; i < DataFiler.basicMoneyGroup.Length; i++)
                {
                    for (int j = 0; j < DataFiler.basicMoneyGroup[i].allMoneyBoth.Length; j++)
                    {
                        if (DataFiler.basicMoneyGroup[i].allMoneyBoth[j].DoubleGreen && (GetLine(i, j) > DataShow.linePos))
                        {
                            DataShow.linePosLast = DataShow.linePos;
                            DataShow.linePos = GetLine(i, j);
                            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[i].allMoneyBoth[j].Clone();

                            return;
                        }
                    }
                }
            }
        }

        private void dwName23_MouseClick(object sender, MouseEventArgs e)
        {
            if (DataShow.doubleSuperNum > 1)
            {
                for (int i = 0; i < DataFiler.basicMoneyGroup.Length; i++)
                {
                    for (int j = 0; j < DataFiler.basicMoneyGroup[i].allMoneyBoth.Length; j++)
                    {
                        if (DataFiler.basicMoneyGroup[i].allMoneyBoth[j].DoubleGreen && (GetLine(i, j) > DataShow.linePos))
                        {
                            DataShow.linePosLast = DataShow.linePos;
                            DataShow.linePos = GetLine(i, j);
                            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[i].allMoneyBoth[j].Clone();

                            return;
                        }
                    }
                }
            }
        }

        private void dwName24_MouseClick(object sender, MouseEventArgs e)
        {
            if (DataShow.doubleSuperNum > 1)
            {
                for (int i = 0; i < DataFiler.basicMoneyGroup.Length; i++)
                {
                    for (int j = 0; j < DataFiler.basicMoneyGroup[i].allMoneyBoth.Length; j++)
                    {
                        if (DataFiler.basicMoneyGroup[i].allMoneyBoth[j].DoubleGreen && (GetLine(i, j) > DataShow.linePos))
                        {
                            DataShow.linePosLast = DataShow.linePos;
                            DataShow.linePos = GetLine(i, j);
                            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[i].allMoneyBoth[j].Clone();

                            return;
                        }
                    }
                }
            }
        }

        private void dwName25_MouseClick(object sender, MouseEventArgs e)
        {
            if (DataShow.doubleSuperNum > 1)
            {
                for (int i = 0; i < DataFiler.basicMoneyGroup.Length; i++)
                {
                    for (int j = 0; j < DataFiler.basicMoneyGroup[i].allMoneyBoth.Length; j++)
                    {
                        if (DataFiler.basicMoneyGroup[i].allMoneyBoth[j].DoubleGreen && (GetLine(i, j) > DataShow.linePos))
                        {
                            DataShow.linePosLast = DataShow.linePos;
                            DataShow.linePos = GetLine(i, j);
                            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[i].allMoneyBoth[j].Clone();

                            return;
                        }
                    }
                }
            }
        }

        private void dwName26_MouseClick(object sender, MouseEventArgs e)
        {
            if (DataShow.doubleSuperNum > 1)
            {
                for (int i = 0; i < DataFiler.basicMoneyGroup.Length; i++)
                {
                    for (int j = 0; j < DataFiler.basicMoneyGroup[i].allMoneyBoth.Length; j++)
                    {
                        if (DataFiler.basicMoneyGroup[i].allMoneyBoth[j].DoubleGreen && (GetLine(i, j) > DataShow.linePos))
                        {
                            DataShow.linePosLast = DataShow.linePos;
                            DataShow.linePos = GetLine(i, j);
                            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[i].allMoneyBoth[j].Clone();

                            return;
                        }
                    }
                }
            }
        }

        private void dwName27_MouseClick(object sender, MouseEventArgs e)
        {
            if (DataShow.doubleSuperNum > 1)
            {
                for (int i = 0; i < DataFiler.basicMoneyGroup.Length; i++)
                {
                    for (int j = 0; j < DataFiler.basicMoneyGroup[i].allMoneyBoth.Length; j++)
                    {
                        if (DataFiler.basicMoneyGroup[i].allMoneyBoth[j].DoubleGreen && (GetLine(i, j) > DataShow.linePos))
                        {
                            DataShow.linePosLast = DataShow.linePos;
                            DataShow.linePos = GetLine(i, j);
                            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[i].allMoneyBoth[j].Clone();

                            return;
                        }
                    }
                }
            }
        }

        private void dwName28_MouseClick(object sender, MouseEventArgs e)
        {
            if (DataShow.doubleSuperNum > 1)
            {
                for (int i = 0; i < DataFiler.basicMoneyGroup.Length; i++)
                {
                    for (int j = 0; j < DataFiler.basicMoneyGroup[i].allMoneyBoth.Length; j++)
                    {
                        if (DataFiler.basicMoneyGroup[i].allMoneyBoth[j].DoubleGreen && (GetLine(i, j) > DataShow.linePos))
                        {
                            DataShow.linePosLast = DataShow.linePos;
                            DataShow.linePos = GetLine(i, j);
                            BasicData.zoushi_MBoth = (MoneyBoth)DataFiler.basicMoneyGroup[i].allMoneyBoth[j].Clone();

                            return;
                        }
                    }
                }
            }
        }
        #endregion

        #region 单击倒计时栏 暂停倒计时
        private void lblWP1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                pauseStartTime = (MyTime)MyTime.nowTime.Clone();
                isPause = true;
            }
        }

        private void lblWP2_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                pauseStartTime = (MyTime)MyTime.nowTime.Clone();
                isPause = true;
            }
        }

        private void lblWP3_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                pauseStartTime = (MyTime)MyTime.nowTime.Clone();
                isPause = true;
            }
        }

        private void lblWP4_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                pauseStartTime = (MyTime)MyTime.nowTime.Clone();
                isPause = true;
            }
        }

        private void lblWP5_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                pauseStartTime = (MyTime)MyTime.nowTime.Clone();
                isPause = true;
            }
        }

        private void lblWP6_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                pauseStartTime = (MyTime)MyTime.nowTime.Clone();
                isPause = true;
            }
        }

        private void lblWP7_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                pauseStartTime = (MyTime)MyTime.nowTime.Clone();
                isPause = true;
            }
        }

        private void lblWP8_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                pauseStartTime = (MyTime)MyTime.nowTime.Clone();
                isPause = true;
            }
        }

        private void lblWP9_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                pauseStartTime = (MyTime)MyTime.nowTime.Clone();
                isPause = true;
            }
        }

        private void lblWP10_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                pauseStartTime = (MyTime)MyTime.nowTime.Clone();
                isPause = true;
            }
        }

        private void lblWP11_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                pauseStartTime = (MyTime)MyTime.nowTime.Clone();
                isPause = true;
            }
        }

        private void lblWP12_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                pauseStartTime = (MyTime)MyTime.nowTime.Clone();
                isPause = true;
            }
        }

        private void lblWP13_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                pauseStartTime = (MyTime)MyTime.nowTime.Clone();
                isPause = true;
            }
        }

        private void lblWP14_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                pauseStartTime = (MyTime)MyTime.nowTime.Clone();
                isPause = true;
            }
        }

        private void lblWP15_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                pauseStartTime = (MyTime)MyTime.nowTime.Clone();
                isPause = true;
            }
        }

        private void lblWP16_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                pauseStartTime = (MyTime)MyTime.nowTime.Clone();
                isPause = true;
            }
        }

        private void lblWP17_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                pauseStartTime = (MyTime)MyTime.nowTime.Clone();
                isPause = true;
            }
        }

        private void lblWP18_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                pauseStartTime = (MyTime)MyTime.nowTime.Clone();
                isPause = true;
            }
        }

        private void lblWP19_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                pauseStartTime = (MyTime)MyTime.nowTime.Clone();
                isPause = true;
            }
        }

        private void lblWP20_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                pauseStartTime = (MyTime)MyTime.nowTime.Clone();
                isPause = true;
            }
        }

        private void lblWP21_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                pauseStartTime = (MyTime)MyTime.nowTime.Clone();
                isPause = true;
            }
        }

        private void lblWP22_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                pauseStartTime = (MyTime)MyTime.nowTime.Clone();
                isPause = true;
            }
        }

        private void lblWP23_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                pauseStartTime = (MyTime)MyTime.nowTime.Clone();
                isPause = true;
            }
        }

        private void lblWP24_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                pauseStartTime = (MyTime)MyTime.nowTime.Clone();
                isPause = true;
            }
        }

        private void lblWP25_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                pauseStartTime = (MyTime)MyTime.nowTime.Clone();
                isPause = true;
            }
        }

        private void lblWP26_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                pauseStartTime = (MyTime)MyTime.nowTime.Clone();
                isPause = true;
            }
        }

        private void lblWP27_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                pauseStartTime = (MyTime)MyTime.nowTime.Clone();
                isPause = true;
            }
        }

        private void lblWP28_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                pauseStartTime = (MyTime)MyTime.nowTime.Clone();
                isPause = true;
            }
        }
        #endregion

        /// <summary>
        /// 根据两个货币的名称找到货币对所在的行数，并将货币赋值到相应的走势图变量上
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        private int FindMBoth(string p1, string p2)
        {
            for(int group = 0;group < DataFiler.basicMoneyGroup.Length; group++)
            {
                for( int i = 0;i < DataFiler.basicMoneyGroup[group].allMoneyBoth.Length; i++)
                {
                    if( (DataFiler.basicMoneyGroup[group].allMoneyBoth[i].moneyA.Name == p1 &&
                        DataFiler.basicMoneyGroup[group].allMoneyBoth[i].MoneyB.Name == p2) )
                    {
                        BasicData.zoushi_MBoth.moneyA = (Money)Setup.chooseMoneyBoth[0].Clone();
                        BasicData.zoushi_MBoth.MoneyB = (Money)Setup.chooseMoneyBoth[1].Clone();
                        return GetLine(group, i);
                    }
                    else if((DataFiler.basicMoneyGroup[group].allMoneyBoth[i].moneyA.Name == p2 && 
                        DataFiler.basicMoneyGroup[group].allMoneyBoth[i].MoneyB.Name == p1) )
                    {
                        
                        BasicData.zoushi_MBoth.moneyA = (Money)Setup.chooseMoneyBoth[1].Clone();
                        BasicData.zoushi_MBoth.MoneyB = (Money)Setup.chooseMoneyBoth[0].Clone();
                        return GetLine(group, i);
                    }
                }
            }
            return 1;
        }

        private int GetLine(int group, int i)
        {
            int line = 0;
            for(int index = 0;index < group;index++)
            {
                line = line + 7 - index;
            }
            return line + i + 1;
        }


    }
}
