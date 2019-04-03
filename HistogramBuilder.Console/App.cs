using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HistogramBuilder.Domain;
using HistogramBuilder.Domain.Contract;
using HistogramBuilder.Plotter;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.PixelFormats;

namespace HistogramBuilder.Console
{
    public class App : IApp
    {
        private readonly IBuildHistogramForImageUseCase buildHistogramForImageUseCase;
        private readonly IHistogramPlotter histogramPlotter;

        public App(IBuildHistogramForImageUseCase buildHistogramForImageUseCase, IHistogramPlotter histogramPlotter)
        {
            this.buildHistogramForImageUseCase = buildHistogramForImageUseCase;
            this.histogramPlotter = histogramPlotter;
        }

        public void Run(string imagePath, string outputPath)
        {
            System.Console.WriteLine($"Reading image from '{imagePath}'");
            System.Console.WriteLine($"Writing histogram to '{outputPath}'");

            if (!File.Exists(imagePath))
            {
                throw new InvalidOperationException("Image does not exist.");
            }

            RgbHistogram histogram;

            using (var image = SixLabors.ImageSharp.Image.Load<Rgb24>(imagePath))
            {
                var rgbPixels = new List<RgbPixel>();
                foreach (var pixel in image.GetPixelSpan())
                {
                    rgbPixels.Add(new RgbPixel(pixel.R, pixel.G, pixel.B));
                }

                var domainImage = new Image(rgbPixels);
                histogram = buildHistogramForImageUseCase.Execute(domainImage).Result;
            }

            using (var outputStream = File.OpenWrite(outputPath))
            {
                histogramPlotter.PlotHistogramAsSvg(histogram, outputStream);
            }
        }
    }
}