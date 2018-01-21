using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommLib.Log;
using CommLib.ShareFun;

namespace MonitorServer
{
    public partial class Monitor : Form
    {
        /// <summary>
        /// 配置信息
        /// </summary>
        private Config cfg;

        /// <summary>
        /// 服务
        /// </summary>
        private AsyncSocketServer Server;

        public Monitor()
        {
            InitializeComponent();
            cfg = new Config();
        }

        private void btnStartStop_Click(object sender, EventArgs e)
        {


            if (btnStartStop.Text == "点击启动")
            {
                //启动服务
                btnStartStop.Text = "点击停止";
                btnStartStop.BackColor = Color.Tomato;
                btnSetting.Enabled = false;

                //启动服务器
                Server = new AsyncSocketServer(cfg.LocalIp, cfg.LocalPort, ClientAdd, ClientDel, Log.Info);
            }
            else
            {
                //停止服务
                btnStartStop.Text = "点击启动";
                btnStartStop.BackColor = Color.LawnGreen;
                btnSetting.Enabled = true;

                //关闭服务
                Server.ShutdownServer();
            }
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            LocalServerSetting lss = new LocalServerSetting();
            if (lss.ShowDialog() == DialogResult.OK)
            {
                //重读一下
                cfg.Read();
            }
        }

        /// <summary>
        /// 客户端接入事件
        /// </summary>
        /// <param name="co"></param>
        private void ClientAdd(ClientObject co)
        {

        }

        /// <summary>
        /// 客户端退出事件
        /// </summary>
        /// <param name="co"></param>
        private void ClientDel(ClientObject co)
        {
            //这个退出事件目前先不处理。。
        }
    }
}
