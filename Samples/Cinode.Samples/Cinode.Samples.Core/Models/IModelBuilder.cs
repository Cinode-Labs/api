
using System.Threading.Tasks;

namespace Cinode.Samples.Core.Models
{
    public interface IModelBuilder<TModel>
    {
        Task<TModel> BuildAsync();
    }
}
