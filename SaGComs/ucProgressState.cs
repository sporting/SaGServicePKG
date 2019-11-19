using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SaGUtil.System.Drawing;

namespace SaGComs
{
    public partial class ucProgressState : UserControl
    {
        private float DeepColorFactor = -0.3f;
        private Color DefaultFontColor = Color.Black;
        private Color DefaultColor = Color.Gray;
        private Color BarcodeActiveColor = Color.YellowGreen;
        private Color CassetteActiveColor = Color.LightBlue;
        private Color GrossActiveColor = Color.LightCoral;
        private Color EmbedActiveColor = Color.Orange;
        private Color SlideActiveColor = Color.Khaki;
        private Color DocDispatchActiveColor = Color.Orchid;

        private bool BarcodeState
        {
            set
            {
                pBarcode.BackColor = value ? BarcodeActiveColor : DefaultColor;
                labelBarcodeT.ForeColor = value ? SaColor.ChangeColor(BarcodeActiveColor,DeepColorFactor) : DefaultFontColor;
            }
        }

        private bool CassetteState
        {
            set
            {
                pCassette.BackColor = value ? CassetteActiveColor : DefaultColor;
                labelCassetteT.ForeColor = value ? SaColor.ChangeColor(CassetteActiveColor,DeepColorFactor) : DefaultFontColor;
            }
        }

        private bool GrossState
        {
            set
            {
                pGross.BackColor = value ? GrossActiveColor : DefaultColor;
                labelGrossT.ForeColor = value ? SaColor.ChangeColor(GrossActiveColor,DeepColorFactor) : DefaultFontColor;
            }
        }

        private bool EmbedState
        {
            set
            {
                pEmbed.BackColor = value ? EmbedActiveColor : DefaultColor;
                labelEmbedT.ForeColor = value ? SaColor.ChangeColor(EmbedActiveColor,DeepColorFactor) : DefaultFontColor;
            }
        }

        private bool SlideState
        {
            set
            {
                pSlide.BackColor = value ? SlideActiveColor : DefaultColor;
                labelSlideT.ForeColor = value ? SaColor.ChangeColor(SlideActiveColor,DeepColorFactor) : DefaultFontColor;
            }
        }
        private bool DocDispatchState
        {
            set
            {
                pDocDispatch.BackColor = value ? DocDispatchActiveColor : DefaultColor;
                labelDocDispatchT.ForeColor = value ? SaColor.ChangeColor(DocDispatchActiveColor,DeepColorFactor) : DefaultFontColor;
            }
        }

        public void SetBarcodeState(string text)
        {
            text = text.Trim();

            BarcodeState = !string.IsNullOrEmpty(text);

            labelBarcode.Text = text;
        }
        public void SetCassetteState(string text)
        {
            text = text.Trim();

            CassetteState = !string.IsNullOrEmpty(text);

            labelCassette.Text = text;
        }

        public void SetGrossState(string text)
        {
            text = text.Trim();

            GrossState = !string.IsNullOrEmpty(text);

            labelGross.Text = text;
        }
        public void SetEmbedState(string text)
        {
            text = text.Trim();

            EmbedState = !string.IsNullOrEmpty(text);

            labelEmbed.Text = text;
        }
        public void SetSlideState(string text)
        {
            text = text.Trim();

            SlideState = !string.IsNullOrEmpty(text);

            labelSlide.Text = text;
        }
        public void SetDocDispatchState(string text)
        {
            text = text.Trim();

            DocDispatchState = !string.IsNullOrEmpty(text);

            labelDocDispatch.Text = text;
        }

        public string Description
        {
            set
            {
                labelDesc.Text = value;
            }
        }
        public ucProgressState()
        {
            InitializeComponent();
        }

        private void ucProgressState_Load(object sender, EventArgs e)
        {
            //Initialize();
        }

        public void BeginSetState()
        {
            this.SuspendLayout();

            SetBarcodeState(string.Empty);
            SetCassetteState(string.Empty);
            SetGrossState(string.Empty);
            SetEmbedState(string.Empty);
            SetSlideState(string.Empty);
            SetDocDispatchState(string.Empty);

            Description = string.Empty;
        }

        public void EndSetState()
        {
            this.ResumeLayout();
        }
    }
}
