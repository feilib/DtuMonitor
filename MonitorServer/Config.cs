using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommLib.ShareFun;

namespace MonitorServer
{
    public sealed class Config : ConfigBase
    {
        public string LocalIp;
        public int LocalPort;
        /// <summary>
        /// 心跳超时时间（秒）
        /// </summary>
        public int HeartBeatTimeOutSecond;
        public Config()
        {
            Section = "DTUServer";
            Read();
            Write();
        }

        public override void Read()
        {
            LocalIp = Read("LocalIp", "127.0.0.1");
            LocalPort = Read("LocalPort", 5001);
            HeartBeatTimeOutSecond = Read("HeartBeatTimeOutSecond", 30);
        }

        public override void Write()
        {
            Write("LocalIp", LocalIp);
            Write("LocalPort", LocalPort);
            Write("HeartBeatTimeOutSecond", HeartBeatTimeOutSecond);
        }
    }
}
