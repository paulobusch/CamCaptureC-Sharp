using System;
using System.Windows.Forms;

namespace CapturaVideo.Janelas
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
