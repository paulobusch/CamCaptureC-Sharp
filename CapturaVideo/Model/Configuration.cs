using CapturaVideo.Model.Dtos;
using CapturaVideo.Model.Enums;
using Dapper;
using System;
using System.Linq;
using System.Windows.Forms;

namespace CapturaVideo.Model
{
    public static class Configuration
    {
        public static ConfigurationDto Data = new ConfigurationDto();

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
	                    config.font_family as _fontFamily,
	                    config.font_size as _fontSize,
	                    config.enable_start as EnableStart,
	                    config.enable_start_minimized as EnableStartMinimized
                    from configuration config
                    where config.is_last=1";

                var sqlSelectDevices = @"
                    select
                        device.moniker_string as MonikerString,
                        device.width as _width,
                        device.height as _height
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
                    insert into devices(moniker_string,id_configuration,width,height)
                    values(@MonikerString,(select id from configuration where is_last=1),@Width,@Height);";

                using (var cnn = SqLite.NewConnection()) {
                    cnn.Open();

                    // Configuration
                    cnn.Execute(sqlInsertConfiguration, new {
                        Data.TimeInterval,
                        Data.EnableInterval,
                        Data.EnableServer,
                        Data.EnableCompressVideo,
                        Data.ViewDateTime,
                        Data.PathSaveVideo,
                        Data.FrameRate,
                        Data.BitRate,
                        Data.LegendAlign,
                        FontFamily = Data.Font.FontFamily.Name,
                        FontSize = Data.Font.Size,
                        Data.EnableStart,
                        Data.EnableStartMinimized
                    });
                    Data.State = EDbState.Unchanged;

                    // Devices
                    Data.Devices = DeviceController.BindDeviceConfiguration();
                    if (Data.Devices == null || Data.Devices.Count() == 0)
                        return;

                    cnn.Execute(sqlInsertDevices, Data.Devices
                        .Select(dev => new {
                            dev.MonikerString,
                            dev.Size.Width,
                            dev.Size.Height
                        }
                   ));
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
                if (ConfigurationForm.save) {
                    Data.State = EDbState.Update;
                    SaveConfiguration();
                    DeviceController.ApplyConfiguration();
                }
            }
        }
    }
}
