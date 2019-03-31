namespace HistogramBuilder.Domain.Contract
{
    public class RgbPixel
    {
        public RgbPixel(byte red, byte green, byte blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }

        public byte Red { get; }
        public byte Green { get; }
        public byte Blue { get; }
    }
}