using CapturaVideo.Model.Enums;
using System.Collections.Generic;
using System.Drawing;

namespace CapturaVideo.Model.Dtos {
    public class ConfigurationDto : BaseDto {
        public int Id { get; set; }
        public int TimeInterval { get; set; }
        public bool EnableInterval { get; set; }
        public bool EnableServer { get; set; }
        public bool EnableCompressVideo { get; set; }
        public bool ViewDateTime { get; set; }
        public string PathSaveVideo { get; set; }
        public int FrameRate { get; set; }
        public int BitRate { get; set; }
        public ELegendAlign LegendAlign { get; set; }
        public bool EnableStart { get; set; }
        public bool EnableStartMinimized { get; set; }
        public IEnumerable<DeviceDto> Devices { get; set; }
        public Font Font {
            get {
                if (_font == null)
                    _font = new Font(_fontFamily, _fontSize);
                return _font;
            }
            set {
                _fontFamily = value.FontFamily.Name;
                _fontSize = value.Size;
                _font = value;
            }
        }

        private string _fontFamily;
        private float _fontSize;
        private Font _font;

        public ConfigurationDto() {
            // Device
            this.Id = 0;
            this.TimeInterval = 5;
            this.EnableInterval = false;
            this.EnableServer = false;
            this.Devices = new List<DeviceDto>();

            // Video
            this.ViewDateTime = false;
            this.EnableCompressVideo = false;
            this.PathSaveVideo = $@"{Consts.CURRENT_PATH}\";
            this.FrameRate = 15;
            this.BitRate = 100000;
            this.LegendAlign = ELegendAlign.BottonRight;
            this.Font = new Font("Arial", 10.0f);

            // Menu
            this.EnableStart = false;
            this.EnableStartMinimized = false;
        }
    }
}
