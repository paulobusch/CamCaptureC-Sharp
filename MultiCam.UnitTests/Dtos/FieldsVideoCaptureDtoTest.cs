using CapturaVideo.Model.Dtos;
using NUnit.Framework;

namespace MultiCam.UnitTests.Dtos
{
    [TestFixture]
    public class FieldsVideoCaptureDtoTest
    {
        [Test]
        public void Ctor()
        {
            var dto = new FieldsVideoCaptureDto(null, null, null, null, null, null, null, null);

            Assert.Null(dto.ListViewDevices);
            Assert.Null(dto.TimerVideoInterval);
            Assert.Null(dto.ImageGrid);
            Assert.Null(dto.ImageState);
            Assert.Null(dto.CmbDevices);
            Assert.Null(dto.CmbResolution);
            Assert.Null(dto.BoxImage);
            Assert.Null(dto.LblLink);
        }
    }
}
