using System.Drawing;

using MultiCam.Domain.DataContext;

namespace MultiCam.Domain.Entities {
    public class Device : EntityBase {
        public string MonikerString { get; set; }
        public string CodNome { get; set; }
        public Size Size {
            get {
                if (_size == null || _size.Value.IsEmpty)
                    _size = new Size(_width, _height);
                return _size.Value;
            }
            set {
                _width = value.Width;
                _height = value.Height;
                _size = value;
            }
        }

        private int _width;
        private int _height;
        private Size? _size;

        public Device() {
            this.MonikerString = null;
            this.CodNome = null;
            this.Size = new Size(640,480);

            // Privates
            _size = null;
        }
    }
}
