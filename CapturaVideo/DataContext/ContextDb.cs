using System.Data;
using System.Data.SQLite;
using System.IO;

using MultiCam.Model;

namespace MultiCam.DataContext{
    /// <summary>
    /// Database Context interface
    /// </summary>
    public interface IContextDb 
    {
        /// <summary>
        /// Create connection SqLite database
        /// </summary>
        /// <returns>The method return an instance SQLiteConnection</returns>
        SQLiteConnection NewConnection();

        /// <summary>
        /// Check database exists
        /// </summary>
        /// <returns>true if database finded</returns>
        bool DatabaseExists();

        /// <summary>
        /// If not exists create database and tables
        /// </summary>
        void CreateDatabase();
    }

    public class SqLite : IContextDb 
    {
        private static string _connectionString = $@"Data Source={Consts.CURRENT_PATH}\{Consts.DATA_PATH}\{Consts.NAME_FILE_DATA}; Version=3;";

        public SQLiteConnection NewConnection() {
            return new SQLiteConnection(_connectionString);
        }
        public bool DatabaseExists() {
            return File.Exists($@"{Consts.CURRENT_PATH}\{Consts.DATA_PATH}\{Consts.NAME_FILE_DATA}");
        }
        public void CreateDatabase() {
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
                    folder_format varchar(10)
                );

                create table devices(
	                id integer primary key autoincrement,
	                moniker_string varchar(100),
                    cod_nome varchar(100),
	                width integer,
	                height integer
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
