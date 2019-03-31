namespace HistogramBuilder.Domain.Contract
{
    public class RgbHistogram
    {
        public RgbHistogram(Histogram redHistogram, Histogram greenHistogram, Histogram blueHistogram)
        {
            RedHistogram = redHistogram;
            GreenHistogram = greenHistogram;
            BlueHistogram = blueHistogram;
        }

        public Histogram RedHistogram { get; }
        public Histogram GreenHistogram { get; }
        public Histogram BlueHistogram { get; }
    }
}