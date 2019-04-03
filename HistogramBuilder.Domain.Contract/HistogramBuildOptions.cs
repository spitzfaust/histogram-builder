namespace HistogramBuilder.Domain.Contract
{
    public class HistogramBuildOptions
    {
        public HistogramBuildOptions(int degreeOfParallelism)
        {
            DegreeOfParallelism = degreeOfParallelism;
        }

        public int DegreeOfParallelism { get; }
    }
}