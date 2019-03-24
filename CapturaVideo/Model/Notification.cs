using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapturaVideo.Model
{
    /// <summary>
    /// Notificate user interface
    /// </summary>
    public interface INotification
    {
        /// <summary>
        /// Information notification method
        /// </summary>
        /// <param name="msg">Message Notification</param>
        void InfoMessage(string msg);

        /// <summary>
        /// Wharning notification method
        /// </summary>
        /// <param name="msg">Message Notification</param>
        void FailMessage(string msg);
    }

    /// <summary>
    /// Notification user interface with message dialog events application
    /// </summary>
    public class Notification : INotification
    {
        public void InfoMessage(string msg) => ShowMessage(msg, MessageBoxIcon.Information);

        public void FailMessage(string msg) => ShowMessage(msg, MessageBoxIcon.Warning);

        /// <summary>
        /// Message notification method
        /// </summary>
        /// <param name="msg">Message Notification</param>
        /// <param name="icon">Icon type</param>
        private void ShowMessage(string msg, MessageBoxIcon icon)
        {
            MessageBox.Show(msg, "Aviso", MessageBoxButtons.OK, icon);
        }

    }
}
