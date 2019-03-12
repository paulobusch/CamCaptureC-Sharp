using CapturaVideo.Model.Dtos;
using CapturaVideo.Model.Enums;
using NUnit.Framework;
using System.Drawing;

namespace UnitTests.Dtos {
    [TestFixture]
    public class ConfigurationDtoTest {
        [Test]
        public void Ctor() {
            var dto = new ConfigurationDto();

            Assert.Zero(dto.Id);
            Assert.AreEqual(dto.TimeInterval, 5);
            Assert.False(dto.EnableInterval);
            Assert.False(dto.EnableServer);
            Assert.NotNull(dto.Devices);

            Assert.False(dto.ViewDateTime);
            Assert.False(dto.EnableCompressVideo);
            Assert.NotNull(dto.PathSaveVideo);
            Assert.AreEqual(15,dto.FrameRate);
            Assert.AreEqual(100000, dto.BitRate);
            Assert.AreEqual(ELegendAlign.BottonRight, dto.LegendAlign);
            Assert.NotNull(dto.Font);
            Assert.AreEqual(new Font("Arial", 10.0f), dto.Font);

            Assert.False(dto.EnableStart);
            Assert.False(dto.EnableStartMinimized);
        }
    }
}
