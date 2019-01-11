using CapturaVideo.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace CapturaVideo.Classes
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
        private static string path_name_config = $@"{Consts.CURRENT_PATH}\{Consts.NAME_FILE_CONFIG}";

        public static void LoadConfiguration(){
            //check
            if (!File.Exists(path_name_config)){
                SaveConfiguration();
                return;
            }

            //load
            string configJson = string.Empty;
            using (StreamReader reader = new StreamReader(path_name_config, Encoding.Unicode)) {
                while (!reader.EndOfStream)
                    configJson += reader.ReadLine();
                reader.Close();
            }

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
                legend_align = (LegendAlign)obj.GetValue("legend_align").Value<int>();
                font = new Font(obj.GetValue("font_family").Value<string>(), obj.GetValue("font_size").Value<int>());

                devices_config = JsonConvert.DeserializeObject<Dictionary<string, Size>>(obj.GetValue("devices_config").ToString());

                start_window = obj.GetValue("start_window").Value<bool>();
                start_window_minimized = obj.GetValue("start_window_minimized").Value<bool>();
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
                    start_window_minimized
                });

                using (StreamWriter file = new StreamWriter(path_name_config, false, Encoding.Unicode))
                {
                    file.WriteLine(configJson);
                    file.Close();
                }
            }
            catch (System.Exception){
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
