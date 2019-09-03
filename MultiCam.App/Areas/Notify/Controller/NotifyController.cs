using MultiCam.Notify.View;
using System.Windows.Forms;

namespace MultiCam.Notify.Controller
{
    /// <summary>
    /// Notificate user interface
    /// </summary>
    public interface INotifyController
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
    public class NotifyController : INotifyController
    {
        private INotifyView _view;

        public void InfoMessage(string msg) => _view.ShowMessage(msg, MessageBoxIcon.Information);

        public void FailMessage(string msg) => _view.ShowMessage(msg, MessageBoxIcon.Warning);

        public NotifyController() => _view = new NotifyView(this);

        public Form Run() =>  throw new System.NotImplementedException();
    }
}
