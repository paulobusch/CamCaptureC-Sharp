using CapturaVideo.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace CapturaVideo.Model
{
    public static class Configuration
    {
        //device
        public static int time_interval = 5;
        public static bool enable_interval = false;
        public static bool enable_server = false;

        //video
        public static string path_save_video = $@"{Consts.CURRENT_PATH}\";
        public static int frame_rate = 15;
        public static int bit_rate = 100000;
        public static bool compress_video = false;
        public static bool show_date_time = false;
        public static LegendAlign legend_align = LegendAlign.BottonRight;
        public static Font font = new Font("Arial", 10);

        //Dictionary<MonikerString, FrameSize>
        public static Dictionary<string, Size> devices_config = new Dictionary<string, Size>();

        //menu
        public static bool start_window = false;
        public static bool start_window_minimized = false;

        //config
        private static string _path_name_config = $@"{Consts.CURRENT_PATH}\{Consts.NAME_FILE_CONFIG}";
        private static string _table_name = "configuration";

        //database
        private static string _connectionString = $@"Data Source={Consts.DATA_PATH}\{Consts.NAME_FILE_DATA}; Version=3;";

        public static void LoadConfiguration(){
            //check
            if (!File.Exists(_path_name_config)){
                SaveConfiguration();
                return;
            }

            //load
            string configJson = string.Empty;
            using (StreamReader reader = new StreamReader(_path_name_config, Encoding.Unicode)) {
                while (!reader.EndOfStream)
                    configJson += reader.ReadLine();
                reader.Close();
            }
            var d = new Devices();
            Convert.ChangeType(d, typeof(string));

            var i = 0;

            var r = i.;

            var db = new SqLite();
            db.Open();
            db.Select("configuration", "c", "where c.is_last=1").Apply(typeof(Configuration));
            db.Close();

            //foreach (System.Data.DataRow row in dt.Rows)
            //{
            //    time_interval = Convert.ToInt32(row["time_interval"]);
            //    enable_interval = Convert.ToBoolean(row["enable_interval"]);
            //    enable_server = Convert.ToBoolean(row["enable_server"]);

            //    path_save_video = Convert.ToString(row["path_save_video"]);
            //    frame_rate = Convert.ToInt32(row["frame_rate"]);
            //    bit_rate = Convert.ToInt32(row["bit_rate"]);
            //    compress_video = Convert.ToBoolean(row["compress_video"]);
            //    show_date_time = Convert.ToBoolean(row["show_date_time"]);
            //    legend_align = (LegendAlign)Convert.ToInt32(row["legend_align"]);
            //    font = new Font(
            //                Convert.ToString(row["font_family"]),
            //                Convert.ToInt32(row["font_size"])
            //            );

            //    devices_config = JsonConvert.DeserializeObject<Dictionary<string, Size>>(Convert.ToString(row["devices_config"]));

            //    start_window = Convert.ToBoolean(row["start_window"]);
            //    start_window_minimized = Convert.ToBoolean(row["start_window_minimized"]);

            //}
            //db.Close();

            //restore
            try
            {

            }catch (System.Exception){
                MessageBox.Show("Falha ao carregar as configurações!", "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }
        public static void SaveConfiguration()
        {
            try
            {
                var config = new
                {
                    time_interval,
                    enable_interval,
                    enable_server,
                    path_save_video,
                    frame_rate,
                    bit_rate,
                    compress_video,
                    show_date_time,
                    legend_align = (int)legend_align,
                    font_family = font.FontFamily.Name,
                    font_size = font.Size,

                    //devices
                    devices_config = JsonConvert.SerializeObject(DeviceController.BindDeviceConfiguration()),

                    start_window,
                    start_window_minimized,
                    date_time = DateTime.Now,
                    is_last = true
                };

                var db = new SqLite();
                db.Open();
                db.Update(new SqlLiteData(new { is_last = false }, _table_name));
                db.Save(new SqlLiteData(config, _table_name));
                db.Close();

                string configJson = JsonConvert.SerializeObject(new
                {
                    time_interval,
                    enable_interval,
                    enable_server,
                    path_save_video,
                    frame_rate,
                    bit_rate,
                    compress_video,
                    show_date_time,
                    legend_align,
                    font_family = font.FontFamily.Name,
                    font_size = font.Size,

                    //devices
                    devices_config = DeviceController.BindDeviceConfiguration(),

                    start_window,
                    start_window_minimized,
                    date_time = DateTime.Now,
                    is_last = true
                });

                using (StreamWriter file = new StreamWriter(_path_name_config, false, Encoding.Unicode))
                {
                    file.WriteLine(configJson);
                    file.Close();
                }
            }
            catch (System.Exception ex){
                MessageBox.Show("Falha ao salvar configurações!", "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }
        public static void ShowOptionsWindow()
        {
            using (var ConfigurationForm = new ConfigurationForm())
            {
                ConfigurationForm.ShowDialog();
                if (ConfigurationForm.save){
                    SaveConfiguration();
                    DeviceController.ApplyConfiguration();
                }
            }
        }
    }
}
