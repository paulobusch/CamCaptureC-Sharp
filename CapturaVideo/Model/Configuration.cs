using CapturaVideo.Model;
using CapturaVideo.Model.Dtos;
using CapturaVideo.Model.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace CapturaVideo.Model
{
    public static class Configuration
    {
        public static ConfigurationDto Data = new ConfigurationDto();

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
        public static ELegendAlign legend_align = ELegendAlign.BottonRight;
        public static Font font = new Font("Arial", 10);

        //Dictionary<MonikerString, FrameSize>
        public static Dictionary<string, Size> devices_config = new Dictionary<string, Size>();

        //menu
        public static bool start_window = false;
        public static bool start_window_minimized = false;

        //config
        private static string _path_name_config = $@"{Consts.CURRENT_PATH}\{Consts.NAME_FILE_CONFIG}";

        public static void LoadConfiguration(){
            // Check database
            if (!SqLite.DatabaseExists()) {
                SqLite.CreateDatabase();
                SaveConfiguration();
                return;
            }

            // Load
            string configJson = string.Empty;
            using (StreamReader reader = new StreamReader(_path_name_config, Encoding.Unicode)) {
                while (!reader.EndOfStream)
                    configJson += reader.ReadLine();
                reader.Close();
            }
            //var d = new Devices();
            //Convert.ChangeType(d, typeof(string));


            //var db = new SqLite();
            //db.Open();
            //db.Select("configuration", "c", "where c.is_last=1").Apply(typeof(Configuration));
            //db.Close();

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
                var obj = JsonConvert.DeserializeObject<JObject>(configJson);
                time_interval = obj.GetValue("time_interval").Value<int>();
                enable_interval = obj.GetValue("enable_interval").Value<bool>();
                enable_server = obj.GetValue("enable_server").Value<bool>();

                path_save_video = obj.GetValue("path_save_video").Value<string>();
                frame_rate = obj.GetValue("frame_rate").Value<int>();
                bit_rate = obj.GetValue("bit_rate").Value<int>();
                compress_video = obj.GetValue("compress_video").Value<bool>();
                show_date_time = obj.GetValue("show_date_time").Value<bool>();
                legend_align = (ELegendAlign)obj.GetValue("legend_align").Value<int>();
                font = new Font(obj.GetValue("font_family").Value<string>(), obj.GetValue("font_size").Value<int>());

                devices_config = JsonConvert.DeserializeObject<Dictionary<string, Size>>(obj.GetValue("devices_config").ToString());

                start_window = obj.GetValue("start_window").Value<bool>();
                start_window_minimized = obj.GetValue("start_window_minimized").Value<bool>();
            }catch (Exception){
                MessageBox.Show("Falha ao carregar as configurações!", "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }
        public static void SaveConfiguration()
        {
            if (Data?.State == EDbState.Unchanged)
                return;

            try
            {
                var sqlInsertConfiguration = @"
                    update configuration set IsLast=0 where IsLast=1;

                    insert into configuration(TimeInterval,
                    EnableInterval,EnableServer,EnableCompressVideo,ViewDateTime,
                    PathSaveVideo,FrameRate,BitRate,LegendAlign,FontFamily,FontSize,
                    EnableStart,EnableStartMinimized,DateTime,IsLast)

                    values(@TimeInterval,@EnableInterval,@EnableServer,@EnableCompressVideo,
                    @ViewDateTime,@PathSaveVideo,@FrameRate,@BitRate,@LegendAlign,
                    @FontFamily,@FontSize,@EnableStart,@EnableStartMinimized,@DateTime,@IsLast);

                    select last_insert_rowid();";

                var sqlInsertDevices = @"insert into devices values (@ConfigurationId,@MonikerString,@Width,@Height);";

                using (var cnn = SqLite.NewConnection()) {
                    cnn.Open();

                    // Configuration
                    using (var command = new SQLiteCommand(sqlInsertConfiguration, cnn)) {
                        command.Parameters.Add("TimeInterval", DbType.Int32).Value = Data.TimeInterval;
                        command.Parameters.Add("EnableInterval", DbType.Boolean).Value = Data.EnableInterval;
                        command.Parameters.Add("EnableServer", DbType.Boolean).Value = Data.EnableServer;
                        command.Parameters.Add("EnableCompressVideo", DbType.Boolean).Value = Data.EnableCompressVideo;
                        command.Parameters.Add("ViewDateTime", DbType.Boolean).Value = Data.ViewDateTime;
                        command.Parameters.Add("PathSaveVideo", DbType.String).Value = Data.PathSaveVideo;
                        command.Parameters.Add("FrameRate", DbType.Int32).Value = Data.FrameRate;
                        command.Parameters.Add("BitRate", DbType.Int32).Value = Data.BitRate;
                        command.Parameters.Add("LegendAlign", DbType.Int32).Value = Data.LegendAlign;
                        command.Parameters.Add("FontFamily", DbType.String).Value = Data.FontFamily;
                        command.Parameters.Add("FontSize", DbType.Int32).Value = Data.FontSize;
                        command.Parameters.Add("EnableStart", DbType.Boolean).Value = Data.EnableStart;
                        command.Parameters.Add("EnableStartMinimized", DbType.Boolean).Value = Data.EnableStartMinimized;
                        command.Parameters.Add("DateTime", DbType.DateTime).Value = DateTime.Now;
                        command.Parameters.Add("IsLast", DbType.Boolean).Value = true;
                        Data.Id = Convert.ToInt32(command.ExecuteScalar());
                    }

                    // Devices
                    if (Data.Devices == null || Data.Devices.Count == 0)
                        return;
                    using (var command = new SQLiteCommand(sqlInsertDevices, cnn)) {
                        SQLiteParameter ConfigurationId = new SQLiteParameter("ConfigurationId", DbType.Int32); 
                        SQLiteParameter MonikerString = new SQLiteParameter("MonikerString", DbType.String); 
                        SQLiteParameter Width = new SQLiteParameter("Width", DbType.String); 
                        SQLiteParameter Height = new SQLiteParameter("Height", DbType.String);

                        command.Parameters.Add(ConfigurationId);
                        command.Parameters.Add(MonikerString);
                        command.Parameters.Add(Width);
                        command.Parameters.Add(Height);

                        ConfigurationId.Value = Data.Id;

                        foreach (var device in Data.Devices) {
                            MonikerString.Value = device.MonikerString;
                            Width.Value = device.Width;
                            Height.Value = device.Height;

                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex){
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
