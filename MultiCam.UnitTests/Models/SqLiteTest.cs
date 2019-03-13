using NUnit.Framework;
using CapturaVideo.Model;
using System.IO;
using System.Data.SQLite;
using System.Data;
using System.Collections.Generic;

namespace MultiCam.UnitTests {
    [TestFixture]
    public class SqLiteTest {

        private static string name_db_test;
        private static string base_path_db;

        [SetUp]
        public void SetUp() {
            name_db_test = "data.test.db";
            base_path_db = $@"{Util.GetCurrentPath()}\{Consts.DATA_PATH}";

            Directory.CreateDirectory(base_path_db);

            Util.SetProperty(typeof(Consts), "NAME_FILE_DATA", name_db_test);
            Util.SetProperty(typeof(Consts), "CURRENT_PATH", Util.GetCurrentPath());
        }

        [TearDown]
        public void TearDown() {
            // Drop database test
            File.Delete($@"{base_path_db}\{name_db_test}");
        }

        [Test]
        public void NewConnection() {
            var connection = Util.GetProperty<string>(typeof(SqLite), "_connectionString");
            Assert.NotNull(connection);

            using (var cnn = SqLite.NewConnection()) {
                cnn.Open();
                Assert.AreEqual(cnn.State, ConnectionState.Open);
                cnn.Close();
                Assert.AreEqual(cnn.State, ConnectionState.Closed);
            }
        }

        [Test]
        public void DatabaseExists() {
            var fileDb = $@"{base_path_db}\{name_db_test}";
            File.Create(fileDb).Close();
            Assert.True(SqLite.DatabaseExists());

            File.Delete(fileDb);
            Assert.False(SqLite.DatabaseExists());
        }

        [Test]
        public void CreateDatabase() {
            var tables = new List<string>();
            var listTables = "select name from sqlite_master where type='table' order by name";

            SqLite.CreateDatabase();
            using (var cnn = SqLite.NewConnection()) {
                cnn.Open();
                using (var command = new SQLiteDataAdapter(listTables, cnn)) {
                    var dt = new DataTable();
                    command.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                        tables.Add((string)row["name"]);
                }
            }
            SqLite.CreateDatabase();

            Assert.Contains("devices", tables);
            Assert.Contains("configuration", tables);
        }
    }
}
