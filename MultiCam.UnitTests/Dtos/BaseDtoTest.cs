using CapturaVideo.Model.Dtos;
using CapturaVideo.Model.Enums;
using NUnit.Framework;

namespace UnitTests.Dtos {
    [TestFixture]
    public class BaseDtoTest {
        [Test]
        public void Ctor() {
            var dto = new BaseDto();

            Assert.AreEqual(EDbState.Unchanged, dto.State);
        }
    }
}
