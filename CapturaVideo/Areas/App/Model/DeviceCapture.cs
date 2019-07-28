using System;
using System.Drawing;
using System.Windows.Forms;
using DirectX.Capture;

using MultiCam.Config.Controller;
using MultiCam.Controller;
using MultiCam.Model.Dtos;
using MultiCam.Model.Enums;
using MultiCam.Notify.Controller;
using Ninject;

namespace MultiCam.Model
{
    public class VideoCapture
    {
        //internal DeviceInterface device;
        public Capture Font;
        public Device Device;

        //public Filter Info;
        //public Size resolution;

        private IAppController _controller;
        private INotifyController _notify;
        private Image process_image;
        private Video _video;

        #region Constructor
        public VideoCapture(Filter video)
        {
            this.Font = new Capture(video, null);
            this.process_image = (Image)Consts.DEFAULT_IMAGE.Clone();
        }
        #endregion

        #region Dependencies
        [Inject]
        public void AppController(IAppController controller) => _controller = controller;

        [Inject]
        public void Notify(INotifyController notify) => _notify = notify;
        #endregion

        #region Device
        public void ReserveDevice()
        {
            //has include this class instance
            _controller.Devices.RemoveAll(x => x == Font.VideoDevice);
        }
        public void RestoreDevice()
        {
            //restore class controller
            _controller.Devices.Add(Font.VideoDevice);
        }
        public void StartDevice()
        {
            if (!(Font?.Running ?? false))
            {
                //_legend = new Legend(_controller.Config.LegendAlign, _controller.Config.Font,Device.Size);
                try
                {
                    Font.FrameSize = Device.Size;
                }
                catch (Exception)
                {
                    _notify.FailMessage("Resolução não suportada pela câmera!");

                    Font.Dispose();
                    Font = null;

                    return;
                }
                Font.FrameRate = _controller.Config.FrameRate;
                Font.NewFrame += NewFrame;
                Font.Start();
            }
        }
        public void StopDevice()
        {
            if (Font?.Running ?? false)
            {
                if (_video?.recording ?? false)
                    StopVideo();

                Font.NewFrame -= NewFrame;
                Font.Stop();
                Font.Dispose();
                Font = null;

                // TODO: Validate into view
                //DeviceController.image_grid.Image = null;
                //DeviceController.image_state.BackColor = SystemColors.Control;
                process_image = (Image)Consts.DEFAULT_IMAGE.Clone();
                //_legend = null;
            }
        }
        public void UpdateConfiguration()
        {
            //_legend?.CalcPosition(_controller.Config.LegendAlign, _controller.Config.Font);
        }
        #endregion

        #region Video
        public EDeviceState GetDeviceState(){
            if (_video?.recording ?? false)
                return EDeviceState.Recording;
            else if (Font?.Running ?? false)
                return EDeviceState.Runing;
            else
                return EDeviceState.Stoped;
        }
        public void StartVideo()
        {
            if (_video == null)
            {
                if (GetDeviceState() == EDeviceState.Stoped)
                    StartDevice();
                //new Legend(_controller.Config.LegendAlign, _controller.Config.Font,Device.Size)
                _video = new Video(this.Device.Id, Device.Size, null);
                _video.StartRecording(_controller.Config.PathSaveVideo, _controller.Config.FrameRate, _controller.Config.BitRate);
            }
        }
        public void StopVideo()
        {
            if (GetDeviceState() == EDeviceState.Recording)
            {
                _video.StopRecording();
                _video = null;
                Video.CompressAllVideoThread(_controller.Config.PathSaveVideo);
            }
        }
        private void NewFrame(Bitmap bmp)
        {
            //force override
            if (_controller.Config.ViewLegend)
                //_legend.WriteLegend(bmp, _controller.Config.Font);

            if (Device.Id == _controller.SelectedDevice)
            {
                // TODO: implements into view
                _controller.UpdateFrame((Image)bmp.Clone());
                //DeviceController.image_state.BackColor = (_video?.recording ?? false) ? Color.Red : Color.Green;
            }

            lock (process_image)
            {
                process_image?.Dispose();
                process_image = (Image)bmp.Clone();
            }

            //se estiver gravando escreve quadro
            if (_video?.recording ?? false)
                _video.WriteFrame(bmp);
            bmp.Dispose();
        }
        #endregion
    }
}
