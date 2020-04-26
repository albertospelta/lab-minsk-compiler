namespace Minsk.CodeAnalysis
{
    public struct TextSpan
    {
        public TextSpan(int start, int lenght)
        {
            Start = start;
            Lenght = lenght;
        }

        public int Start { get; }
        public int Lenght { get; }
        public int End => Start + Lenght;
    }
}
