using FiguresSquare;

namespace MSTests
{
    [TestClass]
    public class SquareCalculatorTests
    {
        [TestMethod]
        public async Task Test1_CalculateSquareAsync()
        {
            SquareCalculator squareCalculator = new SquareCalculator();

            double? square = await squareCalculator.CalculateSquareAsync();

            Assert.IsNull(square);
        }
    }
}
