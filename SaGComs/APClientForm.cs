using SaGBridge;
using SaGBridge.Utils;
using SaGModel;
using SaGUtil.Network;
using SaGUtil.Utils;
using SaGUtil.WinForm;
using System;
using System.ComponentModel.Design;
using System.Drawing;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SaGComs
{
    public  class APClientForm : Form
    {
        private string _opUser = string.Empty;
        public string OpUser
        {
            get
            {
                return _opUser;
            }
            set
            {
                _opUser = value;
                _statusLabelOpUser.Text = string.IsNullOrEmpty(_opUser) ? "" : $"操作人員: {_opUser}";
            }
        }
        //public string AssemblyName
        //{
        //    get
        //    {
        //        return $"{Assembly.GetExecutingAssembly().GetName().Name} {Application.ProductVersion}";
        //        //return $"{AppDomain.CurrentDomain.FriendlyName} {Application.ProductVersion}";
        //    }
        //}
        private string _token = string.Empty;

        public string ServiceToken
        {
            get
            {
                return _token;
            }
            set
            {
                _token = value;

                if (!string.IsNullOrEmpty(_token))
                {
                    _statusLabel.ForeColor = Color.DarkGreen;
                    _statusLabel.Text = "Service is Active";
                }
                else
                {
                    _statusLabel.ForeColor = Color.Red;
                    _statusLabel.Text = "Service Not Active!!!";
                }
            }
        }

        private string ServiceLoginErrorMessage
        {
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _statusLabel.ForeColor = Color.Red;
                    _statusLabel.Text = $"Service Not Active!!! {value}";
                }
            }
        }

        //local datetime
        private DateTime _localLoginDate;
        //server datetime
        private DateTime _systemLoginDate;
        //the best time sync is _localLoginDate=_systemLoginDate
        public DateTime Now
        {
            get
            {
                return _systemLoginDate.Add((DateTime.Now - _localLoginDate));
            }
        }

        public bool Connected
        {
            get
            {
                return !string.IsNullOrEmpty(_token);
            }
        }

        private StatusStrip statusStrip1;
        ToolStripStatusLabel _statusLabel;
        ToolStripStatusLabel _statusLabelOpUser;
        ToolStripStatusLabel _statusLabelDateTime;
        public APClientForm()
        {
            InitializeComponent();            

            Application.Idle += Application_Idle;
            Application.ApplicationExit += Application_ApplicationExit;

            this.Load += APClientForm_Load;

            this.SuspendLayout();
            StatusStrip strip = statusStrip1;
            strip.Dock = DockStyle.Bottom;
            strip.BackColor = SystemColors.Control;
            //strip.Parent = this;
            _statusLabel = new ToolStripStatusLabel();
            _statusLabel.TextAlign = ContentAlignment.MiddleLeft;
            _statusLabel.Spring = true;
            _statusLabelOpUser = new ToolStripStatusLabel();
            _statusLabelOpUser.ForeColor = Color.Navy;
            _statusLabelDateTime = new ToolStripStatusLabel();
            _statusLabelDateTime.ForeColor = Color.Navy;

            strip.Items.AddRange(new ToolStripItem[] { _statusLabel, _statusLabelOpUser, _statusLabelDateTime });

            this.ResumeLayout(false);
            this.PerformLayout();

            ServiceToken = string.Empty;

        }

        private Timer _timer;
        private async void Timer_Tick(object sender, EventArgs e)
        {
            _timer.Stop();
            bool result = await ServiceLogin();
            if (!result)
            {
                _timer.Start();
            }
        }
        

        public virtual void FormLoad(object sender, EventArgs e)
        {

        }

        protected void APClientForm_Load(object sender, EventArgs e)
        {
            //if (!DesignMode)
            if (!(this.GetService(typeof(IDesignerHost)) != null || System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime))
            {
                OpUserPromptAttribute attr = GetAttr<OpUserPromptAttribute>();
                
                if (attr!=null && attr.NeedOpUser)
                {
                    using (OpUserPromptForm prompt = new OpUserPromptForm())
                    {
                        if (prompt.ShowDialog() == DialogResult.OK)
                        {
                            OpUser = prompt.OpUser;
                        }
                        else
                        {
                            this.Close();
                        }
                    }
                }
            }

            Text = $"{Text} {ProductVersion}";
            
            FormLoad(sender,e);

            //if (!DesignMode)
            if (!(this.GetService(typeof(IDesignerHost)) != null || System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime))
            {
                _timer = new Timer();
                _timer.Interval = 3000;
                _timer.Tick += Timer_Tick;

                Timer_Tick(_timer, null);
                //_timer.Start();
            }
        }

        private T GetAttr<T>()
        {
            Attribute[] attrs = Attribute.GetCustomAttributes(this.GetType());

            foreach (Attribute attr in attrs)
            {
                if (attr is T)
                {
                    try
                    {
                        return (T)Convert.ChangeType(attr, typeof(T));
                    }
                    catch (InvalidCastException)
                    {
                        return default(T);
                    }
                }
            }

            return default(T);
        }

        private void Application_ApplicationExit(object sender, EventArgs e)
        {
            Application.Idle -= Application_Idle;
        }

        private void Application_Idle(object sender, EventArgs e)
        {
            //if (Connected)
            //{
            //    _statusLabel.ForeColor = Color.DarkGreen;
            //    _statusLabel.Text = "Service is Active";
            //}
            //else
            //{
            //    _statusLabel.ForeColor = Color.Red;
            //    _statusLabel.Text = "Service Not Active!!!";
            //}
            _statusLabelDateTime.Text = Now.ToString("MM/dd (ddd) HH:mm:ss");
            //_statusLabelDateTime.Text = Now.ToString("MM/dd HH:mm:ss");

            ApplicationIdle(sender, e);
        }

        protected virtual void ApplicationIdle(object sender, EventArgs e)
        {

        }

        protected virtual void AfterLogin()
        {

        }


        private async Task<bool> ServiceLogin()
        {
            ServiceLoginErrorMessage = string.Empty;
            try
            {
                LoginBridge lb = new LoginBridge(string.Empty);

                BridgeResult<ApLoginRequest> res = await lb.Post(new ApLoginRequest { App = $"{SaAssembly.EntryName} {SaAssembly.ProductVersion}", ApMachine = SaMachine.Get(), LoginUser = OpUser });

                if (!res.status)
                {
                    MyLog.Info(this, res.message);
                    //LogMan.Instance.Error("APClientForm", res.message);
                    //MessageBox.Show($"系統服務連接失敗，請確認伺服器服務是否正常, {res.message}", "System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ServiceLoginErrorMessage = res.message;
                    return false;
                }

                ServiceToken = res.result.Token;

                _localLoginDate = DateTime.Now;
                _systemLoginDate = res.result.LoginDate;

                AfterLogin();

                return true;
            }
            catch (Exception ex)
            {
                MyLog.Fatal(this,ex.Message);
                //LogMan.Instance.Error("APClientForm", ex.Message);
                //MessageBox.Show($"系統服務連接失敗，請確認伺服器服務是否正常, {ex.Message}", "System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ServiceLoginErrorMessage = ex.Message;
                return false;
            }
        }

        private void InitializeComponent()
        {
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 240);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(284, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // APClientForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.statusStrip1);
            this.Name = "APClientForm";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
