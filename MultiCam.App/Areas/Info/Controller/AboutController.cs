using System.Windows.Forms;

using MultiCam.About.View;

namespace MultiCam.Info.Controller
{
    public interface IAboutController
    {
        Form Run();
        /// <summary>
        /// specify view by controll
        /// </summary>
        /// <param name="view"></param>
        void SetView(IAboutView view);
    }
    public class AboutController : IAboutController
    {
        private IAboutView _view;

        public Form Run() =>  new AboutForm(this);

        public void SetView(IAboutView view) => _view = view;
    }
}
