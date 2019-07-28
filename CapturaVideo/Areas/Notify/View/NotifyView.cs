using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MultiCam.Notify.Controller;

namespace MultiCam.Notify.View
{
    public interface INotifyView {
        /// <summary>
        /// Message notification method
        /// </summary>
        /// <param name="msg">Message Notification</param>
        /// <param name="icon">Icon type</param>
        void ShowMessage(string msg, MessageBoxIcon icon);
    }
    public class NotifyView : INotifyView
    {
        private readonly INotifyController _controller;
        public NotifyView(INotifyController controller) { 
            _controller = controller;
        }

        public void ShowMessage(string msg, MessageBoxIcon icon)
        {
            MessageBox.Show(msg, "Aviso", MessageBoxButtons.OK, icon);
        }
    }
}
