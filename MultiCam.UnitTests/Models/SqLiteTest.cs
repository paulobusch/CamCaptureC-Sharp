using CapturaVideo;
using NUnit.Framework;
using Moq;
using CapturaVideo.Model;
using System.Reflection;
using System;
using System.IO;
using System.Data.SQLite;
using System.Data;

namespace MultiCam.UnitTests {
    [TestFixture]
    public class SqLiteTest {

        [SetUp]
        public void SetUp() {
            // Define connection by test database
            var connectionString = $@"Data Source=:memory:";
            Util.SetProperty(typeof(SqLite), "_connectionString", connectionString);
        }

        [TearDown]
        public void TearDown() {

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

        }
    }
}
