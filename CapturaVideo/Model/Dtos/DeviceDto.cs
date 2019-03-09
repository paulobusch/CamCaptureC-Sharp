using System.Drawing;

namespace CapturaVideo.Model.Dtos {
    public class DeviceDto {
        public int ConfigurationId { get; set; }
        public string MonikerString { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public DeviceDto() {
            this.MonikerString = null;
            this.Width = 640;
            this.Height = 180;
        }
    }
}
