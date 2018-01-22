using CommLib.ShareFun;

namespace DtuClientSim
{
    public sealed class Config : ConfigBase
    {
        public string ServerIp;
        public int ServerPort;
        /// <summary>
        /// 心跳超时时间（秒）
        /// </summary>
        public int HeartBeatTimeOutSecond;
        public Config()
        {
            Section = "DTUSIM";
            Read();
            Write();
        }

        public override void Read()
        {
            ServerIp = Read("ServerIp", "127.0.0.1");
            ServerPort = Read("ServerPort", 5001);
            HeartBeatTimeOutSecond = Read("HeartBeatTimeOutSecond", 15);
        }

        public override void Write()
        {
            Write("ServerIp", ServerIp);
            Write("ServerPort", ServerPort);
            Write("HeartBeatTimeOutSecond", HeartBeatTimeOutSecond);
        }
    }
}
