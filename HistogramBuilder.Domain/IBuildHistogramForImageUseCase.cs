using System.Threading.Tasks;
using HistogramBuilder.Domain.Contract;

namespace HistogramBuilder.Domain
{
    public interface IBuildHistogramForImageUseCase
    {
        Task<RgbHistogram> Execute(Image image);
    }
}