using CapturaVideo.Model.Dtos;
using CapturaVideo.Model.Enums;
using Dapper;
using System;
using System.Linq;
using System.Windows.Forms;

namespace CapturaVideo.Model
{

    /// <summary>
    /// Configuration interface by inject dependency
    /// </summary>
    public interface IConfiguration
    {
        /// <summary>
        /// Restore configuration by application
        /// </summary>
        /// <returns>The method return an instance ConfigurationDto</returns>
        ConfigurationDto LoadConfiguration();

        /// <summary>
        /// Save Configuration application
        /// </summary>
        /// <param name="dto">Instance ConfigurationDto</param>
        void SaveConfiguration(ConfigurationDto dto);
    }

    public class Config : IConfiguration
    {
        private readonly INotification _notification;
        private readonly IContextDb _context;

        /// <summary>
        /// Contructor Configuration
        /// </summary>
        /// <param name="context">Implementation IContextDb</param>
        /// <param name="notification">Implementation INotification</param>
        public Config(IContextDb context, INotification notification)
        {
            _context = context;
            _notification = notification;
        }

        public ConfigurationDto LoadConfiguration()
        {
            // Check database
            if (!_context.DatabaseExists()) {
                _context.CreateDatabase();
                return new ConfigurationDto();
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
                        device.id_configuration as ConfigurationId,
                        device.moniker_string as MonikerString,
                        device.width as _width,
                        device.height as _height
                    from devices device
                    where device.id_configuration=@IdConfiguration";

                var configurationDto = null as ConfigurationDto;
                using (var cnn = _context.NewConnection()) {
                    cnn.Open();
                    configurationDto = cnn.Query<ConfigurationDto>(sqlSelectConfiguration, null).FirstOrDefault();
                    configurationDto.Devices = cnn.Query<DeviceDto>(sqlSelectDevices, new { IdConfiguration = configurationDto.Id });
                }

                return configurationDto;
            } catch (Exception){
                _notification.FailMessage("Falha ao carregar as configurações!");
            }
            return new ConfigurationDto();
        }

        public void SaveConfiguration(ConfigurationDto dto)
        {
            // Check database
            if (dto.State == EDbState.Unchanged)
                return;
            if (!_context.DatabaseExists())
                _context.CreateDatabase();
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

                using (var cnn = _context.NewConnection()) {
                    cnn.Open();

                    // Configuration
                    cnn.Execute(sqlInsertConfiguration, new {
                        dto.TimeInterval,
                        dto.EnableInterval,
                        dto.EnableServer,
                        dto.EnableCompressVideo,
                        dto.ViewDateTime,
                        dto.PathSaveVideo,
                        dto.FrameRate,
                        dto.BitRate,
                        dto.LegendAlign,
                        FontFamily = dto.Font.FontFamily.Name,
                        FontSize = dto.Font.Size,
                        dto.EnableStart,
                        dto.EnableStartMinimized
                    });
                    dto.State = EDbState.Unchanged;

                    // Devices
                    if (dto.Devices == null || dto.Devices.Count() == 0)
                        return;

                    cnn.Execute(sqlInsertDevices, dto.Devices
                        .Select(dev => new {
                            dev.MonikerString,
                            dev.Size.Width,
                            dev.Size.Height
                        }
                    ));
                    dto.Devices.AsList<DeviceDto>().ForEach(x => x.State = EDbState.Unchanged);
                }
            }
            catch (Exception){
                _notification.FailMessage("Falha ao salvar configurações!");
            }
        }
    }
}
