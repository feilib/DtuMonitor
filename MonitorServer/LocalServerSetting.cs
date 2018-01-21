using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommLib.ShareFun;

namespace MonitorServer
{
    public partial class LocalServerSetting : Form
    {
        public LocalServerSetting()
        {
            InitializeComponent();
        }

        private Config cfg;

        private void LocalServerSetting_Load(object sender, EventArgs e)
        {
            cfg = new Config();
            txtLocalIp.Text = cfg.LocalIp;
            txtLocalPort.Text = cfg.LocalPort.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            #region 有效性检查

            if (!Funs.CheckStringIsIp(txtLocalIp.Text))
            {
                MessageBox.Show("IP设置不合法，请检查！");
                return;
            }

            if (!Funs.CheckStringIsValidPort(txtLocalPort.Text))
            {
                MessageBox.Show("端口设置不合法，请检查！");
                return;
            }

            #endregion

            //保存
            cfg.LocalIp = txtLocalIp.Text;
            cfg.LocalPort = int.Parse(txtLocalPort.Text);

            cfg.Write();

            this.DialogResult = DialogResult.OK;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;

        }
    }
}
