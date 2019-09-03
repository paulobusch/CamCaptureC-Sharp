using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace MultiCam.Model
{
    public static class Consts
    {
        // Data
        public static Image DEFAULT_IMAGE = new Bitmap(400, 300, PixelFormat.Format32bppPArgb);

        // Config
        public static string WEB_PATH = @"web_pages";
        public static string DATA_PATH = @"app_data";
        public static string FFMPEG_PATH = @"ffmpeg_compress";
        public static string NAME_FILE_LOG = @"log.ini";
        public static string NAME_FILE_DATA = @"data.db";
        public static string NAME_FILE_CONFIG = @"config.ini";

        // Video
        public static string CURRENT_PATH = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory.ToString());
        public static Color[] COLORS = { Color.White, Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.Blue, Color.Indigo, Color.Purple, };
        public static int[] FONT_SIZE = { 6, 8, 10, 12, 16, 18, 20, 24, 30, 36 };
        public static Size[] RESOLUTION = new Size[] {
            new Size ( 160 , 120),
            new Size ( 320 , 240),
            new Size ( 640 , 480),
            new Size ( 1024, 768)
        };
        public static Dictionary<int, string> REGISTERS_FOLDER_NAME = 
            new Dictionary<int, string>{
            { 0, "yy"       }, 
            { 1, "yy-MM"    },
            { 2, "yy-MM-dd" }
        };

        // Graph
        public static Point LOCATION = new Point(0, 4);
        public static string FORMAT_DATE_TIME = "dd/MM/yyyy HH:mm:ss";
    }
}
