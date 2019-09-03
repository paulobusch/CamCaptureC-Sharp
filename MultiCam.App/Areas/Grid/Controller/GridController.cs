using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using MultiCam.Grid.View;
using MultiCam.Model;

namespace MultiCam.Grid.Controller
{
    public interface IGridController
    {
        Form Run();
        void SetView(IGridView view);
        void SetVideosCapture(IEnumerable<VideoCapture> videosCpature);
        void Exit();
    }
    public class GridController : IGridController
    {
        private IGridView _view;
        private IEnumerable<VideoCapture> _videosCapture;
        public Form Run()
        {
            return new GridForm(this);
        }
        public void SetView(IGridView view)
        {
            _view = view;
            _view.PrepareImages(_videosCapture);
        }
        public void SetVideosCapture(IEnumerable<VideoCapture> videosCapture)
        {
            _videosCapture = videosCapture;
        }
        public void Exit()
        {
            foreach (var videoCapture in _videosCapture)
                videoCapture.Unsubscribe(_view);
        }
    }
}
