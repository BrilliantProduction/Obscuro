using Obscuro.Abstract;
using Obscuro.Abstract.Unpacking.Inputs;

namespace Obscuro.Pipeline.Unpacking.Inputs
{
    public class ObscuroInputProvider : IObscuroInputProvider
    {
        public IObscuroInput Create(IObscuroContext context)
        {
            if (context.Settings.Packing == AppPackingType.Combined)
                return new SingleStreamInput(context);

            return new AssemblyStreamInput(context);
        }
    }
}
