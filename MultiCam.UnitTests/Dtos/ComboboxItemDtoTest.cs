using CapturaVideo.Model.Dtos;
using MultiCam.UnitTests;
using NUnit.Framework;

namespace UnitTests.Dtos {
    [TestFixture]
    public class ComboboxItemDtoTest {
        [Test]
        public void Ctor() {
            var dto = new ComboboxItemDto();

            Assert.Null(dto.Name);
            Assert.Null(dto.Value);

            var _int = Util.RandInt();
            var _bool = Util.RandBool();
            var _string = Util.RandString();

            dto.Value = _int;
            Assert.AreEqual(_int, dto.GetValue<int>());

            dto.Value = _bool;
            Assert.AreEqual(_bool, dto.GetValue<bool>());

            dto.Value = _string;
            Assert.AreEqual(_string, dto.GetValue<string>());
        }
    }
}
