using System.IO;
using HistogramBuilder.Domain.Contract;

namespace HistogramBuilder.Plotter
{
    public interface IHistogramPlotter
    {
        void PlotHistogramAsSvg(RgbHistogram rgbHistogram, Stream outputStream);
    }
}