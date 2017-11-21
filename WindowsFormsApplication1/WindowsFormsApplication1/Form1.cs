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
            comboBox1.Text = "亮";  //初始化combox1
            comboBox2.Text = "亮";  //初始化combox2
            comboBox3.Text = "亮";  //初始化combox3
            comboBox4.Text = "亮";  //初始化combox4

            comboBox5.Text = "明文传输";  //初始化combox5
            comboBox6.Text = "关闭射频模块";//初始化combox6
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
        {
            //int st = -1;
        if (dev_open != 1)
            MessageBox.Show("请先开启读卡器");
        else
        {
            byte led1 = 0x01;
            byte led2 = 0x01;
            byte led3 = 0x01;
            byte led4 = 0x01;
            String box1 = comboBox1.Text.ToString();
            if (box1.Equals("亮"))
                led1 = 0x10;
            else if (box1.Equals("灭"))
                led1 = 0x01;
            else if (box1.Equals("保持"))
                led1 = 0x00;
            else 
                led1 = 0x11;

            String box2 = comboBox2.Text.ToString();
            if (box2.Equals("亮"))
                led2 = 0x10;
            else if (box2.Equals("灭"))
                led2 = 0x01;
            else if (box2.Equals("保持"))
                led2 = 0x00;
            else
                led2 = 0x11;

            String box3 = comboBox3.Text.ToString();
            if (box3.Equals("亮"))
                led3 = 0x10;
            else if (box3.Equals("灭"))
                led3 = 0x01;
            else if (box3.Equals("保持"))
                led3 = 0x00;
            else
                led3 = 0x11;

            String box4 = comboBox4.Text.ToString();
            if (box4.Equals("亮"))
                led4 = 0x10;
            else if (box4.Equals("灭"))
                led4 = 0x01;
            else if (box4.Equals("保持"))
                led4 = 0x00;
            else
                led4 = 0x11;

            cardReader.mwDevLed(handle, led1, led2, led3, led4);
        }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           // this.comboBox1.SelectedIndex = 1; 
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            string aa = textBox7.Text;
            Regex re = new Regex(@"^\d+$");
            if (!re.IsMatch(aa))//不是整数
            {
                //弹出警告
                MessageBox.Show("请输入整数");
                textBox7.Text = "0";
            } 
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            string aa = textBox8.Text;
            Regex re = new Regex(@"^\d+$");
            if (!re.IsMatch(aa))//不是整数
            {
                //弹出警告
                MessageBox.Show("请输入整数");
                textBox8.Text = "10";
            } 
        }
        byte[] intArray;
        private void button7_Click(object sender, EventArgs e)
        {
            //intArray = new byte[3] {1,2,3};
           // cardReader.mwDevWriteConfig(handle, 0, 10, intArray);
        }

        private void button8_Click(object sender, EventArgs e)
        {
           // cardReader.mwDevReadConfig(handle, 0, 10, intArray);

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            byte flag = 0;
            int st = -1;
            if (dev_open != 1)
                MessageBox.Show("请先开启读卡器");
            else
            {
                if (comboBox5.Text.Equals("明文传输"))
                    flag = 0;
                else
                    flag = 1;
                st = cardReader.mwDevSetTransferMode(handle, flag);
                if (st >= 0)
                    MessageBox.Show("当前传输方式为：" + comboBox5.Text);
                else
                    MessageBox.Show("传输方式设置失败");
            }
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            byte flag = 0;
            int st = -1;
            if (dev_open != 1)
                MessageBox.Show("请先开启读卡器");
            else
            {
                if (comboBox6.Text.Equals("关闭射频模块"))
                    flag = 0;
                else
                    flag = 1;
                st = cardReader.mwDevRFControl(handle, flag);
                if (st >= 0)
                    MessageBox.Show("已" + comboBox5.Text);
                else
                    MessageBox.Show("射频天线设置失败");
            }
        }

        private void 非卡片操作ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 设备操作ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void m1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 frm2 = new Form2();
            frm2.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void 卡片操作ToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

    }
}
