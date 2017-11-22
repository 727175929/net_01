using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        IntPtr handle = IntPtr.Zero;

        Int16 dev_open = 0;  //设备是否连接
        Int16 card_conn = 0;  //卡是否在读取的状态中  0表示不在读取  1表示读取中
        string newLine;
        private string getErrMsg(int errCode)
        {
            StringBuilder message = new StringBuilder();
            cardReader.getErrDescription(errCode, 0, message);
            return message.ToString();
        }
        private bool openReader()
        {
            //连接读写器
            int st = cardReader.mwDevOpen("USB", "", out handle);
            if (st < 0)
            {
                //Console.WriteLine(getErrMsg(st));
                newLine = "连接读写器失败，错误原因：" + getErrMsg(st) + "\n";
                richTextBox1.Text = richTextBox1.Text.Insert(0, newLine);
                newLine = null;
                dev_open = 0;
                return false;
            }
            else
            {
               // textBox1.Text = "连接读写器" + getErrMsg(st);
                newLine = "连接读写器" + getErrMsg(st) + "\n";
                richTextBox1.Text = richTextBox1.Text.Insert(0, newLine);
                newLine = null;
                dev_open = 1;
                return true;
            }
        }

        private void closecard(byte openMode, byte sector)   //openMode 打开模式 0x00 STD模式,只能寻到空闲状态下的卡片，被激活或停活（Halt）的卡片不会响应
        {                                                     // 0x01 ALL模式,能寻到空闲状态和已经被停活（Halt）的卡片
            byte[] cardUid = new byte[10];    //卡片ID
            int st = cardReader.mwOpenCard(handle, openMode, cardUid);
            if(st < 0 && card_conn == 1)
            {
                card_conn = 0;
                timer1.Enabled = false;
                newLine = "卡片已离开\n";
                richTextBox1.Text = richTextBox1.Text.Insert(0, newLine);
                newLine = null;
               // System.Threading.Thread.Sleep(5 * 1000);//停2秒
                timer2.Enabled = false;
                timer1.Enabled = true;
            }

        }
        private void opencard(byte openMode, byte sector)   //openMode 打开模式 0x00 STD模式,只能寻到空闲状态下的卡片，被激活或停活（Halt）的卡片不会响应
        {                                                     // 0x01 ALL模式,能寻到空闲状态和已经被停活（Halt）的卡片
            byte[] cardUid = new byte[10];    //卡片ID
            //打开卡片
            int st = cardReader.mwOpenCard(handle, openMode, cardUid);
            if (st < 0 )
            {
                //newLine = "打开卡片失败，原因：" + getErrMsg(st) + "\n";
                //richTextBox1.Text = richTextBox1.Text.Insert(0, newLine);
                return;
            }
            else if (card_conn == 1)
            {
                timer1.Enabled = false;
            }
            else
            {
                StringBuilder cardUidStr = new StringBuilder();
                cardReader.BinToHex(cardUid, cardUidStr, (uint)st);//将卡号转换为16进制字符串
                newLine = "卡片ID：" + cardUidStr.ToString() + "\n";
                richTextBox1.Text = richTextBox1.Text.Insert(0, newLine);
                newLine = null;
                card_conn = 1;  //标识符表示处于连接状态
            }
            //System.Threading.Thread.Sleep(2 * 1000);//停2秒
            newLine = "读取卡片成功,等待卡片离开\n";
            richTextBox1.Text = richTextBox1.Text.Insert(0, newLine);
            newLine = null;
            timer1.Enabled = false;
            timer2.Enabled = true;
        }
        public Form2()
        {
            InitializeComponent();
        }


        private void 操作设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 frm1 = new Form1();
            frm1.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            newLine = "开始识别卡片\n";
            richTextBox1.Text = richTextBox1.Text.Insert(0, newLine);
            //opencard(1, 2);//M1操作，对2扇区进行操作
            timer1.Enabled = true;  //当开始识别卡片时，调用计时器
           
            
        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openReader())
                button1.Enabled = true;
            else
                button1.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            opencard(0, 2);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Interval = 5000;//毫秒为单位
            timer2.Interval = 5000;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            closecard(1, 2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            timer2.Enabled = false;
        }

      

    }
}
