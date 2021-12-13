
using Obscuro.Abstract.Packing.Outputs;

namespace Obscuro.Abstract.Packing
{
    public interface IObscuroPacker
    {
        void Pack(IObscuroContext context, IObscuroOutput output);
    }
}
