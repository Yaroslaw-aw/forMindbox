using FiguresSquare.Contracts;

namespace FiguresSquare
{
    public class SquareCalculator : ISquareCalculator
    {
        private readonly IFigure? figure;

        public SquareCalculator() { }
        public SquareCalculator(IFigure figure)
        {
            this.figure = figure;
        }

        public async Task<double?> CalculateSquareAsync()
        {
            if (this.figure is null)
                return null;
            else
                return await Task.Run(this.figure.CalculateSquare);

        }

        public async Task<double> CalculateSquareAsync(IFigure figure)
        {
            return await Task.Run(figure.CalculateSquare);
        }
    }
}
