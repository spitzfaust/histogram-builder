using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HistogramBuilder.Domain.Contract;

namespace HistogramBuilder.Domain
{
    public class BuildHistogramForImageUseCase : IBuildHistogramForImageUseCase
    {
        public async Task<RgbHistogram> Execute(Image image)
        {
            var (reds, greens, blues) = image.Pixels.Aggregate((new List<byte>(), new List<byte>(), new List<byte>()),
                (prev, pixel) =>
                {
                    prev.Item1.Add(pixel.Red);
                    prev.Item2.Add(pixel.Green);
                    prev.Item3.Add(pixel.Blue);
                    return prev;
                });

            var redHistogramTask = BuildHistogram(reds, CancellationToken.None);
            var greenHistogramTask = BuildHistogram(greens, CancellationToken.None);
            var blueHistogramTask = BuildHistogram(blues, CancellationToken.None);

            Task.WaitAll(redHistogramTask, greenHistogramTask, blueHistogramTask);


            return new RgbHistogram(await redHistogramTask, await greenHistogramTask, await blueHistogramTask);
        }

        private Task<Histogram> BuildHistogram(IEnumerable<byte> pixels, CancellationToken cancellationToken)
        {
            var tonalValueCounts = pixels.AsParallel()
                .GroupBy(pixel => pixel)
                .ToDictionary(g => g.Key, g => g.Count());

            return Task.FromResult(new Histogram(tonalValueCounts));
        }
    }
}