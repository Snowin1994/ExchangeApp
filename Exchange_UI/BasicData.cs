using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Threading;
using System.Media;

namespace Exchange_UI
{
    public static class BasicData
    {
        /// <summary>
        /// 运行次数
        /// </summary>
        public static int indexNum = 0;
        /// <summary>
        /// 离场符号提示音播放次数
        /// </summary>
        public static int isPlayOutMusicNum = 0;
        public static int isPlayRedMusicNum = 0;
        public static string buffer;
        public static MainUI mainUI;
        public static DataFiler dataFiler;
        public static DataShow dataShow;

        private static SoundPlayer sp = new SoundPlayer();
        private static PlayMusic player = new PlayMusic();

        /// <summary>
        /// 储存当前显示在走势图中的货币对
        /// </summary>
        public static MoneyBoth zoushi_MBoth = new MoneyBoth();

        public static void Run()
        {
            try
            {
                if (BasicData.indexNum == 0)
                    dataFiler = new DataFiler();
                //Thread.Sleep(300);
                dataShow = new DataShow();
                if (dataFiler.IsGetData())
                {
                    dataFiler.GetData();
                    //dataFiler.SetColor();

                    dataShow.ToTable();
                    dataShow.UpdateLineLoc();

                    dataShow.ToZhongzhou();

                    dataShow.ToLong();
                    dataShow.ToOutMark();
                    //dataShow.UpLine();
                    dataShow.UpStar();
                    dataShow.UpCirMark();
                    dataShow.ToRixingtu();
                    dataShow.ToFengxiangtu();
                    dataShow.ToZoushitu();
                    dataShow.ToSignLight();
                    dataShow.ToSignZoushitu();
                    dataShow.UpdateSignnumState();
                }

                if (BasicData.indexNum == 0)
                {
                    foreach (TimeCountDown tcd in FMWarnTime.timeCountDown)
                    {
                        DataFiler.FindMoney(tcd.Name).wakeupList.Add(tcd.CountDown);
                    }
                    foreach (Money money in DataFiler.basicMoney)
                    {
                        if (money.wakeupList.Count != 0)
                        {
                            money.wakeupList.Sort();
                        }
                    }
                }


                //if (BasicData.indexNum == 2)
                //{
                //    dataShow.ToZoushitu();
                //    dataShow.ToFengxiangtu();
                //}

                dataShow.ToShortSuperNum();
                dataShow.ToTime();
                dataShow.ToZSTDownTime();
                dataShow.Countdown();
                //dataShow.ToOutMark();

                dataFiler.PPP();
                dataShow.ToLocation();
                if (dataFiler.IsGetData() || dataShow.IsUpdateDZST())
                {
                    dataShow.ToDoubleZoushitu();
                }
                dataFiler.ToGGG();

                indexNum++;
                Thread.Sleep(1000);     //3000 修改为 1000
                Run();
            }
            catch(Exception ex)
            {
                DataFiler.ErrorLog(ex.ToString());
                Thread.Sleep(2000);
                Run();
            }
        }

        public static void PlayMusic()
        {
            try
            {
                Thread.Sleep(3000);

                //离场符号提示音           ！
                if (Setup.isPlayOutMusic != 0)
                {
                    if (isPlayOutMusicNum == 0)
                    {
                        Setup.outStartTime = new MyTime(0, MyTime.nowTime.Hour, MyTime.nowTime.Min);
                    }
                    if (MyTime.GetTimeDiff(Setup.outStartTime) > Setup.sAfterMinNum || (isPlayOutMusicNum == 0))
                    {
                        Setup.outStartTime = new MyTime(0, MyTime.nowTime.Hour, MyTime.nowTime.Min);
                        isPlayOutMusicNum++;
                        player.PlayEachSound(@".\jingbao.wav",2000);
                    }
                    Setup.isPlayOutMusic = 0;
                }
                else
                {
                    isPlayOutMusicNum = 0;
                }

                #region 已经去掉的提示音
                ////接口文件出错
                //if(Setup.isFoundError)
                //{
                //    player.PlayEachSound(@".\jingbao.wav", 2000);
                //    Setup.isFoundError = false;
                //}


                ////走势图 红灯
                //if (Setup.isStartRedMusic)
                //{
                //    if (isPlayRedMusicNum == 0)
                //    {
                //        Setup.redStartTime = new MyTime(0, MyTime.nowTime.Hour, MyTime.nowTime.Min);
                //    }
                //    if (MyTime.GetTimeDiff(Setup.redStartTime) > Setup.zoushiRed || (isPlayRedMusicNum == 0))
                //    {
                //        Setup.redStartTime = new MyTime(0, MyTime.nowTime.Hour, MyTime.nowTime.Min);
                //        isPlayRedMusicNum++;
                //        player.PlayEachSound(@".\jingbao.wav", 2000);
                //    }
                //    Setup.isPlayOutMusic = 0;
                //    //Setup.isStartRedMusic = false;
                //}
                //else
                //{
                //    isPlayRedMusicNum = 0;


                //    //走势图 绿灯
                //    if (Setup.isStartGreenMusic)
                //    {
                //        player.PlayLoopingSound( @".\jiji.wav",6000,Setup.zoushiGreen);
                        
                //        Setup.isStartGreenMusic = false;
                //    }
                //}

                ////黄灯
                //if(Setup.isStartYellowMusic)
                //{
                //    player.PlayLoopingSound(@".\jingbao.wav", 2000, 1);
                //    Setup.isStartYellowMusic = false;
                //}

                /*
                //绿色提示符
                if (Setup.gIsUseSound)
                {
                    foreach (MoneyGroup mgp in DataFiler.basicMoneyGroup)
                    {
                        foreach (MoneyBoth mbth in mgp.allMoneyBoth)
                        {
                            if (mbth.CirMarkState && mbth.CirMarkPlayNum == 0)
                            {
                                player.PlayEachSound(@".\circle.wav", 2000);
                                mbth.CirMarkPlayNum++;
                            }
                        }
                    }
                }
                */
                #endregion

                if(Setup.isPlaySignnumMusic)
                {
                    player.PlayEachSound(@".\circle.wav", 2000);
                    Setup.isPlaySignnumMusic = false;
                }

                PlayMusic();
            }
            catch(Exception ex)
            {
                BasicData.mainUI.Invoke(BasicData.mainUI.ShowFormText, new object[] { 
                    DataFiler.basicFormText + "*未找到音效文件*",
                    BasicData.mainUI });
                PlayMusic();
            }
        }
    }
}
