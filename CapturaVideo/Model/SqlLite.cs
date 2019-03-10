using System.Data.SQLite;
using System.IO;

namespace CapturaVideo.Model {
    public static class SqLite {
        private static string _connectionString = $@"Data Source={Consts.DATA_PATH}\{Consts.NAME_FILE_DATA}; Version=3;";

        public static SQLiteConnection NewConnection() {
            return new SQLiteConnection(_connectionString);
        }
        public static bool DatabaseExists() {
            return File.Exists($@"{Consts.CURRENT_PATH}\{Consts.DATA_PATH}\{Consts.NAME_FILE_DATA}");
        }
        public static void CreateDatabase() {
            if (DatabaseExists())
                return;

            var sqlCreateTables = @"
                create table configuration(
	                id integer primary key autoincrement,
	                time_interval integer,
	                enable_interval bit,
	                enable_server bit,
	                enable_compress_video bit,
	                view_date_time bit,
	                path_save_video varchar(1000),
	                frame_rate integer,
	                bit_rate integer,
	                legend_align integer,
	                font_family varchar(50),
	                font_size real,
	                enable_start bit,
	                enable_start_minimized bit,
                    date_time date_time,
                    is_last bit
                );

                create table devices(
	                moniker_string varchar(100),
	                id_configuration integer,
	                width integer,
	                height integer,
	                foreign key(id_configuration) references configuration(id)
                );";

            using (var cnn = NewConnection()) {
                using (var command = new SQLiteCommand(sqlCreateTables,cnn)) {
                    cnn.Open();
                    command.ExecuteNonQuery();
                }
            }
        }        
    }    
}
