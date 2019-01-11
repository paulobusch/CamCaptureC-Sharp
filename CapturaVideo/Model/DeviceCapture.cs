using System;
using System.Drawing;
using System.Windows.Forms;
using CapturaVideo.Model;
using DirectX.Capture;

namespace CapturaVideo.Classes
{
    internal class DeviceCapture
    {
        internal int key;
        internal Image process_image;
        internal DeviceInterface device;

        private Video _video;
        private Legend _legend;

        #region Constructor
        public DeviceCapture()
        {
            //define values interface
            process_image = (Image)Consts.DEFAULT_IMAGE.Clone();
        }
        #endregion

        #region Device
        public void ReserveDevice()
        {
            //has include this class instance
            DeviceController.devices.RemoveAll(x => x == device.info);
        }
        public void RestoreDevice()
        {
            //restore class controller
            DeviceController.devices.Add(device.info);
        }
        public void StartDevice()
        {
            if (!(device.font?.Running ?? false))
            {
                _legend = new Legend(device.resolution);
                device.font = new Capture(device.info, null);
                try
                {
                    device.font.FrameSize = device.resolution;
                }
                catch (Exception)
                {
                    MessageBox.Show("Resolução não suportada pela câmera!", "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    device.font.Dispose();
                    device.font = null;
                    return;
                }
                device.font.FrameRate = Configuration.frame_rate;
                device.font.NewFrame += NewFrame;
                device.font.Start();
            }
        }
        public void StopDevice()
        {
            if (device.font?.Running ?? false)
            {
                if (_video?.recording ?? false)
                    StopVideo();

                device.font.NewFrame -= NewFrame;
                device.font.Stop();
                device.font.Dispose();
                device.font = null;

                DeviceController.image_grid.Image = null;
                DeviceController.image_state.BackColor = SystemColors.Control;
                process_image = (Image)Consts.DEFAULT_IMAGE.Clone();
                _legend = null;
            }
        }
        public void UpdateConfiguration()
        {
            if(_legend != null)
                _legend.CalcPosition();
        }
        #endregion

        #region Video
        public DeviceState GetDeviceState(){
            if (_video?.recording ?? false)
                return DeviceState.Recording;
            else if (device.font?.Running ?? false)
                return DeviceState.Runing;
            else
                return DeviceState.Stoped;
        }
        public void StartVideo()
        {
            if (_video == null)
            {
                if (GetDeviceState() == DeviceState.Stoped)
                    StartDevice();
                _video = new Video(this.key, device.resolution);
                _video.StartRecording();
            }
        }
        public void StopVideo()
        {
            if (GetDeviceState() == DeviceState.Recording)
            {
                _video.StopRecording();
                _video = null;
                Video.CompressAllVideoThread();
            }
        }
        private void NewFrame(Bitmap bmp)
        {
            //force override
            _legend.WriteLegend(bmp);

            if (this.key == DeviceController.selected_device)
            {
                if (DeviceController.image_grid.Image != null)
                    DeviceController.image_grid.Image.Dispose();
                DeviceController.image_grid.Image = (Image)bmp.Clone();
                DeviceController.image_state.BackColor = (_video?.recording ?? false) ? Color.Red : Color.Green;
            }

            lock (process_image)
            {
                if (process_image != null)
                    process_image.Dispose();
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
