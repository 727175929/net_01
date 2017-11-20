using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private string getErrMsg(int errCode)
        {
            StringBuilder message = new StringBuilder();
            cardReader.getErrDescription(errCode, 0, message);
            return message.ToString();
        }
        public Form1()
        {
            InitializeComponent();
        }
        IntPtr handle = IntPtr.Zero;

        Int16 dev_open = 0;  //设备是否连接
        Int16 card_conn = 0;  //卡是否在读取的状态中
        private bool openReader()
        {
            //连接读写器
            int st = cardReader.mwDevOpen("USB", "", out handle);
            if (st < 0)
            {
                Console.WriteLine(getErrMsg(st));
                textBox1.Text ="连接读写器失败，错误原因："+ getErrMsg(st);
                dev_open = 0;
                return false;
            }
            else
            {
                textBox1.Text ="连接读写器" + getErrMsg(st);
                dev_open = 1;
                return true;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            openReader();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int st = cardReader.mwDevClose(handle);
            if (st >= 0)
            {
                textBox1.Text = "关闭读写器" + getErrMsg(st);
                dev_open = 0;
            }
            else
            {
                textBox1.Text = "关闭读写器失败，错误原因：" + getErrMsg(st);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int st = -1;
            if(dev_open != 1)
            MessageBox.Show("请先开启读卡器");
            else
            {
                StringBuilder strHardwareVer = new StringBuilder();
                st = cardReader.mwDevGetHardwareVer(handle, strHardwareVer); //读版本号
                if (st >= 0)
                {
                    textBox2.Text = "版本号：" + strHardwareVer.ToString();
                }
                else
                {
                    textBox2.Text = "获取版本号失败，失败原因："+getErrMsg(st);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int st = -1;
            if (dev_open != 1)
                MessageBox.Show("请先开启读卡器");
            else
            {
                StringBuilder strHardwareVer = new StringBuilder();
                st = cardReader.mwDevGetSerialNumber(handle, strHardwareVer); //读版本号
                if (st >= 0)
                {
                    textBox3.Text = "产品序列号：" + strHardwareVer.ToString();
                }
                else
                {
                    textBox3.Text = "获取产品序列号失败，失败原因：" + getErrMsg(st);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int st = -1;
            if (dev_open != 1)
                MessageBox.Show("请先开启读卡器");
            else
            {
                byte a1 = Convert.ToByte(textBox4.Text);
                byte a2 = Convert.ToByte(textBox5.Text);
                byte a3 = Convert.ToByte(textBox6.Text);
                cardReader.mwDevBeep(handle, a1, a2, a3);
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            string aa = textBox5.Text;
            Regex re = new Regex(@"^\d+$");
            if (!re.IsMatch(aa))//不是整数
            {
                //弹出警告
                MessageBox.Show("请输入整数");
                textBox5.Text = "2";
            } 
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            string aa = textBox6.Text;
            Regex re = new Regex(@"^\d+$");
            if (!re.IsMatch(aa))//不是整数
            {
                //弹出警告
                MessageBox.Show("请输入整数");
                textBox6.Text = "2";
            } 
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            string aa = textBox4.Text;
            Regex re = new Regex(@"^\d+$");
            if (!re.IsMatch(aa))//不是整数
            {
                //弹出警告
                MessageBox.Show("请输入整数");
                textBox4.Text = "2";
            } 
        }

        private void button6_Click(object sender, EventArgs e)
        {int st = -1;
        if (dev_open != 1)
            MessageBox.Show("请先开启读卡器");
        else
        {
            cardReader.mwDevLed(handle, 0x10, 0x10, 0x10, 0x10);
        }
        }

    }
}
