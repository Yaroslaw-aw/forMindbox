using FiguresSquare.Exceptions;
using FiguresSquare.Figures;

namespace MSTests
{
    [TestClass]
    public class CircleTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Method should throw ArgumentNullException")]
        public void Test1_Circle_CalculateSquare()
        {
            Circle testCircle = new Circle(null);
        }

        [TestMethod]
        [ExpectedException(typeof(NotCorrectRadiusException), "Method should throw NotCorrectRadiusException")]
        public void Test2_Circle_CalculateSquare()
        {
            Circle testCircle = new Circle(0);
        }

        [TestMethod]
        [ExpectedException(typeof(NotCorrectRadiusException), "Method should throw NotCorrectRadiusException")]
        public void Test3_Circle_CalculateSquare()
        {
            Circle testCircle = new Circle(-0.5);
        }

        [TestMethod]        
        public void Test4_Circle_CreateNewCircle()
        {
            Circle testCircle = Circle.CreateNewCircle(5);
            Assert.IsInstanceOfType(testCircle, typeof(Circle));
        }

        [TestMethod]
        public void Test5_Circle_CalculateSquare()
        {
            Circle testCircle = new Circle(5);

            Circle testCircle2 = Circle.CreateNewCircle(10.25);
            double tolerance = 0.01;

            Assert.AreEqual(25, testCircle.CalculateSquare() / Math.PI);
            Assert.AreEqual(105.06, testCircle2.CalculateSquare() / Math.PI, tolerance);
        }
    }
}