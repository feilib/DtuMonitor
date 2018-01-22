using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommLib.Log;
using CommLib.ShareFun;

namespace DtuClientSim
{
    public partial class DtuClientSim : Form
    {
        private Config cfg;

        public DtuClientSim()
        {
            InitializeComponent();
        }

        private void DtuClientSim_Load(object sender, EventArgs e)
        {
            cfg = new Config();
            txtLocalIp.Text = cfg.ServerIp;
            txtLocalPort.Text = cfg.ServerPort.ToString();
            txtHeartBeatTimeOut.Text = cfg.HeartBeatTimeOutSecond.ToString();


        }

        private void SendText(string val)
        {
            val = DateTime.Now.ToString("HH:mm:ss.fff  -  ") + val;
            FormFuns.DelegateAddLine(txtSend, val, 20000);
        }

        private void ReceiveText(string val)
        {
            val = DateTime.Now.ToString("HH:mm:ss.fff  -  ") + val;
            FormFuns.DelegateAddLine(txtReceive, val, 20000);
        }

        //修改保存
        private void txtLocalIp_Click(object sender, EventArgs e)
        {
            cfg.ServerIp = txtLocalIp.Text;
            if (!Funs.CheckStringIsValidPort(txtLocalPort.Text))
            {
                MessageBox.Show("端口不对");
            }
            cfg.ServerPort = int.Parse(txtLocalPort.Text);
            if (!Funs.CheckStringOnlyHaveNumber(txtHeartBeatTimeOut.Text))
            {
                MessageBox.Show("超时不对");
            }
            cfg.HeartBeatTimeOutSecond = int.Parse(txtHeartBeatTimeOut.Text);

            cfg.Write();
        }

        private void btnStartStop_Click(object sender, EventArgs e)
        {
            if (btnStartStop.Text == "开始")
            {
                btnStartStop.Text = "点击停止";
                txtLocalPort.Enabled = false;
                txtLocalIp.Enabled = false;
                txtHeartBeatTimeOut.Enabled = false;

                StartSocket();
            }
            else
            {
                btnStartStop.Text = "开始";
                txtLocalPort.Enabled = true;
                txtLocalIp.Enabled = true;
                txtHeartBeatTimeOut.Enabled = true;

                StopSocket();
            }
        }

        private Thread heartThread;
        private bool heartThreadRun;
        private AsyncSocketClient asyncClient;
        private void StartSocket()
        {
            try
            {
                asyncClient = new AsyncSocketClient(cfg.ServerIp, cfg.ServerPort, Receive, Disconnect, Log.Debug);
                Thread.Sleep(100);
                ReceiveText(asyncClient.client.LocalEndPoint.ToString() + "已连接,ID" + asyncClient.ClientID);

                heartThreadRun = true;
                heartThread = new Thread(HeartBeat);
                heartThread.IsBackground = true;
                heartThread.Start();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                ReceiveText(e.Message);
            }

        }

        private void Disconnect(AsyncSocketClient client)
        {
            ReceiveText("连接已断开,ID：" + client.ClientID);
        }

        private bool isReceiving = false;
        private void Receive(AsyncSocketClient clent)
        {
            if (isReceiving) return;

            isReceiving = true;

            #region 处理

            Thread.Sleep(100);
            if (clent.ReceiveBuffer.DataCount > 0)
            {
                byte[] buf = new byte[clent.ReceiveBuffer.DataCount];
                clent.ReceiveBuffer.PopBuffer(buf, 0, buf.Length);

                string raw = BitConverter.ToString(buf);
                string val = Encoding.Default.GetString(buf);

                ReceiveText(raw);
                ReceiveText(val);
            }

            #endregion

            isReceiving = false;
        }


        private void HeartBeat()
        {
            DateTime dtlast=DateTime.Now.AddSeconds(-1000);
            while (heartThreadRun)
            {
                Thread.Sleep(1000);
                if ((DateTime.Now - dtlast).TotalSeconds > cfg.HeartBeatTimeOutSecond)
                {
                    dtlast = DateTime.Now;
                    asyncClient.Send("HEART");
                    SendText("HEART");
                }
            }
        }

        private void StopSocket()
        {
            try
            {
                asyncClient.Shuwdown();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                ReceiveText(e.Message);
            }


        }
    }
}
