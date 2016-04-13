using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exchange_UI
{
    public partial class FMSetup : Form
    {
        public FMSetup()
        {
            InitializeComponent();
        }

        #region 各种初始化
        /// <summary>
        /// 各种初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FMSetup_Load(object sender, EventArgs e)
        {
            cbxFLeft.Text = Setup.sFxtLeftNum.ToString();
            cbxLongMax.Text = Setup.longNumMax.ToString();
            cbxShortMax.Text = Setup.shortNumMax.ToString();
            cbxTimeDiff.Text = Setup.timeHourDiff.ToString();

            //短观值非常态初始化
            comboBox9.Text = Setup.sNormalRG.ToString();
            comboBox8.Text = Setup.sNormalGG.ToString();
            checkBox2.Checked = Setup.sIsGreenAndRed;
            comboBox18.Text = Setup.sWithoutNum1.ToString();
            comboBox17.Text = Setup.sWithoutNum2.ToString();
            comboBox19.Text = Setup.sOrderSumLess.ToString();
            comboBox20.Text = Setup.sShortAllMore.ToString();

            //长观值显示条件是否启用
            checkBox3.Checked = Setup.isUseCondition1;
            checkBox4.Checked = Setup.isUseCondition2;
            checkBox5.Checked = Setup.isUseCondition3;

            //进退栏 离场符号显示条件
            comboBox22.Text = Setup.sOutLongSum.ToString();
            comboBox23.Text = Setup.sOutOrderSum.ToString();
            comboBox10.Text = Setup.sAfterMinNum.ToString();

            //货币对栏 货币颜色设置
            comboBox12.Text = Setup.sMHeightLimit.ToString();
            comboBox16.Text = Setup.sMGrayFix1.ToString();
            comboBox15.Text = Setup.sMGrayFix2.ToString();
            comboBox14.Text = Setup.sMGreen.ToString();
            comboBox13.Text = Setup.sMRed.ToString();

            //初始路径
            tbxBath.Text = Setup.pathTemp;

            //报错相关
            cbxErrorStopTime.Text = Setup.stopSec.ToString();

            //倒计时
            comboBox27.Text = Setup.countDownLess.ToString();
            comboBox28.Text = Setup.countDownAgain.ToString();

            //点差栏提示符设置
            comboBox1.Text = Setup.gDayNumMore.ToString();
            comboBox2.Text = Setup.gJinTuiMore.ToString();
            checkBox1.Checked = Setup.gIsColorDiff;
            comboBox3.Text = Setup.gWithoutOrder1.ToString();
            comboBox4.Text = Setup.gWithoutOrder2.ToString();
            comboBox5.Text = Setup.gOrderSumLess.ToString();
            comboBox6.Text = Setup.gShortBothMore.ToString();


            checkBox15.Checked = Setup.gIsUseSound;
            
            //GGG文件写入条件
            checkBox7.Checked = Setup.sBuyOrSell;
            checkBox6.Checked = Setup.sOut;

            //走势图灯音效
            comboBox11.Text = Setup.zoushiGreen.ToString();
            comboBox24.Text = Setup.zoushiRed.ToString();

            #region 红黄绿 初始化

            comboBox7.Text = Setup.signLightRY.ToString();
            comboBox21.Text = Setup.signLightYG.ToString();

            //    //红灯
            //checkBox8.Checked = IntToBool(Setup.sZSRed[0]);
            //checkBox9.Checked = IntToBool(Setup.sZSRed[1]);
            //checkBox10.Checked = IntToBool(Setup.sZSRed[2]);
            //checkBox11.Checked = IntToBool(Setup.sZSRed[3]);
            //checkBox12.Checked = IntToBool(Setup.sZSRed[4]);
            //checkBox13.Checked = IntToBool(Setup.sZSRed[5]);
            //checkBox14.Checked = IntToBool(Setup.sZSRed[6]);
            //checkBox16.Checked = Setup.sZIsUpRed;
            //    //黄灯
            //checkBox25.Checked = IntToBool(Setup.sZSYellow[0]);
            //checkBox24.Checked = IntToBool(Setup.sZSYellow[1]);
            //checkBox23.Checked = IntToBool(Setup.sZSYellow[2]);
            //checkBox22.Checked = IntToBool(Setup.sZSYellow[3]);
            //checkBox21.Checked = IntToBool(Setup.sZSYellow[4]);
            //checkBox20.Checked = IntToBool(Setup.sZSYellow[5]);
            //checkBox19.Checked = IntToBool(Setup.sZSYellow[6]);

            //    //绿灯
            //checkBox34.Checked = IntToBool(Setup.sZSGreen[0]);
            //checkBox33.Checked = IntToBool(Setup.sZSGreen[1]);
            //checkBox32.Checked = IntToBool(Setup.sZSGreen[2]);
            //checkBox31.Checked = IntToBool(Setup.sZSGreen[3]);
            //checkBox30.Checked = IntToBool(Setup.sZSGreen[4]);
            //checkBox29.Checked = IntToBool(Setup.sZSGreen[5]);
            //checkBox28.Checked = IntToBool(Setup.sZSGreen[6]);
            //checkBox26.Checked = Setup.sZIsUpGreen;

            #endregion

            //初始化更改按钮
            btnGGGFinish.Enabled = false;
            btnWarningFinish.Enabled = false;
            btnTimeDiffFinish.Enabled = false;
            btnFXTFinish.Enabled = false;
            btnZSTFinish.Enabled = false;
            btnOutMarkFinish.Enabled = false;
            btnLongFinish.Enabled = false;
            btnShortFinish.Enabled = false;
            btnMoneybothFinish.Enabled = false;
            btnDianchaFinish.Enabled = false;
            btnCountDown.Enabled = false;
        }

        #endregion

        #region 时差设置
        private void cbxTimeDiff_SelectionChangeCommitted(object sender, EventArgs e)
        {
            btnTimeDiffFinish.Enabled = true;
        }

        private void btnTimeDiffFinish_Click(object sender, EventArgs e)
        {
            Setup.timeHourDiff = int.Parse(cbxTimeDiff.SelectedItem.ToString());
            btnTimeDiffFinish.Enabled = false;
        }

        #endregion

        #region 倒计时设置
        private void btnSetWarning_Click(object sender, EventArgs e)
        {
            FMWarnTime warntime = new FMWarnTime();
            warntime.Show();
        }
        #endregion

        #region 接口文件路径设置
        private void button1_Click(object sender, EventArgs e)
        {
            fbdGetPath.ShowDialog();
            tbxBath.Text = fbdGetPath.SelectedPath;
            btnPathOk.Enabled = true;
        }

        private void btnPathOk_Click(object sender, EventArgs e)
        {
            Setup.pathTemp = tbxBath.Text;
            btnPathOk.Enabled = false;
        }

        private void btnPathDefault_Click(object sender, EventArgs e)
        {

            Setup.pathTemp = @"C:\Program Files (x86)\MT4 Exchange\MQL4\Files";
            tbxBath.Text = Setup.pathTemp;
        }
        #endregion

        #region 风向图设置
        private void cbxFLeft_SelectionChangeCommitted(object sender, EventArgs e)
        {
            btnFXTFinish.Enabled = true;
        }

        private void cbxShortMax_SelectionChangeCommitted(object sender, EventArgs e)
        {
            btnFXTFinish.Enabled = true;
        }

        private void cbxLongMax_SelectionChangeCommitted(object sender, EventArgs e)
        {
            btnFXTFinish.Enabled = true;
        }

        private void btnFXTFinish_Click(object sender, EventArgs e)
        {
            Setup.sFxtLeftNum = int.Parse(cbxFLeft.SelectedItem.ToString());
            Setup.shortNumMax = int.Parse(cbxShortMax.SelectedItem.ToString());
            Setup.longNumMax = int.Parse(cbxLongMax.SelectedItem.ToString());

            new DataShow().ToFengxiangtu();

            btnFXTFinish.Enabled = false;
        }

        #endregion

        #region 短观值 非常态初始化


        private void comboBox9_SelectionChangeCommitted_1(object sender, EventArgs e)
        {
            btnShortFinish.Enabled = true;
        }

        private void comboBox8_SelectionChangeCommitted_1(object sender, EventArgs e)
        {
            btnShortFinish.Enabled = true;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            btnShortFinish.Enabled = true;
        }

        private void comboBox18_SelectionChangeCommitted(object sender, EventArgs e)
        {
            btnShortFinish.Enabled = true;
        }

        private void comboBox17_SelectionChangeCommitted(object sender, EventArgs e)
        {
            btnShortFinish.Enabled = true;
        }

        private void comboBox19_SelectionChangeCommitted(object sender, EventArgs e)
        {
            btnShortFinish.Enabled = true;
        }

        private void comboBox20_SelectionChangeCommitted(object sender, EventArgs e)
        {
            btnShortFinish.Enabled = true;
        }

        private void comboBox21_SelectionChangeCommitted(object sender, EventArgs e)
        {
            btnShortFinish.Enabled = true;
        }
        private void btnShortFinish_Click(object sender, EventArgs e)
        {
            Setup.sNormalRG = int.Parse(comboBox9.SelectedItem.ToString());
            Setup.sNormalGG = int.Parse(comboBox8.SelectedItem.ToString());

            Setup.sIsGreenAndRed = checkBox2.Checked;
            Setup.sWithoutNum1 = int.Parse(comboBox18.SelectedItem.ToString());
            Setup.sWithoutNum2 = int.Parse(comboBox17.SelectedItem.ToString());
            Setup.sOrderSumLess = int.Parse(comboBox19.SelectedItem.ToString());
            Setup.sShortAllMore = int.Parse(comboBox20.SelectedItem.ToString());

            new DataShow().ToShortSuperNum();
            btnShortFinish.Enabled = false;
        }
        #endregion

        #region 长观值显示条件
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            btnLongFinish.Enabled = true;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            btnLongFinish.Enabled = true;
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            btnLongFinish.Enabled = true;
        }
        private void btnLongFinish_Click(object sender, EventArgs e)
        {
            Setup.isUseCondition1 = checkBox3.Checked;
            Setup.isUseCondition2 = checkBox4.Checked;
            Setup.isUseCondition3 = checkBox5.Checked;

            new DataShow().ToTable();
            btnLongFinish.Enabled = false;
        }
        #endregion

        #region 进退栏 离场符号设置
        private void comboBox22_SelectionChangeCommitted(object sender, EventArgs e)
        {
            btnOutMarkFinish.Enabled = true;
        }

        private void comboBox23_SelectionChangeCommitted(object sender, EventArgs e)
        {
            btnOutMarkFinish.Enabled = true;
        }
        private void comboBox10_SelectionChangeCommitted(object sender, EventArgs e)
        {
            btnOutMarkFinish.Enabled = true;
        }

        private void btnOutMarkFinish_Click(object sender, EventArgs e)
        {
            Setup.sOutLongSum = int.Parse(comboBox22.SelectedItem.ToString());
            Setup.sOutOrderSum = int.Parse(comboBox23.SelectedItem.ToString());
            Setup.sAfterMinNum = int.Parse(comboBox10.SelectedItem.ToString());

            btnOutMarkFinish.Enabled = false;
        }
        #endregion

        #region 货币对栏 货币颜色设置
        private void comboBox12_SelectionChangeCommitted(object sender, EventArgs e)
        {
            btnMoneybothFinish.Enabled = true;
        }

        private void comboBox16_SelectionChangeCommitted(object sender, EventArgs e)
        {
            btnMoneybothFinish.Enabled = true;
        }

        private void comboBox15_SelectionChangeCommitted(object sender, EventArgs e)
        {
            btnMoneybothFinish.Enabled = true;
        }

        private void comboBox14_SelectionChangeCommitted(object sender, EventArgs e)
        {
            btnMoneybothFinish.Enabled = true;
        }

        private void comboBox13_SelectionChangeCommitted(object sender, EventArgs e)
        {
            btnMoneybothFinish.Enabled = true;
        }
        private void btnMoneybothFinish_Click(object sender, EventArgs e)
        {
            Setup.sMHeightLimit = int.Parse(comboBox12.SelectedItem.ToString());
            Setup.sMGrayFix1 = int.Parse(comboBox16.SelectedItem.ToString());
            Setup.sMGrayFix2 = int.Parse(comboBox15.SelectedItem.ToString());
            Setup.sMGreen = int.Parse(comboBox14.SelectedItem.ToString());
            Setup.sMRed = int.Parse(comboBox13.SelectedItem.ToString());
            
            new DataShow().ToTable();
            btnMoneybothFinish.Enabled = false;
        }
        #endregion

        #region 走势图设置


        private void comboBox21_SelectionChangeCommitted_1(object sender, EventArgs e)
        {
            btnZSTFinish.Enabled = true;
        }

        private void comboBox7_SelectionChangeCommitted(object sender, EventArgs e)
        {
            btnZSTFinish.Enabled = true;
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            btnZSTFinish.Enabled = true;
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            btnZSTFinish.Enabled = true;
        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            btnZSTFinish.Enabled = true;
        }

        private void checkBox11_CheckedChanged(object sender, EventArgs e)
        {
            btnZSTFinish.Enabled = true;
        }

        private void checkBox12_CheckedChanged(object sender, EventArgs e)
        {
            btnZSTFinish.Enabled = true;
        }

        private void checkBox13_CheckedChanged(object sender, EventArgs e)
        {
            btnZSTFinish.Enabled = true;
        }

        private void checkBox14_CheckedChanged(object sender, EventArgs e)
        {
            btnZSTFinish.Enabled = true;
        }

        private void checkBox15_CheckedChanged(object sender, EventArgs e)
        {
            btnZSTFinish.Enabled = true;
        }

        private void checkBox16_CheckedChanged(object sender, EventArgs e)
        {
            btnZSTFinish.Enabled = true;
        }

        private void checkBox25_CheckedChanged(object sender, EventArgs e)
        {
            btnZSTFinish.Enabled = true;
        }

        private void checkBox24_CheckedChanged(object sender, EventArgs e)
        {
            btnZSTFinish.Enabled = true;
        }

        private void checkBox23_CheckedChanged(object sender, EventArgs e)
        {
            btnZSTFinish.Enabled = true;
        }

        private void checkBox22_CheckedChanged(object sender, EventArgs e)
        {
            btnZSTFinish.Enabled = true;
        }

        private void checkBox21_CheckedChanged(object sender, EventArgs e)
        {
            btnZSTFinish.Enabled = true;
        }

        private void checkBox20_CheckedChanged(object sender, EventArgs e)
        {
            btnZSTFinish.Enabled = true;
        }

        private void checkBox19_CheckedChanged(object sender, EventArgs e)
        {
            btnZSTFinish.Enabled = true;
        }

        private void checkBox18_CheckedChanged(object sender, EventArgs e)
        {
            btnZSTFinish.Enabled = true;
        }

        private void checkBox17_CheckedChanged(object sender, EventArgs e)
        {
            btnZSTFinish.Enabled = true;
        }

        private void comboBox11_SelectionChangeCommitted(object sender, EventArgs e)
        {
            btnZSTFinish.Enabled = true;
        }

        private void comboBox24_SelectionChangeCommitted(object sender, EventArgs e)
        {
            btnZSTFinish.Enabled = true;
        }

        private void comboBox25_SelectionChangeCommitted(object sender, EventArgs e)
        {
            btnZSTFinish.Enabled = true;
        }

        private void comboBox29_SelectionChangeCommitted(object sender, EventArgs e)
        {
            btnZSTFinish.Enabled = true;
        }

        private void comboBox26_SelectionChangeCommitted(object sender, EventArgs e)
        {
            btnZSTFinish.Enabled = true;
        }
        private void checkBox26_CheckedChanged(object sender, EventArgs e)
        {
            btnZSTFinish.Enabled = true;
        }


        private void btnZSTFinish_Click(object sender, EventArgs e)
        {
            #region 注释 1.0
            //#region red
            //if (checkBox8.Checked)
            //{
            //    Setup.sZSRed[0] = 0;
            //}
            //else
            //{
            //    Setup.sZSRed[0] = -1;
            //}
            //if (checkBox9.Checked)
            //{
            //    Setup.sZSRed[1] = 1;
            //}
            //else
            //{
            //    Setup.sZSRed[1] = -1;
            //}
            //if (checkBox10.Checked)
            //{
            //    Setup.sZSRed[2] = 2;
            //}
            //else
            //{
            //    Setup.sZSRed[2] = -1;
            //}
            //if (checkBox11.Checked)
            //{
            //    Setup.sZSRed[3] = 3;
            //}
            //else
            //{
            //    Setup.sZSRed[3] = -1;
            //}
            //if (checkBox12.Checked)
            //{
            //    Setup.sZSRed[4] = 4;
            //}
            //else
            //{
            //    Setup.sZSRed[4] = -1;
            //}
            //if (checkBox13.Checked)
            //{
            //    Setup.sZSRed[5] = 5;
            //}
            //else
            //{
            //    Setup.sZSRed[5] = -1;
            //}
            //if (checkBox14.Checked)
            //{
            //    Setup.sZSRed[6] = 6;
            //}
            //else
            //{
            //    Setup.sZSRed[6] = -1;
            //}
            
            //Setup.sZIsUpRed = checkBox16.Checked;
            
            //#endregion

            //#region yellow
            //if (checkBox25.Checked)
            //{
            //    Setup.sZSYellow[0] = 0;
            //}
            //else
            //{
            //    Setup.sZSYellow[0] = -1;
            //}
            //if (checkBox24.Checked)
            //{
            //    Setup.sZSYellow[1] = 1;
            //}
            //else
            //{
            //    Setup.sZSYellow[1] = -1;
            //}
            //if (checkBox23.Checked)
            //{
            //    Setup.sZSYellow[2] = 2;
            //}
            //else
            //{
            //    Setup.sZSYellow[2] = -1;
            //}
            //if (checkBox22.Checked)
            //{
            //    Setup.sZSYellow[3] = 3;
            //}
            //else
            //{
            //    Setup.sZSYellow[3] = -1;
            //}
            //if (checkBox21.Checked)
            //{
            //    Setup.sZSYellow[4] = 4;
            //}
            //else
            //{
            //    Setup.sZSYellow[4] = -1;
            //}
            //if (checkBox20.Checked)
            //{
            //    Setup.sZSYellow[5] = 5;
            //}
            //else
            //{
            //    Setup.sZSYellow[5] = -1;
            //}
            //if (checkBox19.Checked)
            //{
            //    Setup.sZSYellow[6] = 6;
            //}
            //else
            //{
            //    Setup.sZSYellow[6] = -1;
            //}

            //#endregion

            //#region green
            //if (checkBox34.Checked)
            //{
            //    Setup.sZSGreen[0] = 0;
            //}
            //else
            //{
            //    Setup.sZSGreen[0] = -1;
            //}
            //if (checkBox33.Checked)
            //{
            //    Setup.sZSGreen[1] = 1;
            //}
            //else
            //{
            //    Setup.sZSGreen[1] = -1;
            //}
            //if (checkBox32.Checked)
            //{
            //    Setup.sZSGreen[2] = 2;
            //}
            //else
            //{
            //    Setup.sZSGreen[2] = -1;
            //}
            //if (checkBox31.Checked)
            //{
            //    Setup.sZSGreen[3] = 3;
            //}
            //else
            //{
            //    Setup.sZSGreen[3] = -1;
            //}
            //if (checkBox30.Checked)
            //{
            //    Setup.sZSGreen[4] = 4;
            //}
            //else
            //{
            //    Setup.sZSGreen[4] = -1;
            //}
            //if (checkBox29.Checked)
            //{
            //    Setup.sZSGreen[5] = 5;
            //}
            //else
            //{
            //    Setup.sZSGreen[5] = -1;
            //}
            //if (checkBox28.Checked)
            //{
            //    Setup.sZSGreen[6] = 6;
            //}
            //else
            //{
            //    Setup.sZSGreen[6] = -1;
            //}
            //Setup.sZIsUpGreen = checkBox26.Checked;

            //#endregion
            ////green

            //Setup.zoushiRed = int.Parse(comboBox24.SelectedItem.ToString());

            //Setup.zoushiGreen = int.Parse(comboBox11.SelectedItem.ToString());
            #endregion

            Setup.signLightRY = int.Parse(comboBox7.SelectedItem.ToString());
            Setup.signLightYG = int.Parse(comboBox21.SelectedItem.ToString());

            new DataShow().ToSignLight();
            btnZSTFinish.Enabled = false;
        }

        #endregion

        #region 程序报错设置
        private void cbxErrorStopTime_SelectionChangeCommitted(object sender, EventArgs e)
        {
            btnWarningFinish.Enabled = true;
        }
        private void btnWarningFinish_Click(object sender, EventArgs e)
        {
            Setup.stopSec = int.Parse(cbxErrorStopTime.SelectedItem.ToString());
            btnWarningFinish.Enabled = false;
        }
        #endregion

        #region 势值栏提示符

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            btnDianchaFinish.Enabled = true;
        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            btnDianchaFinish.Enabled = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            btnDianchaFinish.Enabled = true;
        }

        private void comboBox3_SelectionChangeCommitted(object sender, EventArgs e)
        {
            btnDianchaFinish.Enabled = true;
        }

        private void comboBox4_SelectionChangeCommitted(object sender, EventArgs e)
        {
            btnDianchaFinish.Enabled = true;
        }

        private void comboBox5_SelectionChangeCommitted(object sender, EventArgs e)
        {
            btnDianchaFinish.Enabled = true;
        }

        private void comboBox6_SelectionChangeCommitted(object sender, EventArgs e)
        {
            btnDianchaFinish.Enabled = true;
        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnDianchaFinish.Enabled = true;
        }

        private void comboBox8_SelectionChangeCommitted(object sender, EventArgs e)
        {
            btnDianchaFinish.Enabled = true;
        }

        private void comboBox9_SelectionChangeCommitted(object sender, EventArgs e)
        {
            btnDianchaFinish.Enabled = true;
        }
        private void checkBox15_CheckedChanged_1(object sender, EventArgs e)
        {
            btnDianchaFinish.Enabled = true;
        }

        private void btnDianchaFinish_Click(object sender, EventArgs e)
        {
            Setup.gDayNumMore = int.Parse(comboBox1.SelectedItem.ToString());
            Setup.gJinTuiMore = int.Parse(comboBox2.SelectedItem.ToString());
            Setup.gIsColorDiff = checkBox1.Checked;
            Setup.gWithoutOrder1 = int.Parse(comboBox3.SelectedItem.ToString());
            Setup.gWithoutOrder2 = int.Parse(comboBox4.SelectedItem.ToString());
            Setup.gOrderSumLess = int.Parse(comboBox5.SelectedItem.ToString());
            Setup.gShortBothMore = int.Parse(comboBox6.SelectedItem.ToString());    


            Setup.gIsUseSound = checkBox15.Checked;

            new DataShow().UpCirMark();

            btnDianchaFinish.Enabled = false;
        }
        #endregion

        #region GGG 文件写入设置
        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            btnGGGFinish.Enabled = true;
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            btnGGGFinish.Enabled = true;
        }
        private void btnGGGFinish_Click(object sender, EventArgs e)
        {
            Setup.sBuyOrSell = checkBox7.Checked;
            Setup.sOut = checkBox6.Checked;

            btnGGGFinish.Enabled = false;
        }
        #endregion

        /// <summary>
        /// 数字转布尔 走势图 信号灯初始化专用
        /// </summary>
        /// <param name="mark"></param>
        /// <returns></returns>
        private bool IntToBool(int mark)
        {
            if (mark == -1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void comboBox27_SelectionChangeCommitted(object sender, EventArgs e)
        {
            btnCountDown.Enabled = true;
        }

        private void comboBox28_SelectionChangeCommitted(object sender, EventArgs e)
        {
            btnCountDown.Enabled = true;
        }

        private void btnCountDown_Click(object sender, EventArgs e)
        {
            Setup.countDownLess = int.Parse(comboBox27.SelectedItem.ToString());
            Setup.countDownAgain = int.Parse(comboBox28.SelectedItem.ToString());

            btnCountDown.Enabled = false;
        }



    }
}