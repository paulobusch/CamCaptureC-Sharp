using System.Drawing;
using System.Collections.Generic;

using MultiCam.Model.Enums;
using MultiCam.Model.Dtos;
using MultiCam.Model;
using MultiCam.DataContext;

namespace MultiCam.Config.Model.Dtos{
    public class Configuration : EntityBase {
        public int TimeInterval { get; set; }
        public bool EnableInterval { get; set; }
        public bool EnableServer { get; set; }
        public bool EnableCompressVideo { get; set; }
        public bool ViewLegend { get; set; }
        public string PathSaveVideo { get; set; }
        public string FolderFormat { get; set; }
        public int FrameRate { get; set; }
        public int BitRate { get; set; }
        public ELegendAlign LegendAlign { get; set; }
        public bool EnableStart { get; set; }
        public bool EnableStartMinimized { get; set; }
        public bool SeparateRegistersCameras { get; set; }
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

        public Configuration() {
            // Device
            this.Id = 0;
            this.TimeInterval = 5;
            this.EnableInterval = false;
            this.EnableServer = false;

            // Video
            this.ViewLegend = false;
            this.EnableCompressVideo = false;
            this.SeparateRegistersCameras = false;
            this.PathSaveVideo = $@"{Consts.CURRENT_PATH}\";
            this.FrameRate = 15;
            this.BitRate = 100000;
            this.LegendAlign = ELegendAlign.BottonRight;
            this.Font = new Font("Arial", 10.0f);

            // Menu
            this.EnableStart = false;
            this.EnableStartMinimized = false;

            // Privates
            _font = null;
        }
    }
}
