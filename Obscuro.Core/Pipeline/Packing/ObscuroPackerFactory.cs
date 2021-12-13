
using Obscuro.Abstract;
using Obscuro.Abstract.Packing;
using Obscuro.Pipeline.Packing.Packers;

namespace Obscuro.Pipeline.Packing
{
    class ObscuroPackerFactory : IObscuroPackerFactory
    {
        public IObscuroPacker GetPacker(IObscuroContext context)
        {
            if (context.Settings.Packing == AppPackingType.Splitted)
                return new SeparateAppPacker(null);

            //TODO: Add logic of creating different packers here
            return new CombinedAppPacker(null);
        }
    }
}
