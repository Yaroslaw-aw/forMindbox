using FiguresSquare.Contracts;
using FiguresSquare.Exceptions;

namespace FiguresSquare.Figures
{
    public class Circle : IFigure
    {
        public double Radius { get; }

        public Circle(double? radius)
        {
            Radius = radius ?? throw new ArgumentNullException("Radius can't be null");

            if (radius <= 0) throw new NotCorrectRadiusException("Radius can't be zero or negative");
        }

        public static Circle CreateNewCircle(double? radius)
        {
            Circle newCircle = new Circle(radius);
            return newCircle;
        }

        public double CalculateSquare()
        {
            return Math.PI * Radius * Radius;
        }
    }
}
