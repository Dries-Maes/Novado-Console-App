using Novado_Console_App;
using Novado_Console_App.Resources;

namespace Novado_Tests
{
    public class FractalTests
    {
        [Fact]
        public void Fractal_SeedTest_2_Dimentions()
        {
            //Arrange
            char[,] fractalKey = { { '#', '.' }, { '.', '.' } };
            char[,] fractalValue = { { '$', '#', '.' }, { '.', '#', '.' }, { '#', '#', '.' } };
            //Act
            FractalSeedFactory.ProduceKeys(fractalKey, fractalValue);
            FractalSeedFactory.RemoveDoublesFromSourceKey();
            //Assert
            Assert.True(FractalSeedFactory.SourceKeys.Count(x => x.Value[0, 0] == '$') == 4);
        }

        [Fact]
        public void Fractal_SeedTest_3_Dimentions()
        {
            //Arrange
            char[,] fractalKey = { { '.', '#', '.' }, { '.', '.', '#' }, { '#', '#', '#' } };
            char[,] fractalValue = { { '@', '.', '.', '#' }, { '.', '.', '.', '.' }, { '.', '.', '.', '.' }, { '#', '.', '.', '#' } };
            //Act
            FractalSeedFactory.ProduceKeys(fractalKey, fractalValue);
            FractalSeedFactory.RemoveDoublesFromSourceKey();
            //Assert
            Assert.True(FractalSeedFactory.SourceKeys.Count(x => x.Value[0, 0] == '@') == 8);
        }

        [Theory]
        [InlineData(171, 5)]
        [InlineData(2498142, 18)]
        public void Fractal_Pixeltest_iterations(int amountOfPixels, int numberOfItirations)
        {
            int pixels = FractalLogic.FractalLogicMethod(numberOfItirations);
            Assert.True(pixels == amountOfPixels);
        }
    }
}