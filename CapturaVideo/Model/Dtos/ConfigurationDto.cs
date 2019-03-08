using CapturaVideo.Model.Enums;
using System.Collections.Generic;

namespace CapturaVideo.Model.Dtos {
    public class ConfigurationDto {
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
        public string FontFamily { get; set; }
        public int FontSize { get; set; }
        public bool EnableStart { get; set; }
        public bool EnableStartMinimized { get; set; }
        public List<DeviceDto> Devices { get; set; }

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
            this.FontFamily = "Arial";
            this.FontSize = 10;

            // Menu
            this.EnableStart = false;
            this.EnableStartMinimized = false;

        }
    }
}
