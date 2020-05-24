using NUnit.Framework;
using System.IO;
using System.Data.SQLite;
using System.Data;
using System.Collections.Generic;
using MultiCam.Domain.DataContext;

namespace MultiCam.UnitTests {
    [TestFixture]
    public class SqLiteTest : Base {

        [Test]
        public void NewConnection() {
            var connection = Util.GetProperty<string>(typeof(SqLite), "_connectionString");
            Assert.NotNull(connection);

            using (var cnn = context.NewConnection()) {
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
            Assert.True(context.DatabaseExists());

            File.Delete(fileDb);
            Assert.False(context.DatabaseExists());
        }

        [Test]
        public void CreateDatabase() {
            var tables = new List<string>();
            var listTables = "select name from sqlite_master where type='table' order by name";

            context.CreateDatabase();
            using (var cnn = context.NewConnection()) {
                cnn.Open();
                using (var command = new SQLiteDataAdapter(listTables, cnn)) {
                    var dt = new DataTable();
                    command.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                        tables.Add((string)row["name"]);
                }
            }
            context.CreateDatabase();

            Assert.Contains("devices", tables);
            Assert.Contains("configuration", tables);
        }
    }
}
