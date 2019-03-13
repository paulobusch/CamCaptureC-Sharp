using System.Drawing;

namespace CapturaVideo.Model.Dtos {
    public class DeviceDto : BaseDto {
        public int ConfigurationId { get; set; }
        public string MonikerString { get; set; }
        public Size Size {
            get {
                if (_size == null || _size.IsEmpty)
                    _size = new Size(_width, _height);
                return _size;
            }
            set {
                _width = value.Width;
                _height = value.Height;
                _size = value;
            }
        }

        private int _width;
        private int _height;
        private Size _size;

        public DeviceDto() {
            this.MonikerString = null;
            this.Size = new Size(640,480);
        }
    }
}
