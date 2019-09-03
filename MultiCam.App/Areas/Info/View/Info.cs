using System;
using System.Windows.Forms;

using MultiCam.Info.Controller;

namespace MultiCam.About.View
{
    public interface IAboutView { 
        
    }
    public partial class AboutForm : Form, IAboutView
    {
        private readonly IAboutController _controller;
        public AboutForm(IAboutController controller)
        {
            InitializeComponent();
            _controller = controller;
            _controller.SetView(this);
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
