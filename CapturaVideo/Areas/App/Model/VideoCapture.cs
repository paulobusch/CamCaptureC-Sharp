using System;
using System.Drawing;
using System.Windows.Forms;
using DirectX.Capture;
using System.Runtime.InteropServices;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Ninject;

using MultiCam.Controller;
using MultiCam.Model.Entities;
using MultiCam.Model.Enums;
using MultiCam.Notify.Controller;

namespace MultiCam.Model
{
    public interface FrameObservable {
        void UpdateFrame(int id, Image img);
    }
    public class VideoCapture : Device
    {

        public Filter Info;
        public EDeviceState DeviceState { get; set; }
        public Image CurrentFrame { 
            get { 
                lock(_process_image)
                    return _process_image;
            } 
        }

        private readonly INotifyController _notify;
        private readonly IAppController _controller;
        private EDeviceState _state { 
            get => DeviceState; 
            set => ChangeState(value); 
        }
        private IEnumerable<FrameObservable> _frame_obs;

        private Image _process_image;
        private Video _video;
        private Legend _legend;
        private Capture _cap;

        #region Constructor
        public VideoCapture(Filter video)
        {
            //Dependêncies
            _notify = RegServices.Ioc.Get<INotifyController>();
            _controller = RegServices.Ioc.Get<IAppController>();

            //Properties
            this.MonikerString = video.MonikerString;
            this.DeviceState = EDeviceState.Stoped;
            this.Info = video;
            this._state = EDeviceState.Stoped;
            this._process_image = (Image)Consts.DEFAULT_IMAGE.Clone();
            this._frame_obs = new List<FrameObservable>();
        }
        #endregion

        #region Device
        public void StartDevice()
        {
            if (_state == EDeviceState.Stoped)
            {
                _legend = new Legend(_controller.Config.LegendAlign, _controller.Config.Font, Size);
                try
                {
                    _cap = new Capture(Info, null);
                    _cap.FrameSize = Size;
                }
                catch (DirectoryNotFoundException)
                {
                    _notify.FailMessage("Dispositivo não conectado!");
                    return;
                }
                catch (COMException)
                {
                    _notify.FailMessage("Resolução não suportada pela câmera!");
                    _cap.Dispose();
                    _cap = null;
                    return;
                }
                _cap.FrameRate = _controller.Config.FrameRate;
                _cap.NewFrame += NewFrame;
                _cap.Start();
                _state = EDeviceState.Runing;
            }
        }
        public void StopDevice()
        {
            if (_state != EDeviceState.Stoped)
            {
                if (_video?.recording ?? false)
                    StopVideo();

                _cap.NewFrame -= NewFrame;
                _cap.Stop();
                _cap.Dispose();
                _cap = null;

                _process_image = (Image)Consts.DEFAULT_IMAGE.Clone();
                _state = EDeviceState.Stoped;
            }
        }
        private void ChangeState(EDeviceState state) { 
            DeviceState = state;
            _controller.UpdateStateDevice(DeviceState, Id);
        }
        public void UpdateConfig()
        {
            _legend?.Update(_controller.Config.LegendAlign, _controller.Config.Font);
        }
        #endregion

        #region Video
        public void StartVideo()
        {
            if (_video == null)
            {
                var path = _controller.Config.PathSaveVideo;
                if (_controller.Config.FolderFormat != null)
                    path += $@"{DateTime.Now.ToString(_controller.Config.FolderFormat)}\";
                if(_controller.Config.SeparateRegistersCameras)
                    path += $@"{CodNome}\";
                if (_state == EDeviceState.Stoped)
                    StartDevice();
                _video = new Video(CodNome, Size);
                _video.StartRecording(path, _controller.Config.FrameRate, _controller.Config.BitRate);
                _state = EDeviceState.Recording;
            }
        }
        public void StopVideo()
        {
            if (_state == EDeviceState.Recording)
            {
                _video.StopRecording();
                _video = null;
                _state = EDeviceState.Runing;
                if(_controller.Config.EnableCompressVideo)
                    Video.CompressAllVideoThread(_controller.Config.PathSaveVideo);
            }
        }
        private void NewFrame(Bitmap bmp)
        {
            //force override
            if (_controller.Config.ViewLegend)
                _legend.WriteLegend(bmp);

            foreach(var obs in _frame_obs)
                obs.UpdateFrame(Id, (Image)bmp.Clone());
            
            lock (_process_image)
            {
                _process_image?.Dispose();
                _process_image = (Image)bmp.Clone();
            }

            //se estiver gravando escreve quadro
            if (_video?.recording ?? false)
                _video.WriteFrame(bmp);
            bmp.Dispose();
        }

        internal void ShowPropertyPage(Control elm)
        {
            _cap.PropertyPages[0].Show(elm);
        }
        #endregion

        #region Observable
        public VideoCapture Subscribe(FrameObservable obs)
        {
            _frame_obs = _frame_obs.Concat(new[]{ obs });
            return this;
        }
        public VideoCapture Unsubscribe(FrameObservable obs)
        {
            _frame_obs = _frame_obs.Where(x => x != obs);
            return this;
        }
        #endregion
    }
}
