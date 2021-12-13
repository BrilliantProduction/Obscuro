
using Obscuro.Abstract.Unpacking.Inputs;
using Obscuro.Models.Data;

namespace Obscuro.Abstract.Unpacking
{
    public interface IObscuroUnpacker
    {
        ObscuroApplication Unpack(IObscuroContext context, IObscuroInput input);
    }
}
