using CapturaVideo.Model;
using CapturaVideo.Model.Dtos;
using CapturaVideo.Model.Enums;
using Dapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
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

        public static void LoadConfiguration(){
            // Check database
            if (!SqLite.DatabaseExists()) {
                SqLite.CreateDatabase();
                Data.State = EDbState.Create;
                SaveConfiguration();
                return;
            }

            // Load
            try
            {
                var sqlSelectConfiguration = @"
                    select 
                        config.id as Id,
	                    config.time_interval as TimeInterval,
	                    config.enable_interval as EnableInterval,
	                    config.enable_server as EnableServer,
	                    config.enable_compress_video as EnableCompressVideo,
	                    config.view_date_time as ViewDateTime,
	                    config.path_save_video as PathSaveVideo,
	                    config.frame_rate as FrameRate,
	                    config.bit_rate as BitRate,
	                    config.legend_align as LegendAlign,
	                    config.font_family as FontFamily,
	                    config.font_size as FontSize,
	                    config.enable_start as EnableStart,
	                    config.enable_start_minimized as EnableStartMinimized
                    from configuration config
                    where config.is_last=1";

                var sqlSelectDevices = @"
                    select
                        device.moniker_string as MonikerString,
                        device.width as Width,
                        device.height as Height
                    from devices device
                    where device.id_configuration=@IdConfiguration";

                using (var cnn = SqLite.NewConnection()) {
                    cnn.Open();
                    Data = cnn.Query<ConfigurationDto>(sqlSelectConfiguration, null).FirstOrDefault();
                    Data.Devices = cnn.Query<DeviceDto>(sqlSelectDevices, new { IdConfiguration = Data.Id });
                }
            }catch (Exception ex){
                MessageBox.Show("Falha ao carregar as configurações!", "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }
        public static void SaveConfiguration()
        {
            if (Data.State == EDbState.Unchanged)
                return;
            try
            {
                var sqlInsertConfiguration = @"
                    update configuration set is_last=0 where is_last=1;

                    insert into configuration(time_interval,
                    enable_interval,enable_server,enable_compress_video,view_date_time,
                    path_save_video,frame_rate,bit_rate,legend_align,font_family,font_size,
                    enable_start,enable_start_minimized,date_time,is_last)

                    values(@TimeInterval,@EnableInterval,@EnableServer,@EnableCompressVideo,
                    @ViewDateTime,@PathSaveVideo,@FrameRate,@BitRate,@LegendAlign,
                    @FontFamily,@FontSize,@EnableStart,@EnableStartMinimized,datetime(),1);";

                var sqlInsertDevices = @"
                    insert into devices values 
                    ((select id from configuration where is_last=1),
                    @MonikerString,@Width,@Height);";

                using (var cnn = SqLite.NewConnection()) {
                    cnn.Open();

                    // Configuration
                    cnn.Execute(sqlInsertConfiguration, Data);

                    // Devices
                    if (Data.Devices == null || Data.Devices.Count() == 0)
                        return;

                    cnn.Execute(sqlInsertDevices, Data.Devices);
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
