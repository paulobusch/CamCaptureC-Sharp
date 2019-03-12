using NUnit.Framework;
using CapturaVideo.Model;
using System.IO;
using System.Data.SQLite;
using System.Data;
using System.Collections.Generic;

namespace MultiCam.UnitTests {
    [TestFixture]
    public class SqLiteTest {

        private static string name_db_test = "data.test.db";

        [SetUp]
        public void SetUp() {
            // Define connection by test database            
            var connectionString = $@"Data Source={Util.GetCurrentPath()}\{name_db_test};";
            Util.SetProperty(typeof(SqLite), "_connectionString", connectionString);
        }

        [TearDown]
        public void TearDown() {
            // Drop database test
            File.Delete($@"{Util.GetCurrentPath()}\{name_db_test}");
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
            var pathDb = $@"{Consts.CURRENT_PATH}\{Consts.DATA_PATH}";
            var fileDb = $@"{pathDb}\{Consts.NAME_FILE_DATA}";
            Directory.CreateDirectory(pathDb);

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

            Assert.Contains("devices", tables);
            Assert.Contains("configuration", tables);
        }
    }
}
