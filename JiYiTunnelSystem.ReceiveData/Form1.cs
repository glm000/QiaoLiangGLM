using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace JiYiTunnelSystem.ReceiveData
{
    public partial class Form1 : Form
    {
        //错误列表
        //错误代码：000X1，开启程序出错
        //错误代码：000X2，接受客户连接出错
        //错误代码：000X3，Textbox显示错误
        //错误代码：000X4，定时清空Textbox显示信息出错

        public delegate void Delegate_Clear();//定义委托类型
        public delegate void Delegate_Send(string str);
        public delegate void Delegate_OpenFile();
        public delegate void Delegate_SendFile();
        //List<Socket> socketList = new List<Socket>();
        Dictionary<string, Socket> socketItems = new Dictionary<string, Socket>();
        Dictionary<string, string> nameItems = new Dictionary<string, string>();
        string path = "";// 文件路径
        //static AutoResetEvent myEvent = new AutoResetEvent(false);
        public Form1()
        {
            InitializeComponent();
            IPList.SelectedIndex = 0;
            COMList.SelectedIndex = 0;
            Btn_Send.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Interval = 1000 * 3600*2;//一个小时执行一次
            timer1.Start();

            timer2.Enabled = true;
            timer2.Interval = 1000 * 3600;
            timer2.Start();
        }

        private void Btn_Start_Click(object sender, EventArgs e)
        {
            if (Btn_Start.Text == "开启")
            {
                Btn_Start.Text = "关闭";
                IPList.Enabled = false;
                COMList.Enabled = false;
                Btn_Send.Enabled = true;
                try
                {
                    Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    IPAddress address = IPAddress.Parse(IPList.Text);
                    IPEndPoint point = new IPEndPoint(address, Convert.ToInt32(COMList.Text));
                    listenSocket.Bind(point);
                    listenSocket.Listen(10);
                    ThreadPool.QueueUserWorkItem(new WaitCallback(AcceptTask), listenSocket);
                }
                catch(Exception ex)
                {
                    AppendTxt("错误代码：000X1 "+ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                }
            }
            else
            {
                try
                {
                    Environment.Exit(0);
                }
                catch (Exception ex)
                {
                    AppendTxt("关闭软件出错！！！\r\n" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                }
            }
        }

        private void AcceptTask(object listenSocket)
        {
            try
            {
                Socket socket = listenSocket as Socket;
                AppendTxt(" 服务器已开启... "+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                while (true)
                {
                    Socket newsocket = socket.Accept();
                    AppendTxt(newsocket.RemoteEndPoint.ToString() + " 连接成功... \r\n"+ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                    ThreadPool.QueueUserWorkItem(new WaitCallback(ReceiveTask), newsocket);
                }
            }
            catch (Exception ex)
            {
                AppendTxt("错误代码：000X2 " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            }
        }

        private void RefreshTargetList(string name,bool save = true)
        {
            try
            {
                if (TargetList.InvokeRequired)
                {
                    TargetList.Invoke(new Action(() =>
                    {
                        if (save)
                        {
                            TargetList.Items.Add(name);
                        }
                        else
                        {
                            TargetList.Items.Remove(name);
                        }
                    }));
                }
            }
            catch(Exception ex)
            {
                AppendTxt("更新目标列表错误！！！\r\n" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            }
        }
        private void ReceiveTask(object socket)
        {
            Socket mysoc = socket as Socket;
            byte[] dataReceive = new byte[1024*1204];
            byte[] dataSend = new byte[1024];

            mysoc.ReceiveTimeout = 1000 * 60 * 10;//接收数据时堵塞10min
            mysoc.SendTimeout = 1000 * 10;
            int len = 0;
            string shuju;
            bool Fight1 = true, Fight2 = false; //Fight1为连接校验位，Fight2为数据校验位

            bool baoLink = true;
            int bao = 0;
            bool addSocket = true;

            bool addSocket_YB_WY = true;

            while (true)
            {
                dataReceive.Initialize();//初始化接收数据数组数据
                dataSend.Initialize();

                try
                {
                    string shujubao = null;
                    len = mysoc.Receive(dataReceive, SocketFlags.None);
                    shuju = BytetoString(dataReceive, len);
                    try
                    {
                        shujubao = shuju.Substring(0, 2);
                    }
                    catch (Exception e)
                    {
                        try
                        {
                            //socketList.Remove(mysoc);
                            if (socketItems.Any(m=>m.Key==mysoc.RemoteEndPoint.ToString()))
                            {
                                socketItems.Remove(mysoc.RemoteEndPoint.ToString());
                                RefreshTargetList(nameItems[mysoc.RemoteEndPoint.ToString()], false);
                                nameItems.Remove(mysoc.RemoteEndPoint.ToString());
                            }
                            AppendTxt("客户端："+mysoc.RemoteEndPoint+"退出... " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                            CloseSocket(mysoc);
                        }
                        catch (Exception es)
                        {
                            continue;//关闭线程出错后，再一次循环，直至关闭线程
                        }
                        return;
                    }

                    if (baoLink)
                    {
                        if (shujubao == "[H")
                        {
                            bao++;
                            if (bao >= 120)
                            {
                                try
                                {
                                    if (mysoc.Connected)
                                    {
                                        dataSend = Encoding.ASCII.GetBytes("[LINKTEST]");
                                        mysoc.Send(dataSend);
                                        //Fight1 = false;
                                        baoLink = false;
                                    }
                                }
                                catch (Exception e)
                                {
                                    AppendTxt("客户端：" + mysoc.RemoteEndPoint + "验证连接返回信息出错\r\n" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                                    continue;
                                }
                            }
                            if (addSocket_YB_WY)
                            {
                                int p1 = shuju.IndexOf("Id");
                                int p2 = shuju.IndexOf("Type");
                                if (p1 > 0 && p2 > 0)
                                {
                                    string name = "";
                                    name = shuju.Substring(p2 + 6, 2) + shuju.Substring(p1 + 4, p2 - 3 - (p1 + 3));
                                    if (nameItems.Any(m => m.Value == name))
                                    {
                                        string endpoint = nameItems.Where(m => m.Value == name).Select(m => m.Key).First();
                                        RefreshTargetList(nameItems[endpoint], false);
                                        AppendTxt("客户端：" + endpoint + "退出...\r\n" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                                        nameItems.Remove(endpoint);
                                        socketItems.Remove(endpoint);
                                    }
                                    socketItems.Add(mysoc.RemoteEndPoint.ToString(), mysoc);
                                    nameItems.Add(mysoc.RemoteEndPoint.ToString(), name);
                                    RefreshTargetList(nameItems[mysoc.RemoteEndPoint.ToString()]);
                                    addSocket_YB_WY = false;
                                    continue;
                                }
                            }
                        }
                        else
                        {
                            bao = 0;
                            Fight1 = true;
                            if (shuju == "[LINKOK]")
                            {
                                AppendTxt(string.Format("客户端：{0}数据成功接收\r\n{1}" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), mysoc.RemoteEndPoint, shuju));
                                continue;
                            }
                            if (shuju == "[SORTOK]")
                            {
                                AppendTxt(string.Format("客户端：{0}数据成功接收\r\n{1}" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), mysoc.RemoteEndPoint, shuju));
                                continue;
                            }
                            if (shuju == "[Link_Req]")
                            {
                                AppendTxt(string.Format("客户端：{0}数据成功接收\r\n{1}" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), mysoc.RemoteEndPoint, shuju));
                                //告诉设备是否连接上
                                try
                                {
                                    if (mysoc.Connected)
                                    {
                                        dataSend = Encoding.ASCII.GetBytes("[Link_Ack]");
                                        mysoc.Send(dataSend);
                                        Fight1 = true;
                                    }
                                    continue;
                                }
                                catch (Exception es)
                                {
                                    AppendTxt("客户端：" + mysoc.RemoteEndPoint + "验证连接返回信息出错\r\n" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                                    continue;
                                }
                            }
                            if (shuju == "[LINK_REQ]")//刚连接上时，设备发送[LINK_REQ]
                            {
                                AppendTxt(string.Format("客户端：{0}数据成功接收\r\n{1}" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), mysoc.RemoteEndPoint, shuju));
                                //告诉设备是否连接上
                                try
                                {
                                    if (mysoc.Connected)
                                    {
                                        dataSend = Encoding.ASCII.GetBytes("[LINK_ACK]");
                                        mysoc.Send(dataSend);
                                        Fight1 = true;
                                    }
                                }
                                catch (Exception es)
                                {
                                    AppendTxt("客户端："+mysoc.RemoteEndPoint+"验证连接返回信息出错\r\n" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                                    continue;
                                }
                            }
                            else
                            {   //已经验证连接上了
                                if (len <= 0)
                                {
                                    //已经断开连接
                                    try
                                    {
                                        //socketList.Remove(mysoc);
                                        if (socketItems.Any(m => m.Key == mysoc.RemoteEndPoint.ToString()))
                                        {
                                            socketItems.Remove(mysoc.RemoteEndPoint.ToString());
                                            RefreshTargetList(nameItems[mysoc.RemoteEndPoint.ToString()], false);
                                            nameItems.Remove(mysoc.RemoteEndPoint.ToString());
                                        }
                                        AppendTxt("客户端：" + mysoc.RemoteEndPoint + "退出...\r\n" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                                        CloseSocket(mysoc);
                                    }
                                    catch (Exception es)
                                    {
                                        continue;//关闭线程出错后，再一次循环，直至关闭线程
                                    }
                                    return;
                                }
                                else
                                {
                                    try
                                    {
                                        if (addSocket)
                                        {
                                            string name = "";
                                            if (shuju.Substring(1, 3) == "ZD1")
                                            {
                                                name = "2斜井+ZD1";
                                            }
                                            else if(shuju.Substring(1, 3) == "ZD2")
                                            {
                                                name = "2正洞+ZD2";
                                            }
                                            else if(shuju.Substring(1, 3) == "ZD3")
                                            {
                                                name = "4斜井+ZD3";
                                            }
                                            else if(shuju.Substring(1, 3) == "ZD4")
                                            {
                                                name = "4正洞+ZD4";
                                            }
                                            if (!string.IsNullOrEmpty(name))
                                            {
                                                if (nameItems.Any(m => m.Value == name))
                                                {
                                                    string endpoint = nameItems.Where(m => m.Value == name).Select(m => m.Key).First();
                                                    RefreshTargetList(nameItems[endpoint], false);
                                                    AppendTxt("客户端：" + endpoint + "退出...\r\n" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                                                    nameItems.Remove(endpoint);
                                                    socketItems.Remove(endpoint);
                                                }
                                                socketItems.Add(mysoc.RemoteEndPoint.ToString(), mysoc);
                                                nameItems.Add(mysoc.RemoteEndPoint.ToString(), name);
                                                RefreshTargetList(nameItems[mysoc.RemoteEndPoint.ToString()]);
                                                addSocket = false;
                                                continue;
                                            }
                                        }                                        
                                        //shuju = HexStringToJson(shuju);// 如果是十六进制字符串则转为Json格式，否则返回原字符串
                                        try
                                        {
                                            if (shuju.Substring(1, 2) == "ZD")
                                            {
                                                List<string> list = new List<string>(shuju.Split(']'));
                                                string res = "";
                                                for (int i = 0; i < list.Count; i++)
                                                {
                                                    byte[] dec = dataReceive.Skip(i * (list[i].Length + 1)).Take(list[i].Length).ToArray();
                                                    try
                                                    {

                                                        double data1 = 0.0, data2 = 0.0, data3 = 0.0, data4 = 0.0, data5 = 0.0;
                                                        if (list[i].Substring(1, 3) == "ZD1")// 2号斜井
                                                        {
                                                            int k = 0;
                                                            try
                                                            {
                                                                for (int j = 59; j < 71; j++)// 12个截面各两个振动节点
                                                                {
                                                                    if (j < 65)
                                                                    {
                                                                        data1 = (dec[4 + k * 4] + dec[5 + k * 4] * 256 - 4000) / 320.0;
                                                                        if (data1 < 0 || data1 > 50)
                                                                            data1 = 0;
                                                                        data2 = (dec[6 + k * 4] + dec[7 + k * 4] * 256 - 4000) / 320.0;
                                                                        if (data2 < 0 || data2 > 50) { data2 = 0; }
                                                                        res += string.Format("{{Id:\"{0:D}\",Type:\"ZD\",D1:\"{1:D}\",D2:\"{2:D}\"}},\r\n", j,
                                                                            data1.ToString("0.00"), data2.ToString("0.00"));
                                                                    }
                                                                    else
                                                                    {
                                                                        data1 = (dec[4 + k * 4] + dec[5 + k * 4] * 256 - 4000) / 80.0;
                                                                        if (data1 < 0 || data1 > 200)
                                                                            data1 = 0;
                                                                        data2 = (dec[6 + k * 4] + dec[7 + k * 4] * 256 - 4000) / 80.0;
                                                                        if (data2 < 0 || data2 > 200) { data2 = 0; }
                                                                        res += string.Format("{{Id:\"{0:D}\",Type:\"ZD\",D1:\"{1:D}\",D2:\"{2:D}\"}},\r\n", j,
                                                                            data1.ToString("0.00"), data2.ToString("0.00"));
                                                                    }
                                                                    k++;
                                                                }
                                                            }
                                                            catch { }
                                                            k = 0;
                                                            try
                                                            {
                                                                for(int j = 16; j < 24; j++)// 正洞的8个
                                                                {
                                                                    data1 = (dec[52 + k * 2] + dec[53 + k * 2] * 256 - 4000) / 320.0;
                                                                    if (data1 < 0 || data1 > 50)// 将小于4000和大于20000的数据剔除掉
                                                                        data1 = 0;
                                                                    res += string.Format("{{Id:\"{0:D}\",Type:\"ZD\",D1:\"{1:D}\"}},\r\n", j, data1.ToString("0.00"));
                                                                    k++;
                                                                }
                                                            }
                                                            catch { }
                                                        }
                                                        else if (list[i].Substring(1, 3) == "ZD2")// 2号正洞
                                                        {
                                                            int k = 0;
                                                            try
                                                            {
                                                                for (int j = 1; j < 31; j++)// 30个截面各一个振动节点,4567不在这一组
                                                                {
                                                                    if (j > 3 && j < 8)// 没装
                                                                    {
                                                                        data1 = ((dec[20+j] + dec[21+j] * 256+dec[22+j]+dec[23+j]*256)/2 - 4000) / 320.0;
                                                                        if (data1 < 0 || data1 > 50)// 将小于4000和大于20000的数据剔除掉
                                                                            data1 = 0;
                                                                        res += string.Format("{{Id:\"{0:D}\",Type:\"ZD\",D1:\"{1:D}\"}},\r\n", j, data1.ToString("0.00"));
                                                                        k++;
                                                                        continue;
                                                                    }
                                                                    if (j > 15 && j < 24)// 装在斜井里
                                                                    {
                                                                        continue;
                                                                    }
                                                                    data1 = (dec[4 + k * 2] + dec[5 + k * 2] * 256 - 4000) / 320.0;
                                                                    if (data1 < 0 || data1 > 50)// 将小于4000和大于20000的数据剔除掉
                                                                        data1 = 0;
                                                                    res += string.Format("{{Id:\"{0:D}\",Type:\"ZD\",D1:\"{1:D}\"}},\r\n", j, data1.ToString("0.00"));
                                                                    k++;
                                                                }
                                                            }
                                                            catch { }
                                                            k = 83;
                                                            try
                                                            {
                                                                data1 = (dec[58] + dec[59] * 256 - 4000) / 800.0;
                                                                if (data1 < 0 || data1 > 20) { data1 = 0; }
                                                                data2 = (dec[60] + dec[61] * 256 - 4000) / 800.0;
                                                                if (data2 < 0 || data2 > 20) { data2 = 0; }
                                                                data3 = (dec[62] + dec[63] * 256 - 4000) / 800.0;
                                                                if (data3 < 0 || data3 > 20) { data3 = 0; }
                                                                data4 = (dec[64] + dec[65] * 256 - 4000) / 800.0;
                                                                if (data4 < 0 || data4 > 20) { data4 = 0; }
                                                                data5 = (dec[66] + dec[67] * 256 - 4000) / 800.0;
                                                                if (data5 < 0 || data5 > 20) { data5 = 0; }
                                                                res += string.Format("{{Id:\"{0:D}\",Type:\"ZD\",D1:\"{1:D}\",D2:\"{2:D}\",D3:\"{3:D}\",D4:\"{4:D}\",D5:\"{5:D}\"}},\r\n",
                                                                k, data1.ToString("0.00"), data2.ToString("0.00"), data3.ToString("0.00"), data4.ToString("0.00"), data5.ToString("0.00"));
                                                            }
                                                            catch { }
                                                        }
                                                        else if (list[i].Substring(1, 3) == "ZD3")// 4号斜井
                                                        {
                                                            int k = 0;
                                                            try
                                                            {   
                                                                for (int j = 71; j < 83; j++)// 12个截面各两个振动节点
                                                                {
                                                                    if (j < 77)
                                                                    {
                                                                        data1 = (dec[4 + k * 4] + dec[5 + k * 4] * 256 - 4000) / 320.0;
                                                                        if (data1 < 0 || data1 > 50)
                                                                            data1 = 0;
                                                                        data2 = (dec[6 + k * 4] + dec[7 + k * 4] * 256 - 4000) / 320.0;
                                                                        if (data2 < 0 || data2 > 50) { data2 = 0; }
                                                                        res += string.Format("{{Id:\"{0:D}\",Type:\"ZD\",D1:\"{1:D}\",D2:\"{2:D}\"}},\r\n", j,
                                                                            data1.ToString("0.00"), data2.ToString("0.00"));
                                                                    }
                                                                    else
                                                                    {
                                                                        data1 = (dec[4 + k * 4] + dec[5 + k * 4] * 256 - 4000) / 80.0;
                                                                        if (data1 < 0 || data1 > 200)
                                                                            data1 = 0;
                                                                        data2 = (dec[6 + k * 4] + dec[7 + k * 4] * 256 - 4000) / 80.0;
                                                                        if (data2 < 0 || data2 > 200) { data2 = 0; }
                                                                        res += string.Format("{{Id:\"{0:D}\",Type:\"ZD\",D1:\"{1:D}\",D2:\"{2:D}\"}},\r\n", j,
                                                                            data1.ToString("0.00"), data2.ToString("0.00"));
                                                                    }
                                                                    k++;
                                                                }
                                                            }
                                                            catch { }
                                                        }
                                                        else if (list[i].Substring(1, 3) == "ZD4")
                                                        {
                                                            int k = 0;
                                                            try
                                                            {
                                                                for (int j = 31; j < 59; j++)// 28个截面各一个振动节点
                                                                {
                                                                    if (j == 42)
                                                                    {
                                                                        data1 = ((dec[22]+dec[23]*256+dec[24] + dec[25] * 256)/2 - 4000) / 320.0;
                                                                        if (data1 < 0 || data1 > 50)// 将小于4000和大于20000的数据剔除掉
                                                                            data1 = 0;
                                                                        res += string.Format("{{Id:\"{0:D}\",Type:\"ZD\",D1:\"{1:D}\"}},\r\n", j, data1.ToString("0.00"));
                                                                        k++;
                                                                        continue;
                                                                    }
                                                                    if (j == 43)
                                                                    {
                                                                        data1 = ((dec[30] + dec[31] * 256 + dec[32] + dec[33] * 256) / 2 - 4000) / 320.0;
                                                                        if (data1 < 0 || data1 > 50)// 将小于4000和大于20000的数据剔除掉
                                                                            data1 = 0;
                                                                        res += string.Format("{{Id:\"{0:D}\",Type:\"ZD\",D1:\"{1:D}\"}},\r\n", j, data1.ToString("0.00"));
                                                                        k++;
                                                                        continue;
                                                                    }
                                                                    data1 = (dec[4 + k * 2] + dec[5 + k * 2] * 256 - 4000) / 320.0;
                                                                    if (data1 < 0 || data1 > 50)// 将小于4000和大于20000的数据剔除掉
                                                                        data1 = 0;
                                                                    res += string.Format("{{Id:\"{0:D}\",Type:\"ZD\",D1:\"{1:D}\"}},\r\n", j, data1.ToString("0.00"));
                                                                    k++;
                                                                }
                                                            }
                                                            catch { }
                                                            k = 84;
                                                            try
                                                            {
                                                                data1 = (dec[58] + dec[59] * 256 - 4000) / 800.0;
                                                                if (data1 < 0 || data1 > 20) { data1 = 0; }
                                                                data2 = (dec[60] + dec[61] * 256 - 4000) / 800.0;
                                                                if (data2 < 0 || data2 > 20) { data2 = 0; }
                                                                data3 = (dec[62] + dec[63] * 256 - 4000) / 800.0;
                                                                if (data3 < 0 || data3 > 20) { data3 = 0; }
                                                                data4 = (dec[64] + dec[65] * 256 - 4000) / 800.0;
                                                                if (data4 < 0 || data4 > 20) { data4 = 0; }
                                                                data5 = (dec[66] + dec[67] * 256 - 4000) / 800.0;
                                                                if (data5 < 0 || data5 > 20) { data5 = 0; }
                                                                res += string.Format("{{Id:\"{0:D}\",Type:\"ZD\",D1:\"{1:D}\",D2:\"{2:D}\",D3:\"{3:D}\",D4:\"{4:D}\",D5:\"{5:D}\"}},\r\n",
                                                                k, data1.ToString("0.00"), data2.ToString("0.00"), data3.ToString("0.00"), data4.ToString("0.00"), data5.ToString("0.00"));
                                                            }
                                                            catch { }
                                                        }
                                                    }
                                                    catch
                                                    {
                                                        continue;
                                                    }
                                                }
                                                res = res.Remove(res.LastIndexOf(","), 1);// 删除最后一个逗号
                                                shuju = "[" + res + "]";
                                            }
                                        }
                                        catch
                                        {
                                            // 不做处理
                                        }
                                        if (shuju.IndexOf("ID2") > 0 && shuju.IndexOf("ERROR") == -1)// 应变 位移 压力 钢筋
                                        {
                                            if (StorgeData.StorgeYBorWY(shuju).Result)
                                            {
                                                Fight2 = true;
                                                AppendTxt(string.Format("客户端：{0}数据保存成功\r\n" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), mysoc.RemoteEndPoint));
                                            }
                                        }
                                        else if (StorgeData.Storge(shuju).Result)
                                        {
                                            Fight2 = true;
                                            AppendTxt(string.Format("客户端：{0}数据保存成功\r\n" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), mysoc.RemoteEndPoint));
                                        }
                                        {
                                            AppendTxt(string.Format("客户端：{0}数据成功接收\r\n{1}" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), mysoc.RemoteEndPoint, shuju));
                                        }
                                    }
                                    catch (Exception es)
                                    {
                                        //出错，有可能是数据存储出错，也可能是数据解析出错，造成的结果就是数据存储失败，不用回应设备接受到数据
                                        continue;
                                    }
                                    //接收到数据后 发送一个标志
                                    if (mysoc.Connected && Fight2 == true)
                                    {
                                        Fight2 = false;
                                        try
                                        {   //数据无误后，返回确认信息
                                            dataSend = Encoding.ASCII.GetBytes("[RETU]");
                                            mysoc.Send(dataSend);
                                        }
                                        catch (Exception es)
                                        {
                                            AppendTxt("客户端："+mysoc.RemoteEndPoint+"数据确认接收返回信息出错！！！\r\n" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                                            continue;
                                        }
                                    }
                                }
                            }

                        }

                    }
                    else
                    {
                        if (shuju == "[LINKOK]")
                        {
                            AppendTxt(string.Format("客户端：{0}数据成功接收\r\n{1}" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), mysoc.RemoteEndPoint, shuju));
                            //告诉设备是否连接上
                            try
                            {
                                if (mysoc.Connected)
                                {
                                    Fight1 = true;
                                    baoLink = true;
                                }
                            }
                            catch (Exception es)
                            {
                                AppendTxt("客户端：" + mysoc.RemoteEndPoint + "验证连接返回信息出错\r\n" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                                continue;
                            }
                        }
                        else
                        {
                            //已经断开连接
                            try
                            {
                                //socketList.Remove(mysoc);
                                if (socketItems.Any(m => m.Key == mysoc.RemoteEndPoint.ToString()))
                                {
                                    socketItems.Remove(mysoc.RemoteEndPoint.ToString());
                                    RefreshTargetList(nameItems[mysoc.RemoteEndPoint.ToString()], false);
                                    nameItems.Remove(mysoc.RemoteEndPoint.ToString());
                                }
                                AppendTxt("客户端：" + mysoc.RemoteEndPoint + "非正常退出...\r\n" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                                CloseSocket(mysoc);
                            }
                            catch (Exception e)
                            {
                                continue;//关闭线程出错后，再一次循环，直至关闭线程
                            }
                            return;
                        }
                    }
                }
                catch (SocketException es)
                {
                    //throw new Exception(es.Message);
                    if (es.ErrorCode == 10060)
                    {
                        AppendTxt(string.Format("客户端"+mysoc.RemoteEndPoint.ToString()+"：数据堵塞\r\n" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
                        //告诉设备是否连接上
                        if (Fight1)
                        {
                            try
                            {
                                if (mysoc.Connected)
                                {
                                    dataSend = Encoding.ASCII.GetBytes("[RESTART]");
                                    mysoc.Send(dataSend);
                                    Fight1 = false;
                                    baoLink = true;
                                }
                            }
                            catch (Exception e)
                            {
                                AppendTxt("客户端：" + mysoc.RemoteEndPoint + "验证连接返回信息出错\r\n" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                                continue;
                            }
                        }
                        else
                        {
                            //连续第二次堵塞则断开
                            try
                            {
                                //socketList.Remove(mysoc);
                                if (socketItems.Any(m => m.Key == mysoc.RemoteEndPoint.ToString()))
                                {
                                    socketItems.Remove(mysoc.RemoteEndPoint.ToString());
                                    RefreshTargetList(nameItems[mysoc.RemoteEndPoint.ToString()], false);
                                    nameItems.Remove(mysoc.RemoteEndPoint.ToString());
                                }
                                AppendTxt("客户端：" + mysoc.RemoteEndPoint + "非正常退出...\r\n" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                                CloseSocket(mysoc);
                            }
                            catch (Exception e)
                            {
                                continue;//关闭线程出错后，再一次循环，直至关闭线程
                            }
                            return;
                        }
                        continue;
                    }
                    else
                    {    //出现其他错误
                        //已经断开连接
                        try
                        {
                            //socketList.Remove(mysoc);
                            if (socketItems.Any(m => m.Key == mysoc.RemoteEndPoint.ToString()))
                            {
                                socketItems.Remove(mysoc.RemoteEndPoint.ToString());
                                RefreshTargetList(nameItems[mysoc.RemoteEndPoint.ToString()], false);
                                nameItems.Remove(mysoc.RemoteEndPoint.ToString());
                            }
                            AppendTxt("客户端：" + mysoc.RemoteEndPoint + "非正常退出...\r\n" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                            CloseSocket(mysoc);
                        }
                        catch (Exception e)
                        {
                            continue;//关闭线程出错后，再一次循环，直至关闭线程
                        }
                        return;
                    }
                }
            }
        }
        private string HexStringToJson(string shuju)
        {
            if (shuju.Substring(1, 2) == "ZD")
            {
                List<string> list = new List<string>(shuju.Split(']'));
                string res = "";
                foreach(var str in list)
                {
                    try
                    {
                        if (str.Substring(1, 3) == "ZD2")
                        {
                            int i = 0;
                            double data1 = 0.0,data2=0.0,data3=0.0,data4=0.0,data5=0.0;
                            for (int j = 1; j < 31; j++)// 30个截面各一个振动节点
                            {
                                data1 = (str[4 + i * 2] + str[5 + i * 2] * 256 - 4000)/800.0;
                                if (data1 < 0)
                                {
                                    data1 = 0;
                                }
                                res += string.Format("{{Id:\"{0:D}\",Type:\"ZD\",D1:\"{1:D}\"}},\r\n", j, data1.ToString("0.00"));
                                i++;
                            }
                            i = 0;
                            for (int j = 59; j < 71; j++)// 12个截面各两个振动节点
                            {
                                data1 = (str[66 + i * 4] + str[67 + i * 4] * 256 - 4000)/800.0;
                                if (data1 < 0)
                                {
                                    data1 = 0;
                                }
                                data2 = (str[68 + i * 4] + str[69 + i * 4] * 256)/800.0;
                                if (data2 < 0) { data2 = 0; }
                                res += string.Format("{{Id:\"{0:D}\",Type:\"ZD\",D1:\"{1:D}\",D2:\"{2:D}\"}},\r\n", j,
                                    data1.ToString("0.00"), data2.ToString("0.00"));
                                i++;
                            }
                            i = 83;
                            try
                            {
                                data1 = (str[108] + str[109] * 256) / 800.0;
                                if (data1 < 0) { data1 = 0; }
                                data2 = (str[110] + str[111] * 256) / 800.0;
                                if (data2 < 0) { data2 = 0; }
                                data3 = (str[112] + str[113] * 256) / 800.0;
                                if (data3 < 0) { data3 = 0; }
                                data4 = (str[114] + str[115] * 256) / 800.0;
                                if (data4 < 0) { data4 = 0; }
                                data5 = (str[116] + str[117] * 256) / 800.0;
                                if (data5 < 0) { data5 = 0; }
                                res += string.Format("{{Id:\"{0:D}\",Type:\"ZD\",D1:\"{1:D}\",D2:\"{2:D}\",D3:\"{3:D}\",D4:\"{4:D}\",D5:\"{5:D}\"}},\r\n",
                                i, data1.ToString("0.00"), data2.ToString("0.00"), data3.ToString("0.00"), data4.ToString("0.00"), data5.ToString("0.00"));
                            }
                            catch { }
                        }
                        else if (str.Substring(1, 3) == "ZD4")
                        {
                            int i = 45;
                            res += string.Format("{{Id:\"{0:D}\",Type:\"ZD\",D1:\"{1:D}\",D2:\"{2:D}\",D3:\"{3:D}\",D4:\"{4:D}\",D5:\"{5:D}\"}},\r\n",
                            i, str[3] + str[4] * 256, str[5] + str[6] * 256, str[7] + str[8] * 256, str[9] + str[10] * 256, str[11] + str[12] * 256);
                            i = 0;
                            for (int j = 48; j < 76; j++)// 28个截面各一个振动节点
                            {
                                res += string.Format("{{Id:\"{0:D}\",Type:\"ZD\",D1:\"{1:D}\"}},\r\n", j, str[13 + i * 2] + str[14 + i * 2] * 256);
                                i++;
                            }
                            i = 0;
                            for (int j = 76; j < 88; j++)// 12个截面各两个振动节点
                            {
                                res += string.Format("{{Id:\"{0:D}\",Type:\"ZD\",D1:\"{1:D}\",D2:\"{2:D}\"}},\r\n", j,
                                    str[71 + i * 4] + str[72 + i * 4] * 256, str[73 + i * 4] + str[74 + i * 4] * 256);
                                i++;
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        continue;
                    }
                }
                res = res.Remove(res.LastIndexOf(","), 1);// 删除最后一个逗号
                res = "[" + res + "]";
                return res;
            }
            else
            {
                return shuju;
            }
        }

        private string BytetoString(byte[] a, int b)
        {
            string str = Encoding.ASCII.GetString(a, 0, b);
            return str;
        }

        private void AppendTxt(string txt)
        {
            try
            {
                if (txt_Receive.InvokeRequired)
                {
                    txt_Receive.Invoke(new Action(() =>
                    {
                        txt_Receive.AppendText("\r\n" + txt + "\r\n");
                    }));
                }
                else
                {
                    txt_Receive.AppendText("\r\n" + txt + "\r\n");
                }
            }
            catch (Exception ex)
            {
                txt_Receive.AppendText("\r\n错误代码：000X3 " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "\r\n");
            }
        }

        private void CloseSocket(Socket socket)
        {
            try
            {
                if (socket.Connected)
                {
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close(100);
                }
            }
            catch (Exception ex)
            {
                AppendTxt("客户端" + socket.RemoteEndPoint+"关闭连接出错！！！ "+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (txt_Receive.InvokeRequired)
                {
                    txt_Receive.Invoke(new Delegate_Clear(ClearTxt));
                }
                else
                {
                    ClearTxt();
                }
            }
            catch (Exception ex)
            {
                AppendTxt("错误代码：000X4 " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            }
        }

        private void ClearTxt()
        {
            txt_Receive.Clear();
        }

        private void ReStart(string str)
        {
            byte[] send = Encoding.ASCII.GetBytes(str);
            if (radioButton2.Checked)
            {
                string hex = "";
                foreach(byte item in send)
                {
                    hex = hex + string.Format("{0:X2}", item);
                }
                for (int i = 0, j = 0; j <= hex.Length - 2; j += 2, i++)
                {
                    send[i] = Convert.ToByte(hex.Substring(j, 2), 16);
                }
            }
            string ip = "";// 存储所选目标的IP地址
            string name = TargetList.Text;// 目标下拉列表
            try
            {
                if (name != "")
                {
                    if (nameItems.Any(m => m.Value == name))
                    {
                        ip = nameItems.Where(m => m.Value == name).Select(m => m.Key).First();
                        socketItems[ip].Send(send);
                    }
                    else
                    {
                        MessageBox.Show("目标不存在", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                    }
                }
                else
                {
                    // MessageBox.Show("请选择发送目标", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                    foreach (var item in socketItems.Values)
                    {
                        item.Send(send);
                    }
                }
            }
            catch (Exception ex)
            {
                AppendTxt(socketItems[ip].RemoteEndPoint.ToString() +  "发送指令出错！！！\r\n"+ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            }
        }

        private void Btn_Send_Click(object sender, EventArgs e)
        {
            try
            {
                if (Btn_Send.InvokeRequired)
                {
                    Btn_Send.Invoke(new Delegate_Send(ReStart));
                }
                else
                {
                    ReStart(OrderTxt.Text);
                }
            }
            catch(Exception ex)
            {
                AppendTxt("发送失败 " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            }
        }


        private void timer2_Tick(object sender, EventArgs e)
        {
            byte[] send = Encoding.ASCII.GetBytes("[RESTART]");
            if (nameItems.Any(m => m.Value.IndexOf("ZD")!=-1))
            {
                string[] ips = nameItems.Where(m => m.Value.IndexOf("ZD") != -1).Select(m => m.Key).ToArray();
                foreach(string ip in ips)
                {
                    try
                    {
                        socketItems[ip].Send(send);
                    }
                    catch
                    {

                    }
                }
            }
        }
        private void OpenFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = @"C:\";
            ofd.Title = "请选择要发送的文件";
            ofd.Filter = "所有文件|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txt_FoldPath.Text = ofd.FileName;
            }
        }
        private void Btn_OpenFile_Click(object sender, EventArgs e)
        {
            if (Btn_OpenFile.InvokeRequired)
            {
                Btn_OpenFile.Invoke(new Delegate_OpenFile(OpenFile));
            }
            else
            {
                OpenFile();
            }
        }

        private void Btn_SendFile_Click(object sender, EventArgs e)
        {
            if (Btn_SendFile.InvokeRequired)
            {
                Btn_SendFile.Invoke(new Delegate_SendFile(SendFile));
            }
            else
            {
                SendFile();
            }
        }
        private void SendFile()
        {
            path = txt_FoldPath.Text;
            if (!File.Exists(path))
            {
                MessageBox.Show("文件不存在！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }
            string ip = "";// 存储所选目标的IP地址
            string name = TargetList.Text;// 目标下拉列表
            try
            {
                if (name != "")
                {
                    if (nameItems.Any(m => m.Value == name))
                    {
                        ip = nameItems.Where(m => m.Value == name).Select(m => m.Key).First();
                        Socket socket = socketItems[ip];
                        ThreadPool.QueueUserWorkItem(new WaitCallback(SendFileTask), socket);
                        
                    }
                    else
                    {
                        MessageBox.Show("目标不存在", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                    }
                }
                else
                {
                    MessageBox.Show("请选择发送目标", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                }
            }
            catch (Exception ex)
            {
                AppendTxt(socketItems[ip].RemoteEndPoint.ToString() + "发送文件出错！！！\r\n" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            }
        }

        private void SendFileTask(object obj)
        {
            Socket socket = obj as Socket;
            using (FileStream fsRead = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                byte[] buffer = new byte[1024 * 1024];
                byte[] bufferReceive = new byte[20];
                socket.ReceiveTimeout = 1000 * 30;
                while (true)
                {
                    int r = fsRead.Read(buffer, 0, buffer.Length);
                    for (int i = 0; i < 5; i++)
                    {
                        try
                        {
                            socket.Send(buffer, 0, r, SocketFlags.None);
                            int len = socket.Receive(bufferReceive, SocketFlags.None);
                            string flag = BytetoString(bufferReceive, len);
                            if (flag != "[OK]")
                            {
                                if (i == 4)
                                {
                                    MessageBox.Show("文件发送失败！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                                    return;
                                }
                                continue;
                            }
                            else
                            {
                                break;
                            }
                        }
                        catch (SocketException ex)
                        {
                            if (ex.ErrorCode == 10060)
                            {
                                continue;
                            }
                        }
                    }
                    if (r < buffer.Length)
                    {
                        MessageBox.Show("文件发送成功！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                        break;
                    }
                }
            }
            socket.Close();
        }
    }
}
