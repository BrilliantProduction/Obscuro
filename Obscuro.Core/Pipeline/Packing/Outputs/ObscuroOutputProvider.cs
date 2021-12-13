
using Obscuro.Abstract;
using Obscuro.Abstract.Packing.Outputs;

namespace Obscuro.Pipeline.Packing.Outputs
{
    class ObscuroOutputProvider : IObscuroOutputProvider
    {
        public IObscuroOutput Create(IObscuroContext context)
        {
            if (context.Settings.Packing == AppPackingType.Splitted)
                return new MultiStreamOutput(context);

            return new SingleStreamOutput(context.FileName);
        }
    }
}
