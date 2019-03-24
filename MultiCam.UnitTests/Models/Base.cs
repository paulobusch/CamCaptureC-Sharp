using CapturaVideo.Model;
using MultiCam.UnitTests;
using NUnit.Framework;
using System.IO;

namespace MultiCam.UnitTests {
    [TestFixture]
    public class Base {
        protected static string name_db_test;
        protected static string base_path_db;
        protected static IContextDb context;

        [SetUp]
        public void SetUp() {
            name_db_test = "data.test.db";
            base_path_db = $@"{Util.GetCurrentPath()}\{Consts.DATA_PATH}";

            Directory.CreateDirectory(base_path_db);

            Util.SetProperty(typeof(Consts), "NAME_FILE_DATA", name_db_test);
            Util.SetProperty(typeof(Consts), "CURRENT_PATH", Util.GetCurrentPath());

            context = new SqLite();
        }

        [TearDown]
        public void TearDown() {
            // Drop database test
            File.Delete($@"{base_path_db}\{name_db_test}");
        }
    }
}
