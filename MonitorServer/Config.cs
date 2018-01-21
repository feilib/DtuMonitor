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
        }

        public override void Write()
        {
            Write("LocalIp", LocalIp);
            Write("LocalPort", LocalPort);
        }
    }
}
