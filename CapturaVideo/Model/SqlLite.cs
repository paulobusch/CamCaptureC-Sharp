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
	                Id integer primary key autoincrement,
	                TimeInterval integer,
	                EnableInterval bit,
	                EnableServer bit,
	                EnableCompressVideo bit,
	                ViewDateTime bit,
	                PathSaveVideo varchar(1000),
	                FrameRate integer,
	                BitRate integer,
	                LegendAlign integer,
	                FontFamily varchar(50),
	                FontSize integer,
	                EnableStart bit,
	                EnableStartMinimized bit,
                    DateTime datetime,
                    IsLast bit
                );

                create table devices(
	                ConfigurationId integer,
	                MonikerString varchar(100),
	                Width integer,
	                Height integer,
	                foreign key(ConfigurationId) references configuration(Id)
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
