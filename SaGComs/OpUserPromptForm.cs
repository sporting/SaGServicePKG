using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SaGComs
{
    public partial class OpUserPromptForm : Form
    {
        public string OpUser {
            get
            {
                return txbOpUser.Text.Trim();
            }
        }

        public OpUserPromptForm()
        {
            InitializeComponent();
        }

        private void OpUserPromptForm_Load(object sender, EventArgs e)
        {
            txbOpUser.Clear();
        }

        private void OpUserPromptForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(OpUser))
                {
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("請刷入個人卡");
                }
            }
        }
    }
}
