using CommandLine;
using System;
using HistogramBuilder.Domain;
using HistogramBuilder.Domain.Contract;
using HistogramBuilder.Plotter;
using Microsoft.Extensions.DependencyInjection;

namespace HistogramBuilder.Console
{
    public class Program
    {
        private class Options
        {
            [Option('i', "image", Required = true, HelpText = "Path to the image.")]
            public string ImagePath { get; set; }

            [Option('o', "output", Required = true, HelpText = "Path where the histogram should be stored.")]
            public string OutputPath { get; set; }


            [Option('d', "degree", Required = false, HelpText = "Degree of parallelism.", Default = 1)]
            public int DegreeOfParallelism { get; set; }

            [Option('p', "partitioner", Required = false, HelpText = "Enable the ParallelQuery.Partitioner.", Default = false)]
            public bool UsePartitioner { get; set; }
        }

        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args).WithParsed(options =>
            {
                var serviceCollection = new ServiceCollection();
                ConfigureServices(serviceCollection, options);
                var serviceProvider = serviceCollection.BuildServiceProvider();
                
                var app = serviceProvider.GetService<IApp>();
                app.Run(options.ImagePath, options.OutputPath);
            });
        }

        private static void ConfigureServices(IServiceCollection serviceCollection, Options options)
        {
            // Options
            serviceCollection.AddSingleton<HistogramBuildOptions>(provider =>
                new HistogramBuildOptions(options.DegreeOfParallelism, options.UsePartitioner));

            // UseCases
            serviceCollection.AddSingleton<IBuildHistogramForImageUseCase, BuildHistogramForImageUseCase>();

            // Plotter
            serviceCollection.AddSingleton<IHistogramPlotter, HistogramPlotter>();

            // App
            serviceCollection.AddSingleton<IApp, App>();
        }
    }
}