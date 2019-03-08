using System.Drawing;

namespace CapturaVideo.Model.Dtos {
    public class DeviceDto {
        public string MonikerString { get; set; }
        public Size Size { get; set; }

        public DeviceDto() {
            this.MonikerString = null;
            this.Size = new Size();
        }
    }
}
