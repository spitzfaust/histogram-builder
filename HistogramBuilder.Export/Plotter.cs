using System;
using System.IO;
using System.Linq;
using HistogramBuilder.Domain.Contract;
using OxyPlot;
using OxyPlot.Series;

namespace HistogramBuilder.Plotter
{
    public class Plotter
    {
        public Plotter(RgbHistogram rgbHistogram)
        {
            this.Model = new PlotModel {Title = "Color histogram"};
            this.Model.Series.Add(BuildHistogramArea(rgbHistogram.RedHistogram, OxyColor.FromArgb(150, 255, 0, 0)));
            this.Model.Series.Add(BuildHistogramArea(rgbHistogram.GreenHistogram, OxyColor.FromArgb(150, 0, 255, 0)));
            this.Model.Series.Add(BuildHistogramArea(rgbHistogram.BlueHistogram, OxyColor.FromArgb(150, 0, 0, 255)));
        }

        private PlotModel Model { get; }

        private AreaSeries BuildHistogramArea(Histogram histogram, OxyColor color)
        {
            var areaSeries = new AreaSeries();

            var dataPoints = Enumerable.Range(0, 255)
                .Select(i => (byte) i)
                .Select(i => (i, histogram.TonalValueCounts.ContainsKey(i) ? histogram.TonalValueCounts[i] : 0))
                .Select(pair => new DataPoint(x: pair.Item1, y: pair.Item2))
                .ToList();

            areaSeries.Points.AddRange(dataPoints);
            areaSeries.Color = color;

            return areaSeries;
        }

        public void ExportAsSvg(string fileName, int width, int height)
        {
            using (var stream = File.Create(fileName))
            {
                var exporter = new SvgExporter {Width = width, Height = height};
                exporter.Export(Model, stream);
            }
        }
    }
}