using System;
using System.IO;
using System.Linq;
using HistogramBuilder.Domain.Contract;
using OxyPlot;
using OxyPlot.Series;

namespace HistogramBuilder.Plotter
{
    public class HistogramPlotter : IHistogramPlotter
    {
        public void PlotHistogramAsSvg(RgbHistogram rgbHistogram, Stream outputStream)
        {
            var model = new PlotModel {Title = "Color histogram"};
            model.Series.Add(BuildHistogramArea(rgbHistogram.RedHistogram, OxyColor.FromArgb(150, 255, 0, 0)));
            model.Series.Add(BuildHistogramArea(rgbHistogram.GreenHistogram, OxyColor.FromArgb(150, 0, 255, 0)));
            model.Series.Add(BuildHistogramArea(rgbHistogram.BlueHistogram, OxyColor.FromArgb(150, 0, 0, 255)));
            ExportAsSvg(model, outputStream);
        }

        private static void ExportAsSvg(IPlotModel model, Stream outputStream)
        {
            var exporter = new SvgExporter();
            exporter.Export(model, outputStream);
        }

        private static AreaSeries BuildHistogramArea(Histogram histogram, OxyColor color)
        {
            var areaSeries = new AreaSeries();

            var dataPoints = Enumerable.Range(0, 255)
                .Select(i => (byte) i)
                .Select(i => (i, histogram.TonalValueCounts.ContainsKey(i) ? histogram.TonalValueCounts[i] : 0))
                .Select(pair => new DataPoint(x: pair.Item1, y: pair.Item2));

            areaSeries.Points.AddRange(dataPoints);
            areaSeries.Color = color;

            return areaSeries;
        }
    }
}