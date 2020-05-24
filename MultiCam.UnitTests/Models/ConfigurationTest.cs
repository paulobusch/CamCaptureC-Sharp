using NUnit.Framework;
using Moq;

using MultiCam.Domain.Enums; 
using MultiCam.Domain.Entities; 
using MultiCam.Domain.DataContext; 
using MultiCam.Domain.Repository; 
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace MultiCam.UnitTests {
    [TestFixture]
    class ConfigurationTest : Base {

        [Test]
        public void SaveAndLoadConfiguration() {
            //// Commom Arrage
            //var mockNotigication = new Mock<INotification>();
            //var config = new Config(context, mockNotigication.Object);


            //// ** Configuration ** //
            //// Arrage
            //var fontFamily = FontFamily.Families[
            //    Util.RandInt(0, FontFamily.Families.Length)];

            //var configDto = new Configuration() {
            //    Id = Util.RandInt(),
            //    TimeInterval = Util.RandInt(),
            //    EnableInterval = Util.RandBool(),
            //    EnableServer = Util.RandBool(),
            //    EnableCompressVideo = Util.RandBool(),
            //    ViewDateTime = Util.RandBool(),
            //    PathSaveVideo = Util.RandString(),
            //    FrameRate = Util.RandInt(),
            //    BitRate = Util.RandInt(),
            //    LegendAlign = (ELegendAlign)Util.RandInt(0, 3),
            //    EnableStart = Util.RandBool(),
            //    EnableStartMinimized = Util.RandBool(),
            //    Font = new Font(fontFamily, Util.RandFloat()),
            //    State = EDbState.Create,
            //    Devices = null
            //};

            //// Act
            //config.SaveConfiguration(configDto);

            //var configData = config.LoadConfiguration();

            //// Assert
            //Assert.NotNull(configData.Id);
            //Assert.NotZero(configData.Id);
            //Assert.AreEqual(configDto.TimeInterval, configData.TimeInterval);
            //Assert.AreEqual(configDto.EnableInterval, configData.EnableInterval);
            //Assert.AreEqual(configDto.EnableServer, configData.EnableServer);
            //Assert.AreEqual(configDto.EnableCompressVideo, configData.EnableCompressVideo);
            //Assert.AreEqual(configDto.ViewDateTime, configData.ViewDateTime);
            //Assert.AreEqual(configDto.PathSaveVideo, configData.PathSaveVideo);
            //Assert.AreEqual(configDto.FrameRate, configData.FrameRate);
            //Assert.AreEqual(configDto.BitRate, configData.BitRate);
            //Assert.AreEqual(configDto.LegendAlign, configData.LegendAlign);
            //Assert.AreEqual(configDto.EnableStart, configData.EnableStart);
            //Assert.AreEqual(configDto.EnableStartMinimized, configData.EnableStartMinimized);
            //Assert.AreEqual(configDto.Font.FontFamily.Name, configData.Font.FontFamily.Name);
            //Assert.AreEqual(configDto.Font.Size, configData.Font.Size);
            //Assert.AreEqual(configDto.State, EDbState.Unchanged);
            //Assert.Null(configDto.Devices);


            //// ** Device ** //
            //// Arrage
            //var devicesDto = new DeviceDto()
            //{
            //    ConfigurationId = configDto.Id,
            //    MonikerString = Util.RandString(),
            //    Size = new Size(Util.RandInt(), Util.RandInt()),
            //    State = EDbState.Create
            //};
            //configDto.Devices = new List<DeviceDto>() { devicesDto };
            //configDto.State = EDbState.Create;

            //// Act
            //config.SaveConfiguration(configDto);

            //configData = config.LoadConfiguration();

            //// Device
            //Assert.NotNull(configData);
            //Assert.NotNull(configData.Devices);
            //Assert.NotZero(configData.Devices.Count());

            //var deviceData = configData.Devices.First();

            //Assert.NotNull(deviceData.ConfigurationId);
            //Assert.NotZero(deviceData.ConfigurationId);
            //Assert.AreEqual(devicesDto.MonikerString, deviceData.MonikerString);
            //Assert.AreEqual(devicesDto.Size.Width, deviceData.Size.Width);
            //Assert.AreEqual(devicesDto.Size.Height, deviceData.Size.Height);
            //Assert.AreEqual(devicesDto.State, EDbState.Unchanged);
        }

        [Test]
        public void FeedbackThrowSaveAndLoad() {
            //// Arrage
            //var mockNotigication = new Mock<INotification>();
            //var mockContextDb = new Mock<IContextDb>();
            //var mockConfigDto = new Mock<ConfigurationDto>();
            //var config = new Config(mockContextDb.Object, mockNotigication.Object);

            //mockConfigDto.Setup(m => m.State).Returns(EDbState.Create);
            //mockContextDb.Setup(m => m.DatabaseExists()).Returns(true);
            //mockContextDb.Setup(m => m.NewConnection()).Throws(new System.Exception());

            //// Act
            //config.SaveConfiguration(mockConfigDto.Object);
            //config.LoadConfiguration();

            //// Asset
            //mockNotigication.Verify(m => m.FailMessage(It.IsAny<string>()), Times.Exactly(2));
        }

        [Test]
        public void EmptyDatabase()
        {
            //// Commom Arrage
            //var mockNotigication = new Mock<INotification>();
            //var mockContextDb = new Mock<IContextDb>();
            //var mockConfigDto = new Mock<ConfigurationDto>();
            //var config = new Config(mockContextDb.Object, mockNotigication.Object);
                        
            //// *** SaveConfiguration *** //
            //// Arrage
            //mockConfigDto.Setup(m => m.State).Returns(EDbState.Unchanged);

            //// Act
            //config.SaveConfiguration(mockConfigDto.Object);

            //// Assert
            //mockContextDb.Verify(m => m.NewConnection(), Times.Never());

            //// *** LoadConfiguration *** //
            //// Arrage
            //mockContextDb.Setup(m => m.DatabaseExists()).Returns(false);

            //// Act
            //config.LoadConfiguration();

            //// Assert
            //mockContextDb.Verify(m => m.CreateDatabase(), Times.Once());

        }
    }
}
