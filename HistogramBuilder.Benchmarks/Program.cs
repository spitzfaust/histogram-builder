using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Exporters.Csv;
using BenchmarkDotNet.Mathematics;
using BenchmarkDotNet.Running;
using HistogramBuilder.Domain;
using HistogramBuilder.Domain.Contract;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.PixelFormats;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HistogramBuilder.Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<BenchmarkHarness>();
        }
    }
    [HtmlExporter]
    [CsvMeasurementsExporter]
    [CsvExporter]
    [MarkdownExporter]
    [RankColumn(NumeralSystem.Arabic)]
    [RankColumn(NumeralSystem.Stars)]
    public class BenchmarkHarness
    {
        private string fileName = @"./images/tiger.jpg";
        private Image testImage;

        [GlobalSetup]
        public void GlobalSetup()
        {
            using (var image = SixLabors.ImageSharp.Image.Load<Rgb24>(fileName))
            {
                var rgbPixels = new List<RgbPixel>();
                foreach (var pixel in image.GetPixelSpan())
                {
                    rgbPixels.Add(new RgbPixel(pixel.R, pixel.G, pixel.B));
                }

                testImage = new Image(rgbPixels);
            }
        }

        //[Params(1, 2, 4, 8, 10, 12)]
        //public int MaxDegreeOfParallelism { get; set; }
        //[ParamsAllValues]
        //public bool UsePartitioning { get; set; }

        //[Benchmark]
        //public async Task Benchmark()
        //{
        //    var uc = new BuildHistogramForImageUseCase(new HistogramBuildOptions(MaxDegreeOfParallelism, UsePartitioning));
        //    await uc.Execute(testImage);
        //}

        [Benchmark(Baseline = true)]
        public async Task P_1_T()
        {
            var uc = new BuildHistogramForImageUseCase(new HistogramBuildOptions(1, true));
            await uc.Execute(testImage);
        }
        [Benchmark]
        public async Task P_1_F()
        {
            var uc = new BuildHistogramForImageUseCase(new HistogramBuildOptions(1, false));
            await uc.Execute(testImage);
        }
        [Benchmark]
        public async Task P_2_T()
        {
            var uc = new BuildHistogramForImageUseCase(new HistogramBuildOptions(2, true));
            await uc.Execute(testImage);
        }
        [Benchmark]
        public async Task P_2_F()
        {
            var uc = new BuildHistogramForImageUseCase(new HistogramBuildOptions(2, false));
            await uc.Execute(testImage);
        }
        [Benchmark]
        public async Task P_4_T()
        {
            var uc = new BuildHistogramForImageUseCase(new HistogramBuildOptions(4, true));
            await uc.Execute(testImage);
        }
        [Benchmark]
        public async Task P_4_F()
        {
            var uc = new BuildHistogramForImageUseCase(new HistogramBuildOptions(4, false));
            await uc.Execute(testImage);
        }
        [Benchmark]
        public async Task P_8_T()
        {
            var uc = new BuildHistogramForImageUseCase(new HistogramBuildOptions(8, true));
            await uc.Execute(testImage);
        }
        [Benchmark]
        public async Task P_8_F()
        {
            var uc = new BuildHistogramForImageUseCase(new HistogramBuildOptions(8, false));
            await uc.Execute(testImage);
        }
        //[Benchmark]
        //public async Task P_10_T()
        //{
        //    var uc = new BuildHistogramForImageUseCase(new HistogramBuildOptions(10, true));
        //    await uc.Execute(testImage);
        //}
        //[Benchmark]
        //public async Task P_10_F()
        //{
        //    var uc = new BuildHistogramForImageUseCase(new HistogramBuildOptions(10, false));
        //    await uc.Execute(testImage);
        //}
        //[Benchmark]
        //public async Task P_12_T()
        //{
        //    var uc = new BuildHistogramForImageUseCase(new HistogramBuildOptions(12, true));
        //    await uc.Execute(testImage);
        //}
        //[Benchmark]
        //public async Task P_12_F()
        //{
        //    var uc = new BuildHistogramForImageUseCase(new HistogramBuildOptions(12, false));
        //    await uc.Execute(testImage);
        //}

    }
}
