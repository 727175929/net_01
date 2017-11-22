using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class cardReader
    {
        #region 声明方法

        /// <summary>
        /// 磁道数据结构体，用于存放读取到的磁卡的数据
        /// </summary>
        public struct MagCardData
        {
            /// <summary>
            /// 第一磁道数据长度
            /// </summary>
            public byte track1_len;
            /// <summary>
            /// 第二磁道数据长度
            /// </summary>
            public byte track2_len;
            /// <summary>
            /// 第三磁道数据长度
            /// </summary>
            public byte track3_len;

            /// <summary>
            /// 第一磁道数据 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 80)]
            public byte[] track1_data;

            /// <summary>
            /// 第二磁道数据
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 41)]
            public byte[] track2_data;

            /// <summary>
            /// 第三磁道数据
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 107)]
            public byte[] track3_data;
        };

        [DllImport("mwReader.dll", EntryPoint = "mwDevOpen", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwDevOpen(string port, string paras, out IntPtr handle);

        [DllImport("mwReader.dll", EntryPoint = "mwDevClose", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwDevClose(IntPtr handle);

        [DllImport("mwReader.dll", EntryPoint = "getErrDescription", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 getErrDescription(Int32 errcode, Int32 languageCode, StringBuilder message);

        [DllImport("mwReader.dll", EntryPoint = "mwDevSetBaud", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwDevSetBaud(IntPtr handle, Int32 baud);

        #region 版本号，序列号及备注区操作
        [DllImport("mwReader.dll", EntryPoint = "mwDevGetSerialNumber", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwDevGetSerialNumber(IntPtr handle, StringBuilder strSerialNumber);

        [DllImport("mwReader.dll", EntryPoint = "mwDevGetHardwareVer", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwDevGetHardwareVer(IntPtr handle, StringBuilder strHardwareVer);

        [DllImport("mwReader.dll", EntryPoint = "mwDevWriteConfig", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwDevWriteConfig(IntPtr handle, UInt32 offset, UInt32 length, byte[] data);

        [DllImport("mwReader.dll", EntryPoint = "mwDevReadConfig", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwDevReadConfig(IntPtr handle, UInt32 offset, UInt32 length, byte[] data);
        #endregion

        [DllImport("mwReader.dll", EntryPoint = "mwDevLed", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwDevLed(IntPtr handle, byte lcd1, byte lcd2, byte lcd3, byte lcd4);

        [DllImport("mwReader.dll", EntryPoint = "mwDevBeep", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwDevBeep(IntPtr handle, byte beepTimes, byte interval, byte time);

        [DllImport("mwReader.dll", EntryPoint = "mwDevRFControl", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwDevRFControl(IntPtr handle, byte mode);

        [DllImport("mwReader.dll", EntryPoint = "mwDevSetTransferMode", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwDevSetTransferMode(IntPtr handle, byte mode);

        //下载密钥
        [DllImport("mwReader.dll", EntryPoint = "mwKeyPadDownLoadMasterKey", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwKeyPadDownLoadMasterKey(IntPtr icdev, byte MKeyNo, byte[] MasterKey);

        [DllImport("mwReader.dll", EntryPoint = "mwKeyPadDownLoadWorkkey", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwKeyPadDownLoadWorkkey(IntPtr icdev, byte MKeyNo, byte WKeyNo, byte[] Workkey);
        [DllImport("mwReader.dll", EntryPoint = "mwKeyPadActiveKey", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwKeyPadActiveKey(IntPtr icdev, byte WKeyNo);

        //磁条卡
        [DllImport("mwReader.dll", EntryPoint = "mwReadMagCard", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwReadMagCard(IntPtr handle, byte ctime, out MagCardData pMagCardData);

        #region LCD显示屏操作
        [DllImport("mwReader.dll", EntryPoint = "mwLcdDispInfo", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwLcdDispInfo(IntPtr icdev, byte line, byte offset, byte[] data);

        [DllImport("mwReader.dll", EntryPoint = "mwLcdClear", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwLcdClear(IntPtr icdev, byte line);

        [DllImport("mwReader.dll", EntryPoint = "mwLcdCtlBackLight", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwLcdCtlBackLight(IntPtr icdev, byte flag);
        #endregion

        #region 密码键盘
        /// <summary>
        /// 设置密码键盘ID号，16字节长度的16进制字符串
        /// </summary>
        /// <param name="icdev"></param>
        /// <param name="strID"></param>
        /// <returns></returns>
        [DllImport("mwReader.dll", EntryPoint = "mwPassGetInput", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwKeyPadSetId(IntPtr icdev, byte[] strID);

        /// <summary>
        /// 获取密码键盘ID号，至少分配17字节的存储空间
        /// </summary>
        /// <param name="icdev"></param>
        /// <param name="strID"></param>
        /// <returns></returns>
        [DllImport("mwReader.dll", EntryPoint = "mwPassGetInput", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwKeyPadGetId(IntPtr icdev, byte[] strID);

        /// <summary>
        /// 选择密码键盘工作模式，0-DES 2-3DES
        /// </summary>
        /// <param name="icdev"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        [DllImport("mwReader.dll", EntryPoint = "mwPassGetInput", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwKeyPadSetMode(IntPtr icdev, byte mode);

        /// <summary>
        /// 下载主密钥16/32/48
        /// </summary>
        /// <param name="icdev"></param>
        /// <param name="MasterKey"></param>
        /// <returns></returns>
        [DllImport("mwReader.dll", EntryPoint = "mwPassGetInput", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwKeyPadDownLoadMasterKey(IntPtr icdev, byte[] MasterKey);

        /// <summary>
        /// 下载工作密钥16/32/48
        /// </summary>
        /// <param name="icdev"></param>
        /// <param name="Workkey"></param>
        /// <returns></returns>
        [DllImport("mwReader.dll", EntryPoint = "mwPassGetInput", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwKeyPadDownLoadWorkkey(IntPtr icdev, byte[] Workkey);

        /// <summary>
        /// 获得用户键盘密码的输入,该函数会一直等待用户输入直到超时
        /// </summary>
        /// <param name="icdev"></param>
        /// <param name="ctime">等待用户按键输入的超时时间，以second为单位；最大255s，超过该时间退出. 如果为0 则表示不启用超时。</param>
        /// <param name="cmd">
        /// 语音提示
        ///         0x00-不提示
        ///         0x01-请输入密码,同时LCD显示提示信息
        ///         0x02-请重新输入密码,同时LCD显示提示信息
        ///         0x03-请输入旧密码,同时LCD显示提示信息
        ///         0x04-请输入新密码 ,同时LCD显示提示信息
        /// </param>
        /// <param name="passwordLen">指定用户要输入的密码长度，当用户输入足够的长度密码键盘会直接返回。如果为0，则表示等待用户确认键</param>
        /// <param name="cpass">输入的密码，如果没有下载主密钥/工作密钥并激活，则显示的是明文，否则显示的是加密后的密文数据(16进制字符串格式)</param>
        /// <returns>密码位数（根据客户输入而定）</returns>
        /// <!--该接口默认在第2行显示提示信息，在第3行显示*号，如果用户想要指定密文所在行数请参考mwPassGetInputU()-->
        [DllImport("mwReader.dll", EntryPoint = "mwPassGetInputExt", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwPassGetInput(IntPtr icdev, byte ctime, byte cmd, byte passwordLen, byte[] cpass);

        /// <summary>
        /// 获得用户键盘密码的输入,该函数会一直等待用户输入直到超时
        /// </summary>
        /// <param name="icdev"></param>
        /// <param name="ctime">等待用户按键输入的超时时间，以second为单位；最大255s，超过该时间退出. 如果为0 则表示不启用超时。</param>
        /// <param name="lineStar">*号所在的行,取值1-4 </param>
        /// <param name="passwordLen">指定用户要输入的密码长度，当用户输入足够的长度密码键盘会直接返回。如果为0，则表示等待用户确认键</param>
        /// <param name="cpass">输入的密码，如果没有下载主密钥/工作密钥并激活，则显示的是明文，否则显示的是加密后的密文数据(16进制字符串格式)</param>
        /// <returns>密码位数（根据客户输入而定）</returns>
        /// <!--该接口不控制语音以及提示信息的显示,用户需自行调用 mwLcdDispInfo()和 mwVoiceControl()-->
        [DllImport("mwReader.dll", EntryPoint = "mwPassGetInput", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwPassGetInputU(IntPtr icdev, byte ctime, byte lineStar, byte passwordLen, byte[] cpass);

        /// <summary>
        /// 进入获取键盘密码的状态,进入该状态后只接收 mwPassGet 和 mwPassCancel 命令.
        /// </summary>
        /// <param name="icdev"></param>
        /// <param name="ctime">ctime 等待用户按键输入的超时时间，以second为单位；最大255s，最小1s；超过该时间退出.</param>
        /// <param name="lineStar">*号所在的行,取值1-4 </param>
        /// <param name="passwordLen">指定用户要输入的密码长度，当用户输入足够的长度密码键盘会直接返回。</param>
        /// <returns> >=0	正确;	&lt;0	错误	</returns>
        [DllImport("mwReader.dll", EntryPoint = "mwPassIn", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwPassIn(IntPtr icdev, byte ctime, byte lineStar, byte passwordLen);

        /// <summary>
        /// 查询和获取输入的密码
        /// </summary>
        /// <param name="icdev"></param>
        /// <param name="cpass">输入的密码，如果没有下载主密钥/工作密钥并激活，则显示的是明文，否则显示的是加密后的密文数据(16进制字符串格式)</param>
        /// <returns>
        ///     0x00， 成功取得密码，cpass 为加密后的密文密码,rlen 为加密后的密文密码长度
        ///     -0X0031，用户取消密码输入
        ///     -0X0032，用户密码输入操作超时
        ///     -0X0033，未处于密码输入状态
        ///     -0X0034，用户输入密码还未完成，返回按键个数、*号串
        ///</returns>
        ///<!---0X0034 这个返回值很重要,在开始查询中会一直遇到,表示输入还没有完成,可以再次执行mwPassGet函数来获取密码-->
        [DllImport("mwReader.dll", EntryPoint = "mwPassGet", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwPassGet(IntPtr icdev, byte[] cpass);

        /// <summary>
        /// 取消键盘密码的状态,执行后设备恢复普通状态
        /// </summary>
        /// <param name="icdev"></param>
        /// <returns>>=0	正确;	&lt;0	错误</returns>
        [DllImport("mwReader.dll", EntryPoint = "mwPassCancel", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwPassCancel(IntPtr icdev);
        #endregion

        #region  CPU操作
        [DllImport("mwReader.dll", EntryPoint = "mwSmartCardReset", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwSmartCardReset(IntPtr handle, byte slotNumber, byte[] atrInfo);

        [DllImport("mwReader.dll", EntryPoint = "mwSmartCardCommand", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwSmartCardCommand(IntPtr handle, byte slotNumber, byte[] srcData, UInt32 srcLen, byte[] dstInfo);

        [DllImport("mwReader.dll", EntryPoint = "mwSmartCardReset_HEX", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwSmartCardReset_HEX(IntPtr handle, byte slotNumber, StringBuilder atrInfo);

        [DllImport("mwReader.dll", EntryPoint = "mwSmartCardCommand_HEX", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwSmartCardCommand_HEX(IntPtr handle, byte slotNumber, string srcData, StringBuilder dstInfo);

        [DllImport("mwReader.dll", EntryPoint = "mwSmartCardPowerDown", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwSmartCardPowerDown(IntPtr handle, byte slotNumber);
        #endregion

        #region mifare卡操作
        /// <summary>
        /// 打开非接触卡片
        /// </summary>
        /// <param name="icdev">通讯设备句柄</param>
        /// <param name="openMode">
        /// 打开模式：
        ///	    0x00	STD模式,只能寻到空闲状态下的卡片，被激活或停活（Halt）的卡片不会响应
        ///	    0x01	ALL模式,能寻到空闲状态和已经被停活（Halt）的卡片
        /// </param>
        /// <param name="cardType">卡片类型 </param>
        /// <param name="cardSak">卡片SAK</param>
        /// <param name="cardUid">卡片序列号,格式为byte数组</param>
        /// <returns> >=0	正确;	&lt;0	错误	</returns>
        [DllImport("mwReader.dll", EntryPoint = "mwOpenCard", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwOpenCard(IntPtr icdev, byte openMode, byte[] cardUid);

        //获取卡片状态
        [DllImport("mwReader.dll", EntryPoint = "mwGetCardStatus", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwGetCardStatus(IntPtr icdev, byte slotNumber, out int status);

        /// <summary>
        /// 将选定的卡片置于HALT模式，需要Request All将其唤醒
        /// </summary>
        /// <param name="icdev">通讯设备句柄</param>
        /// <returns> >=0	正确;	&lt;0	错误</returns>
        [DllImport("mwReader.dll", EntryPoint = "mwHalt", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwHalt(IntPtr icdev);

        /// <summary>
        /// 请求卡TYPE A 类型卡片
        /// </summary>
        /// <param name="icdev">通讯设备句柄</param>
        /// <param name="openMode">
        /// 打开模式
        ///	    0x00	STD模式,只能寻到空闲状态下的卡片，被激活或停活（Halt）的卡片不会响应
        ///	    0x01	ALL模式,能寻到空闲状态和已经被停活（Halt）的卡片
        /// </param>
        /// <param name="cardType">
        /// 卡片类型
        ///     Mifare 标准 1k: 0x0004
        ///		Mifare 标准 4k: 0x0002
        ///		Mifare Light: 0x0010
        ///		Mifare UtraLight: 0x0044
        ///		CPU :0x0008
        /// </param>
        /// <returns> >=0	正确;	&lt;0	错误</returns>
        [DllImport("mwReader.dll", EntryPoint = "mwRequest", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwRequest(IntPtr icdev, byte openMode, out UInt16 cardType);

        /// <summary>
        /// 防冲突
        /// </summary>
        /// <param name="icdev">通讯设备句柄</param>
        /// <param name="cardUid">卡片序列号,格式为byte数组</param>
        /// <returns>卡号长度,如果返回值小于0表示错误，请查看错误代码表</returns>
        [DllImport("mwReader.dll", EntryPoint = "mwAnticoll", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwAnticoll(IntPtr icdev, byte[] cardUid);

        /// <summary>
        /// 选卡
        /// </summary>
        /// <param name="icdev">通讯设备句柄</param>
        /// <param name="cardUid">卡片序列号,格式为byte数组</param>
        /// <param name="idLen">卡号长度</param>
        /// <param name="cardSak">卡片SAK</param>
        /// <returns> >=0	正确;	&lt;0	错误</returns>
        [DllImport("mwReader.dll", EntryPoint = "mwSelect", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwSelect(IntPtr icdev, byte[] cardUid, UInt32 idLen, out byte cardSak);

        /// <summary>
        /// 获取TYPE-A类型的智能卡复位信息
        /// </summary>
        /// <param name="icdev">通讯设备句柄</param>
        /// <param name="atrInfo">卡片复位信息,格式为byte数组 </param>
        /// <returns>复位信息长度,如果返回值小于0表示错误，请查看错误代码表	</returns>
        [DllImport("mwReader.dll", EntryPoint = "mwRats", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwRats(IntPtr icdev, byte[] atrInfo);

        /// <summary>
        /// 验证密码
        /// </summary>
        /// <param name="icdev">通讯设备句柄</param>
        /// <param name="mode">
        /// 密码类型：	
        ///	    0x00	验证A密码
        ///	    0x01	验证B密码
        ///	    0x10	验证加载的A密码
        ///	    0x11	验证加载的B密码
        /// </param>
        /// <param name="sectorNo">要验证的扇区号</param>
        /// <param name="key">6字节长度的密码，格式为byte数组</param>
        /// <returns> >=0	正确;	&lt;0	错误</returns>
        [DllImport("mwReader.dll", EntryPoint = "mwMifareAuth", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwMifareAuth(IntPtr icdev, byte mode, UInt32 sectorNo, byte[] key);

        /// <summary>
        /// 以字符串验证密码
        /// </summary>
        /// <param name="icdev">通讯设备句柄</param>
        /// <param name="mode">
        /// 密码类型：	
        ///	    0x00	验证A密码
        ///	    0x01	验证B密码
        ///	    0x10	验证加载的A密码
        ///	    0x11	验证加载的B密码
        /// </param>
        /// <param name="sectorNo">要验证的扇区号</param>
        /// <param name="key">以'\0'为结尾的16进制字符串，密码长度应为12。</param>
        /// <returns> >=0	正确;	&lt;0	错误</returns>
        [DllImport("mwReader.dll", EntryPoint = "mwMifareAuthHex", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwMifareAuthHex(IntPtr icdev, byte mode, UInt32 sectorNo, string strKey);

        /// <summary>
        /// 读数据
        /// </summary>
        /// <param name="icdev">通讯设备句柄</param>
        /// <param name="blockNo">要读取的块号，对于S50卡，取值为0～63;对于S70卡，取值为0～255;</param>
        /// <param name="blockData">读取的数据，mifare卡每块数据共16字节。</param>
        /// <returns>数据长度,如果返回值小于0表示错误，请查看错误代码表</returns>
        [DllImport("mwReader.dll", EntryPoint = "mwMifareRead", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwMifareRead(IntPtr icdev, UInt32 blockNo, byte[] blockData);

        /// <summary>
        /// 写数据
        /// </summary>
        /// <param name="icdev">通讯设备句柄</param>
        /// <param name="blockNo">要读取的块号，对于S50卡，取值为0～63;对于S70卡，取值为0～255;</param>
        /// <param name="blockData">要写入的数据。</param>
        /// <returns> >=0	正确;	&lt;0	错误</returns>
        [DllImport("mwReader.dll", EntryPoint = "mwMifareWrite", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwMifareWrite(IntPtr icdev, UInt32 blockNo, byte[] blockData);

        /// <summary>
        /// 将数据块初始化为值存储区	 
        /// </summary>
        /// <param name="icdev">通讯设备句柄</param>
        /// <param name="blockNo">块号</param>
        /// <param name="initValue">写入的值</param>
        /// <returns> >=0	正确;	&lt;0	错误</returns>
        [DllImport("mwReader.dll", EntryPoint = "mwMifareInitVal", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwMifareInitVal(IntPtr icdev, UInt32 blockNo, UInt32 initValue);

        /// <summary>
        /// 读值
        /// </summary>
        /// <param name="icdev">通讯设备句柄</param>
        /// <param name="blockNo">块号</param>
        /// <param name="value">读取的数值</param>
        /// <returns> >=0	正确;	&lt;0	错误</returns>
        [DllImport("mwReader.dll", EntryPoint = "mwMifareReadVal", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwMifareReadVal(IntPtr icdev, UInt32 blockNo, out UInt32 value);

        /// <summary>
        /// 增值
        /// </summary>
        /// <param name="icdev">通讯设备句柄</param>
        /// <param name="blockNo">块号</param>
        /// <param name="value">增加的数值</param>
        /// <returns> >=0	正确;	&lt;0	错误</returns>
        [DllImport("mwReader.dll", EntryPoint = "mwMifareIncrement", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwMifareIncrement(IntPtr icdev, UInt32 blockNo, UInt32 value);

        /// <summary>
        /// 减值
        /// </summary>
        /// <param name="icdev">通讯设备句柄</param>
        /// <param name="blockNo">块号</param>
        /// <param name="value">减少 的数值</param>
        /// <returns> >=0	正确;	&lt;0	错误</returns>
        [DllImport("mwReader.dll", EntryPoint = "mwMifareDecrement", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwMifareDecrement(IntPtr icdev, UInt32 blockNo, UInt32 value);

        /// <summary>
        /// 加载密码
        /// </summary>
        /// <param name="icdev">通讯设备句柄</param>
        /// <param name="mode">
        /// 密码类型：	
        ///	    0x00	加载A密码
        ///	    0x01	加载B密码
        /// </param>
        /// <param name="sectorNo">要验证的扇区号</param>
        /// <param name="key">6字节长度的密码，格式为byte数组</param>
        /// <returns> >=0	正确;	&lt;0	错误</returns>
        [DllImport("mwReader.dll", EntryPoint = "mwMifareDecrement", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwMifareLoadKey(IntPtr icdev, byte mode, UInt32 sectorNo, byte[] key);
        #endregion

        /// <summary>
        /// 选择卡片
        /// </summary>
        /// <param name="icdev">通讯设备句柄</param>
        /// <param name="memoryCardType">存储卡类型标志，请参考存储卡类型表</param>
        /// <returns> >=0	正确;	&lt;0	错误	</returns>
        [DllImport("mwReader.dll", EntryPoint = "mwSelectMemoryCard", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwSelectMemoryCard(IntPtr icdev, byte memoryCardType);

        #region SLE4442卡操作
        /// <summary>
        /// 读4442卡
        /// </summary>
        /// <param name="icdev">通讯设备句柄</param>
        /// <param name="offset">偏移地址，其值范围0～255</param>
        /// <param name="length">字符串长度，其值范围1～256</param>
        /// <param name="data">读出的数据</param>
        /// <returns> >=0	正确;	&lt;0	错误	</returns>
        [DllImport("mwReader.dll", EntryPoint = "mwRead4442", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwRead4442(IntPtr icdev, UInt32 offset, UInt32 length, byte[] data);

        /// <summary>
        /// 写4442卡
        /// </summary>
        /// <param name="icdev">通讯设备句柄</param>
        /// <param name="offset">偏移地址，其值范围0～255</param>
        /// <param name="length">字符串长度，其值范围1～256</param>
        /// <param name="data">写入的数据</param>
        /// <returns>>=0	正确;	&lt;0	错误	</returns>
        [DllImport("mwReader.dll", EntryPoint = "mwWrite4442", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwWrite4442(IntPtr icdev, UInt32 offset, UInt32 length, byte[] data);

        /// <summary>
        /// 读4442/4432保护位,字节地址0～31为保护区，共32个字节，每个字节用1 bit的保护位来标志是否被置保护，为0表示已置保护，为1表示未置保护。已置的保护位不可恢复，被保护的数据只可读，不可更改，成为固化数据。
        /// </summary>
        /// <param name="icdev">通讯设备句柄</param>
        /// <param name="offset">偏移地址，其值范围0～31</param>
        /// <param name="length">字符串长度，其值范围1～32</param>
        /// <param name="pData">读出保护位标志，</param>
        /// <returns>数据长度,如果返回值小于0表示错误，请查看错误代码表</returns>
        [DllImport("mwReader.dll", EntryPoint = "mwPRead4442", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwPRead4442(IntPtr icdev, UInt32 offset, UInt32 length, byte[] pData);

        /// <summary>
        /// 4442/4432卡校验数据并写保护
        /// </summary>
        /// <param name="icdev">通讯设备句柄</param>
        /// <param name="offset">偏移地址，其值范围0～31</param>
        /// <param name="length">字符串长度，其值范围1～32</param>
        /// <param name="data">保护数据，必须和卡中已存在的数据一致</param>
        /// <returns> >=0	正确;	&lt;0	错误</returns>
        [DllImport("mwReader.dll", EntryPoint = "mwPWrite4442", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwPWrite4442(IntPtr icdev, UInt32 offset, UInt32 length, byte[] data);

        /// <summary>
        /// 校验密码，4442卡密码长度为3字节.密码核对正确前，全部数据只可读，不可改写。
        /// </summary>
        /// <param name="icdev">通讯设备句柄</param>
        /// <param name="key">密码</param>
        /// <returns> >=0	正确;	&lt;0	错误	</returns>
        [DllImport("mwReader.dll", EntryPoint = "mwVerifyPassword4442", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwVerifyPassword4442(IntPtr icdev, byte[] key);

        /// <summary>
        /// 读密码，4442卡密码长度为3字节
        /// </summary>
        /// <param name="icdev">通讯设备句柄</param>
        /// <param name="key">密码</param>
        /// <returns>密码长度,如果返回值小于0表示错误，请查看错误代码表</returns>
        [DllImport("mwReader.dll", EntryPoint = "mwReadPassword4442", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwReadPassword4442(IntPtr icdev, byte[] key);

        /// <summary>
        /// 修改密码，4442卡密码长度为3字节
        /// </summary>
        /// <param name="icdev">通讯设备句柄</param>
        /// <param name="key">新密码</param>
        /// <returns> >=0	正确;	&lt;0	错误	</returns>
        [DllImport("mwReader.dll", EntryPoint = "mwChangePassword4442", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwChangePassword4442(IntPtr icdev, byte[] key);

        /// <summary>
        /// 读取错误计数错误计数器，初始值为3，密码核对出错1次，便减1，若计数器值为0,
        /// 则卡自动锁死，数据只可读出，不可再进行更改也无法再进行密码核对；
        /// 若不为零时，有一次密码核对正确，可恢复到初始值3
        /// </summary>
        /// <param name="icdev">通讯设备句柄</param>
        /// <param name="counter">错误计数器的值</param>
        /// <returns></returns>
        [DllImport("mwReader.dll", EntryPoint = "mwGetErrorCounter4442", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwGetErrorCounter4442(IntPtr icdev, out int counter);
        #endregion

        #region SLE4428卡操作
        /// <summary>
        /// 读4428/4418卡， 其容量为1024字节
        /// </summary>
        /// <param name="icdev">通讯设备句柄</param>
        /// <param name="offset">偏移地址，其值范围0～1023</param>
        /// <param name="length">length: 字符串长度，其值范围1～1024</param>
        /// <param name="data">读出数据</param>
        /// <returns>数据长度,如果返回值小于0表示错误，请查看错误代码表</returns>
        [DllImport("mwReader.dll", EntryPoint = "mwRead4428", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwRead4428(IntPtr icdev, UInt32 offset, UInt32 length, byte[] data);

        /// <summary>
        /// 写4428/4418卡， 其容量为1024字节
        /// </summary>
        /// <param name="icdev">通讯设备句柄</param>
        /// <param name="offset">偏移地址，其值范围0～1023</param>
        /// <param name="length">字符串长度，其值范围1～1024</param>
        /// <param name="data">写入的数据</param>
        /// <returns> >=0	正确;	&lt;0	错误	</returns>
        [DllImport("mwReader.dll", EntryPoint = "mwWrite4428", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwWrite4428(IntPtr icdev, UInt32 offset, UInt32 length, byte[] data);

        /// <summary>
        /// 读4428/4418数据以及保护位
        /// </summary>
        /// <param name="icdev">通讯设备句柄</param>
        /// <param name="offset">偏移地址，其值范围0～31</param>
        /// <param name="length">字符串长度，其值范围1～32</param>
        /// <param name="data">存放要读出的数据,其大小应为2*length </param>
        /// <returns></returns>
        /// <example>
        /// unsigned char databuff[4];
        /// st=mwPReadData4428(icdev,0,2,databuff);
        /// 从偏移地址0开始带保护位读出2个字节数据放入databuff中，每个字节的后面跟一个保护位标志字节，该字节值为0x00表示,相应的字节已保护，0xff表示未被保护。
        /// 例如：读出
        /// databuff[0]=0x13,databuff[1]=0x00,
        /// databuff[2]=0x20,databuff[3]=0xff;
        /// 表示偏移地址0字节被保护，偏移地址1字节未被保护。
        /// </example>
        [DllImport("mwReader.dll", EntryPoint = "mwPReadData4428", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwPReadData4428(IntPtr icdev, UInt32 offset, UInt32 length, byte[] data);

        /// <summary>
        /// 428/4418卡校验数据并写保护
        /// </summary>
        /// <param name="icdev">通讯设备句柄</param>
        /// <param name="offset">偏移地址，其值范围0～1023</param>
        /// <param name="length">字符串长度，其值范围1～1024</param>
        /// <param name="data">保护数据，必须和卡中已存在的数据一致</param>
        /// <returns> >=0	正确;	&lt;0	错误	</returns>
        [DllImport("mwReader.dll", EntryPoint = "mwPWrite4428", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwPWrite4428(IntPtr icdev, UInt32 offset, UInt32 length, byte[] data);

        /// <summary>
        /// 4428/4418卡写数据并置保护
        /// </summary>
        /// <param name="icdev">通讯设备句柄</param>
        /// <param name="offset">偏移地址，其值范围0～1023</param>
        /// <param name="length">字符串长度，其值范围1～1024</param>
        /// <param name="data">要写入的数据</param>
        /// <returns>数据长度,如果返回值小于0表示错误，请查看错误代码表</returns>
        [DllImport("mwReader.dll", EntryPoint = "mwPWriteData4428", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwPWriteData4428(IntPtr icdev, UInt32 offset, UInt32 length, byte[] data);

        /// <summary>
        /// 校验密码，4428卡密码长度为2字节.密码核对正确前，全部数据只可读，不可改写。核对密码正确后可以更改数据，包括密码再内。
        /// </summary>
        /// <param name="icdev">通讯设备句柄</param>
        /// <param name="key">密码</param>
        /// <returns> >=0	正确;	&lt;0	错误	 </returns>
        [DllImport("mwReader.dll", EntryPoint = "mwVerifyPassword4428", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwVerifyPassword4428(IntPtr icdev, byte[] key);

        /// <summary>
        /// 读密码，4428卡密码长度为2字节
        /// </summary>
        /// <param name="icdev">通讯设备句柄</param>
        /// <param name="key">密码</param>
        /// <returns>密码长度,如果返回值小于0表示错误，请查看错误代码表</returns>
        [DllImport("mwReader.dll", EntryPoint = "mwReadPassword4428", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwReadPassword4428(IntPtr icdev, byte[] key);

        /// <summary>
        /// 修改密码，4428卡密码长度为2字节
        /// </summary>
        /// <param name="icdev">通讯设备句柄</param>
        /// <param name="key">新密码</param>
        /// <returns> >=0	正确;	&lt;0	错误	</returns>
        [DllImport("mwReader.dll", EntryPoint = "mwChangePassword4428", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwChangePassword4428(IntPtr icdev, byte[] key);

        /// <summary>
        /// 读取错误计数错误计数器，初始值为8，密码核对出错1次，便减1，若计数器值为0
        /// 则卡自动锁死，数据只可读出，不可再进行更改也无法再进行密码核对；
        /// 若不为零时，有一次密码核对正确，可恢复到初始值8
        /// </summary>
        /// <param name="icdev">通讯设备句柄</param>
        /// <param name="counter">密码错误计数器的值</param>
        /// <returns> >=0	正确;	&lt;0	错误	</returns>
        [DllImport("mwReader.dll", EntryPoint = "mwGetErrorCounter4428", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwGetErrorCounter4428(IntPtr icdev, out int counter);
        #endregion

        #region 24C系列卡片操作
        /// <summary>
        /// 读24CXX系列卡片
        /// </summary>
        /// <param name="icdev">通讯设备句柄</param>
        /// <param name="offset">偏移地址</param>
        /// <param name="length">数据长度</param>
        /// <param name="data">数据缓冲区</param>
        /// <returns> >=0	正确;	&lt;0	错误	</returns>
        /// <!--
        /// AT24CXX卡（XX为01A、02、04、08、16、32、64、128、256）是XXKb位的非加密存储器卡，只存在读、写两种操作。
        ///                 AT24C01A 卡容量: 01 * 1024/8 = 256 字节
        ///                 AT24C02 卡容量: 02 * 1024/8 = 512 字节
        ///                 AT24C04 卡容量: 04 * 1024/8 = 1024 字节
        ///                 AT24C08 卡容量: 08 * 1024/8 = 2024 字节
        ///                 AT24C16 卡容量: 16 * 1024/8 = 4096 字节
        ///                 AT24C32 卡容量: 32 * 1024/8 = 8192 字节
        ///                 AT24C64 卡容量: 64 * 1024/8 = 16384 字节
        ///                 AT24C128 卡容量: 128 * 1024/8 = 32768 字节
        ///                 AT24C256 卡容量: 256 * 1024/8 = 65536 字节
        /// -->
        [DllImport("mwReader.dll", EntryPoint = "mwRead24CXX", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwRead24CXX(IntPtr icdev, UInt32 offset, UInt32 length, byte[] data);

        /// <summary>
        /// 写24CXX系列卡片&lt;0	错误
        /// </summary>
        /// <param name="icdev">通讯设备句柄</param>
        /// <param name="offset">偏移地址</param>
        /// <param name="length">数据长度</param>
        /// <param name="data">数据缓冲区</param>
        /// <returns> >=0	正确;	&lt;0	错误	</returns>
        /// <remarks>
        /// AT24CXX卡（XX为01A、02、04、08、16、32、64、128、256）是XXKb位的非加密存储器卡，只存在读、写两种操作。
        ///                 AT24C01A 卡容量: 01 * 1024/8 = 256 字节
        ///                 AT24C02 卡容量: 02 * 1024/8 = 512 字节
        ///                 AT24C04 卡容量: 04 * 1024/8 = 1024 字节
        ///                 AT24C08 卡容量: 08 * 1024/8 = 2024 字节
        ///                 AT24C16 卡容量: 16 * 1024/8 = 4096 字节
        ///                 AT24C32 卡容量: 32 * 1024/8 = 8192 字节
        ///                 AT24C64 卡容量: 64 * 1024/8 = 16384 字节
        ///                 AT24C128 卡容量: 128 * 1024/8 = 32768 字节
        ///                 AT24C256 卡容量: 256 * 1024/8 = 65536 字节
        /// </remarks>
        [DllImport("mwReader.dll", EntryPoint = "mwWrite24CXX", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwWrite24CXX(IntPtr icdev, UInt32 offset, UInt32 length, byte[] data);
        #endregion

        #region 工具方法
        /// <summary>
        /// 将一个byte数据转化为2个字节的ASCII.例如:输入数据为0X3a,则转化后的数据为0x33,0x41,即字符串"3A"
        /// </summary>
        /// <param name="src">要被转换的数据</param>
        /// <param name="dst">保存转换后的16进制ASCII字符串,该存储空间至少是 srcLen*2+1 个字节的长度</param>
        /// <param name="srcLen">src长度</param>
        /// <returns>转换后的字符串长度,如果返回值小于0表示错误,请查看错误代码表</returns>
        [DllImport("mwReader.dll", EntryPoint = "BinToHex", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 BinToHex(byte[] src, StringBuilder dst, UInt32 srcLen);

        /// <summary>
        /// 将16进制ASCII字符串转化为byte数组。例如：输入数据为"32"，则转化后的数据为0x32
        /// </summary>
        /// <param name="src">要被转换的16进制ASCII字符串</param>
        /// <param name="dst">保存转换后的byte数组。</param>
        /// <param name="srcLen">src长度,srcLen为0取字符串实际长度</param>
        /// <returns>转换后的字节长度,如果返回值小于0表示错误，请查看错误代码表</returns>
        /// <remarks>函数具有较强的容错性，它将忽略16进制字符串中的所有非法字符</remarks>
        [DllImport("mwReader.dll", EntryPoint = "HexToBin", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 HexToBin(string src, byte[] dst, UInt32 srcLen);

        [DllImport("mwReader.dll", EntryPoint = "mwEntrdes_HEX", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 mwEntrdes_HEX(byte flag, byte[] key, byte[] ptrsource, byte[] ptrdest);
        #endregion

        #endregion
    }
}
