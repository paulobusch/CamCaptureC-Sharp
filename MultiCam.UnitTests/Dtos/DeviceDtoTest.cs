using CapturaVideo.Model.Dtos;
using CapturaVideo.Model.Enums;
using NUnit.Framework;
using System.Drawing;

namespace UnitTests.Dtos {
    [TestFixture]
    public class DeviceDtoTest {
        [Test]
        public void Ctor() {
            var dto = new DeviceDto();

            Assert.Null(dto.MonikerString);
            Assert.Zero(dto.ConfigurationId);
            Assert.AreEqual(EDbState.Unchanged, dto.State);
            Assert.AreEqual(640, dto.Size.Width);
            Assert.AreEqual(480, dto.Size.Height);
        }
    }
}
