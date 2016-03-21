using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Exchange_UI
{
    public class DataShow
    {
        DrawGraph draw;
        public static int maxLengthY = 100;
        public int maxMark = 100;

        /// <summary>
        /// 风向图X向长度的一半
        /// </summary>
        private readonly int longMax = (BasicData.mainUI.pnlFengxiang.Size.Width - BasicData.mainUI.tlpFengxiang.Size.Width) / 2;
        private readonly int longMaxStart = 1;
        private readonly int shortMax = 59;

        private readonly int zoushituX_Unit = 1;

        public static int doubleSuperNum = 0;
        public readonly MyTime oneTime = new MyTime(0, 0, 1);

        #region 走势图下方的两个时间
        public static string zsDownTimeMid = "";
        public static string zsDownTimeLast = "";
        #endregion

        #region 控件数组
        public Label[] lblZTime = {   
                                      BasicData.mainUI.lblZT1, BasicData.mainUI.lblZT2, 
                                      BasicData.mainUI.lblZT3, BasicData.mainUI.lblZT4,
                                      BasicData.mainUI.lblZT5, BasicData.mainUI.lblZT6, 
                                      BasicData.mainUI.lblZT7, BasicData.mainUI.lblZT8 
                                  };

        public static Label[] lblTableTime = {
                                          BasicData.mainUI.lblWP1,BasicData.mainUI.lblWP2,
                                          BasicData.mainUI.lblWP3,BasicData.mainUI.lblWP4,
                                          BasicData.mainUI.lblWP5,BasicData.mainUI.lblWP6,
                                          BasicData.mainUI.lblWP7,BasicData.mainUI.lblWP8,
                                          BasicData.mainUI.lblWP9,BasicData.mainUI.lblWP10,
                                          BasicData.mainUI.lblWP11,BasicData.mainUI.lblWP12,
                                          BasicData.mainUI.lblWP13,BasicData.mainUI.lblWP14,
                                          BasicData.mainUI.lblWP15,BasicData.mainUI.lblWP16,
                                          BasicData.mainUI.lblWP17,BasicData.mainUI.lblWP18,
                                          BasicData.mainUI.lblWP19,BasicData.mainUI.lblWP20,
                                          BasicData.mainUI.lblWP21,BasicData.mainUI.lblWP22,
                                          BasicData.mainUI.lblWP23,BasicData.mainUI.lblWP24,
                                          BasicData.mainUI.lblWP25,BasicData.mainUI.lblWP26,
                                          BasicData.mainUI.lblWP27,BasicData.mainUI.lblWP28
                                      };

        public Label[] lblMoneyNameA = {
                                          BasicData.mainUI.lblMA1,BasicData.mainUI.lblMA2,
                                          BasicData.mainUI.lblMA3,BasicData.mainUI.lblMA4,
                                          BasicData.mainUI.lblMA5,BasicData.mainUI.lblMA6,
                                          BasicData.mainUI.lblMA7,BasicData.mainUI.lblMA8,
                                          BasicData.mainUI.lblMA9,BasicData.mainUI.lblMA10,
                                          BasicData.mainUI.lblMA11,BasicData.mainUI.lblMA12,
                                          BasicData.mainUI.lblMA13,BasicData.mainUI.lblMA14,
                                          BasicData.mainUI.lblMA15,BasicData.mainUI.lblMA16,
                                          BasicData.mainUI.lblMA17,BasicData.mainUI.lblMA18,
                                          BasicData.mainUI.lblMA19,BasicData.mainUI.lblMA20,
                                          BasicData.mainUI.lblMA21,BasicData.mainUI.lblMA22,
                                          BasicData.mainUI.lblMA23,BasicData.mainUI.lblMA24,
                                          BasicData.mainUI.lblMA25,BasicData.mainUI.lblMA26,
                                          BasicData.mainUI.lblMA27,BasicData.mainUI.lblMA28
                                       };
        public Label[] lblMoneyNameB = {
                                          BasicData.mainUI.lblMB1,BasicData.mainUI.lblMB2,
                                          BasicData.mainUI.lblMB3,BasicData.mainUI.lblMB4,
                                          BasicData.mainUI.lblMB5,BasicData.mainUI.lblMB6,
                                          BasicData.mainUI.lblMB7,BasicData.mainUI.lblMB8,
                                          BasicData.mainUI.lblMB9,BasicData.mainUI.lblMB10,
                                          BasicData.mainUI.lblMB11,BasicData.mainUI.lblMB12,
                                          BasicData.mainUI.lblMB13,BasicData.mainUI.lblMB14,
                                          BasicData.mainUI.lblMB15,BasicData.mainUI.lblMB16,
                                          BasicData.mainUI.lblMB17,BasicData.mainUI.lblMB18,
                                          BasicData.mainUI.lblMB19,BasicData.mainUI.lblMB20,
                                          BasicData.mainUI.lblMB21,BasicData.mainUI.lblMB22,
                                          BasicData.mainUI.lblMB23,BasicData.mainUI.lblMB24,
                                          BasicData.mainUI.lblMB25,BasicData.mainUI.lblMB26,
                                          BasicData.mainUI.lblMB27,BasicData.mainUI.lblMB28
                                       };
        public TableLayoutPanel[] tlpTable = {
                                                 BasicData.mainUI.tlpTable1,
                                                 BasicData.mainUI.tlpTable2,
                                                 BasicData.mainUI.tlpTable3,
                                                 BasicData.mainUI.tlpTable4,
                                                 BasicData.mainUI.tlpTable5,
                                                 BasicData.mainUI.tlpTable6,
                                                 BasicData.mainUI.tlpTable7,
                                             };
        public Label[] lcName = {
                                    BasicData.mainUI.lcName1,BasicData.mainUI.lcName2,
                                    BasicData.mainUI.lcName3,BasicData.mainUI.lcName4,
                                    BasicData.mainUI.lcName5,BasicData.mainUI.lcName6,
                                    BasicData.mainUI.lcName7,BasicData.mainUI.lcName8,
                                    BasicData.mainUI.lcName9,BasicData.mainUI.lcName10,
                                    BasicData.mainUI.lcName11,BasicData.mainUI.lcName12,
                                    BasicData.mainUI.lcName13,BasicData.mainUI.lcName14,
                                    BasicData.mainUI.lcName15,BasicData.mainUI.lcName16,
                                    BasicData.mainUI.lcName17,BasicData.mainUI.lcName18,
                                    BasicData.mainUI.lcName19,BasicData.mainUI.lcName20,
                                    BasicData.mainUI.lcName21,BasicData.mainUI.lcName22,
                                    BasicData.mainUI.lcName23,BasicData.mainUI.lcName24,
                                    BasicData.mainUI.lcName25,BasicData.mainUI.lcName26,
                                    BasicData.mainUI.lcName27,BasicData.mainUI.lcName28
                                };

        public Label[] lblShortA = {
                                          BasicData.mainUI.shortA1,BasicData.mainUI.shortA2,
                                          BasicData.mainUI.shortA3,BasicData.mainUI.shortA4,
                                          BasicData.mainUI.shortA5,BasicData.mainUI.shortA6,
                                          BasicData.mainUI.shortA7,BasicData.mainUI.shortA8,
                                          BasicData.mainUI.shortA9,BasicData.mainUI.shortA10,
                                          BasicData.mainUI.shortA11,BasicData.mainUI.shortA12,
                                          BasicData.mainUI.shortA13,BasicData.mainUI.shortA14,
                                          BasicData.mainUI.shortA15,BasicData.mainUI.shortA16,
                                          BasicData.mainUI.shortA17,BasicData.mainUI.shortA18,
                                          BasicData.mainUI.shortA19,BasicData.mainUI.shortA20,
                                          BasicData.mainUI.shortA21,BasicData.mainUI.shortA22,
                                          BasicData.mainUI.shortA23,BasicData.mainUI.shortA24,
                                          BasicData.mainUI.shortA25,BasicData.mainUI.shortA26,
                                          BasicData.mainUI.shortA27,BasicData.mainUI.shortA28
                                       };
        public Label[] lblShortB = {
                                          BasicData.mainUI.shortB1,BasicData.mainUI.shortB2,
                                          BasicData.mainUI.shortB3,BasicData.mainUI.shortB4,
                                          BasicData.mainUI.shortB5,BasicData.mainUI.shortB6,
                                          BasicData.mainUI.shortB7,BasicData.mainUI.shortB8,
                                          BasicData.mainUI.shortB9,BasicData.mainUI.shortB10,
                                          BasicData.mainUI.shortB11,BasicData.mainUI.shortB12,
                                          BasicData.mainUI.shortB13,BasicData.mainUI.shortB14,
                                          BasicData.mainUI.shortB15,BasicData.mainUI.shortB16,
                                          BasicData.mainUI.shortB17,BasicData.mainUI.shortB18,
                                          BasicData.mainUI.shortB19,BasicData.mainUI.shortB20,
                                          BasicData.mainUI.shortB21,BasicData.mainUI.shortB22,
                                          BasicData.mainUI.shortB23,BasicData.mainUI.shortB24,
                                          BasicData.mainUI.shortB25,BasicData.mainUI.shortB26,
                                          BasicData.mainUI.shortB27,BasicData.mainUI.shortB28
                                       };
        public Label[] lblLongA = {
                                          BasicData.mainUI.longA1,BasicData.mainUI.longA2,
                                          BasicData.mainUI.longA3,BasicData.mainUI.longA4,
                                          BasicData.mainUI.longA5,BasicData.mainUI.longA6,
                                          BasicData.mainUI.longA7,BasicData.mainUI.longA8,
                                          BasicData.mainUI.longA9,BasicData.mainUI.longA10,
                                          BasicData.mainUI.longA11,BasicData.mainUI.longA12,
                                          BasicData.mainUI.longA13,BasicData.mainUI.longA14,
                                          BasicData.mainUI.longA15,BasicData.mainUI.longA16,
                                          BasicData.mainUI.longA17,BasicData.mainUI.longA18,
                                          BasicData.mainUI.longA19,BasicData.mainUI.longA20,
                                          BasicData.mainUI.longA21,BasicData.mainUI.longA22,
                                          BasicData.mainUI.longA23,BasicData.mainUI.longA24,
                                          BasicData.mainUI.longA25,BasicData.mainUI.longA26,
                                          BasicData.mainUI.longA27,BasicData.mainUI.longA28
                                       };
        public Label[] lblLongB = {
                                          BasicData.mainUI.longB1,BasicData.mainUI.longB2,
                                          BasicData.mainUI.longB3,BasicData.mainUI.longB4,
                                          BasicData.mainUI.longB5,BasicData.mainUI.longB6,
                                          BasicData.mainUI.longB7,BasicData.mainUI.longB8,
                                          BasicData.mainUI.longB9,BasicData.mainUI.longB10,
                                          BasicData.mainUI.longB11,BasicData.mainUI.longB12,
                                          BasicData.mainUI.longB13,BasicData.mainUI.longB14,
                                          BasicData.mainUI.longB15,BasicData.mainUI.longB16,
                                          BasicData.mainUI.longB17,BasicData.mainUI.longB18,
                                          BasicData.mainUI.longB19,BasicData.mainUI.longB20,
                                          BasicData.mainUI.longB21,BasicData.mainUI.longB22,
                                          BasicData.mainUI.longB23,BasicData.mainUI.longB24,
                                          BasicData.mainUI.longB25,BasicData.mainUI.longB26,
                                          BasicData.mainUI.longB27,BasicData.mainUI.longB28
                                       };
        public Label[] lblMoneyNameRXT = {
                                                BasicData.mainUI.lblUSD,
                                                BasicData.mainUI.lblJPY,
                                                BasicData.mainUI.lblEUR,
                                                BasicData.mainUI.lblGBP,
                                                BasicData.mainUI.lblCHF,
                                                BasicData.mainUI.lblCAD,
                                                BasicData.mainUI.lblAUD,
                                                BasicData.mainUI.lblNZD
                                        };

        
        #endregion

        /// <summary>
        /// 线框位置标记
        /// </summary>
        public static int linePos = 1;


        /// <summary>
        /// 线框上一次的位置
        /// </summary>
        public static int linePosLast = 0;
        public static int[] linePosY = { 2, 28, 53, 79, 105, 131, 158 };

        //int unit;   //一个单元格的长度

        /// <summary>
        /// 进退栏前点的横坐标
        /// </summary>
        private int locationX = 494;
        public DataShow()
        {
            draw = new DrawGraph();
            //for (int i = 0; i < linePosY.Length; i++)
            //{
            //    linePosY[i] = 3 + 25 * i;
            //}
        }
        public void ToTable()
        {
            ToMoneyBoth();
            ToDiancha();
            ToShort();
            //ToLong();
            ToLocation();
            ToBuyOrSell();
            ToPriceChange();
            //IsLocHaveData();
        }

        public void IsLocHaveData() 
        {
            Setup.isLocHaveNum = 0;
            foreach(Label lb in lcName)
            {
                if(lb.Text != "")
                {
                    Setup.isLocHaveNum++;
                }
            }
        }

        /// <summary>
        /// 画突出线框
        /// </summary>
        public void UpLine()
        {
            Color upLineColor = Color.WhiteSmoke;

            int diff = 1;
            DownLine(linePosLast);

            InitTableCo();

            #region 画线框
            switch (DataShow.linePos)
            {
                case 1:         //1
                    draw.PaintRectangle(BasicData.mainUI.tlpTable1, upLineColor, new Point(2, linePosY[0] ), DrawGraph.rectX, DrawGraph.rectY, 1);
                    break;
                case 2:
                    draw.PaintRectangle(BasicData.mainUI.tlpTable1, upLineColor, new Point(2, linePosY[1] ), DrawGraph.rectX, DrawGraph.rectY, 1);
                    break;
                case 3:
                    draw.PaintRectangle(BasicData.mainUI.tlpTable1, upLineColor, new Point(2, linePosY[2] + diff), DrawGraph.rectX, DrawGraph.rectY, 1);
                    break;
                case 4:
                    draw.PaintRectangle(BasicData.mainUI.tlpTable1, upLineColor, new Point(2, linePosY[3] + diff), DrawGraph.rectX, DrawGraph.rectY, 1);
                    break;
                case 5:
                    draw.PaintRectangle(BasicData.mainUI.tlpTable1, upLineColor, new Point(2, linePosY[4] + diff), DrawGraph.rectX, DrawGraph.rectY, 1);
                    break;
                case 6:
                    draw.PaintRectangle(BasicData.mainUI.tlpTable1, upLineColor, new Point(2, linePosY[5] + diff), DrawGraph.rectX, DrawGraph.rectY, 1);
                    break;
                case 7:
                    draw.PaintRectangle(BasicData.mainUI.tlpTable1, upLineColor, new Point(2, linePosY[6] ), DrawGraph.rectX, DrawGraph.rectY, 1);
                    break;
                case 8:         //2
                    draw.PaintRectangle(BasicData.mainUI.tlpTable2, upLineColor, new Point(2, linePosY[0] ), DrawGraph.rectX, DrawGraph.rectY, 1);
                    break;
                case 9:
                    draw.PaintRectangle(BasicData.mainUI.tlpTable2, upLineColor, new Point(2, linePosY[1] ), DrawGraph.rectX, DrawGraph.rectY, 1);
                    break;
                case 10:
                    draw.PaintRectangle(BasicData.mainUI.tlpTable2, upLineColor, new Point(2, linePosY[2] + diff), DrawGraph.rectX, DrawGraph.rectY, 1);
                    break;
                case 11:
                    draw.PaintRectangle(BasicData.mainUI.tlpTable2, upLineColor, new Point(2, linePosY[3] + diff), DrawGraph.rectX, DrawGraph.rectY, 1);
                    break;
                case 12:
                    draw.PaintRectangle(BasicData.mainUI.tlpTable2, upLineColor, new Point(2, linePosY[4] + diff), DrawGraph.rectX, DrawGraph.rectY, 1);
                    break;
                case 13:
                    draw.PaintRectangle(BasicData.mainUI.tlpTable2, upLineColor, new Point(2, linePosY[5] + diff), DrawGraph.rectX, DrawGraph.rectY, 1);
                    break;
                case 14:         //3
                    draw.PaintRectangle(BasicData.mainUI.tlpTable3, upLineColor, new Point(2, linePosY[0] ), DrawGraph.rectX, DrawGraph.rectY, 1);
                    break;
                case 15:
                    draw.PaintRectangle(BasicData.mainUI.tlpTable3, upLineColor, new Point(2, linePosY[1] ), DrawGraph.rectX, DrawGraph.rectY, 1);
                    break;
                case 16:
                    draw.PaintRectangle(BasicData.mainUI.tlpTable3, upLineColor, new Point(2, linePosY[2] + diff), DrawGraph.rectX, DrawGraph.rectY, 1);
                    break;
                case 17:
                    draw.PaintRectangle(BasicData.mainUI.tlpTable3, upLineColor, new Point(2, linePosY[3] + diff), DrawGraph.rectX, DrawGraph.rectY, 1);
                    break;
                case 18:
                    draw.PaintRectangle(BasicData.mainUI.tlpTable3, upLineColor, new Point(2, linePosY[4] + diff), DrawGraph.rectX, DrawGraph.rectY, 1);
                    break;
                case 19:        //4
                    draw.PaintRectangle(BasicData.mainUI.tlpTable4, upLineColor, new Point(2, linePosY[0] ), DrawGraph.rectX, DrawGraph.rectY, 1);
                    break;
                case 20:
                    draw.PaintRectangle(BasicData.mainUI.tlpTable4, upLineColor, new Point(2, linePosY[1] ), DrawGraph.rectX, DrawGraph.rectY, 1);
                    break;
                case 21:
                    draw.PaintRectangle(BasicData.mainUI.tlpTable4, upLineColor, new Point(2, linePosY[2] + diff), DrawGraph.rectX, DrawGraph.rectY, 1);
                    break;
                case 22:
                    draw.PaintRectangle(BasicData.mainUI.tlpTable4, upLineColor, new Point(2, linePosY[3] + diff), DrawGraph.rectX, DrawGraph.rectY, 1);
                    break;
                case 23:        //5
                    draw.PaintRectangle(BasicData.mainUI.tlpTable5, upLineColor, new Point(2, linePosY[0] ), DrawGraph.rectX, DrawGraph.rectY, 1);
                    break;
                case 24:
                    draw.PaintRectangle(BasicData.mainUI.tlpTable5, upLineColor, new Point(2, linePosY[1] ), DrawGraph.rectX, DrawGraph.rectY, 1);
                    break;
                case 25:
                    draw.PaintRectangle(BasicData.mainUI.tlpTable5, upLineColor, new Point(2, linePosY[2] + diff), DrawGraph.rectX, DrawGraph.rectY, 1);
                    break;
                case 26:        //6
                    draw.PaintRectangle(BasicData.mainUI.tlpTable6, upLineColor, new Point(2, linePosY[0] ), DrawGraph.rectX, DrawGraph.rectY, 1);
                    break;
                case 27:
                    draw.PaintRectangle(BasicData.mainUI.tlpTable6, upLineColor, new Point(2, linePosY[1] ), DrawGraph.rectX, DrawGraph.rectY, 1);
                    break;
                case 28:        //7
                    draw.PaintRectangle(BasicData.mainUI.tlpTable7, upLineColor, new Point(2, linePosY[0] + diff), DrawGraph.rectX, DrawGraph.rectY, 1);
                    break;
            }
            #endregion
        }

        /// <summary>
        /// 初始化表格标线
        /// </summary>
        private void InitTableCo()
        {
            //横线
            int tableHPosX = 1;      //线起点 横坐标
            int[] tableHPosY = { 0, 26, 52, 78, 104, 130, 156 ,182};     //线终点 纵坐标
            for (int j = 0; j < DataFiler.basicMoneyGroup.Length; j++)
            {
                for (int i = 0; i < DataFiler.basicMoneyGroup[j].allMoneyBoth.Length + 1; i++)
                {
                    draw.PaintLine(
                        tlpTable[j],
                        DrawGraph.lineColor,
                        new Point(tableHPosX, tableHPosY[i]),
                        new Point(tableHPosX + DrawGraph.rectX, tableHPosY[i])
                        );
                }
            }

            //纵线
            int[] tableVPosX = { 0, 82, 164, 246, 309, 378, 479, 572, 658 };     //线终点 纵坐标
            int tableVPosY = 1;      //线起点 横坐标
            int[] tableVLength = { 182, 156, 130, 104, 78, 52, 28 };
            for (int j = 0; j < DataFiler.basicMoneyGroup.Length; j++)
            {
                for (int i = 0; i < 9; i++)
                {
                    draw.PaintLine(
                                tlpTable[j],
                                DrawGraph.lineColor,
                                new Point(tableVPosX[i], tableVPosY),
                                new Point(tableVPosX[i], tableVPosY + tableVLength[j])
                                );
                }
            }

        }

        /// <summary>
        /// 删除指定的线框（行）
        /// </summary>
        /// <param name="num"></param>
        public void DownLine(int num)
        {
            #region 去掉指定的线框
            Color downLineColorA = BasicData.mainUI.tlpTable1.BackColor;     //Color.Gray
            Color downLineColorB = BasicData.mainUI.tlpTable2.BackColor;     //Color.Gray

            int diff = 1;
            switch (num)
            {
                case 1:         //1
                    //draw.PaintRectangle(BasicData.mainUI.tlpTable1, Color.Gray, new Point(2, 2), DrawGraph.rectX, DrawGraph.rectY, 1);
                    draw.PaintRectangle(BasicData.mainUI.tlpTable1, downLineColorA, new Point(2, linePosY[0] ), DrawGraph.rectX, DrawGraph.rectY, 1);
                    break;
                case 2:
                    draw.PaintRectangle(BasicData.mainUI.tlpTable1, downLineColorA, new Point(2, linePosY[1] ), DrawGraph.rectX, DrawGraph.rectY, 1);
                    break;
                case 3:
                    draw.PaintRectangle(BasicData.mainUI.tlpTable1, downLineColorA, new Point(2, linePosY[2] + diff), DrawGraph.rectX, DrawGraph.rectY, 1);
                    break;
                case 4:
                    draw.PaintRectangle(BasicData.mainUI.tlpTable1, downLineColorA, new Point(2, linePosY[3] + diff), DrawGraph.rectX, DrawGraph.rectY, 1);
                    break;
                case 5:
                    draw.PaintRectangle(BasicData.mainUI.tlpTable1, downLineColorA, new Point(2, linePosY[4] + diff), DrawGraph.rectX, DrawGraph.rectY, 1);
                    break;
                case 6:
                    draw.PaintRectangle(BasicData.mainUI.tlpTable1, downLineColorA, new Point(2, linePosY[5] + diff), DrawGraph.rectX, DrawGraph.rectY, 1);
                    break;
                case 7:
                    draw.PaintRectangle(BasicData.mainUI.tlpTable1, downLineColorA, new Point(2, linePosY[6] ), DrawGraph.rectX, DrawGraph.rectY, 1);
                    break;
                case 8:         //2
                    draw.PaintRectangle(BasicData.mainUI.tlpTable2, downLineColorB, new Point(2, linePosY[0] ), DrawGraph.rectX, DrawGraph.rectY, 1);
                    break;
                case 9:
                    draw.PaintRectangle(BasicData.mainUI.tlpTable2, downLineColorB, new Point(2, linePosY[1] ), DrawGraph.rectX, DrawGraph.rectY, 1);
                    break;
                case 10:
                    draw.PaintRectangle(BasicData.mainUI.tlpTable2, downLineColorB, new Point(2, linePosY[2] + diff), DrawGraph.rectX, DrawGraph.rectY, 1);
                    break;
                case 11:
                    draw.PaintRectangle(BasicData.mainUI.tlpTable2, downLineColorB, new Point(2, linePosY[3] + diff), DrawGraph.rectX, DrawGraph.rectY, 1);
                    break;
                case 12:
                    draw.PaintRectangle(BasicData.mainUI.tlpTable2, downLineColorB, new Point(2, linePosY[4] + diff), DrawGraph.rectX, DrawGraph.rectY, 1);
                    break;
                case 13:
                    draw.PaintRectangle(BasicData.mainUI.tlpTable2, downLineColorB, new Point(2, linePosY[5] + diff), DrawGraph.rectX, DrawGraph.rectY, 1);
                    break;
                case 14:         //3
                    draw.PaintRectangle(BasicData.mainUI.tlpTable3, downLineColorA, new Point(2, linePosY[0] ), DrawGraph.rectX, DrawGraph.rectY, 1);
                    break;
                case 15:
                    draw.PaintRectangle(BasicData.mainUI.tlpTable3, downLineColorA, new Point(2, linePosY[1] ), DrawGraph.rectX, DrawGraph.rectY, 1);
                    break;
                case 16:
                    draw.PaintRectangle(BasicData.mainUI.tlpTable3, downLineColorA, new Point(2, linePosY[2] + diff), DrawGraph.rectX, DrawGraph.rectY, 1);
                    break;
                case 17:
                    draw.PaintRectangle(BasicData.mainUI.tlpTable3, downLineColorA, new Point(2, linePosY[3] + diff), DrawGraph.rectX, DrawGraph.rectY, 1);
                    break;
                case 18:
                    draw.PaintRectangle(BasicData.mainUI.tlpTable3, downLineColorA, new Point(2, linePosY[4] + diff), DrawGraph.rectX, DrawGraph.rectY, 1);
                    break;
                case 19:        //4
                    draw.PaintRectangle(BasicData.mainUI.tlpTable4, downLineColorB, new Point(2, linePosY[0] ), DrawGraph.rectX, DrawGraph.rectY, 1);
                    break;
                case 20:
                    draw.PaintRectangle(BasicData.mainUI.tlpTable4, downLineColorB, new Point(2, linePosY[1] ), DrawGraph.rectX, DrawGraph.rectY, 1);
                    break;
                case 21:
                    draw.PaintRectangle(BasicData.mainUI.tlpTable4, downLineColorB, new Point(2, linePosY[2] + diff), DrawGraph.rectX, DrawGraph.rectY, 1);
                    break;
                case 22:
                    draw.PaintRectangle(BasicData.mainUI.tlpTable4, downLineColorB, new Point(2, linePosY[3] + diff), DrawGraph.rectX, DrawGraph.rectY, 1);
                    break;
                case 23:        //5
                    draw.PaintRectangle(BasicData.mainUI.tlpTable5, downLineColorA, new Point(2, linePosY[0] ), DrawGraph.rectX, DrawGraph.rectY, 1);
                    break;
                case 24:
                    draw.PaintRectangle(BasicData.mainUI.tlpTable5, downLineColorA, new Point(2, linePosY[1] ), DrawGraph.rectX, DrawGraph.rectY, 1);
                    break;
                case 25:
                    draw.PaintRectangle(BasicData.mainUI.tlpTable5, downLineColorA, new Point(2, linePosY[2] + diff), DrawGraph.rectX, DrawGraph.rectY, 1);
                    break;
                case 26:        //6
                    draw.PaintRectangle(BasicData.mainUI.tlpTable6, downLineColorB, new Point(2, linePosY[0] ), DrawGraph.rectX, DrawGraph.rectY, 1);
                    break;
                case 27:
                    draw.PaintRectangle(BasicData.mainUI.tlpTable6, downLineColorB, new Point(2, linePosY[1] ), DrawGraph.rectX, DrawGraph.rectY, 1);
                    break;
                case 28:        //7
                    draw.PaintRectangle(BasicData.mainUI.tlpTable7, downLineColorA, new Point(2, linePosY[0] + diff), DrawGraph.rectX, DrawGraph.rectY, 1);
                    break;
            }
            #endregion
        }

        /// <summary>
        /// 更新刷新按钮上的时间
        /// </summary>
        public void ToTime()
        {
            if(Setup.timeWaihui != null)        //2016.02.12 21:58
            {
                int waihuiH = (int.Parse(Setup.timeWaihui.Substring(11, 2)) + Setup.timeHourDiff) % 24;
                string waihuiM = Setup.timeWaihui.Substring(14, 2);
                string time = waihuiH + ":" + waihuiM;
                BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { time, BasicData.mainUI.lblUpdataTime });
            }
        }
        public void ToZhongzhou()
        {
            Color toMoneyRed = Color.FromArgb(204, 0, 0);

            #region 中轴
            //1
            if (DataFiler.basicMoney[0].LengthY > Setup.sMHeightLimit &&
                DataFiler.basicMoney[0].ColorX != Setup.sMGrayFix1 && 
                DataFiler.basicMoney[0].ColorX != Setup.sMGrayFix2
                )
            {
                if(DataFiler.basicMoney[0].ColorX > Setup.sMGreen)
                {
                    BasicData.mainUI.button3.BackColor = Color.Green;
                    BasicData.mainUI.button5.BackColor = Color.Green;
                    BasicData.mainUI.lblM_USU.BackColor = Color.Green;
                    DataFiler.basicMoney[0].MoneyColor = Color.Green;
                }
                else if(DataFiler.basicMoney[0].ColorX < Setup.sMRed)
                {
                    BasicData.mainUI.button3.BackColor = toMoneyRed;
                    BasicData.mainUI.button5.BackColor = toMoneyRed;
                    BasicData.mainUI.lblM_USU.BackColor = toMoneyRed;
                    DataFiler.basicMoney[0].MoneyColor = toMoneyRed;
                }
            }
            else
            {
                BasicData.mainUI.button3.BackColor = Color.Gray;
                BasicData.mainUI.button5.BackColor = Color.Gray;
                BasicData.mainUI.lblM_USU.BackColor = Color.Gray;
                DataFiler.basicMoney[0].MoneyColor = Color.Gray;
            }

            //2
            if (DataFiler.basicMoney[1].LengthY > Setup.sMHeightLimit &&
                DataFiler.basicMoney[1].ColorX != Setup.sMGrayFix1 &&
                DataFiler.basicMoney[1].ColorX != Setup.sMGrayFix2)
            {
                if (DataFiler.basicMoney[1].ColorX > Setup.sMGreen)
                {
                    BasicData.mainUI.button8.BackColor = Color.Green;
                    BasicData.mainUI.button10.BackColor = Color.Green;
                    BasicData.mainUI.lblM_JPY.BackColor = Color.Green;
                    DataFiler.basicMoney[1].MoneyColor = Color.Green;
                }
                else if (DataFiler.basicMoney[1].ColorX < Setup.sMRed)
                {
                    BasicData.mainUI.button8.BackColor = toMoneyRed;
                    BasicData.mainUI.button10.BackColor = toMoneyRed;
                    BasicData.mainUI.lblM_JPY.BackColor = toMoneyRed;
                    DataFiler.basicMoney[1].MoneyColor = toMoneyRed;
                }
            }
            else
            {
                BasicData.mainUI.button8.BackColor = Color.Gray;
                BasicData.mainUI.button10.BackColor = Color.Gray;
                BasicData.mainUI.lblM_JPY.BackColor = Color.Gray;
                DataFiler.basicMoney[1].MoneyColor = Color.Gray;
            }
            //3
            if (DataFiler.basicMoney[2].LengthY > Setup.sMHeightLimit &&
                DataFiler.basicMoney[2].ColorX != Setup.sMGrayFix1 &&
                DataFiler.basicMoney[2].ColorX != Setup.sMGrayFix2)
            {
                if (DataFiler.basicMoney[2].ColorX > Setup.sMGreen)
                {
                    BasicData.mainUI.button18.BackColor = Color.Green;
                    BasicData.mainUI.button16.BackColor = Color.Green;
                    BasicData.mainUI.lblM_EUR.BackColor = Color.Green;
                    DataFiler.basicMoney[2].MoneyColor = Color.Green;
                }
                else if (DataFiler.basicMoney[2].ColorX < Setup.sMRed)
                {
                    BasicData.mainUI.button18.BackColor = toMoneyRed;
                    BasicData.mainUI.button16.BackColor = toMoneyRed;
                    BasicData.mainUI.lblM_EUR.BackColor = toMoneyRed;
                    DataFiler.basicMoney[2].MoneyColor = toMoneyRed;
                }
            }
            else
            {
                BasicData.mainUI.button18.BackColor = Color.Gray;
                BasicData.mainUI.button16.BackColor = Color.Gray;
                BasicData.mainUI.lblM_EUR.BackColor = Color.Gray;
                DataFiler.basicMoney[2].MoneyColor = Color.Gray;
            }

            //4
            if (DataFiler.basicMoney[3].LengthY > Setup.sMHeightLimit &&
                DataFiler.basicMoney[3].ColorX != Setup.sMGrayFix1 &&
                DataFiler.basicMoney[3].ColorX != Setup.sMGrayFix2)
            {
                if (DataFiler.basicMoney[3].ColorX > Setup.sMGreen)
                {
                    BasicData.mainUI.button14.BackColor = Color.Green;
                    BasicData.mainUI.button12.BackColor = Color.Green;
                    BasicData.mainUI.lblM_GBP.BackColor = Color.Green;
                    DataFiler.basicMoney[3].MoneyColor = Color.Green;
                }
                else if (DataFiler.basicMoney[3].ColorX < Setup.sMRed)
                {
                    BasicData.mainUI.button14.BackColor = toMoneyRed;
                    BasicData.mainUI.button12.BackColor = toMoneyRed;
                    BasicData.mainUI.lblM_GBP.BackColor = toMoneyRed;
                    DataFiler.basicMoney[3].MoneyColor = toMoneyRed;
                }
            }
            else
            {
                BasicData.mainUI.button14.BackColor = Color.Gray;
                BasicData.mainUI.button12.BackColor = Color.Gray;
                BasicData.mainUI.lblM_GBP.BackColor = Color.Gray;
                DataFiler.basicMoney[3].MoneyColor = Color.Gray;
            }
            //5
            if (DataFiler.basicMoney[4].LengthY > Setup.sMHeightLimit &&
                DataFiler.basicMoney[4].ColorX != Setup.sMGrayFix1 &&
                DataFiler.basicMoney[4].ColorX != Setup.sMGrayFix2)
            {
                if (DataFiler.basicMoney[4].ColorX > Setup.sMGreen)
                {
                    BasicData.mainUI.button34.BackColor = Color.Green;
                    BasicData.mainUI.button32.BackColor = Color.Green;
                    BasicData.mainUI.lblM_CHF.BackColor = Color.Green;
                    DataFiler.basicMoney[4].MoneyColor = Color.Green;
                }
                else if (DataFiler.basicMoney[4].ColorX < Setup.sMRed)
                {
                    BasicData.mainUI.button34.BackColor = toMoneyRed;
                    BasicData.mainUI.button32.BackColor = toMoneyRed;
                    BasicData.mainUI.lblM_CHF.BackColor = toMoneyRed;
                    DataFiler.basicMoney[4].MoneyColor = toMoneyRed;
                }
            }
            else
            {
                BasicData.mainUI.button34.BackColor = Color.Gray;
                BasicData.mainUI.button32.BackColor = Color.Gray;
                BasicData.mainUI.lblM_CHF.BackColor = Color.Gray;
                DataFiler.basicMoney[4].MoneyColor = Color.Gray;
            }

            //6
            if (DataFiler.basicMoney[5].LengthY > Setup.sMHeightLimit &&
                DataFiler.basicMoney[5].ColorX != Setup.sMGrayFix1 &&
                DataFiler.basicMoney[5].ColorX != Setup.sMGrayFix2)
            {
                if (DataFiler.basicMoney[5].ColorX > Setup.sMGreen)
                {
                    BasicData.mainUI.button30.BackColor = Color.Green;
                    BasicData.mainUI.button28.BackColor = Color.Green;
                    BasicData.mainUI.lblM_CAD.BackColor = Color.Green;
                    DataFiler.basicMoney[5].MoneyColor = Color.Green;
                }
                else if (DataFiler.basicMoney[5].ColorX < Setup.sMRed)
                {
                    BasicData.mainUI.button30.BackColor = toMoneyRed;
                    BasicData.mainUI.button28.BackColor = toMoneyRed;
                    BasicData.mainUI.lblM_CAD.BackColor = toMoneyRed;
                    DataFiler.basicMoney[5].MoneyColor = toMoneyRed;
                }
            }
            else
            {
                BasicData.mainUI.button30.BackColor = Color.Gray;
                BasicData.mainUI.button28.BackColor = Color.Gray;
                BasicData.mainUI.lblM_CAD.BackColor = Color.Gray;
                DataFiler.basicMoney[5].MoneyColor = Color.Gray;
            }
            //7
            if (DataFiler.basicMoney[6].LengthY > Setup.sMHeightLimit &&
                DataFiler.basicMoney[6].ColorX != Setup.sMGrayFix1 &&
                DataFiler.basicMoney[6].ColorX != Setup.sMGrayFix2)
            {
                if (DataFiler.basicMoney[6].ColorX > Setup.sMGreen)
                {
                    BasicData.mainUI.button26.BackColor = Color.Green;
                    BasicData.mainUI.button24.BackColor = Color.Green;
                    BasicData.mainUI.lblM_AUD.BackColor = Color.Green;
                    DataFiler.basicMoney[6].MoneyColor = Color.Green;
                }
                else if (DataFiler.basicMoney[6].ColorX < Setup.sMRed)
                {
                    BasicData.mainUI.button26.BackColor = toMoneyRed;
                    BasicData.mainUI.button24.BackColor = toMoneyRed;
                    BasicData.mainUI.lblM_AUD.BackColor = toMoneyRed;
                    DataFiler.basicMoney[6].MoneyColor = toMoneyRed;
                }
            }
            else
            {
                BasicData.mainUI.button26.BackColor = Color.Gray;
                BasicData.mainUI.button24.BackColor = Color.Gray;
                BasicData.mainUI.lblM_AUD.BackColor = Color.Gray;
                DataFiler.basicMoney[6].MoneyColor = Color.Gray;
            }

            //8
            if (DataFiler.basicMoney[7].LengthY > Setup.sMHeightLimit &&
                DataFiler.basicMoney[7].ColorX != Setup.sMGrayFix1 &&
                DataFiler.basicMoney[7].ColorX != Setup.sMGrayFix2)
            {
                if (DataFiler.basicMoney[7].ColorX > Setup.sMGreen)
                {
                    BasicData.mainUI.button22.BackColor = Color.Green;
                    BasicData.mainUI.button20.BackColor = Color.Green;
                    BasicData.mainUI.lblM_NZD.BackColor = Color.Green;
                    DataFiler.basicMoney[7].MoneyColor = Color.Green;
                }
                else if (DataFiler.basicMoney[7].ColorX < Setup.sMRed)
                {
                    BasicData.mainUI.button22.BackColor = toMoneyRed;
                    BasicData.mainUI.button20.BackColor = toMoneyRed;
                    BasicData.mainUI.lblM_NZD.BackColor = toMoneyRed;
                    DataFiler.basicMoney[7].MoneyColor = toMoneyRed;
                }
            }
            else
            {
                BasicData.mainUI.button22.BackColor = Color.Gray;
                BasicData.mainUI.button20.BackColor = Color.Gray;
                BasicData.mainUI.lblM_NZD.BackColor = Color.Gray;
                DataFiler.basicMoney[7].MoneyColor = Color.Gray;
            }
            #endregion
        }
        public void UpStar()
        {
            //int[] Y = { 3, 24, 48, 70, 92, 115, 137 };
            int locX = 70;
            int locDiffY = 3;
            Color starColor = Color.Yellow;
            #region 星号标记
            //1
            if (DataFiler.basicMoneyGroup[0].allMoneyBoth[0].PosLocation == 'L')
            {
                draw.PaintStar(BasicData.mainUI.tlpTable1, new Point(locX, linePosY[0] + locDiffY ), starColor);
            }
            else
            {
                draw.PaintStar(BasicData.mainUI.tlpTable1, new Point(locX, linePosY[0] + locDiffY + locDiffY), BasicData.mainUI.tlpTable1.BackColor);
            }
            if (DataFiler.basicMoneyGroup[0].allMoneyBoth[1].PosLocation == 'L')
            {
                draw.PaintStar(BasicData.mainUI.tlpTable1, new Point(locX, linePosY[1] + locDiffY + locDiffY), starColor);
            }
            else
            {
                draw.PaintStar(BasicData.mainUI.tlpTable1, new Point(locX, linePosY[1] + locDiffY + locDiffY), BasicData.mainUI.tlpTable1.BackColor);
            }

            if (DataFiler.basicMoneyGroup[0].allMoneyBoth[2].PosLocation == 'L')
            {
                draw.PaintStar(BasicData.mainUI.tlpTable1, new Point(locX, linePosY[2] + locDiffY), starColor);
            }
            else
            {
                draw.PaintStar(BasicData.mainUI.tlpTable1, new Point(locX, linePosY[2] + locDiffY), BasicData.mainUI.tlpTable1.BackColor);
            }

            if (DataFiler.basicMoneyGroup[0].allMoneyBoth[3].PosLocation == 'L')
            {
                draw.PaintStar(BasicData.mainUI.tlpTable1, new Point(locX, linePosY[3] + locDiffY), starColor);
            }
            else
            {
                draw.PaintStar(BasicData.mainUI.tlpTable1, new Point(locX, linePosY[3] + locDiffY), BasicData.mainUI.tlpTable1.BackColor);
            }

            if (DataFiler.basicMoneyGroup[0].allMoneyBoth[4].PosLocation == 'L')
            {
                draw.PaintStar(BasicData.mainUI.tlpTable1, new Point(locX, linePosY[4] + locDiffY), starColor);
            }
            else
            {
                draw.PaintStar(BasicData.mainUI.tlpTable1, new Point(locX, linePosY[4] + locDiffY), BasicData.mainUI.tlpTable1.BackColor);
            }

            if (DataFiler.basicMoneyGroup[0].allMoneyBoth[5].PosLocation == 'L')
            {
                draw.PaintStar(BasicData.mainUI.tlpTable1, new Point(locX, linePosY[5] + locDiffY), starColor);
            }
            else
            {
                draw.PaintStar(BasicData.mainUI.tlpTable1, new Point(locX, linePosY[5] + locDiffY), BasicData.mainUI.tlpTable1.BackColor);
            }

            if (DataFiler.basicMoneyGroup[0].allMoneyBoth[6].PosLocation == 'L')
            {
                draw.PaintStar(BasicData.mainUI.tlpTable1, new Point(locX, linePosY[6] + locDiffY), starColor);
            }
            else
            {
                draw.PaintStar(BasicData.mainUI.tlpTable1, new Point(locX, linePosY[6] + locDiffY), BasicData.mainUI.tlpTable1.BackColor);
            }

            //2
            if (DataFiler.basicMoneyGroup[1].allMoneyBoth[0].PosLocation == 'L')
            {
                draw.PaintStar(BasicData.mainUI.tlpTable2, new Point(locX, linePosY[0] + locDiffY), starColor);
            }
            else
            {
                draw.PaintStar(BasicData.mainUI.tlpTable2, new Point(locX, linePosY[0] + locDiffY), BasicData.mainUI.tlpTable2.BackColor);
            }

            if (DataFiler.basicMoneyGroup[1].allMoneyBoth[1].PosLocation == 'L')
            {
                draw.PaintStar(BasicData.mainUI.tlpTable2, new Point(locX, linePosY[1] + locDiffY), starColor);
            }
            else
            {
                draw.PaintStar(BasicData.mainUI.tlpTable2, new Point(locX, linePosY[1] + locDiffY), BasicData.mainUI.tlpTable2.BackColor);
            }

            if (DataFiler.basicMoneyGroup[1].allMoneyBoth[2].PosLocation == 'L')
            {
                draw.PaintStar(BasicData.mainUI.tlpTable2, new Point(locX, linePosY[2] + locDiffY), starColor);
            }
            else
            {
                draw.PaintStar(BasicData.mainUI.tlpTable2, new Point(locX, linePosY[2] + locDiffY), BasicData.mainUI.tlpTable2.BackColor);
            }

            if (DataFiler.basicMoneyGroup[1].allMoneyBoth[3].PosLocation == 'L')
            {
                draw.PaintStar(BasicData.mainUI.tlpTable2, new Point(locX, linePosY[3] + locDiffY), starColor);
            }
            else
            {
                draw.PaintStar(BasicData.mainUI.tlpTable2, new Point(locX, linePosY[3] + locDiffY), BasicData.mainUI.tlpTable2.BackColor);
            }

            if (DataFiler.basicMoneyGroup[1].allMoneyBoth[4].PosLocation == 'L')
            {
                draw.PaintStar(BasicData.mainUI.tlpTable2, new Point(locX, linePosY[4] + locDiffY), starColor);
            }
            else
            {
                draw.PaintStar(BasicData.mainUI.tlpTable2, new Point(locX, linePosY[4] + locDiffY), BasicData.mainUI.tlpTable2.BackColor);
            }

            if (DataFiler.basicMoneyGroup[1].allMoneyBoth[5].PosLocation == 'L')
            {
                draw.PaintStar(BasicData.mainUI.tlpTable2, new Point(locX, linePosY[5] + locDiffY), starColor);
            }
            else
            {
                draw.PaintStar(BasicData.mainUI.tlpTable2, new Point(locX, linePosY[5] + locDiffY), BasicData.mainUI.tlpTable2.BackColor);
            }


            //3
            if (DataFiler.basicMoneyGroup[2].allMoneyBoth[0].PosLocation == 'L')
            {
                draw.PaintStar(BasicData.mainUI.tlpTable3, new Point(locX, linePosY[0] + locDiffY), starColor);
            }
            else
            {
                draw.PaintStar(BasicData.mainUI.tlpTable3, new Point(locX, linePosY[0] + locDiffY), BasicData.mainUI.tlpTable3.BackColor);
            }

            if (DataFiler.basicMoneyGroup[2].allMoneyBoth[1].PosLocation == 'L')
            {
                draw.PaintStar(BasicData.mainUI.tlpTable3, new Point(locX, linePosY[1] + locDiffY), starColor);
            }
            else
            {
                draw.PaintStar(BasicData.mainUI.tlpTable3, new Point(locX, linePosY[1] + locDiffY), BasicData.mainUI.tlpTable3.BackColor);
            }

            if (DataFiler.basicMoneyGroup[2].allMoneyBoth[2].PosLocation == 'L')
            {
                draw.PaintStar(BasicData.mainUI.tlpTable3, new Point(locX, linePosY[2] + locDiffY), starColor);
            }
            else
            {
                draw.PaintStar(BasicData.mainUI.tlpTable3, new Point(locX, linePosY[2] + locDiffY), BasicData.mainUI.tlpTable3.BackColor);
            }

            if (DataFiler.basicMoneyGroup[2].allMoneyBoth[3].PosLocation == 'L')
            {
                draw.PaintStar(BasicData.mainUI.tlpTable3, new Point(locX, linePosY[3] + locDiffY), starColor);
            }
            else
            {
                draw.PaintStar(BasicData.mainUI.tlpTable3, new Point(locX, linePosY[3] + locDiffY), BasicData.mainUI.tlpTable3.BackColor);
            }

            if (DataFiler.basicMoneyGroup[2].allMoneyBoth[4].PosLocation == 'L')
            {
                draw.PaintStar(BasicData.mainUI.tlpTable3, new Point(locX, linePosY[4] + locDiffY), starColor);
            }
            else
            {
                draw.PaintStar(BasicData.mainUI.tlpTable3, new Point(locX, linePosY[4] + locDiffY), BasicData.mainUI.tlpTable3.BackColor);
            }


            //4

            if (DataFiler.basicMoneyGroup[3].allMoneyBoth[0].PosLocation == 'L')
            {
                draw.PaintStar(BasicData.mainUI.tlpTable4, new Point(locX, linePosY[0] + locDiffY), starColor);
            }
            else
            {
                draw.PaintStar(BasicData.mainUI.tlpTable4, new Point(locX, linePosY[0] + locDiffY), BasicData.mainUI.tlpTable4.BackColor);
            }

            if (DataFiler.basicMoneyGroup[3].allMoneyBoth[1].PosLocation == 'L')
            {
                draw.PaintStar(BasicData.mainUI.tlpTable4, new Point(locX, linePosY[1] + locDiffY), starColor);
            }
            else
            {
                draw.PaintStar(BasicData.mainUI.tlpTable4, new Point(locX, linePosY[1] + locDiffY), BasicData.mainUI.tlpTable4.BackColor);
            }

            if (DataFiler.basicMoneyGroup[3].allMoneyBoth[2].PosLocation == 'L')
            {
                draw.PaintStar(BasicData.mainUI.tlpTable4, new Point(locX, linePosY[2] + locDiffY), starColor);
            }
            else
            {
                draw.PaintStar(BasicData.mainUI.tlpTable4, new Point(locX, linePosY[2] + locDiffY), BasicData.mainUI.tlpTable4.BackColor);
            }

            if (DataFiler.basicMoneyGroup[3].allMoneyBoth[3].PosLocation == 'L')
            {
                draw.PaintStar(BasicData.mainUI.tlpTable4, new Point(locX, linePosY[3] + locDiffY), starColor);
            }
            else
            {
                draw.PaintStar(BasicData.mainUI.tlpTable4, new Point(locX, linePosY[3] + locDiffY), BasicData.mainUI.tlpTable4.BackColor);
            }

            //5
            if (DataFiler.basicMoneyGroup[4].allMoneyBoth[0].PosLocation == 'L')
            {
                draw.PaintStar(BasicData.mainUI.tlpTable5, new Point(locX, linePosY[0] + locDiffY), starColor);
            }
            else
            {
                draw.PaintStar(BasicData.mainUI.tlpTable5, new Point(locX, linePosY[0] + locDiffY), BasicData.mainUI.tlpTable5.BackColor);
            }

            if (DataFiler.basicMoneyGroup[4].allMoneyBoth[1].PosLocation == 'L')
            {
                draw.PaintStar(BasicData.mainUI.tlpTable5, new Point(locX, linePosY[1] + locDiffY), starColor);
            }
            else
            {
                draw.PaintStar(BasicData.mainUI.tlpTable5, new Point(locX, linePosY[1] + locDiffY), BasicData.mainUI.tlpTable5.BackColor);
            }

            if (DataFiler.basicMoneyGroup[4].allMoneyBoth[2].PosLocation == 'L')
            {
                draw.PaintStar(BasicData.mainUI.tlpTable5, new Point(locX, linePosY[2] + locDiffY), starColor);
            }
            else
            {
                draw.PaintStar(BasicData.mainUI.tlpTable5, new Point(locX, linePosY[2] + locDiffY), BasicData.mainUI.tlpTable5.BackColor);
            }
            //6
            if (DataFiler.basicMoneyGroup[5].allMoneyBoth[0].PosLocation == 'L')
            {
                draw.PaintStar(BasicData.mainUI.tlpTable6, new Point(locX, linePosY[0] + locDiffY), starColor);
            }
            else
            {
                draw.PaintStar(BasicData.mainUI.tlpTable6, new Point(locX, linePosY[0] + locDiffY), BasicData.mainUI.tlpTable6.BackColor);
            }
            if (DataFiler.basicMoneyGroup[5].allMoneyBoth[1].PosLocation == 'L')
            {
                draw.PaintStar(BasicData.mainUI.tlpTable6, new Point(locX, linePosY[1] + locDiffY), starColor);
            }
            else
            {
                draw.PaintStar(BasicData.mainUI.tlpTable6, new Point(locX, linePosY[1] + locDiffY), BasicData.mainUI.tlpTable6.BackColor);
            }
            //7
            if (DataFiler.basicMoneyGroup[6].allMoneyBoth[0].PosLocation == 'L')
            {
                draw.PaintStar(BasicData.mainUI.tlpTable7, new Point(locX, linePosY[0] + locDiffY), starColor);
            }
            else
            {
                draw.PaintStar(BasicData.mainUI.tlpTable7, new Point(locX, linePosY[0] + locDiffY), BasicData.mainUI.tlpTable7.BackColor);
            }

            #endregion
        }

        /// <summary>
        /// 画圆形标记
        /// </summary>
        public void UpCirMark()
        {
            Color cGreen = Color.Green;
            int x = 240;
            int[] Y = { 20, 46, 72, 98, 124, 150, 176 };
            int radius = 15;

            for (int i = 0; i < tlpTable.Length; i++ )
            {
                for(int j = 0; j < DataFiler.basicMoneyGroup[i].allMoneyBoth.Length;j++)
                {
                    if (IsShowCirMark(DataFiler.basicMoneyGroup[i].allMoneyBoth[j]))
                    {
                        //Setup.isPlayGreenMusic = true;
                        draw.PaintCir(tlpTable[i], cGreen, new Point(x, linePosY[j]), radius);
                    }
                    else
                    {
                        //Setup.isPlayGreenMusic = false;
                        draw.PaintCir(tlpTable[i], tlpTable[i].BackColor, new Point(x, Y[j]), radius);
                    }
                }
            }
        }

        /// <summary>
        /// 是否满足条件，画圆形标记
        /// </summary>
        /// <param name="mBoth">遍历每行的货币对</param>
        /// <returns></returns>
        private bool IsShowCirMark(MoneyBoth mBoth)
        {
            if (mBoth.PriceChange > Setup.gDayNumMore && 
                mBoth.PosNum > Setup.gJinTuiMore &&
                (mBoth.moneyA.Order != Setup.gWithoutOrder1 && mBoth.moneyA.Order != Setup.gWithoutOrder2 && mBoth.MoneyB.Order != Setup.gWithoutOrder1 && mBoth.MoneyB.Order != Setup.gWithoutOrder2 ) &&
                (mBoth.moneyA.Order + mBoth.MoneyB.Order) < Setup.gOrderSumLess &&
                (mBoth.moneyA.ShortM > Setup.gShortBothMore && mBoth.MoneyB.ShortM > Setup.gShortBothMore) &&
                IsContainOrderLess(mBoth)
                )
            {
                if(Setup.gIsColorDiff)
                {
                    if (mBoth.moneyA.MoneyColor != mBoth.MoneyB.MoneyColor)
                        return true;
                    else
                        return false;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsContainOrderLess(MoneyBoth mBoth)
        {
            for(int i = 0; i < Setup.gPChangeOrderNum; i++)
            {
                if(Setup.priceChangeOrder[i].Name == mBoth.Name)
                {
                    return true;
                }
            }
            return false;
        }

        public void ToRixingtu()
        {
            #region Ri Xing Tu
            draw.PaintClear(BasicData.mainUI.pnlRixing);            //清屏
            InitRixingtoCo();

            int[] X = { 51, 151, 251, 351, 451, 551, 651, 754 };
            int Y = 100;
            int Y_Red = 126;    //153

            for (int i = 0; i < X.Length; i++)
            {

                #region 先画影线
                for(int j = 0; j < 4;j++)
                {
                    if (DataFiler.basicMoney[i].YColorX[j] > 0)
                    {
                        draw.PaintRectangle(
                            BasicData.mainUI.pnlRixing,
                            Color.DimGray,
                            new Point(X[i] - DrawGraph.xPercent * DataFiler.basicMoney[i].YColorX[j] / 2,
                                Y - maxMark * DataFiler.basicMoney[i].YLengthY[j]/maxLengthY),
                            DrawGraph.xPercent * DataFiler.basicMoney[i].YColorX[j],
                            maxMark * DataFiler.basicMoney[i].YLengthY[j] / maxLengthY,
                            'a'
                            );
                    }
                    else
                    {
                        draw.PaintRectangle(
                            BasicData.mainUI.pnlRixing,
                            Color.DimGray,
                            new Point(X[i] + DrawGraph.xPercent * DataFiler.basicMoney[i].YColorX[j] / 2,
                                Y_Red),
                            DrawGraph.xPercent * (-DataFiler.basicMoney[i].YColorX[j]),
                            maxMark * DataFiler.basicMoney[i].YLengthY[j] / maxLengthY,
                            'a'
                            );
                    }
                }
                #endregion

                #region 后画主线
                if (DataFiler.basicMoney[i].ColorX > 0)
                {
                    draw.PaintRectangle(
                        BasicData.mainUI.pnlRixing,
                        Color.Green,
                        new Point(X[i] - DrawGraph.xPercent * DataFiler.basicMoney[i].ColorX / 2,
                            Y - maxMark * DataFiler.basicMoney[i].LengthY / maxLengthY),
                        DrawGraph.xPercent * DataFiler.basicMoney[i].ColorX,
                        maxMark * DataFiler.basicMoney[i].LengthY /maxLengthY
                        );
                }
                else
                {
                    draw.PaintRectangle(
                        BasicData.mainUI.pnlRixing,
                        Color.FromArgb(204, 0, 0),
                        new Point(X[i] + DrawGraph.xPercent * DataFiler.basicMoney[i].ColorX / 2,
                            Y_Red),
                        DrawGraph.xPercent * (-DataFiler.basicMoney[i].ColorX),
                        maxMark * DataFiler.basicMoney[i].LengthY / maxLengthY
                        );
                }
                #endregion
            }

            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { "MAX:" + maxLengthY, BasicData.mainUI.lblMax });

            #endregion
        }

        private void InitRixingtoCo()
        {
            int[] x = { 0, 101, 202, 302, 403, 502, 602, 703 , 807};
            int startY = 1;
            int endY = 26;
            Color lineColor = Color.FromArgb(64,64,64);
            for (int i = 0; i < BasicData.mainUI.tlpRixing.ColumnCount + 1; i++)
            {
                draw.PaintLine(BasicData.mainUI.tlpRixing, lineColor, new Point(x[i], startY), new Point(x[i], endY),1);
            }
        }
        public void ToFengxiangtu()
        {
            #region Feng Xiang Tu
            draw.PaintClear(BasicData.mainUI.pnlFengxiang);
            InitFengxiangtuCo();

            int X = 454;
            int X_Unit = 101;
            int[] Y ={1,42,83,124,165,206};
            int Y_Unit = 38;
            int selectX = 4;
            int selectY = 12;
            int chang = 37;
            int kuan = 17;
            Color toColor = Color.FromArgb(204, 0, 0);
            Color nowColor = Color.Gray;
            Color upLineColor = Color.WhiteSmoke;

            int percentL = longMax / Setup.longNumMax;
            double percentS = (double)shortMax / Setup.shortNumMax;

            foreach(Money m in DataFiler.basicMoney)
            {
                switch(m.Order)
                {
                    case 1:
                        BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { m.Name, BasicData.mainUI.lblOne });
                        BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { m.OrderLeft.ToString(), BasicData.mainUI.lblOneT });
                        BasicData.mainUI.lblOne.ForeColor = m.MoneyColor;
                        if (m.Name == BasicData.zoushi_MBoth.moneyA.Name || m.Name == BasicData.zoushi_MBoth.MoneyB.Name)
                        {
                            //BasicData.mainUI.lblOne.BackColor = Color.Navy;
                            //BasicData.mainUI.lblOneT.BackColor = Color.Navy;
                            draw.PaintRectangle(BasicData.mainUI.lblOne, upLineColor, new Point(selectX, selectY), chang, kuan, 1);
                        }
                        else
                        {
                            //BasicData.mainUI.lblOne.BackColor = Color.Black;
                            //BasicData.mainUI.lblOneT.BackColor = Color.Black;
                            draw.PaintRectangle(BasicData.mainUI.lblOne, BasicData.mainUI.lblOne.BackColor, new Point(selectX, selectY), chang, kuan, 1);
                        }
                        //if(m.OrderLeft > Setup.sFxtLeftNum)
                        //{
                        //    BasicData.mainUI.lblOneT.ForeColor = toColor;
                        //}
                        //else
                        //{
                        //    BasicData.mainUI.lblOneT.ForeColor = nowColor;
                        //}
                        //画颜色块
                        if(m.LongM >= 0)
                        {
                            if(m.LongM < Setup.longNumMax)
                                draw.PaintRectangle(BasicData.mainUI.pnlFengxiang, Color.Green, new Point(X, Y[0]), m.LongM * percentL, Y_Unit);
                            else
                                draw.PaintRectangle(BasicData.mainUI.pnlFengxiang, Color.Green, new Point(X, Y[0]), longMax, Y_Unit);
                        }
                        else
                        {
                            if(-m.LongM < Setup.longNumMax)
                                draw.PaintRectangle(BasicData.mainUI.pnlFengxiang, toColor, new Point(X - X_Unit + m.LongM * percentL, Y[0]), -m.LongM * percentL, Y_Unit);
                            else
                                draw.PaintRectangle(BasicData.mainUI.pnlFengxiang, toColor, new Point(longMaxStart, Y[0]), longMax, Y_Unit);
                        }
                        //画括号
                        if(m.ShortM >= 0)
                        {
                            if (m.ShortM < Setup.shortNumMax)
                            {
                                for (int i = 0; i < m.ShortM * percentS; i++)
                                {
                                    draw.PaintArc(BasicData.mainUI.pnlFengxiang, Color.LightGray, new Point(X + 6 * i, Y[0] + 10), 1);
                                }
                            }
                            else
                            {
                                for (int i = 0; i < shortMax; i++)
                                {
                                    draw.PaintArc(BasicData.mainUI.pnlFengxiang, Color.LightGray, new Point(X + 6 * i, Y[0] + 10), 1);
                                }
                            }
                        }
                        else
                        {
                            if (-m.ShortM < Setup.shortNumMax)
                            {
                                for (int i = 0; i < -m.ShortM * percentS; i++)
                                {
                                    draw.PaintArc(BasicData.mainUI.pnlFengxiang, Color.LightGray, new Point(X - X_Unit - 5 - 6 * i, Y[0] + 10), 0);
                                }
                            }
                            else
                            {
                                for (int i = 0; i < shortMax; i++)
                                {
                                    draw.PaintArc(BasicData.mainUI.pnlFengxiang, Color.LightGray, new Point(X - X_Unit - 5 - longMaxStart - 6 * i, Y[0] + 10), 0);
                                }
                            }
                        }
                        break;
                    case 2:
                        BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { m.Name, BasicData.mainUI.lblTwo });
                        BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { m.OrderLeft.ToString(), BasicData.mainUI.lblTwoT });

                        BasicData.mainUI.lblTwo.ForeColor = m.MoneyColor;
                        if (m.Name == BasicData.zoushi_MBoth.moneyA.Name || m.Name == BasicData.zoushi_MBoth.MoneyB.Name)
                        {
                            //BasicData.mainUI.lblTwo.BackColor = Color.Navy;
                            //BasicData.mainUI.lblTwoT.BackColor = Color.Navy;
                            draw.PaintRectangle(BasicData.mainUI.lblTwo, upLineColor, new Point(selectX, selectY), chang, kuan, 1);
                        }
                        else
                        {
                            //BasicData.mainUI.lblTwo.BackColor = Color.Black;
                            //BasicData.mainUI.lblTwoT.BackColor = Color.Black;
                            draw.PaintRectangle(BasicData.mainUI.lblTwo, BasicData.mainUI.lblTwo.BackColor, new Point(selectX, selectY), chang, kuan, 1);
                        }
                        //if (m.OrderLeft > Setup.sFxtLeftNum)
                        //{
                        //    BasicData.mainUI.lblTwoT.ForeColor = toColor;
                        //}
                        //else
                        //{
                        //    BasicData.mainUI.lblTwoT.ForeColor = nowColor;
                        //}
                        if (m.LongM >= 0)
                        {
                            if (m.LongM < Setup.longNumMax)
                                draw.PaintRectangle(BasicData.mainUI.pnlFengxiang, Color.Green, new Point(X, Y[1]), m.LongM * percentL, Y_Unit);
                            else
                                draw.PaintRectangle(BasicData.mainUI.pnlFengxiang, Color.Green, new Point(X, Y[1]), longMax, Y_Unit);
                        }
                        else
                        {
                            if (-m.LongM < Setup.longNumMax)
                                draw.PaintRectangle(BasicData.mainUI.pnlFengxiang, toColor, new Point(X - X_Unit + m.LongM * percentL, Y[1]), -m.LongM * percentL, Y_Unit);
                            else
                                draw.PaintRectangle(BasicData.mainUI.pnlFengxiang, toColor, new Point(longMaxStart, Y[1]), longMax, Y_Unit);
                        }
                        if(m.ShortM >= 0)
                        {
                            if (m.ShortM < Setup.shortNumMax)
                            {
                                for (int i = 0; i < m.ShortM * percentS; i++)
                                {
                                    draw.PaintArc(BasicData.mainUI.pnlFengxiang, Color.LightGray, new Point(X + 6 * i, Y[1] + 10), 1);    //X - 6 + 6 * i, Y[0] + 10
                                }
                            }
                            else
                            {
                                for (int i = 0; i < shortMax; i++)
                                {
                                    draw.PaintArc(BasicData.mainUI.pnlFengxiang, Color.LightGray, new Point(X + 6 * i, Y[1] + 10), 1);
                                }
                            }
                        }
                        else
                        {
                            if (-m.ShortM < Setup.shortNumMax)
                            {
                                for (int i = 0; i < -m.ShortM * percentS; i++)
                                {
                                    draw.PaintArc(BasicData.mainUI.pnlFengxiang, Color.LightGray, new Point(X - X_Unit - 5 - 6 * i, Y[1] + 10), 0);
                                }
                            }
                            else
                            {
                                for (int i = 0; i < shortMax; i++)
                                {
                                    draw.PaintArc(BasicData.mainUI.pnlFengxiang, Color.LightGray, new Point(X - X_Unit - 5 - longMaxStart - 6 * i, Y[1] + 10), 0);
                                }
                            }
                        }
                        break;
                    case 3:
                        BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { m.Name, BasicData.mainUI.lblThree });
                        BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { m.OrderLeft.ToString(), BasicData.mainUI.lblThreeT });
                        
                        BasicData.mainUI.lblThree.ForeColor = m.MoneyColor;
                        if (m.Name == BasicData.zoushi_MBoth.moneyA.Name || m.Name == BasicData.zoushi_MBoth.MoneyB.Name)
                        {
                            //BasicData.mainUI.lblThree.BackColor = Color.Navy;
                            //BasicData.mainUI.lblThreeT.BackColor = Color.Navy;
                            draw.PaintRectangle(BasicData.mainUI.lblThree, upLineColor, new Point(selectX, selectY), chang, kuan, 1);
                        }
                        else
                        {
                            //BasicData.mainUI.lblThree.BackColor = Color.Black;
                            //BasicData.mainUI.lblThreeT.BackColor = Color.Black;
                            draw.PaintRectangle(BasicData.mainUI.lblThree, BasicData.mainUI.lblThree.BackColor, new Point(selectX, selectY), chang, kuan, 1);
                        }
                        //if (m.OrderLeft > Setup.sFxtLeftNum)
                        //{
                        //    BasicData.mainUI.lblThreeT.ForeColor = toColor;
                        //}
                        //else
                        //{
                        //    BasicData.mainUI.lblThreeT.ForeColor = nowColor;
                        //}
                        if (m.LongM >= 0)
                        {
                            if (m.LongM < Setup.longNumMax)
                                draw.PaintRectangle(BasicData.mainUI.pnlFengxiang, Color.Green, new Point(X, Y[2]), m.LongM * percentL, Y_Unit);
                            else
                                draw.PaintRectangle(BasicData.mainUI.pnlFengxiang, Color.Green, new Point(X, Y[2]), longMax, Y_Unit);
                        }
                        else
                        {
                            if (-m.LongM < Setup.longNumMax)
                                draw.PaintRectangle(BasicData.mainUI.pnlFengxiang, toColor, new Point(X - X_Unit + m.LongM * percentL, Y[2]), -m.LongM * percentL, Y_Unit);
                            else
                                draw.PaintRectangle(BasicData.mainUI.pnlFengxiang, toColor, new Point(longMaxStart, Y[2]), longMax, Y_Unit);
                        }
                        if(m.ShortM >= 0)
                        {
                            if (m.ShortM < Setup.shortNumMax)
                            {
                                for (int i = 0; i < m.ShortM * percentS; i++)
                                {
                                    draw.PaintArc(BasicData.mainUI.pnlFengxiang, Color.LightGray, new Point(X + 6 * i, Y[2] + 10), 1);
                                }
                            }
                            else
                            {
                                for (int i = 0; i < shortMax; i++)
                                {
                                    draw.PaintArc(BasicData.mainUI.pnlFengxiang, Color.LightGray, new Point(X + 6 * i, Y[2] + 10), 1);
                                }
                            }
                        }
                        else
                        {
                            if (-m.ShortM < Setup.shortNumMax)
                            {
                                for (int i = 0; i < -m.ShortM * percentS; i++)
                                {
                                    draw.PaintArc(BasicData.mainUI.pnlFengxiang, Color.LightGray, new Point(X - X_Unit - 5 - 6 * i, Y[2] + 10), 0);
                                }
                            }
                            else
                            {
                                for (int i = 0; i < shortMax; i++)
                                {
                                    draw.PaintArc(BasicData.mainUI.pnlFengxiang, Color.LightGray, new Point(X - X_Unit - 5 - longMaxStart - 6 * i, Y[2] + 10), 0);
                                }
                            }
                        }
                        break;
                    case 4:
                        BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { m.Name, BasicData.mainUI.lblFour });
                        BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { m.OrderLeft.ToString(), BasicData.mainUI.lblFourT });
                        
                        BasicData.mainUI.lblFour.ForeColor = m.MoneyColor;
                        if (m.Name == BasicData.zoushi_MBoth.moneyA.Name || m.Name == BasicData.zoushi_MBoth.MoneyB.Name)
                        {
                            //BasicData.mainUI.lblFour.BackColor = Color.Navy;
                            //BasicData.mainUI.lblFourT.BackColor = Color.Navy;
                            draw.PaintRectangle(BasicData.mainUI.lblFour, upLineColor, new Point(selectX, selectY), chang, kuan, 1);
                        }
                        else
                        {
                            //BasicData.mainUI.lblFour.BackColor = Color.Black;
                            //BasicData.mainUI.lblFourT.BackColor = Color.Black;
                            draw.PaintRectangle(BasicData.mainUI.lblFour, BasicData.mainUI.lblFour.BackColor, new Point(selectX, selectY), chang, kuan, 1);
                        }
                        //if (m.OrderLeft > Setup.sFxtLeftNum)
                        //{
                        //    BasicData.mainUI.lblFourT.ForeColor = toColor;
                        //}
                        //else
                        //{
                        //    BasicData.mainUI.lblFourT.ForeColor = nowColor;
                        //}
                        if (m.LongM >= 0)
                        {
                            if (m.LongM < Setup.longNumMax)
                                draw.PaintRectangle(BasicData.mainUI.pnlFengxiang, Color.Green, new Point(X, Y[3]), m.LongM * percentL, Y_Unit);
                            else
                                draw.PaintRectangle(BasicData.mainUI.pnlFengxiang, Color.Green, new Point(X, Y[3]), longMax, Y_Unit);
                        }
                        else
                        {
                            if (-m.LongM < Setup.longNumMax)
                                draw.PaintRectangle(BasicData.mainUI.pnlFengxiang, toColor, new Point(X - X_Unit + m.LongM * percentL, Y[3]), -m.LongM * percentL, Y_Unit);
                            else
                                draw.PaintRectangle(BasicData.mainUI.pnlFengxiang, toColor, new Point(longMaxStart, Y[3]), longMax, Y_Unit);
                        }
                        if(m.ShortM >= 0)
                        {
                            if (m.ShortM < Setup.shortNumMax)
                            {
                                for (int i = 0; i < m.ShortM * percentS; i++)
                                {
                                    draw.PaintArc(BasicData.mainUI.pnlFengxiang, Color.LightGray, new Point(X + 6 * i, Y[3] + 10), 1);
                                }
                            }
                            else
                            {
                                for (int i = 0; i < shortMax; i++)
                                {
                                    draw.PaintArc(BasicData.mainUI.pnlFengxiang, Color.LightGray, new Point(X + 6 * i, Y[3] + 10), 1);
                                }
                            }
                        }
                        else
                        {
                            if (-m.ShortM < Setup.shortNumMax)
                            {
                                for (int i = 0; i < -m.ShortM * percentS; i++)
                                {
                                    draw.PaintArc(BasicData.mainUI.pnlFengxiang, Color.LightGray, new Point(X - X_Unit - 5 - 6 * i, Y[3] + 10), 0);
                                }
                            }
                            else
                            {
                                for (int i = 0; i < shortMax; i++)
                                {
                                    draw.PaintArc(BasicData.mainUI.pnlFengxiang, Color.LightGray, new Point(X - X_Unit - 5 - longMaxStart - 6 * i, Y[3] + 10), 0);
                                }
                            }
                        }
                        break;
                    case 5:
                        BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { m.Name, BasicData.mainUI.lblFive });
                        BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { m.OrderLeft.ToString(), BasicData.mainUI.lblFiveT });

                        BasicData.mainUI.lblFive.ForeColor = m.MoneyColor;
                        if (m.Name == BasicData.zoushi_MBoth.moneyA.Name || m.Name == BasicData.zoushi_MBoth.MoneyB.Name)
                        {
                            //BasicData.mainUI.lblFive.BackColor = Color.Navy;
                            //BasicData.mainUI.lblFiveT.BackColor = Color.Navy;
                            draw.PaintRectangle(BasicData.mainUI.lblFive, upLineColor, new Point(selectX, selectY), chang, kuan, 1);
                        }
                        else
                        {
                            //BasicData.mainUI.lblFive.BackColor = Color.Black;
                            //BasicData.mainUI.lblFiveT.BackColor = Color.Black;
                            draw.PaintRectangle(BasicData.mainUI.lblFive, BasicData.mainUI.lblFive.BackColor, new Point(selectX, selectY), chang, kuan, 1);
                        }
                        //if (m.OrderLeft > Setup.sFxtLeftNum)
                        //{
                        //    BasicData.mainUI.lblFiveT.ForeColor = toColor;
                        //}
                        //else
                        //{
                        //    BasicData.mainUI.lblFiveT.ForeColor = nowColor;
                        //}
                        if (m.LongM >= 0)
                        {
                            if (m.LongM < Setup.longNumMax)
                                draw.PaintRectangle(BasicData.mainUI.pnlFengxiang, Color.Green, new Point(X, Y[4]), m.LongM * percentL, Y_Unit);
                            else
                                draw.PaintRectangle(BasicData.mainUI.pnlFengxiang, Color.Green, new Point(X, Y[4]), longMax, Y_Unit);
                        }
                        else
                        {
                            if (-m.LongM < Setup.longNumMax)
                                draw.PaintRectangle(BasicData.mainUI.pnlFengxiang, toColor, new Point(X - X_Unit + m.LongM * percentL, Y[4]), -m.LongM * percentL, Y_Unit);
                            else
                                draw.PaintRectangle(BasicData.mainUI.pnlFengxiang, toColor, new Point(longMaxStart, Y[4]), longMax, Y_Unit);
                        }
                        if(m.ShortM >= 0)
                        {
                            if (m.ShortM < Setup.shortNumMax)
                            {
                                for (int i = 0; i < m.ShortM * percentS; i++)
                                {
                                    draw.PaintArc(BasicData.mainUI.pnlFengxiang, Color.LightGray, new Point(X + 6 * i, Y[4] + 10), 1);
                                }
                            }
                            else
                            {
                                for (int i = 0; i < shortMax; i++)
                                {
                                    draw.PaintArc(BasicData.mainUI.pnlFengxiang, Color.LightGray, new Point(X + 6 * i, Y[4] + 10), 1);
                                }
                            }
                        }
                        else
                        {
                            if (-m.ShortM < Setup.shortNumMax)
                            {
                                for (int i = 0; i < -m.ShortM * percentS; i++)
                                {
                                    draw.PaintArc(BasicData.mainUI.pnlFengxiang, Color.LightGray, new Point(X - X_Unit - 5 - 6 * i, Y[4] + 10), 0);
                                }
                            }
                            else
                            {
                                for (int i = 0; i < shortMax; i++)
                                {
                                    draw.PaintArc(BasicData.mainUI.pnlFengxiang, Color.LightGray, new Point(X - X_Unit - 5 - longMaxStart - 6 * i, Y[4] + 10), 0);
                                }
                            }
                        }
                        break;
                    case 6:
                        BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { m.Name, BasicData.mainUI.lblSix });
                        BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { m.OrderLeft.ToString(), BasicData.mainUI.lblSixT });
                        
                        BasicData.mainUI.lblSix.ForeColor = m.MoneyColor;
                        if (m.Name == BasicData.zoushi_MBoth.moneyA.Name || m.Name == BasicData.zoushi_MBoth.MoneyB.Name)
                        {
                            //BasicData.mainUI.lblSix.BackColor = Color.Navy;
                            //BasicData.mainUI.lblSixT.BackColor = Color.Navy;
                            draw.PaintRectangle(BasicData.mainUI.lblSix, upLineColor, new Point(selectX, selectY), chang, kuan, 1);
                        }
                        else
                        {
                            //BasicData.mainUI.lblSix.BackColor = Color.Black;
                            //BasicData.mainUI.lblSixT.BackColor = Color.Black;
                            draw.PaintRectangle(BasicData.mainUI.lblSix, BasicData.mainUI.lblSix.BackColor, new Point(selectX, selectY), chang, kuan, 1);
                        }
                        //if (m.OrderLeft > Setup.sFxtLeftNum)
                        //{
                        //    BasicData.mainUI.lblSixT.ForeColor = toColor;
                        //}
                        //else
                        //{
                        //    BasicData.mainUI.lblSixT.ForeColor = nowColor;
                        //}
                        if (m.LongM >= 0)
                        {
                            if (m.LongM < Setup.longNumMax)
                                draw.PaintRectangle(BasicData.mainUI.pnlFengxiang, Color.Green, new Point(X, Y[5]), m.LongM * percentL, Y_Unit);
                            else
                                draw.PaintRectangle(BasicData.mainUI.pnlFengxiang, Color.Green, new Point(X, Y[5]), longMax, Y_Unit);
                        }
                        else
                        {
                            if (-m.LongM < Setup.longNumMax)
                                draw.PaintRectangle(BasicData.mainUI.pnlFengxiang, toColor, new Point(X - X_Unit + m.LongM * percentL, Y[5]), -m.LongM * percentL, Y_Unit);
                            else
                                draw.PaintRectangle(BasicData.mainUI.pnlFengxiang, toColor, new Point(longMaxStart, Y[5]), longMax, Y_Unit);
                        }
                        if(m.ShortM >= 0)
                        {
                            if (m.ShortM < Setup.shortNumMax)
                            {
                                for (int i = 0; i < m.ShortM * percentS; i++)
                                {
                                    draw.PaintArc(BasicData.mainUI.pnlFengxiang, Color.LightGray, new Point(X + 6 * i, Y[5] + 10), 1);
                                }
                            }
                            else
                            {
                                for (int i = 0; i < shortMax; i++)
                                {
                                    draw.PaintArc(BasicData.mainUI.pnlFengxiang, Color.LightGray, new Point(X + 6 * i, Y[5] + 10), 1);
                                }
                            }
                        }
                        else
                        {
                            if (-m.ShortM < Setup.shortNumMax)
                            {
                                for (int i = 0; i < -m.ShortM * percentS; i++)
                                {
                                    draw.PaintArc(BasicData.mainUI.pnlFengxiang, Color.LightGray, new Point(X - X_Unit - 5 - 6 * i, Y[5] + 10), 0);
                                }
                            }
                            else
                            {
                                for (int i = 0; i < shortMax; i++)
                                {
                                    draw.PaintArc(BasicData.mainUI.pnlFengxiang, Color.LightGray, new Point(X - X_Unit - 5 - longMaxStart - 6 * i, Y[5] + 10), 0);
                                }
                            }
                        }
                        break;
                }
            }
            #endregion
        }

        private void InitFengxiangtuCo()
        {
            int startX = 1;
            int endX = 99;
            int[] y = { 0, 41, 82, 123, 164, 205, 249};
            Color lineColor = Color.FromArgb(60, 60, 60);

            for (int i = 1; i < BasicData.mainUI.tlpFengxiang.RowCount; i++)
            {
                draw.PaintLine(
                    BasicData.mainUI.tlpFengxiang,
                    lineColor,
                    new Point(startX, y[i]),
                    new Point(endX, y[i])
                    );
            }

            int[] x = { 88, 176, 264, 543, 631, 719 };
            int startY = 1;
            int endY = 250;
            lineColor = Color.FromArgb(39, 39, 39);
            for(int i = 0; i < x.Length; i++)
            {
                draw.PaintLine(
                    BasicData.mainUI.pnlFengxiang,
                    lineColor,
                    new Point(x[i], startY),
                    new Point(x[i], endY)
                    );
            }
        }
        public void ToZoushitu()
        {
            #region Zou Shi Tu
            ////右上角提示灯位置及半径参数
            //int lightX = 795;
            //int lightY = 25;
            //int lightRadius = 21;

            InitCo();

            if (BasicData.indexNum != 0)
            {
                //走势图 左图
                for (int i = 0; i < BasicData.zoushi_MBoth.moneyA.fengzhong.Length; i++)
                {
                    draw.PaintRectangle(
                        BasicData.mainUI.pnlZoushi,
                        BasicData.zoushi_MBoth.moneyA.MoneyColor,
                        new Point(zoushituX_Unit - 1 + i * (zoushituX_Unit + 2), 230 - 2 * BasicData.zoushi_MBoth.moneyA.fengzhong[i]),
                        zoushituX_Unit,
                        2 * BasicData.zoushi_MBoth.moneyA.fengzhong[i]);
                }
                //走势图 右图
                for (int i = 0; i < BasicData.zoushi_MBoth.MoneyB.fengzhong.Length; i++)
                {
                    draw.PaintRectangle(
                        BasicData.mainUI.pnlZoushi,
                        BasicData.zoushi_MBoth.MoneyB.MoneyColor,
                        new Point(zoushituX_Unit - 1 + i * (zoushituX_Unit + 2) + 410, 230 - 2 * BasicData.zoushi_MBoth.MoneyB.fengzhong[i]),
                        zoushituX_Unit,
                        2 * BasicData.zoushi_MBoth.MoneyB.fengzhong[i]);
                }

                //上方货币名称 + 排序号
                BasicData.mainUI.Invoke(
                    BasicData.mainUI.ShowText,
                    new object[] { BasicData.zoushi_MBoth.moneyA.Name, BasicData.mainUI.lblMoneyL }
                    );
                BasicData.mainUI.Invoke(
                    BasicData.mainUI.ShowText,
                    new object[] { BasicData.zoushi_MBoth.MoneyB.Name, BasicData.mainUI.lblMoneyR }
                    );
                BasicData.mainUI.Invoke(
                    BasicData.mainUI.ShowText,
                    new object[] { BasicData.zoushi_MBoth.moneyA.Order.ToString(), BasicData.mainUI.lblOrderL }
                    );
                BasicData.mainUI.Invoke(
                    BasicData.mainUI.ShowText,
                    new object[] { BasicData.zoushi_MBoth.MoneyB.Order.ToString(), BasicData.mainUI.lblOrderR }
                    );
                //货币名称颜色
                BasicData.mainUI.lblMoneyL.ForeColor = BasicData.zoushi_MBoth.moneyA.MoneyColor;
                BasicData.mainUI.lblMoneyR.ForeColor = BasicData.zoushi_MBoth.MoneyB.MoneyColor;

                //右上角数字
                BasicData.mainUI.Invoke(
                    BasicData.mainUI.ShowText,
                    new object[] { DataShow.doubleSuperNum.ToString(), BasicData.mainUI.lblDSuperNum }
                    );
            }
            else
            {
                BasicData.zoushi_MBoth = DataFiler.basicMoneyGroup[0].allMoneyBoth[0];
            }
            #endregion
        }

        public void ToZSTDownTime()
        {
            try
            {
                int midHour = (int.Parse(DataShow.zsDownTimeMid.Substring(0,2)) + Setup.timeHourDiff ) % 24;
                string midMin = DataShow.zsDownTimeMid.Substring(3, 2);
                string midTime = midHour + ":" + midMin;

                midHour = (int.Parse(DataShow.zsDownTimeLast.Substring(0, 2)) + Setup.timeHourDiff) % 24;
                midMin = DataShow.zsDownTimeLast.Substring(3, 2);
                string lastTime = midHour + ":" + midMin;

                //坐标轴下方 时间 * 4
                BasicData.mainUI.Invoke(
                    BasicData.mainUI.ShowText,
                    new object[] { midTime, BasicData.mainUI.lblZSTimeA1 }
                    );
                BasicData.mainUI.Invoke(
                    BasicData.mainUI.ShowText,
                    new object[] { lastTime, BasicData.mainUI.lblZSTimeA2 }
                    );
                BasicData.mainUI.Invoke(
                    BasicData.mainUI.ShowText,
                    new object[] { midTime, BasicData.mainUI.lblZSTimeB1 }
                    );
                BasicData.mainUI.Invoke(
                    BasicData.mainUI.ShowText,
                    new object[] { lastTime, BasicData.mainUI.lblZSTimeB2 }
                    );
            }
            catch(Exception ex)
            {
                MessageBox.Show("时间转换失败！");
            }
        }

        /// <summary>
        /// 信号灯颜色刷新
        /// </summary>
        public void ToSignLight()
        {
            int lightX = 69;
            int lightY = 54;
            int lightRadius = 45;

            #region 【已注释】原信号灯触发机制
            //public static int[] sZSRed = { 0, 1, 2, 3, 4, 5, 6, >6, 提示 };
            //foreach (int i in Setup.sZSRed)       //draw.PaintCir(BasicData.mainUI.pnlZoushi, Color.Green, new Point(675, 40), 23);
            //{
            //    if ((DataShow.doubleSuperNum == i && DataShow.doubleSuperNum < 7) || (DataShow.doubleSuperNum >= 7 && i == 7))
            //    {
            //        draw.PaintCir(BasicData.mainUI.panel2, Color.Red, new Point(lightX, lightY), lightRadius);
            //        if (Setup.sZSRed[8] == 8 && Setup.isLocHaveNum != 0)
            //            Setup.isStartRedMusic = true;
            //        return;
            //    }
            //}
            //foreach (int i in Setup.sZSGreen)       //draw.PaintCir(BasicData.mainUI.pnlZoushi, Color.Green, new Point(675, 40), 23);
            //{
            //    if ((DataShow.doubleSuperNum == i && DataShow.doubleSuperNum < 7) || (DataShow.doubleSuperNum >= 7 && i == 7))
            //    {
            //        draw.PaintCir(BasicData.mainUI.panel2, Color.Green, new Point(lightX, lightY), lightRadius);

            //        if (Setup.sZSGreen[8] == 8)
            //            Setup.isStartGreenMusic = true;
            //        return;
            //    }
            //}
            //foreach (int i in Setup.sZSYellow)       //draw.PaintCir(BasicData.mainUI.pnlZoushi, Color.Green, new Point(675, 40), 23);
            //{
            //    if ((DataShow.doubleSuperNum == i && DataShow.doubleSuperNum < 7) || (DataShow.doubleSuperNum >= 7 && i == 7))
            //    {
            //        draw.PaintCir(BasicData.mainUI.panel2, Color.Yellow, new Point(lightX, lightY), lightRadius);

            //        if (Setup.sZSYellow[8] == 8)
            //            Setup.isStartGreenMusic = true;
            //        return;
            //    }
            //}
            #endregion

            Setup.lastLightColor = Setup.nowLightColor;

            //黄灯
            foreach(int i in Setup.sZSYellow)
            {
                if(Money.ShortTrueNum == i)
                {
                    draw.PaintCir(BasicData.mainUI.panel2, Color.Yellow, new Point(lightX, lightY), lightRadius,1);
                    Setup.nowLightColor = 1;
                    return;
                }

            }
            
            //绿灯
            foreach(int i in Setup.sZSGreen)
            {
                if(Money.ShortTrueNum == i)
                {
                    draw.PaintCir(BasicData.mainUI.panel2, Color.LimeGreen, new Point(lightX, lightY), lightRadius,1);
                    Setup.nowLightColor = 3;
                    if (Setup.sZIsUpGreen && Setup.nowLightColor != Setup.lastLightColor)
                        Setup.isStartGreenMusic = true;
                    return;
                }
            }
            
            //红灯
            foreach(int i in Setup.sZSRed)
            {
                if(Money.ShortTrueNum == i)
                {
                    draw.PaintCir(BasicData.mainUI.panel2, Color.Red, new Point(lightX, lightY), lightRadius,1);
                    Setup.nowLightColor = 2;
                    if (Setup.sZIsUpRed && Setup.nowLightColor != Setup.lastLightColor)
                        Setup.isStartRedMusic = true;
                    return;
                }
            }
            Setup.isStartRedMusic = false;
        }

        /// <summary>
        /// 初始化坐标轴
        /// </summary>
        public void InitCo()
        {
            int leftStartX = 1;
            int[] leftY = { 30, 70, 110, 150, 190, 230 };   //左右均可用
            int leftEndX = 391;
            int shortX = 33;

            int rightStartX = 410;
            int rightEndX = 800;
            Color lineColor = Color.FromArgb(40,40,40);        //线颜色

            draw.PaintClear(BasicData.mainUI.pnlZoushi);

            #region 初始化坐标格
            draw.PaintLine(BasicData.mainUI.pnlZoushi, lineColor, new Point(leftStartX, leftY[0]), new Point(leftEndX, leftY[0]));   //zuo6
            draw.PaintLine(BasicData.mainUI.pnlZoushi, lineColor, new Point(rightStartX, leftY[0]), new Point(rightEndX, leftY[0]));   //you6

            draw.PaintLine(BasicData.mainUI.pnlZoushi, lineColor, new Point(leftStartX, leftY[1]), new Point(leftEndX - shortX, leftY[1]));   //zuo5
            draw.PaintLine(BasicData.mainUI.pnlZoushi, lineColor, new Point(rightStartX, leftY[1]), new Point(rightEndX - shortX, leftY[1]));   //you5

            draw.PaintLine(BasicData.mainUI.pnlZoushi, lineColor, new Point(leftStartX, leftY[2]), new Point(leftEndX - shortX, leftY[2]));   //zuo4
            draw.PaintLine(BasicData.mainUI.pnlZoushi, lineColor, new Point(rightStartX, leftY[2]), new Point(rightEndX - shortX, leftY[2]));   //you4

            draw.PaintLine(BasicData.mainUI.pnlZoushi, lineColor, new Point(leftStartX, leftY[3]), new Point(leftEndX - shortX, leftY[3]));   //zuo3
            draw.PaintLine(BasicData.mainUI.pnlZoushi, lineColor, new Point(rightStartX, leftY[3]), new Point(rightEndX - shortX, leftY[3]));   //you3

            draw.PaintLine(BasicData.mainUI.pnlZoushi, lineColor, new Point(leftStartX, leftY[4]), new Point(leftEndX - shortX, leftY[4]));   //zuo2
            draw.PaintLine(BasicData.mainUI.pnlZoushi, lineColor, new Point(rightStartX, leftY[4]), new Point(rightEndX - shortX, leftY[4]));   //you2

            draw.PaintLine(BasicData.mainUI.pnlZoushi, lineColor, new Point(leftStartX, leftY[5]), new Point(leftEndX, leftY[5]));   //zuo1
            draw.PaintLine(BasicData.mainUI.pnlZoushi, lineColor, new Point(rightStartX, leftY[5]), new Point(rightEndX, leftY[5]));   //you1
            #endregion
        }
        public void ToMoneyBoth()
        {
            #region MoneyBoth

            int line = 0;
            foreach(MoneyGroup m in DataFiler.basicMoneyGroup)
            {
                for (int i = 0; i < m.allMoneyBoth.Length; i++)
                {
                    BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { m.allMoneyBoth[i].moneyA.Name, lblMoneyNameA[line] });
                    BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { m.allMoneyBoth[i].MoneyB.Name, lblMoneyNameB[line] });
                    lblMoneyNameA[line].ForeColor = m.allMoneyBoth[i].moneyA.MoneyColor;
                    lblMoneyNameB[line].ForeColor = m.allMoneyBoth[i].MoneyB.MoneyColor;

                    line++;
                }
            }

            #endregion
        }
        public void ToDiancha()
        {
            #region diancha
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[0].allMoneyBoth[0].Diancha.ToString(), BasicData.mainUI.dcName1 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[0].allMoneyBoth[1].Diancha.ToString(), BasicData.mainUI.dcName2 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[0].allMoneyBoth[2].Diancha.ToString(), BasicData.mainUI.dcName3 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[0].allMoneyBoth[3].Diancha.ToString(), BasicData.mainUI.dcName4 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[0].allMoneyBoth[4].Diancha.ToString(), BasicData.mainUI.dcName5 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[0].allMoneyBoth[5].Diancha.ToString(), BasicData.mainUI.dcName6 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[0].allMoneyBoth[6].Diancha.ToString(), BasicData.mainUI.dcName7 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[1].allMoneyBoth[0].Diancha.ToString(), BasicData.mainUI.dcName8 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[1].allMoneyBoth[1].Diancha.ToString(), BasicData.mainUI.dcName9 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[1].allMoneyBoth[2].Diancha.ToString(), BasicData.mainUI.dcName10 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[1].allMoneyBoth[3].Diancha.ToString(), BasicData.mainUI.dcName11 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[1].allMoneyBoth[4].Diancha.ToString(), BasicData.mainUI.dcName12 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[1].allMoneyBoth[5].Diancha.ToString(), BasicData.mainUI.dcName13 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[2].allMoneyBoth[0].Diancha.ToString(), BasicData.mainUI.dcName14 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[2].allMoneyBoth[1].Diancha.ToString(), BasicData.mainUI.dcName15 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[2].allMoneyBoth[2].Diancha.ToString(), BasicData.mainUI.dcName16 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[2].allMoneyBoth[3].Diancha.ToString(), BasicData.mainUI.dcName17 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[2].allMoneyBoth[4].Diancha.ToString(), BasicData.mainUI.dcName18 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[3].allMoneyBoth[0].Diancha.ToString(), BasicData.mainUI.dcName19 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[3].allMoneyBoth[1].Diancha.ToString(), BasicData.mainUI.dcName20 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[3].allMoneyBoth[2].Diancha.ToString(), BasicData.mainUI.dcName21 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[3].allMoneyBoth[3].Diancha.ToString(), BasicData.mainUI.dcName22 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[4].allMoneyBoth[0].Diancha.ToString(), BasicData.mainUI.dcName23 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[4].allMoneyBoth[1].Diancha.ToString(), BasicData.mainUI.dcName24 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[4].allMoneyBoth[2].Diancha.ToString(), BasicData.mainUI.dcName25 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[5].allMoneyBoth[0].Diancha.ToString(), BasicData.mainUI.dcName26 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[5].allMoneyBoth[1].Diancha.ToString(), BasicData.mainUI.dcName27 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[6].allMoneyBoth[0].Diancha.ToString(), BasicData.mainUI.dcName28 });
            #endregion
        }
        public void ToShort()
        {
            #region shortLook
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { Math.Abs(DataFiler.basicMoneyGroup[0].allMoneyBoth[0].moneyA.ShortM).ToString(), BasicData.mainUI.shortA1 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { Math.Abs(DataFiler.basicMoneyGroup[0].allMoneyBoth[0].MoneyB.ShortM).ToString(), BasicData.mainUI.shortB1 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { Math.Abs(DataFiler.basicMoneyGroup[0].allMoneyBoth[1].moneyA.ShortM).ToString(), BasicData.mainUI.shortA2 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { Math.Abs(DataFiler.basicMoneyGroup[0].allMoneyBoth[1].MoneyB.ShortM).ToString(), BasicData.mainUI.shortB2 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { Math.Abs(DataFiler.basicMoneyGroup[0].allMoneyBoth[2].moneyA.ShortM).ToString(), BasicData.mainUI.shortA3 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { Math.Abs(DataFiler.basicMoneyGroup[0].allMoneyBoth[2].MoneyB.ShortM).ToString(), BasicData.mainUI.shortB3 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { Math.Abs(DataFiler.basicMoneyGroup[0].allMoneyBoth[3].moneyA.ShortM).ToString(), BasicData.mainUI.shortA4 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { Math.Abs(DataFiler.basicMoneyGroup[0].allMoneyBoth[3].MoneyB.ShortM).ToString(), BasicData.mainUI.shortB4 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { Math.Abs(DataFiler.basicMoneyGroup[0].allMoneyBoth[4].moneyA.ShortM).ToString(), BasicData.mainUI.shortA5 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { Math.Abs(DataFiler.basicMoneyGroup[0].allMoneyBoth[4].MoneyB.ShortM).ToString(), BasicData.mainUI.shortB5 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { Math.Abs(DataFiler.basicMoneyGroup[0].allMoneyBoth[5].moneyA.ShortM).ToString(), BasicData.mainUI.shortA6 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { Math.Abs(DataFiler.basicMoneyGroup[0].allMoneyBoth[5].MoneyB.ShortM).ToString(), BasicData.mainUI.shortB6 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { Math.Abs(DataFiler.basicMoneyGroup[0].allMoneyBoth[6].moneyA.ShortM).ToString(), BasicData.mainUI.shortA7 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { Math.Abs(DataFiler.basicMoneyGroup[0].allMoneyBoth[6].MoneyB.ShortM).ToString(), BasicData.mainUI.shortB7 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { Math.Abs(DataFiler.basicMoneyGroup[1].allMoneyBoth[0].moneyA.ShortM).ToString(), BasicData.mainUI.shortA8 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { Math.Abs(DataFiler.basicMoneyGroup[1].allMoneyBoth[0].MoneyB.ShortM).ToString(), BasicData.mainUI.shortB8 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { Math.Abs(DataFiler.basicMoneyGroup[1].allMoneyBoth[1].moneyA.ShortM).ToString(), BasicData.mainUI.shortA9 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { Math.Abs(DataFiler.basicMoneyGroup[1].allMoneyBoth[1].MoneyB.ShortM).ToString(), BasicData.mainUI.shortB9 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { Math.Abs(DataFiler.basicMoneyGroup[1].allMoneyBoth[2].moneyA.ShortM).ToString(), BasicData.mainUI.shortA10 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { Math.Abs(DataFiler.basicMoneyGroup[1].allMoneyBoth[2].MoneyB.ShortM).ToString(), BasicData.mainUI.shortB10 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { Math.Abs(DataFiler.basicMoneyGroup[1].allMoneyBoth[3].moneyA.ShortM).ToString(), BasicData.mainUI.shortA11 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { Math.Abs(DataFiler.basicMoneyGroup[1].allMoneyBoth[3].MoneyB.ShortM).ToString(), BasicData.mainUI.shortB11 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { Math.Abs(DataFiler.basicMoneyGroup[1].allMoneyBoth[4].moneyA.ShortM).ToString(), BasicData.mainUI.shortA12 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { Math.Abs(DataFiler.basicMoneyGroup[1].allMoneyBoth[4].MoneyB.ShortM).ToString(), BasicData.mainUI.shortB12 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { Math.Abs(DataFiler.basicMoneyGroup[1].allMoneyBoth[5].moneyA.ShortM).ToString(), BasicData.mainUI.shortA13 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { Math.Abs(DataFiler.basicMoneyGroup[1].allMoneyBoth[5].MoneyB.ShortM).ToString(), BasicData.mainUI.shortB13 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { Math.Abs(DataFiler.basicMoneyGroup[2].allMoneyBoth[0].moneyA.ShortM).ToString(), BasicData.mainUI.shortA14 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { Math.Abs(DataFiler.basicMoneyGroup[2].allMoneyBoth[0].MoneyB.ShortM).ToString(), BasicData.mainUI.shortB14 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { Math.Abs(DataFiler.basicMoneyGroup[2].allMoneyBoth[1].moneyA.ShortM).ToString(), BasicData.mainUI.shortA15 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { Math.Abs(DataFiler.basicMoneyGroup[2].allMoneyBoth[1].MoneyB.ShortM).ToString(), BasicData.mainUI.shortB15 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { Math.Abs(DataFiler.basicMoneyGroup[2].allMoneyBoth[2].moneyA.ShortM).ToString(), BasicData.mainUI.shortA16 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { Math.Abs(DataFiler.basicMoneyGroup[2].allMoneyBoth[2].MoneyB.ShortM).ToString(), BasicData.mainUI.shortB16 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { Math.Abs(DataFiler.basicMoneyGroup[2].allMoneyBoth[3].moneyA.ShortM).ToString(), BasicData.mainUI.shortA17 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { Math.Abs(DataFiler.basicMoneyGroup[2].allMoneyBoth[3].MoneyB.ShortM).ToString(), BasicData.mainUI.shortB17 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { Math.Abs(DataFiler.basicMoneyGroup[2].allMoneyBoth[4].moneyA.ShortM).ToString(), BasicData.mainUI.shortA18 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { Math.Abs(DataFiler.basicMoneyGroup[2].allMoneyBoth[4].MoneyB.ShortM).ToString(), BasicData.mainUI.shortB18 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { Math.Abs(DataFiler.basicMoneyGroup[3].allMoneyBoth[0].moneyA.ShortM).ToString(), BasicData.mainUI.shortA19 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { Math.Abs(DataFiler.basicMoneyGroup[3].allMoneyBoth[0].MoneyB.ShortM).ToString(), BasicData.mainUI.shortB19 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { Math.Abs(DataFiler.basicMoneyGroup[3].allMoneyBoth[1].moneyA.ShortM).ToString(), BasicData.mainUI.shortA20 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { Math.Abs(DataFiler.basicMoneyGroup[3].allMoneyBoth[1].MoneyB.ShortM).ToString(), BasicData.mainUI.shortB20 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { Math.Abs(DataFiler.basicMoneyGroup[3].allMoneyBoth[2].moneyA.ShortM).ToString(), BasicData.mainUI.shortA21 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { Math.Abs(DataFiler.basicMoneyGroup[3].allMoneyBoth[2].MoneyB.ShortM).ToString(), BasicData.mainUI.shortB21 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { Math.Abs(DataFiler.basicMoneyGroup[3].allMoneyBoth[3].moneyA.ShortM).ToString(), BasicData.mainUI.shortA22 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { Math.Abs(DataFiler.basicMoneyGroup[3].allMoneyBoth[3].MoneyB.ShortM).ToString(), BasicData.mainUI.shortB22 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { Math.Abs(DataFiler.basicMoneyGroup[4].allMoneyBoth[0].moneyA.ShortM).ToString(), BasicData.mainUI.shortA23 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { Math.Abs(DataFiler.basicMoneyGroup[4].allMoneyBoth[0].MoneyB.ShortM).ToString(), BasicData.mainUI.shortB23 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { Math.Abs(DataFiler.basicMoneyGroup[4].allMoneyBoth[1].moneyA.ShortM).ToString(), BasicData.mainUI.shortA24 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { Math.Abs(DataFiler.basicMoneyGroup[4].allMoneyBoth[1].MoneyB.ShortM).ToString(), BasicData.mainUI.shortB24 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { Math.Abs(DataFiler.basicMoneyGroup[4].allMoneyBoth[2].moneyA.ShortM).ToString(), BasicData.mainUI.shortA25 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { Math.Abs(DataFiler.basicMoneyGroup[4].allMoneyBoth[2].MoneyB.ShortM).ToString(), BasicData.mainUI.shortB25 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { Math.Abs(DataFiler.basicMoneyGroup[5].allMoneyBoth[0].moneyA.ShortM).ToString(), BasicData.mainUI.shortA26 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { Math.Abs(DataFiler.basicMoneyGroup[5].allMoneyBoth[0].MoneyB.ShortM).ToString(), BasicData.mainUI.shortB26 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { Math.Abs(DataFiler.basicMoneyGroup[5].allMoneyBoth[1].moneyA.ShortM).ToString(), BasicData.mainUI.shortA27 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { Math.Abs(DataFiler.basicMoneyGroup[5].allMoneyBoth[1].MoneyB.ShortM).ToString(), BasicData.mainUI.shortB27 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { Math.Abs(DataFiler.basicMoneyGroup[6].allMoneyBoth[0].moneyA.ShortM).ToString(), BasicData.mainUI.shortA28 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { Math.Abs(DataFiler.basicMoneyGroup[6].allMoneyBoth[0].MoneyB.ShortM).ToString(), BasicData.mainUI.shortB28 });
            #endregion
        }

        /// <summary>
        /// 检查是否显示长观值
        /// </summary>
        /// <param name="num"></param>
        /// <param name="s"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        private bool IsShowLong(int num, string s, int state)
        {
            if(Setup.isUseCondition1)
            {
                if (DataShow.linePos == num)
                {
                    return true;
                }
            }
            if(Setup.isUseCondition2)
            {
                if (s != "")
                {
                    return true;
                }
            }
            if(Setup.isUseCondition3)
            {
                if (state == 1)
                {
                    return true;
                }
            }
            return false;
        }
        public void ToLong()
        {
            int diffY = 3;
            int lineNum = 0;
            #region 长观栏 与 进退栏

            for (int i = 0; i < DataFiler.basicMoneyGroup.Length;i++ )
            {
                for(int j = 0; j < DataFiler.basicMoneyGroup[i].allMoneyBoth.Length; j++)
                {
                    lineNum = GetLine(i, j);
                    if (IsShowLong(lineNum + 1, lcName[lineNum].Text, DataFiler.basicMoneyGroup[i].allMoneyBoth[j].ShortState))
                    {
                        if (DataFiler.basicMoneyGroup[i].allMoneyBoth[j].moneyA.LongM < 0)
                        {
                            lblLongA[lineNum].ForeColor = Color.Red;
                        }
                        else
                        {
                            lblLongA[lineNum].ForeColor = Color.Green;
                        }
                        if (DataFiler.basicMoneyGroup[i].allMoneyBoth[j].MoneyB.LongM < 0)
                        {
                            lblLongB[lineNum].ForeColor = Color.Red;
                        }
                        else
                        {
                            lblLongB[lineNum].ForeColor = Color.Green;
                        }
                        BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { 
                            Math.Abs(DataFiler.basicMoneyGroup[i].allMoneyBoth[j].moneyA.LongM).ToString(), 
                            lblLongA[lineNum] });
                        BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] {
                            Math.Abs(DataFiler.basicMoneyGroup[i].allMoneyBoth[j].MoneyB.LongM).ToString(), 
                            lblLongB[lineNum] });
                        if (Setup.sBuyOrSell)
                            DataFiler.basicMoneyGroup[i].allMoneyBoth[j].BsState = true;
                        draw.PaintStr(                           //买卖符号
                            tlpTable[i],
                            DataFiler.basicMoneyGroup[i].allMoneyBoth[j].BuyOrSell.ToString(),
                            new Point(locationX, linePosY[j] + diffY)
                            );
                    }
                    else
                    {
                        BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { 
                            null, 
                            lblLongA[lineNum] });
                        BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] {
                            null, 
                            lblLongB[lineNum] });
                        draw.PaintStr(
                            tlpTable[i],
                            DataFiler.basicMoneyGroup[i].allMoneyBoth[j].BuyOrSell.ToString(),
                            new Point(locationX, linePosY[j] + diffY),
                            1
                            );
                    }
                }
            }

            #endregion
        }
        public void ToLocation()
        {
            #region Location

            int lineNum = 0;
            for (int i = 0; i < DataFiler.basicMoneyGroup.Length; i++ )
            {
                for(int j = 0; j < DataFiler.basicMoneyGroup[i].allMoneyBoth.Length; j++)
                {
                    if (DataFiler.basicMoneyGroup[i].allMoneyBoth[j].PosLocation != 'x')
                    {
                        BasicData.mainUI.Invoke(
                            BasicData.mainUI.ShowText,
                            new object[] { 
                            Math.Abs(DataFiler.basicMoneyGroup[i].allMoneyBoth[j].PosNum).ToString(), 
                            lcName[lineNum]
                        });

                        if (DataFiler.basicMoneyGroup[i].allMoneyBoth[j].PosLocation == 'R')
                        {
                            lcName[lineNum].TextAlign = ContentAlignment.MiddleRight;
                        }
                        else
                        {
                            lcName[lineNum].TextAlign = ContentAlignment.MiddleLeft;
                        }
                        if (DataFiler.basicMoneyGroup[i].allMoneyBoth[j].PosNum < 0)
                        {
                            lcName[lineNum].ForeColor = Color.Yellow;
                        }
                        else
                        {
                            lcName[lineNum].ForeColor = Color.Gray;
                        }
                    }
                    else
                    {
                        BasicData.mainUI.Invoke(
                            BasicData.mainUI.ShowText,
                            new object[] { 
                            "", 
                            lcName[lineNum]
                        });
                    }
                    lineNum++;
                }
            }


            #endregion
        }
        public void ToBuyOrSell()
        {
            #region Buy Or Sell
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[0].allMoneyBoth[0].BuyOrSellNum.ToString(), BasicData.mainUI.pcName1 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[0].allMoneyBoth[1].BuyOrSellNum.ToString(), BasicData.mainUI.pcName2 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[0].allMoneyBoth[2].BuyOrSellNum.ToString(), BasicData.mainUI.pcName3 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[0].allMoneyBoth[3].BuyOrSellNum.ToString(), BasicData.mainUI.pcName4 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[0].allMoneyBoth[4].BuyOrSellNum.ToString(), BasicData.mainUI.pcName5 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[0].allMoneyBoth[5].BuyOrSellNum.ToString(), BasicData.mainUI.pcName6 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[0].allMoneyBoth[6].BuyOrSellNum.ToString(), BasicData.mainUI.pcName7 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[1].allMoneyBoth[0].BuyOrSellNum.ToString(), BasicData.mainUI.pcName8 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[1].allMoneyBoth[1].BuyOrSellNum.ToString(), BasicData.mainUI.pcName9 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[1].allMoneyBoth[2].BuyOrSellNum.ToString(), BasicData.mainUI.pcName10 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[1].allMoneyBoth[3].BuyOrSellNum.ToString(), BasicData.mainUI.pcName11 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[1].allMoneyBoth[4].BuyOrSellNum.ToString(), BasicData.mainUI.pcName12 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[1].allMoneyBoth[5].BuyOrSellNum.ToString(), BasicData.mainUI.pcName13 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[2].allMoneyBoth[0].BuyOrSellNum.ToString(), BasicData.mainUI.pcName14 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[2].allMoneyBoth[1].BuyOrSellNum.ToString(), BasicData.mainUI.pcName15 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[2].allMoneyBoth[2].BuyOrSellNum.ToString(), BasicData.mainUI.pcName16 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[2].allMoneyBoth[3].BuyOrSellNum.ToString(), BasicData.mainUI.pcName17 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[2].allMoneyBoth[4].BuyOrSellNum.ToString(), BasicData.mainUI.pcName18 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[3].allMoneyBoth[0].BuyOrSellNum.ToString(), BasicData.mainUI.pcName19 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[3].allMoneyBoth[1].BuyOrSellNum.ToString(), BasicData.mainUI.pcName20 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[3].allMoneyBoth[2].BuyOrSellNum.ToString(), BasicData.mainUI.pcName21 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[3].allMoneyBoth[3].BuyOrSellNum.ToString(), BasicData.mainUI.pcName22 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[4].allMoneyBoth[0].BuyOrSellNum.ToString(), BasicData.mainUI.pcName23 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[4].allMoneyBoth[1].BuyOrSellNum.ToString(), BasicData.mainUI.pcName24 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[4].allMoneyBoth[2].BuyOrSellNum.ToString(), BasicData.mainUI.pcName25 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[5].allMoneyBoth[0].BuyOrSellNum.ToString(), BasicData.mainUI.pcName26 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[5].allMoneyBoth[1].BuyOrSellNum.ToString(), BasicData.mainUI.pcName27 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[6].allMoneyBoth[0].BuyOrSellNum.ToString(), BasicData.mainUI.pcName28 });
            #endregion
        }
        public void ToPriceChange()
        {
            #region Price Change
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[0].allMoneyBoth[0].PriceChange.ToString(), BasicData.mainUI.dwName1 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[0].allMoneyBoth[1].PriceChange.ToString(), BasicData.mainUI.dwName2 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[0].allMoneyBoth[2].PriceChange.ToString(), BasicData.mainUI.dwName3 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[0].allMoneyBoth[3].PriceChange.ToString(), BasicData.mainUI.dwName4 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[0].allMoneyBoth[4].PriceChange.ToString(), BasicData.mainUI.dwName5 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[0].allMoneyBoth[5].PriceChange.ToString(), BasicData.mainUI.dwName6 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[0].allMoneyBoth[6].PriceChange.ToString(), BasicData.mainUI.dwName7 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[1].allMoneyBoth[0].PriceChange.ToString(), BasicData.mainUI.dwName8 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[1].allMoneyBoth[1].PriceChange.ToString(), BasicData.mainUI.dwName9 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[1].allMoneyBoth[2].PriceChange.ToString(), BasicData.mainUI.dwName10 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[1].allMoneyBoth[3].PriceChange.ToString(), BasicData.mainUI.dwName11 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[1].allMoneyBoth[4].PriceChange.ToString(), BasicData.mainUI.dwName12 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[1].allMoneyBoth[5].PriceChange.ToString(), BasicData.mainUI.dwName13 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[2].allMoneyBoth[0].PriceChange.ToString(), BasicData.mainUI.dwName14 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[2].allMoneyBoth[1].PriceChange.ToString(), BasicData.mainUI.dwName15 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[2].allMoneyBoth[2].PriceChange.ToString(), BasicData.mainUI.dwName16 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[2].allMoneyBoth[3].PriceChange.ToString(), BasicData.mainUI.dwName17 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[2].allMoneyBoth[4].PriceChange.ToString(), BasicData.mainUI.dwName18 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[3].allMoneyBoth[0].PriceChange.ToString(), BasicData.mainUI.dwName19 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[3].allMoneyBoth[1].PriceChange.ToString(), BasicData.mainUI.dwName20 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[3].allMoneyBoth[2].PriceChange.ToString(), BasicData.mainUI.dwName21 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[3].allMoneyBoth[3].PriceChange.ToString(), BasicData.mainUI.dwName22 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[4].allMoneyBoth[0].PriceChange.ToString(), BasicData.mainUI.dwName23 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[4].allMoneyBoth[1].PriceChange.ToString(), BasicData.mainUI.dwName24 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[4].allMoneyBoth[2].PriceChange.ToString(), BasicData.mainUI.dwName25 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[5].allMoneyBoth[0].PriceChange.ToString(), BasicData.mainUI.dwName26 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[5].allMoneyBoth[1].PriceChange.ToString(), BasicData.mainUI.dwName27 });
            BasicData.mainUI.Invoke(BasicData.mainUI.ShowText, new object[] { DataFiler.basicMoneyGroup[6].allMoneyBoth[0].PriceChange.ToString(), BasicData.mainUI.dwName28 });
            #endregion
        }

        /// <summary>
        /// 倒计时
        /// </summary>
        internal void Countdown()
        {
            Money.CheckCountDownLose();
            int ind = 0;
            foreach (MoneyGroup m in DataFiler.basicMoneyGroup)
            {
                for (int j = 0; j < m.allMoneyBoth.Length; j++)
                {
                    if (m.allMoneyBoth[j].moneyA.wakeupList.Count > 0 && m.allMoneyBoth[j].MoneyB.wakeupList.Count > 0)
                    {
                        BasicData.mainUI.Invoke(
                                    BasicData.mainUI.ShowText,
                                    new object[] { 
                            ((m.allMoneyBoth[j].moneyA.wakeupList[0] < m.allMoneyBoth[j].MoneyB.wakeupList[0]) - MyTime.nowTime - oneTime).ToStr(),
                            lblTableTime[ind] }
                                    );
                    }
                    else if (m.allMoneyBoth[j].moneyA.wakeupList.Count > 0 && m.allMoneyBoth[j].MoneyB.wakeupList.Count == 0)
                    {
                        BasicData.mainUI.Invoke(
                                    BasicData.mainUI.ShowText,
                                    new object[] { 
                            (m.allMoneyBoth[j].moneyA.wakeupList[0] - MyTime.nowTime  - oneTime).ToStr(),
                            lblTableTime[ind] }
                                    );
                    }
                    else if (m.allMoneyBoth[j].moneyA.wakeupList.Count == 0 && m.allMoneyBoth[j].MoneyB.wakeupList.Count > 0)
                    {
                        BasicData.mainUI.Invoke(
                                    BasicData.mainUI.ShowText,
                                    new object[] { 
                            (m.allMoneyBoth[j].MoneyB.wakeupList[0] - MyTime.nowTime  - oneTime).ToStr(),
                            lblTableTime[ind] }
                                    );
                    }
                    else
                    {
                        BasicData.mainUI.Invoke(
                                    BasicData.mainUI.ShowText,
                                    new object[] { 
                            "",
                            lblTableTime[ind] }
                                    );
                        BasicData.mainUI.Invoke(
                                    BasicData.mainUI.ShowText,
                                    new object[] { 
                            "",
                            lblTableTime[ind] }
                                    );
                    }
                    ind++;
                }
            }

            for (int index = 0; index < DataFiler.basicMoney.Length; index++)
            {
                if (DataFiler.basicMoney[index].wakeupList.Count > 0)
                {
                    BasicData.mainUI.Invoke(
                        BasicData.mainUI.ShowText,
                        new object[] { (DataFiler.basicMoney[index].wakeupList[0] - MyTime.nowTime - oneTime).ToStr(), lblZTime[index] }
                        );
                }
                else
                {
                    BasicData.mainUI.Invoke(
                        BasicData.mainUI.ShowText,
                        new object[] { "", lblZTime[index] }
                        );
                }
            }
        }

        #region 离场符号
        /// <summary>
        /// 离场标记显示
        /// </summary>
        internal void ToOutMark()
        {
            int lineNum = 0;
            int diffY = 5;
            Setup.isPlayOutMusic = 0;
            for (int i = 0; i < DataFiler.basicMoneyGroup.Length; i++)
            {
                for (int j = 0; j < DataFiler.basicMoneyGroup[i].allMoneyBoth.Length; j++)
                {
                    lineNum = GetLine(i, j);
                    if (IsShowOutM(lcName[lineNum].Text, DataFiler.basicMoneyGroup[i].allMoneyBoth[j]))
                    {
                        draw.PaintMark(tlpTable[i],
                            new Point(locationX, linePosY[j] + diffY),
                            Color.Yellow
                            );
                    }
                    else
                    {
                        draw.PaintMark(tlpTable[i],
                            new Point(locationX, linePosY[j] + diffY),
                            tlpTable[i].BackColor
                            );
                    }
                }
            }
        }

        private bool IsShowOutM(string s, MoneyBoth m)
        {
            if( s != "" &&
                (   
                    (m.moneyA.LongM + m.MoneyB.LongM < Setup.sOutLongSum )||
                    (m.moneyA.Order +m.MoneyB.Order > Setup.sOutOrderSum) ) 
                )
            {
                if(Setup.sOut)
                    m.OutState = true;
                Setup.isPlayOutMusic++;
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion


        internal void UpFXTMoneyNameColor()
        {
            #region 在日行图的货币名称上 突显在走势图中展示的货币


            int selectX = 30;
            int selectY = 5;
            int chang = 37;
            int kuan = 17;
            Color upLineColor = Color.WhiteSmoke;

            for(int i = 0; i < lblMoneyNameRXT.Length; i++)
            {
                lblMoneyNameRXT[i].ForeColor = DataFiler.FindMoney(lblMoneyNameRXT[i].Text).MoneyColor;
            }


            //1
            if (BasicData.zoushi_MBoth.moneyA.Name == BasicData.mainUI.lblUSD.Text ||
                BasicData.zoushi_MBoth.MoneyB.Name == BasicData.mainUI.lblUSD.Text)
            {
                //BasicData.mainUI.lblUSD.BackColor = Color.Navy;
                draw.PaintRectangle(BasicData.mainUI.lblUSD, upLineColor, new Point(selectX, selectY), chang, kuan, 1);
            }
            else
            {
                //BasicData.mainUI.lblUSD.BackColor = Color.Black;
                draw.PaintRectangle(BasicData.mainUI.lblUSD, BasicData.mainUI.lblUSD.BackColor, new Point(selectX, selectY), chang, kuan, 1);
            }
            //2
            if (BasicData.zoushi_MBoth.moneyA.Name == BasicData.mainUI.lblJPY.Text ||
                BasicData.zoushi_MBoth.MoneyB.Name == BasicData.mainUI.lblJPY.Text)
            {
                //BasicData.mainUI.lblJPY.BackColor = Color.Navy;
                draw.PaintRectangle(BasicData.mainUI.lblJPY, upLineColor, new Point(selectX, selectY), chang, kuan, 1);
            }
            else
            {
                //BasicData.mainUI.lblJPY.BackColor = Color.Black;
                draw.PaintRectangle(BasicData.mainUI.lblJPY, BasicData.mainUI.lblJPY.BackColor, new Point(selectX, selectY), chang, kuan, 1);
            }
            //3
            if (BasicData.zoushi_MBoth.moneyA.Name == BasicData.mainUI.lblEUR.Text ||
                BasicData.zoushi_MBoth.MoneyB.Name == BasicData.mainUI.lblEUR.Text)
            {
                //BasicData.mainUI.lblEUR.BackColor = Color.Navy;
                draw.PaintRectangle(BasicData.mainUI.lblEUR, upLineColor, new Point(selectX, selectY), chang, kuan, 1);
            }
            else
            {
                //BasicData.mainUI.lblEUR.BackColor = Color.Black;
                draw.PaintRectangle(BasicData.mainUI.lblEUR, BasicData.mainUI.lblEUR.BackColor, new Point(selectX, selectY), chang, kuan, 1);
            }
            //4
            if (BasicData.zoushi_MBoth.moneyA.Name == BasicData.mainUI.lblGBP.Text ||
                BasicData.zoushi_MBoth.MoneyB.Name == BasicData.mainUI.lblGBP.Text)
            {
                //BasicData.mainUI.lblGBP.BackColor = Color.Navy;
                draw.PaintRectangle(BasicData.mainUI.lblGBP, upLineColor, new Point(selectX, selectY), chang, kuan, 1);
            }
            else
            {
                //BasicData.mainUI.lblGBP.BackColor = Color.Black;
                draw.PaintRectangle(BasicData.mainUI.lblGBP, BasicData.mainUI.lblGBP.BackColor, new Point(selectX, selectY), chang, kuan, 1);
            }
            //5
            if (BasicData.zoushi_MBoth.moneyA.Name == BasicData.mainUI.lblCHF.Text ||
                BasicData.zoushi_MBoth.MoneyB.Name == BasicData.mainUI.lblCHF.Text)
            {
                //BasicData.mainUI.lblCHF.BackColor = Color.Navy;
                draw.PaintRectangle(BasicData.mainUI.lblCHF, upLineColor, new Point(selectX, selectY), chang, kuan, 1);
            }
            else
            {
                //BasicData.mainUI.lblCHF.BackColor = Color.Black;
                draw.PaintRectangle(BasicData.mainUI.lblCHF, BasicData.mainUI.lblCHF.BackColor, new Point(selectX, selectY), chang, kuan, 1);
            }
            //6
            if (BasicData.zoushi_MBoth.moneyA.Name == BasicData.mainUI.lblCAD.Text ||
                BasicData.zoushi_MBoth.MoneyB.Name == BasicData.mainUI.lblCAD.Text)
            {
                //BasicData.mainUI.lblCAD.BackColor = Color.Navy;
                draw.PaintRectangle(BasicData.mainUI.lblCAD, upLineColor, new Point(selectX, selectY), chang, kuan, 1);
            }
            else
            {
                //BasicData.mainUI.lblCAD.BackColor = Color.Black;
                draw.PaintRectangle(BasicData.mainUI.lblCAD, BasicData.mainUI.lblCAD.BackColor, new Point(selectX, selectY), chang, kuan, 1);
            }
            //7
            if (BasicData.zoushi_MBoth.moneyA.Name == BasicData.mainUI.lblAUD.Text ||
                BasicData.zoushi_MBoth.MoneyB.Name == BasicData.mainUI.lblAUD.Text)
            {
                //BasicData.mainUI.lblAUD.BackColor = Color.Navy;
                draw.PaintRectangle(BasicData.mainUI.lblAUD, upLineColor, new Point(selectX, selectY), chang, kuan, 1);
            }
            else
            {
                //BasicData.mainUI.lblAUD.BackColor = Color.Black;
                draw.PaintRectangle(BasicData.mainUI.lblAUD, BasicData.mainUI.lblAUD.BackColor, new Point(selectX, selectY), chang, kuan, 1);
            }
            //8
            if (BasicData.zoushi_MBoth.moneyA.Name == BasicData.mainUI.lblNZD.Text ||
                BasicData.zoushi_MBoth.MoneyB.Name == BasicData.mainUI.lblNZD.Text)
            {
                //BasicData.mainUI.lblNZD.BackColor = Color.Navy;
                draw.PaintRectangle(BasicData.mainUI.lblNZD, upLineColor, new Point(selectX, selectY), chang, kuan, 1);
            }
            else
            {
                //BasicData.mainUI.lblNZD.BackColor = Color.Black;
                draw.PaintRectangle(BasicData.mainUI.lblNZD, BasicData.mainUI.lblNZD.BackColor, new Point(selectX, selectY), chang, kuan, 1);
            }
            #endregion
        }

        /// <summary>
        /// 判断货币对的短观值的状态 常态与非常态
        /// </summary>
        /// <param name="mBoth">货币对 对象</param>
        /// <returns>是与否</returns>
        private bool IsSuperState(MoneyBoth mBoth)
        {
            mBoth.DoubleGreen = false;
            mBoth.ShortState = 0;
            if (Setup.sIsGreenAndRed)
            {
                if (mBoth.moneyA.MoneyColor == mBoth.MoneyB.MoneyColor)
                    return false;
            }
            if (mBoth.moneyA.Order == Setup.sWithoutNum1 ||
                mBoth.moneyA.Order == Setup.sWithoutNum2 ||
                mBoth.MoneyB.Order == Setup.sWithoutNum1 ||
                mBoth.MoneyB.Order == Setup.sWithoutNum2
                )
                return false;
            if (mBoth.moneyA.Order + mBoth.MoneyB.Order >= Setup.sOrderSumLess)
                return false;
            if (mBoth.moneyA.ShortM <= Setup.sShortAllMore ||
                mBoth.MoneyB.ShortM <= Setup.sShortAllMore
                )
                return false;
            mBoth.ShortState = 1;
            DataShow.doubleSuperNum++;
            mBoth.DoubleGreen = true;
            return true;
        }

        /// <summary>
        /// 短观值非常态显示
        /// </summary>
        internal void ToShortSuperNum()
        {
            DataShow.doubleSuperNum = 0;
            for (int i = 0; i < DataFiler.basicMoneyGroup.Length; i++)
            {
                for (int j = 0; j < DataFiler.basicMoneyGroup[i].allMoneyBoth.Length; j++)
                {
                    if (IsSuperState(DataFiler.basicMoneyGroup[i].allMoneyBoth[j]))
                    {
                        BasicData.mainUI.Invoke(BasicData.mainUI.ShowLabelFont, new object[] { new Font("黑体", 12, FontStyle.Bold), lblShortA[GetLine(i, j)] });
                        //lblShortA[GetLine(i,j)].Font = new Font("黑体", 12, FontStyle.Regular);
                        lblShortA[GetLine(i, j)].BackColor = Color.Green;
                        lblShortA[GetLine(i, j)].ForeColor = Color.Black;
                        BasicData.mainUI.Invoke(BasicData.mainUI.ShowLabelFont, new object[] { new Font("黑体", 12, FontStyle.Bold), lblShortB[GetLine(i, j)] });
                        //lblShortB[GetLine(i, j)].Font = new Font("黑体", 12, FontStyle.Regular);
                        lblShortB[GetLine(i, j)].BackColor = Color.Green;
                        lblShortB[GetLine(i, j)].ForeColor = Color.Black;
                    }
                    else
                    {
                        BasicData.mainUI.Invoke(BasicData.mainUI.ShowLabelFont, new object[] { new Font("Arial", 10, FontStyle.Bold), lblShortA[GetLine(i, j)] });
                        //lblShortA[GetLine(i, j)].Font = new Font("Arial", 10, FontStyle.Bold);
                        lblShortA[GetLine(i, j)].BackColor = tlpTable[i].BackColor;
                        BasicData.mainUI.Invoke(BasicData.mainUI.ShowLabelFont, new object[] { new Font("Arial", 10, FontStyle.Bold), lblShortB[GetLine(i, j)] });
                        //lblShortB[GetLine(i, j)].Font = new Font("Arial", 10, FontStyle.Bold);
                        lblShortB[GetLine(i, j)].BackColor = tlpTable[i].BackColor;
                        if (DataFiler.basicMoneyGroup[i].allMoneyBoth[j].moneyA.ShortM > 0)
                        {
                            lblShortA[GetLine(i, j)].ForeColor = Color.Green;
                        }
                        else
                        {
                            lblShortA[GetLine(i, j)].ForeColor = Color.Red;
                        }
                        if (DataFiler.basicMoneyGroup[i].allMoneyBoth[j].MoneyB.ShortM > 0)
                        {
                            lblShortB[GetLine(i, j)].ForeColor = Color.Green;
                        }
                        else
                        {
                            lblShortB[GetLine(i, j)].ForeColor = Color.Red;
                        }
                    }
                }
            }
        }
        
        /// <summary>
        /// 根据分组和小行计算总行数 从1开始
        /// </summary>
        /// <param name="group"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        private int GetLine(int group, int i)
        {
            int line = 0;
            for (int index = 0; index < group; index++)
            {
                line = line + 7 - index;
            }
            return line + i;
        }
    }
}
