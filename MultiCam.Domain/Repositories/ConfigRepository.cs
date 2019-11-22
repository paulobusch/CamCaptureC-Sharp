using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

using MultiCam.Domain.DataContext;
using MultiCam.Domain.Entities;

namespace MultiCam.Domain.Repository
{
    public class ConfigRepository : IRepository<Configuration>
    {
        private readonly IContextDb _context;

        public ConfigRepository(IContextDb context) { 
            _context = context;

            if (!_context.DatabaseExists()) { 
                _context.CreateDatabase();
                InsertAsync(new Configuration()).Wait();
            }
        }
        public async Task InsertAsync(Configuration entity)
        {
            var sqlInsert = @"
                insert into configuration(time_interval,
                enable_interval,enable_server,enable_compress_video,view_date_time,
                path_save_video,frame_rate,bit_rate,legend_align,font_family,font_size,
                enable_start,enable_start_minimized,folder_format,separate_registers_cameras)

                values(@TimeInterval,@EnableInterval,@EnableServer,@EnableCompressVideo,
                @ViewLegend,@PathSaveVideo,@FrameRate,@BitRate,@LegendAlign,@FontFamily,
                @FontSize,@EnableStart,@EnableStartMinimized,@FolderFormat,@SeparateRegistersCameras);";

            var sqlIdent = @"select last_insert_rowid();";

            using (var cnn = _context.NewConnection())
            {
                using (var command = new SQLiteCommand(sqlInsert, cnn))
                {
                    cnn.Open();
                    await cnn.ExecuteAsync(sqlInsert, Bind(entity));
                    entity.Id = cnn.Query<int>(sqlIdent, null).First();
                }
            }
        }
        public async Task DeleteAsync(int id)
        {
            var sql = @"delete from configuration where id=@Id";

            using (var cnn = _context.NewConnection())
            {
                cnn.Open();
                await cnn.ExecuteAsync(sql, new { Id = id });
            }
        }

        public async Task<IEnumerable<Configuration>> FindAsync(Func<Configuration, bool> predicate)
        {
            var list = await GetAllAsync();
            return list.Where(predicate);
        }

        public async Task<IEnumerable<Configuration>> GetAllAsync()
        {
            var sql = @"
                select 
                    config.id as Id,
	                config.time_interval as TimeInterval,
	                config.enable_interval as EnableInterval,
	                config.enable_server as EnableServer,
	                config.enable_compress_video as EnableCompressVideo,
	                config.view_date_time as ViewLegend,
	                config.path_save_video as PathSaveVideo,
	                config.frame_rate as FrameRate,
	                config.bit_rate as BitRate,
	                config.legend_align as LegendAlign,
	                config.font_family as _fontFamily,
	                config.font_size as _fontSize,
	                config.enable_start as EnableStart,
	                config.enable_start_minimized as EnableStartMinimized,
                    config.folder_format as FolderFormat,
                    config.separate_registers_cameras as SeparateRegistersCameras
                from configuration config";

            using (var cnn = _context.NewConnection())
            {
                cnn.Open();
                return await cnn.QueryAsync<Configuration>(sql, null);
            }
        }

        public async Task<Configuration> GetByIdAsync(int id)
        {
            var sql = @"
                select 
                    config.id as Id,
	                config.time_interval as TimeInterval,
	                config.enable_interval as EnableInterval,
	                config.enable_server as EnableServer,
	                config.enable_compress_video as EnableCompressVideo,
	                config.view_date_time as ViewLegend,
	                config.path_save_video as PathSaveVideo,
	                config.frame_rate as FrameRate,
	                config.bit_rate as BitRate,
	                config.legend_align as LegendAlign,
	                config.font_family as _fontFamily,
	                config.font_size as _fontSize,
	                config.enable_start as EnableStart,
	                config.enable_start_minimized as EnableStartMinimized,
                    config.folder_format as FolderFormat,
                    config.separate_registers_cameras as SeparateRegistersCameras
                from configuration config
                where config.id=@Id";

            using (var cnn = _context.NewConnection())
            {
                cnn.Open();
                return await cnn.QueryFirstAsync<Configuration>(sql, new { Id = id });
            }
        }

        public async Task UpdateAsync(Configuration entity)
        {
            var sql = @"
                update configuration set
	                time_interval=@TimeInterval,
	                enable_interval=@EnableInterval,
	                enable_server=@EnableServer,
	                enable_compress_video=@EnableCompressVideo,
	                view_date_time=@ViewLegend,
	                path_save_video=@PathSaveVideo,
	                frame_rate=@FrameRate,
	                bit_rate=@BitRate,
	                legend_align=@LegendAlign,
	                font_family=@FontFamily,
	                font_size=@FontSize,
	                enable_start=@EnableStart,
	                enable_start_minimized=@EnableStartMinimized,
                    folder_format=@FolderFormat,
                    separate_registers_cameras=@SeparateRegistersCameras
                where id=@Id";

            using (var cnn = _context.NewConnection())
            {
                cnn.Open();
                await cnn.ExecuteAsync(sql, Bind(entity));
            }
        }

        private object Bind(Configuration entity) { 
            return new
            {
                entity.Id,
                entity.TimeInterval,
                entity.EnableInterval,
                entity.EnableServer,
                entity.EnableCompressVideo,
                entity.ViewLegend,
                entity.PathSaveVideo,
                entity.FrameRate,
                entity.BitRate,
                entity.LegendAlign,
                FontFamily = entity.Font.FontFamily.Name,
                FontSize = entity.Font.Size,
                entity.EnableStart,
                entity.EnableStartMinimized,
                entity.FolderFormat,
                entity.SeparateRegistersCameras
            };
        }
    }
}
