using CapturaVideo.Model;
using CapturaVideo.Model.Dtos;
using DirectX.Capture;
using System.Drawing;

namespace CapturaVideo.Model
{
    public class DeviceInterface
    {
        public Filter info;
        public Capture font;
        public Size resolution;

        #region Acess Values
        public DeviceInterface Clone()
        {
            return new DeviceInterface()
            {
                font = this.font,
                info = this.info,
                resolution = this.resolution
            };
        }
        public void SetValuesInterface()
        {
            DeviceController.device_interface = Clone();
            DeviceController.cmb_device.Text = this.info.Name;
            DeviceController.cmb_resolution.Text = $"{this.resolution.Width} x {this.resolution.Height}";

            DeviceController.box_image.Text = $"Câmera - [{this.info.Name}]";
        }
        public void GetInfoInteface()
        {
            var info = (ComboboxItemDto)DeviceController.cmb_device.SelectedItem;
            this.info = (info != null && info.Value != null) ? info.GetValue<Filter>() : null;
        }
        public void GetResolutionInteface()
        {
            this.resolution = ((ComboboxItemDto)DeviceController.cmb_resolution.SelectedItem).GetValue<Size>();
        }
        #endregion
    }
}
