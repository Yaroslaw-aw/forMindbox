using FiguresSquare;
using FiguresSquare.Contracts;
using FiguresSquare.Figures;

namespace TestSquareCalculator
{
    internal static class Program
    {
        static async Task Main(string[] args)
        {
            

            IFigure testTriangle = new Triangle(3, 4, 8);
            IFigure circle = new Circle(0.5);

            IFigure triangle = Triangle.CreateNewTriangle(4, 5, 6);

            if (testTriangle is Triangle) 
                await Console.Out.WriteLineAsync($"Type of triangle is {(testTriangle as Triangle)?.TypeOfTriangle()}");

            TaskStarter(Task.Run(async () =>
            {
                for ( ; ; )
                {
                    Console.Write("*");
                    await Task.Delay(100);
                }
            }));

            SquareCalculator squareCalculator = new SquareCalculator(testTriangle);

            await Task.Delay(1000);
            Console.WriteLine("\n" + await squareCalculator.CalculateSquareAsync());

            double res = await squareCalculator.CalculateSquareAsync(circle);

            await Task.Delay(1000);
            Console.WriteLine("\n" + res);

            await Task.Delay(1000);
            Console.WriteLine("\n" + await squareCalculator.CalculateSquareAsync(triangle));
        }

        private static void TaskStarter(this Task t)
        {
            ;
        }
    }
}
