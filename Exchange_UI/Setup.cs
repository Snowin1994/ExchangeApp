using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Exchange_UI
{
    public class Setup
    {
        /// <summary>
        /// 外汇时间
        /// </summary>
        public static string timeWaihui;
        /// <summary>
        /// 倒计时开始闪烁时间
        /// </summary>
        public static int countDownLess = 90;
        /// <summary>
        /// 再次开始闪烁的时间
        /// </summary>
        public static int countDownAgain = 15;

        /// <summary>
        /// 时差
        /// </summary>
        public static int timeHourDiff = 8;

        #region 中轴 - 货币颜色
        public static int sMHeightLimit = 10;   //高度分界线
        public static int sMGrayFix1 = -1;      //灰色特值1
        public static int sMGrayFix2 = 1;      //灰色特值2
        public static int sMGreen = 1;  //颜色值大于此值时为绿色
        public static int sMRed = -1;   //颜色值小于此值时为红色
        #endregion

        #region 风向图相关设置
        /// <summary>
        /// 短观值 上限
        /// </summary>
        public static int shortNumMax = 30;

        /// <summary>
        /// 长观值上限
        /// </summary>
        public static int longNumMax = 30;

        /// <summary>
        /// 风向图左侧的第一个数字颜色的变化分界值
        /// </summary>
        public static int sFxtLeftNum = 60;
        #endregion

        #region 点差栏提示符设置
        public static int gDayNumMore = 30;
        public static int gJinTuiMore = 60;
        public static bool gIsColorDiff = true;
        public static int gWithoutOrder1 = 7;
        public static int gWithoutOrder2 = 8;
        public static int gOrderSumLess = 9;
        public static int gShortBothMore = 0;
        public static int gLongSumMore = -5;
        public static int gFengzhongSumMore = 100;

        public static int gPChangeOrderNum = 3;
        public static bool gIsUseSound = false;

        /// <summary>
        /// 点差 绿色标记 提示音播放次数
        /// </summary>
        public static int gPlayNum = 1;
        #endregion

        #region 短观值非常态设置
        /// <summary>
        /// 货币对颜色相反，即一红一绿 选中与否
        /// </summary>
        public static bool sIsGreenAndRed = true;
        /// <summary>
        /// 不包含的排序号1
        /// </summary>
        public static int sWithoutNum1 = 7;
        /// <summary>
        /// 不包含的排序号2
        /// </summary>
        public static int sWithoutNum2 = 8;
        /// <summary>
        /// 排序号之和小于
        /// </summary>
        public static int sOrderSumLess = 9;
        /// <summary>
        /// 短观值同时大于
        /// </summary>
        public static int sShortAllMore = 0;
        /// <summary>
        /// 长观值之和大于
        /// </summary>
        public static int sLongSumMore = -5;
        
        #endregion

        #region 长观栏显示条件是否显示

        public static bool isUseCondition1 = true;
        public static bool isUseCondition2 = true;
        public static bool isUseCondition3 = true;

        public static int isLocHaveNum = 0;

        #endregion


        /// <summary>
        /// 路径
        /// </summary>
        public static string pathTemp = @"C:\Program Files (x86)\MT4 Exchange\MQL4\Files";

        //public static string pathTemp = "F:/ProgramAndLearn/MyProject/【外包】外汇程序/2016版外汇程序/ExchangeApp2016/Exchange_UI/bin/Debug";

        #region 走势图 信号灯设置
        /// <summary>
        /// 绿灯 提示音播放次数
        /// </summary>
        public static int zoushiGreen = 1;
        /// <summary>
        /// 走势图 红灯提示音提示时间间隔
        /// </summary>
        public static int zoushiRed = 2;

        public static int[] sZSRed = { 0, 1, 2, -1, -1, -1, -1 };
        public static int[] sZSYellow = { -1, -1, -1, 3, -1, -1, -1 };
        public static int[] sZSGreen = { -1, -1, -1, -1, 4, 5, 6 };

        public static int sZSRedLess = 0;
        public static int sZSYellowMore = 0;
        public static int sZSGreenMore = 0;

        /// <summary>
        /// 用户是否勾选 红灯 声音提示
        /// </summary>
        public static bool sZIsUpRed = true;
        /// <summary>
        /// 用户是否勾选 绿灯 声音提示
        /// </summary>
        public static bool sZIsUpGreen = true;
        /// <summary>
        /// 是否达到启动绿灯提示音的条件
        /// </summary>
        public static bool isStartGreenMusic = false;       // 绿灯 黄灯
        /// <summary>
        /// 是否达到启动红灯提示音的条件
        /// </summary>
        public static bool isStartRedMusic = false;
        //public static bool isStartYellowMusic = false;

        /// <summary>
        /// 代表当前刷新时的颜色 黄1 红2 绿3  初始化0
        /// </summary>
        public static int nowLightColor = 0;

        public static int lastLightColor = 0;

        #endregion

        public static int fengxiangCNum = 0;
        public static Money[] chooseMoneyBoth = new Money[2];
        public static List<MyPriceChange> priceChangeOrder = new List<MyPriceChange>();

        #region 离场符号设置
        /// <summary>
        /// 离场符号 长观值之和
        /// </summary>
        public static int sOutLongSum = -10;
        /// <summary>
        /// 离场符号 排序号之和
        /// </summary>
        public static int sOutOrderSum = 8;
        /// <summary>
        /// 离场符号提示音间隔时间（分钟）
        /// </summary>
        public static int sAfterMinNum = 1;

        #endregion

        #region 播放音乐

        public static bool isPlayGreenMusic = false;        //是否可以播放Green音乐
        public static bool GreenMusicState = false;         //当前是否在播放Green音乐
        public static int GreenMusicNum = 0;                //已经播放Green 次数

        /// <summary>
        /// 
        /// </summary>
        public static int isPlayOutMusic = 0;


        public static MyTime outStartTime;
        public static MyTime redStartTime;

        #endregion

        #region 报错相关
        /// <summary>
        /// 提示信息停留时间（秒）
        /// </summary>
        public static int stopSec = 3;
        /// <summary>
        /// 标识是否接口文件相关出现错误
        /// </summary>
        public static bool isFoundError = false;
        #endregion

        #region GGG文件生成设置
        public static bool sBuyOrSell = true;
        public static bool sOut = true;
        #endregion

    }
}