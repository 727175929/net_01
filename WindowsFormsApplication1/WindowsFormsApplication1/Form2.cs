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
        Int16 card_conn = 1;  //卡是否在读取的状态中  0表示不在读取  1表示读取中
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
                //.Text = "连接读写器失败，错误原因：" + getErrMsg(st);
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
        private void opencard(byte openMode, byte sector)   //openMode 打开模式 0x00 STD模式,只能寻到空闲状态下的卡片，被激活或停活（Halt）的卡片不会响应
        {                                                     // 0x01 ALL模式,能寻到空闲状态和已经被停活（Halt）的卡片
            byte[] cardUid = new byte[10];    //卡片ID
            //打开卡片
            int st = cardReader.mwOpenCard(handle, openMode, cardUid);
            if (st < 0)
            {
                //newLine = "打开卡片失败，原因：" + getErrMsg(st) + "\n";
                //richTextBox1.Text = richTextBox1.Text.Insert(0, newLine);
                return;
            }
            else
            {
                StringBuilder cardUidStr = new StringBuilder();
                cardReader.BinToHex(cardUid, cardUidStr, (uint)st);//将卡号转换为16进制字符串
                newLine = "卡片ID：" + cardUidStr.ToString() + "\n";
                richTextBox1.Text = richTextBox1.Text.Insert(0, newLine);
                newLine = null;
            }
            //System.Threading.Thread.Sleep(2 * 1000);//停2秒
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
            button1.Enabled = false;
            //opencard(1, 2);//M1操作，对2扇区进行操作
            //利用线程
            System.Timers.Timer t = new System.Timers.Timer(100);//实例化Timer类，设置时间间隔
            t.Elapsed += new System.Timers.ElapsedEventHandler(Method2);//到达时间的时候执行事件
            t.AutoReset = true;//设置是执行一次（false）还是一直执行(true)
            t.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件
            while (true)
            {
                Console.WriteLine("test_" + Thread.CurrentThread.ManagedThreadId.ToString());
                Thread.Sleep(100);
            }
            
        }
        void Method2(object source, System.Timers.ElapsedEventArgs e)
        {
            opencard(1, 2);
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
    }
}
