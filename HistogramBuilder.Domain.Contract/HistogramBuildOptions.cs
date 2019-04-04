namespace HistogramBuilder.Domain.Contract
{
    public class HistogramBuildOptions
    {
        public HistogramBuildOptions(int degreeOfParallelism, bool usePartitioning)
        {
            DegreeOfParallelism = degreeOfParallelism;
            UsePartitioning = usePartitioning;
        }

        public int DegreeOfParallelism { get; }
        public bool UsePartitioning { get; }
    }
}