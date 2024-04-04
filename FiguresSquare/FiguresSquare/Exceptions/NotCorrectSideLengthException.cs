namespace FiguresSquare.Exceptions
{
    public class NotCorrectSideLengthException : Exception
    {
        public NotCorrectSideLengthException() { }
        public NotCorrectSideLengthException(string message)
            : base(message) { }
    }
}
