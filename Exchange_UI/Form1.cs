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
        public delegate void showConVisible(bool result, System.Windows.Forms.Control con);
        public showText ShowText;
        public showButtonText ShowButtonText;
        public showFormText ShowFormText;
        public showLabelFont ShowLabelFont;
        public showConVisible ShowConVisible;

        public DrawGraph draw;

        private int[] colorRNum = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        public MainUI()
        {
            InitializeComponent();
            //OpenMyDoor(DateTime.Now);
            CountDownInit();
        }

        /// <summary>
        /// MyDoor is my door
        /// No key not come in!!!
        /// </summary>
        /// <param name="dateTime">当前系统</param>
        private void OpenMyDoor(DateTime dateTime)
        {
            if(Setup.stopTime - dateTime < TimeSpan.Zero)
            {
                MessageBox.Show("请联系开发者！");
            }
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
            

            Setup.linePosOldName = AllSettings.Default.linePosOldName;

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

            #region 势值非常态设置
            Setup.sNormalRG = AllSettings.Default.sNormalRG;
            Setup.sNormalGG = AllSettings.Default.sNormalGG;
            Setup.sIsGreenAndRed = AllSettings.Default.sIsGreenAndRed;
            Setup.sWithoutNum1 = AllSettings.Default.sWithoutNum1;
            Setup.sWithoutNum2 = AllSettings.Default.sWithoutNum2;
            Setup.sOrderSumLess = AllSettings.Default.sOrderSumLess;
            Setup.sShortAllMore = AllSettings.Default.sShortAllMore;
            Setup.sLongSumMore = AllSettings.Default.sLongSumMore;
            #endregion

            Setup.pathTemp = AllSettings.Default.pathTemp;

            #region 走势图 信号灯设置

            Setup.signLightRY = AllSettings.Default.signLightRY;
            Setup.signLightYG = AllSettings.Default.signLightYG;

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
            FM.Text = s;
        }
        public void setLabelFont(Font f, System.Windows.Forms.Label LB)
        {
            LB.Font = f;
        }
        public void setConVisible(bool result, System.Windows.Forms.Control con)
        {
            con.Visible = result;
        }
        private void MainUI_Load(object sender, EventArgs e)
        {

            ShowText = new showText(setText);
            ShowButtonText = new showButtonText(setButtonText);
            ShowFormText = new showFormText(setFormText);
            ShowLabelFont = new showLabelFont(setLabelFont);
            ShowConVisible = new showConVisible(setConVisible);

            MyTime.nowTime = new MyTime(DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute);     //系统时间初始化
            BasicData.mainUI = this;
            draw = new DrawGraph();
            SetupInit();

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
            AllSettings.Default.linePosOldName = Setup.linePosOldName;

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

            #region 势值非常态设置
            AllSettings.Default.sNormalRG = Setup.sNormalRG;
            AllSettings.Default.sNormalGG = Setup.sNormalGG;
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

            #region 信号灯设置

            AllSettings.Default.signLightRY = Setup.signLightRY;
            AllSettings.Default.signLightYG = Setup.signLightYG;


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

            MyTime.nowTime = new MyTime(DateTime.Now.Day,DateTime.Now.Hour, DateTime.Now.Minute);

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
            //drawS.ToLong();
            //drawS.ToOutMark();
            drawS.IsLocHaveData();
            drawS.UpFXTMoneyNameColor();
            drawS.Update7_DJZSTLine();
            drawS.CheckDJZSTLine();

            if(watchNum == 0)
            {
                watchNum = DataShow.linePos;
            }

            if(DataShow.linePosLast == watchNum)
            {
                watchNum = DataShow.linePos;

                drawS.ToLong();
                drawS.ToOutMark();

                drawS.ToZoushitu();

                drawS.ToDoubleZoushitu();
                //drawS.ToAllDoubleZST();
                drawS.ToFengxiangtu();
            }

            #region 检查是否发现错误
            if (Setup.isFoundError)
            {
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
            //new FMSetup().Show();
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
            dataShow.ToSignLight();
            dataShow.ToSignZoushitu();
            dataShow.ToDoubleZoushitu();
            dataShow.ToAllDoubleZST();
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
            dataShow.ToSignZoushitu();
            dataShow.ToDoubleZoushitu();
            dataShow.ToAllDoubleZST();
            dataShow.ToSignLight();
            dataShow.UpCirMark();
        }

        private void MainUI_TextChanged(object sender, EventArgs e)
        {
            
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

        #region 点击货币对触发事件

        //1.1-7
        private void mbName1_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 1;
            BasicData.zoushi_MBoth = DataFiler.basicMoneyGroup[0].allMoneyBoth[0];
            Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
        }
        private void lblMB1_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 1;
            BasicData.zoushi_MBoth = DataFiler.basicMoneyGroup[0].allMoneyBoth[0];
            Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
        }
        private void mbName2_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 2;
            BasicData.zoushi_MBoth = DataFiler.basicMoneyGroup[0].allMoneyBoth[1];
            Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
        }
        private void lblMB2_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 2;
            BasicData.zoushi_MBoth = DataFiler.basicMoneyGroup[0].allMoneyBoth[1];
            Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
        }
        private void mbName3_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 3;
            BasicData.zoushi_MBoth = DataFiler.basicMoneyGroup[0].allMoneyBoth[2];
            Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
        }
        private void lblMB3_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 3;
            BasicData.zoushi_MBoth = DataFiler.basicMoneyGroup[0].allMoneyBoth[2];
            Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
        }
        private void mbName4_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 4;
            BasicData.zoushi_MBoth = DataFiler.basicMoneyGroup[0].allMoneyBoth[3];
            Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
        }
        private void lblMB4_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 4;
            BasicData.zoushi_MBoth = DataFiler.basicMoneyGroup[0].allMoneyBoth[3];
            Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
        }
        private void mbName5_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 5;
            BasicData.zoushi_MBoth = DataFiler.basicMoneyGroup[0].allMoneyBoth[4];
            Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
        }
        private void lblMB5_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 5;
            BasicData.zoushi_MBoth = DataFiler.basicMoneyGroup[0].allMoneyBoth[4];
            Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
        }
        private void mbName6_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 6;
            BasicData.zoushi_MBoth = DataFiler.basicMoneyGroup[0].allMoneyBoth[5];
            Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
        }
        private void lblMB6_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 6;
            BasicData.zoushi_MBoth = DataFiler.basicMoneyGroup[0].allMoneyBoth[5];
            Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
        }
        private void mbName7_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 7;
            BasicData.zoushi_MBoth = DataFiler.basicMoneyGroup[0].allMoneyBoth[6];
            Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
        }
        private void lblMB7_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 7;
            BasicData.zoushi_MBoth = DataFiler.basicMoneyGroup[0].allMoneyBoth[6];
            Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
        }

        //2.1-6
        private void mbName8_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 8;
            BasicData.zoushi_MBoth = DataFiler.basicMoneyGroup[1].allMoneyBoth[0];
            Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
        }
        private void lblMB8_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 8;
            BasicData.zoushi_MBoth = DataFiler.basicMoneyGroup[1].allMoneyBoth[0];
            Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
        }
        private void mbName9_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 9;
            BasicData.zoushi_MBoth = DataFiler.basicMoneyGroup[1].allMoneyBoth[1];
            Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
        }
        private void lblMB9_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 9;
            BasicData.zoushi_MBoth = DataFiler.basicMoneyGroup[1].allMoneyBoth[1];
            Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
        }
        private void mbName10_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 10;
            BasicData.zoushi_MBoth = DataFiler.basicMoneyGroup[1].allMoneyBoth[2];
            Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
        }
        private void lblMB10_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 10;
            BasicData.zoushi_MBoth = DataFiler.basicMoneyGroup[1].allMoneyBoth[2];
            Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
        }
        private void mbName11_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 11;
            BasicData.zoushi_MBoth = DataFiler.basicMoneyGroup[1].allMoneyBoth[3];
            Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
        }
        private void lblMB11_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 11;
            BasicData.zoushi_MBoth = DataFiler.basicMoneyGroup[1].allMoneyBoth[3];
            Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
        }
        private void mbName12_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 12;
            BasicData.zoushi_MBoth = DataFiler.basicMoneyGroup[1].allMoneyBoth[4];
            Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
        }
        private void lblMB12_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 12;
            BasicData.zoushi_MBoth = DataFiler.basicMoneyGroup[1].allMoneyBoth[4];
            Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
        }
        private void mbName13_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 13;
            BasicData.zoushi_MBoth = DataFiler.basicMoneyGroup[1].allMoneyBoth[5];
            Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
        }
        private void lblMB13_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 13;
            BasicData.zoushi_MBoth = DataFiler.basicMoneyGroup[1].allMoneyBoth[5];
            Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
        }

        //3.1-5
        private void mbName14_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 14;
            BasicData.zoushi_MBoth = DataFiler.basicMoneyGroup[2].allMoneyBoth[0];
            Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
        }
        private void lblMB14_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 14;
            BasicData.zoushi_MBoth = DataFiler.basicMoneyGroup[2].allMoneyBoth[0];
            Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
        }
        private void mbName15_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 15;
            BasicData.zoushi_MBoth = DataFiler.basicMoneyGroup[2].allMoneyBoth[1];
            Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
        }
        private void lblMB15_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 15;
            BasicData.zoushi_MBoth = DataFiler.basicMoneyGroup[2].allMoneyBoth[1];
            Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
        }
        private void mbName16_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 16;
            BasicData.zoushi_MBoth = DataFiler.basicMoneyGroup[2].allMoneyBoth[2];
            Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
        }
        private void lblMB16_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 16;
            BasicData.zoushi_MBoth = DataFiler.basicMoneyGroup[2].allMoneyBoth[2];
            Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
        }
        private void mbName17_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 17;
            BasicData.zoushi_MBoth = DataFiler.basicMoneyGroup[2].allMoneyBoth[3];
            Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
        }
        private void lblMB17_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 17;
            BasicData.zoushi_MBoth = DataFiler.basicMoneyGroup[2].allMoneyBoth[3];
            Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
        }
        private void mbName18_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 18;
            BasicData.zoushi_MBoth = DataFiler.basicMoneyGroup[2].allMoneyBoth[4];
            Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
        }
        private void lblMB18_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 18;
            BasicData.zoushi_MBoth = DataFiler.basicMoneyGroup[2].allMoneyBoth[4];
            Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
        }

        //4.1-4
        private void mbName19_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 19;
            BasicData.zoushi_MBoth = DataFiler.basicMoneyGroup[3].allMoneyBoth[0];
            Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
        }
        private void lblMB19_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 19;
            BasicData.zoushi_MBoth = DataFiler.basicMoneyGroup[3].allMoneyBoth[0];
            Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
        }
        private void mbName20_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 20;
            BasicData.zoushi_MBoth = DataFiler.basicMoneyGroup[3].allMoneyBoth[1];
            Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
        }
        private void lblMB20_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 20;
            BasicData.zoushi_MBoth = DataFiler.basicMoneyGroup[3].allMoneyBoth[1];
            Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
        }
        private void mbName21_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 21;
            BasicData.zoushi_MBoth = DataFiler.basicMoneyGroup[3].allMoneyBoth[2];
            Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
        }
        private void lblMB21_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 21;
            BasicData.zoushi_MBoth = DataFiler.basicMoneyGroup[3].allMoneyBoth[2];
            Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
        }
        private void mbName22_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 22;
            BasicData.zoushi_MBoth = DataFiler.basicMoneyGroup[3].allMoneyBoth[3];
            Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
        }
        private void lblMB22_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 22;
            BasicData.zoushi_MBoth = DataFiler.basicMoneyGroup[3].allMoneyBoth[3];
            Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
        }

        //5.1-3
        private void mbName23_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 23;
            BasicData.zoushi_MBoth = DataFiler.basicMoneyGroup[4].allMoneyBoth[0];
            Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
        }
        private void lblMB23_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 23;
            BasicData.zoushi_MBoth = DataFiler.basicMoneyGroup[4].allMoneyBoth[0];
            Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
        }
        private void mbName24_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 24;
            BasicData.zoushi_MBoth = DataFiler.basicMoneyGroup[4].allMoneyBoth[1];
            Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
        }
        private void lblMB24_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 24;
            BasicData.zoushi_MBoth = DataFiler.basicMoneyGroup[4].allMoneyBoth[1];
            Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
        }
        private void mbName25_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 25;
            BasicData.zoushi_MBoth = DataFiler.basicMoneyGroup[4].allMoneyBoth[2];
            Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
        }
        private void lblMB25_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 25;
            BasicData.zoushi_MBoth = DataFiler.basicMoneyGroup[4].allMoneyBoth[2];
            Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
        }

        //6.1-2
        private void mbName26_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 26;
            BasicData.zoushi_MBoth = DataFiler.basicMoneyGroup[5].allMoneyBoth[0];
            Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
        }
        private void lblMB26_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 26;
            BasicData.zoushi_MBoth = DataFiler.basicMoneyGroup[5].allMoneyBoth[0];
            Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
        }
        private void mbName27_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 27;
            BasicData.zoushi_MBoth = DataFiler.basicMoneyGroup[5].allMoneyBoth[1];
            Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
        }
        private void lblMB27_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 27;
            BasicData.zoushi_MBoth = DataFiler.basicMoneyGroup[5].allMoneyBoth[1];
            Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
        }

        //7.1-1
        private void mbName28_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 28;
            BasicData.zoushi_MBoth = DataFiler.basicMoneyGroup[6].allMoneyBoth[0];
            Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
        }
        private void lblMB28_Click(object sender, EventArgs e)
        {
            DataShow.linePosLast = DataShow.linePos;
            DataShow.linePos = 28;
            BasicData.zoushi_MBoth = DataFiler.basicMoneyGroup[6].allMoneyBoth[0];
            Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
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

        #region 单击中轴部分 倒计时区域 设置中心
        private void lblZT1_Click(object sender, EventArgs e)
        {
            new FMSetup().Show();
        }

        private void lblZT2_Click(object sender, EventArgs e)
        {
            new FMSetup().Show();
        }

        private void lblZT3_Click(object sender, EventArgs e)
        {
            new FMSetup().Show();
        }

        private void lblZT4_Click(object sender, EventArgs e)
        {
            new FMSetup().Show();
        }

        private void lblZT5_Click(object sender, EventArgs e)
        {
            new FMSetup().Show();
        }

        private void lblZT6_Click(object sender, EventArgs e)
        {
            new FMSetup().Show();
        }

        private void lblZT7_Click(object sender, EventArgs e)
        {
            new FMSetup().Show();
        }

        private void lblZT8_Click(object sender, EventArgs e)
        {
            new FMSetup().Show();
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

        private void pnlDoubleZoushi_Click(object sender, EventArgs e)
        {
            if(pnlAllDJZoushi.Visible == false)
            {
                pnlAllDJZoushi.Visible = true;
                new DataShow().ToAllDoubleZST();
            }
            else
            {
                pnlAllDJZoushi.Visible = false;
                new DataShow().ToFengxiangtu();
            }
        }

        private int GetLineFromName(string p)
        {
            for(int i  = 0; i < DataFiler.basicMoneyGroup.Length; i++)
            {
                for(int j = 0; j < DataFiler.basicMoneyGroup[i].allMoneyBoth.Length; j++)
                {

                    if(DataFiler.basicMoneyGroup[i].allMoneyBoth[j].Name == p)
                    {
                        return GetLine(i, j);
                    }
                }
            }

            return 1;
        }
        private void pnlAllZST1_Click(object sender, EventArgs e)
        {
            if (DataFiler.doubleZST.Count >= 1)
            {
                DataShow.linePosLast = DataShow.linePos;
                DataShow.linePos = GetLineFromName(DataFiler.doubleZST[0].Name);
                BasicData.zoushi_MBoth = DataFiler.doubleZST[0];
                Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
            }
        }


        private void pnlAllZST2_Click(object sender, EventArgs e)
        {
            if (DataFiler.doubleZST.Count >= 2)
            {
                DataShow.linePosLast = DataShow.linePos;
                DataShow.linePos = GetLineFromName(DataFiler.doubleZST[1].Name);
                BasicData.zoushi_MBoth = DataFiler.doubleZST[1];
                Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
            }
        }

        private void pnlAllZST3_Click(object sender, EventArgs e)
        {
            if (DataFiler.doubleZST.Count >= 3)
            {
                DataShow.linePosLast = DataShow.linePos;
                DataShow.linePos = GetLineFromName(DataFiler.doubleZST[2].Name);
                BasicData.zoushi_MBoth = DataFiler.doubleZST[2];
                Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
            }
        }

        private void pnlAllZST4_Click(object sender, EventArgs e)
        {
            if (DataFiler.doubleZST.Count >= 4)
            {
                DataShow.linePosLast = DataShow.linePos;
                DataShow.linePos = GetLineFromName(DataFiler.doubleZST[3].Name);
                BasicData.zoushi_MBoth = DataFiler.doubleZST[3];
                Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
            }
        }

        private void pnlAllZST5_Click(object sender, EventArgs e)
        {
            if (DataFiler.doubleZST.Count >= 5)
            {
                DataShow.linePosLast = DataShow.linePos;
                DataShow.linePos = GetLineFromName(DataFiler.doubleZST[4].Name);
                BasicData.zoushi_MBoth = DataFiler.doubleZST[4];
                Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
            }
        }

        private void pnlAllZST6_Click(object sender, EventArgs e)
        {
            if (DataFiler.doubleZST.Count >= 6)
            {
                DataShow.linePosLast = DataShow.linePos;
                DataShow.linePos = GetLineFromName(DataFiler.doubleZST[5].Name);
                BasicData.zoushi_MBoth = DataFiler.doubleZST[5];
                Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
            }
        }

        private void pnlAllZST7_Click(object sender, EventArgs e)
        {
            if (DataFiler.doubleZST.Count >= 7)
            {
                DataShow.linePosLast = DataShow.linePos;
                DataShow.linePos = GetLineFromName(DataFiler.doubleZST[6].Name);
                BasicData.zoushi_MBoth = DataFiler.doubleZST[6];
                Setup.linePosOldName = BasicData.zoushi_MBoth.Name;
            }
        }



    }
}
