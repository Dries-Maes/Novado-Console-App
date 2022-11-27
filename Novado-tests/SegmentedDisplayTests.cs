using Novado_Main;

namespace Novado_Tests
{
    public class SegmentedDisplayTests
    {
        [Fact]
        public void Fractal_Test()
        {
            int sumOfDigits = SegmentedDisplay.SegmentedDisplayMethod();
            Assert.Equal(sumOfDigits, 908067);
        }
    }
}