using System;
using CommLib.ShareFun;
using System.Timers;

namespace MonitorServer
{
    public class DtuClient
    {
        /// <summary>
        /// 配置文件
        /// </summary>
        private readonly Config cfg;
        /// <summary>
        /// 本对象客户端
        /// </summary>
        public ClientObject transClinet;
        /// <summary>
        /// 登陆事件--只读
        /// </summary>
        public DateTime LoginTime;
        /// <summary>
        /// 最后传输数据时间
        /// </summary>
        public DateTime LastUpdateTime;
        /// <summary>
        /// DTU发来的ID号码
        /// </summary>
        public string DtuId;
        /// <summary>
        /// 电话号码
        /// </summary>
        public string PhoneNum;


        public DtuClient(ClientObject co)
        {
            cfg = new Config();

            //初始化对象
            transClinet = co;

            //然后把接受数据回调发上来。。
            transClinet.SetReceiveCallBack(MessageReceived);

            //初始化对象
            LoginTime = DateTime.Now;
            LastUpdateTime = DateTime.Now;

            //开个计时器，判断是否超时
            Timer tmTimeOutTick = new Timer(1000);
            tmTimeOutTick.Elapsed += TmTimeOutTick_Elapsed;
            tmTimeOutTick.AutoReset = true;
            tmTimeOutTick.Start();
        }

        //判断是否超时
        private void TmTimeOutTick_Elapsed(object sender, ElapsedEventArgs e)
        {
            if ((DateTime.Now - LastUpdateTime).TotalSeconds > cfg.HeartBeatTimeOutSecond)
            {
                //超时了。直接关掉。。
                transClinet.Shutdown();
            }
        }

        //正在处理接受事件。。
        private bool isReceiving = false;
        /// <summary>
        /// 数据已经收到
        /// </summary>
        /// <param name="co"></param>
        private void MessageReceived(ClientObject co)
        {
            //正在处理，就不重复进入了。。
            if (isReceiving) return;

            isReceiving = true;

            #region 开始处理收到的数据
            //todo 数据处理，首次注册信息，接收心跳，其他数据。。

            #endregion


            isReceiving = false;
        }
    }
}
