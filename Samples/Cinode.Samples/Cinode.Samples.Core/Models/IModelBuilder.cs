
using System.Threading.Tasks;

namespace Cinode.Samples.Models
{
    public interface IModelBuilder<TModel>
    {
        Task<TModel> BuildAsync();
    }
}
