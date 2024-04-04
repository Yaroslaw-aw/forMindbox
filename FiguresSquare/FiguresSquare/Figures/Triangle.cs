using FiguresSquare.Contracts;
using FiguresSquare.Exceptions;

namespace FiguresSquare.Figures
{
    public class Triangle : IFigure
    {
        private double sideA;
        private double sideB;
        private double sideC;
        private readonly TriangleType type;
        public double SideA
        { 
            get { return sideA; }
            
            private set { sideA = SetSide(value); } 
        }

        public double SideB
        {
            get { return sideB; }

            private set { sideB = SetSide(value); }
        }

        public double SideC
        {
            get { return sideC; }

            private set { sideC = SetSide(value); }
        }

        public Triangle(double sideA, double sideB, double sideC)
        {
            SideA = sideA;
            SideB = sideB;
            SideC = sideC;

            if (isTriangle())
                this.type = CheckType();
            else
                throw new NotCorrectSideLengthException("The figure can't be triangle with this sides");
        }
        public static Triangle CreateNewTriangle(double sideA, double sideB, double sideC)
        {
            Triangle newTriangle = new Triangle(sideA, sideB, sideC);
            return newTriangle;
        }

        public double CalculateSquare()
        {
            double p = (sideA + sideB + sideC) / 2.0;
            double square = Math.Sqrt(p * Math.Abs((p - sideA)) * Math.Abs((p - sideB)) * Math.Abs((p - sideC)));
            return square;
        }

        public string TypeOfTriangle()
        {
            return type.ToString();
        }


        private double SetSide(double side)
        {
            if (side <= 0) throw new NotCorrectSideLengthException("Side of triangle can't be zero or negative");

            return side;
        }

        private bool isTriangle()
        {
            if (sideA + sideB > sideC ||
                sideB + sideC > sideA ||
                sideC + sideA > sideB) return true;

            return false;
        }

        private TriangleType CheckType()
        {
            if (sideA == sideB && sideA == sideC) return TriangleType.Equilateral;

            if (sideA == sideB && sideA != sideC ||
                sideA == sideC && sideA != sideB ||
                sideB == sideC && sideB != sideA) return TriangleType.Isosceles;

            if (sideA * sideA == sideB * sideB + sideC * sideC ||
                sideB * sideB == sideA * sideA + sideC * sideC ||
                sideC * sideC == sideA * sideA + sideB * sideB) return TriangleType.RightAngled;

            return TriangleType.Other;
        }
    }
}
