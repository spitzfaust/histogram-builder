using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using HistogramBuilder.Domain.Contract;
using Xunit;

namespace HistogramBuilder.Domain.Tests
{
    public class BuildHistogramForImageUseCaseTests
    {
        private BuildHistogramForImageUseCase sut;


        [Fact]
        public async Task ItShallCreateAHistogramWithoutPartitioning()
        {

            // Given
            sut = new BuildHistogramForImageUseCase(new HistogramBuildOptions(1, false));
            var image = new Image(new[]
            {
                new RgbPixel(1, 2, 3),
                new RgbPixel(1, 2, 3),
                new RgbPixel(1, 2, 3),
                new RgbPixel(1, 2, 3),
            });

            // When
            var actual = await sut.Execute(image);

            // Then
            var expected = new RgbHistogram(
                new Histogram(new Dictionary<byte, int> {{1, 4}}),
                new Histogram(new Dictionary<byte, int> {{2, 4}}),
                new Histogram(new Dictionary<byte, int> {{3, 4}}));
            actual.Should().BeEquivalentTo(expected);
        }
        [Fact]
        public async Task ItShallCreateAHistogramWithPartitioning()
        {

            // Given
            sut = new BuildHistogramForImageUseCase(new HistogramBuildOptions(1, true));
            var image = new Image(new[]
            {
                new RgbPixel(1, 2, 3),
                new RgbPixel(1, 2, 3),
                new RgbPixel(1, 2, 3),
                new RgbPixel(1, 2, 3),
            });

            // When
            var actual = await sut.Execute(image);

            // Then
            var expected = new RgbHistogram(
                new Histogram(new Dictionary<byte, int> { { 1, 4 } }),
                new Histogram(new Dictionary<byte, int> { { 2, 4 } }),
                new Histogram(new Dictionary<byte, int> { { 3, 4 } }));
            actual.Should().BeEquivalentTo(expected);
        }
    }
}