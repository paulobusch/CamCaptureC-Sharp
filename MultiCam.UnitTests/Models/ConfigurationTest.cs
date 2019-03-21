using CapturaVideo.Model;
using CapturaVideo.Model.Dtos;
using CapturaVideo.Model.Enums;
using Moq;
using NUnit.Framework;
using Smocks;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Urasandesu.Prig;

namespace MultiCam.UnitTests {
    [TestFixture]
    class ConfigurationTest : Base {

        [Test]
        public void SaveAndLoadConfiguration() {
            var devicesDto = new DeviceDto() {
                ConfigurationId = Util.RandInt(),
                MonikerString = Util.RandString(),
                Size = new Size(Util.RandInt(), Util.RandInt()),
                State = EDbState.Create
            };

            var fontFamily = FontFamily.Families[
                Util.RandInt(0, FontFamily.Families.Length)];

            var configDto = new ConfigurationDto() {
                Id = devicesDto.ConfigurationId,
                TimeInterval = Util.RandInt(),
                EnableInterval = Util.RandBool(),
                EnableServer = Util.RandBool(),
                EnableCompressVideo = Util.RandBool(),
                ViewDateTime = Util.RandBool(),
                PathSaveVideo = Util.RandString(),
                FrameRate = Util.RandInt(),
                BitRate = Util.RandInt(),
                LegendAlign = (ELegendAlign)Util.RandInt(0, 3),
                EnableStart = Util.RandBool(),
                EnableStartMinimized = Util.RandBool(),
                Font = new Font(fontFamily, Util.RandFloat()),
                State = EDbState.Create,
                Devices = new List<DeviceDto>() { devicesDto }
            };

            Config.SaveConfiguration(configDto);

            var configData = Config.LoadConfiguration();

            // Configuration
            Assert.NotNull(configData.Id);
            Assert.NotZero(configData.Id);
            Assert.AreEqual(configDto.TimeInterval, configData.TimeInterval);
            Assert.AreEqual(configDto.EnableInterval, configData.EnableInterval);
            Assert.AreEqual(configDto.EnableServer, configData.EnableServer);
            Assert.AreEqual(configDto.EnableCompressVideo, configData.EnableCompressVideo);
            Assert.AreEqual(configDto.ViewDateTime, configData.ViewDateTime);
            Assert.AreEqual(configDto.PathSaveVideo, configData.PathSaveVideo);
            Assert.AreEqual(configDto.FrameRate, configData.FrameRate);
            Assert.AreEqual(configDto.BitRate, configData.BitRate);
            Assert.AreEqual(configDto.LegendAlign, configData.LegendAlign);
            Assert.AreEqual(configDto.EnableStart, configData.EnableStart);
            Assert.AreEqual(configDto.EnableStartMinimized, configData.EnableStartMinimized);
            Assert.AreEqual(configDto.Font.FontFamily.Name, configData.Font.FontFamily.Name);
            Assert.AreEqual(configDto.Font.Size, configData.Font.Size);
            Assert.AreEqual(configDto.State, EDbState.Unchanged);

            // Device
            Assert.NotNull(configData);
            Assert.NotNull(configData.Devices);
            Assert.NotZero(configData.Devices.Count());

            var deviceData = configData.Devices.First();

            Assert.NotNull(deviceData.ConfigurationId);
            Assert.NotZero(deviceData.ConfigurationId);
            Assert.AreEqual(devicesDto.MonikerString, deviceData.MonikerString);
            Assert.AreEqual(devicesDto.Size.Width, deviceData.Size.Width);
            Assert.AreEqual(devicesDto.Size.Height, deviceData.Size.Height);
            Assert.AreEqual(devicesDto.State, EDbState.Unchanged);
            
        }
    }
}
