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

        //TODO: Tipos de campos
        //tables mapping [TableName, ColumnName, Type]
        //public static Dictionary<string, Dictionary<string, Type>> TABLES_MAPPING =
        //    new Dictionary<string, Dictionary<string, Type>>()
        //    {
        //        ["configuration"] = new Dictionary<string, Type>()
        //        {
        //            ["id_configuration"]       = typeof(PrimaryKey),
        //            ["time_interval"]          = typeof(Int32),
        //            ["enable_interval"]        = typeof(Boolean),
        //            ["enable_server"]          = typeof(Boolean),
        //            ["path_save_video"]        = typeof(String),
        //            ["frame_rate"]             = typeof(Int32),
        //            ["bit_rate"]               = typeof(Int32),
        //            ["compress_video"]         = typeof(Boolean),
        //            ["show_date_time"]         = typeof(Boolean),
        //            ["legend_align"]           = typeof(Int32),
        //            ["font_family"]            = typeof(String),
        //            ["font_size"]              = typeof(Int32),
        //            ["devices_config"]         = typeof(Devices),
        //            ["start_window"]           = typeof(Boolean),
        //            ["start_window_minimized"] = typeof(Boolean),
        //            ["date_time"]              = typeof(DateTime),
        //            ["is_last"]                = typeof(Boolean)
        //        }
        //    };

        ////database
        //public static Dictionary<Type, DbType> DB_TYPES =
        //    new Dictionary<Type, DbType>()
        //    {
        //        [typeof(PrimaryKey)] = DbType.Int32,
        //        [typeof(ForeignKey)] = DbType.Int32,
        //        [typeof(String)] = DbType.String,
        //        [typeof(Int32)] = DbType.Int32,
        //        [typeof(Boolean)] = DbType.Boolean,
        //        [typeof(Single)] = DbType.Single,
        //        [typeof(DateTime)] = DbType.DateTime
        //    };
        //public static Dictionary<Type, string> DB_TYPES_TEXT =
        //    new Dictionary<Type, string>()
        //    {
        //        [typeof(PrimaryKey)] = "integer primary key autoincrement",
        //        [typeof(ForeignKey)] = "integer foreign key({0}) references {1}({2})",
        //        [typeof(String)] = "text",
        //        [typeof(Int32)] = "integer",
        //        [typeof(Boolean)] = "bit",
        //        [typeof(Single)] = "real",
        //        [typeof(DateTime)] = "datetime"
        //    };
    }
}
