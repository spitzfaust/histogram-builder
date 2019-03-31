using System.Collections.Generic;

namespace HistogramBuilder.Domain.Contract
{
    public class Image
    {
        public Image(IEnumerable<RgbPixel> pixels)
        {
            Pixels = pixels;
        }

        public IEnumerable<RgbPixel> Pixels { get; }
    }
}