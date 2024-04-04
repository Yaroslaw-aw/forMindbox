using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FiguresSquare.Contracts
{
    internal interface ISquareCalculator
    {
        Task<double?> CalculateSquareAsync();
        Task<double> CalculateSquareAsync(IFigure figure);
    }
}
