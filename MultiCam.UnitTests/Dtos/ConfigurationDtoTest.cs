using CapturaVideo.Model.Dtos;
using CapturaVideo.Model.Enums;
using MultiCam.UnitTests;
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

            Util.SetProperty(dto, "_font", null);
            Util.SetProperty(dto, "_fontSize", 12.0f);
            Util.SetProperty(dto, "_fontFamily", "Comic Sans MS");

            Assert.AreEqual(12.0f, dto.Font.Size);
            Assert.AreEqual("Comic Sans MS", dto.Font.FontFamily.Name);

            Assert.False(dto.EnableStart);
            Assert.False(dto.EnableStartMinimized);
        }
    }
}
