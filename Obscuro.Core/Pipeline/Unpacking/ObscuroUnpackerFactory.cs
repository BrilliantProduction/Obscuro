
using Obscuro.Abstract;
using Obscuro.Abstract.Unpacking;
using Obscuro.Pipeline.Unpacking.Unpackers;

namespace Obscuro.Pipeline.Unpacking
{
    public class ObscuroUnpackerFactory : IObscuroUnpackerFactory
    {
        public IObscuroUnpacker GetUnpacker(IObscuroContext context)
        {
            if (context.Settings.Packing == AppPackingType.Splitted)
                return new SeparateLibraryUnpacker();

            //TODO: Add here an other logic for different unpackers
            return new CombinedAppUnPacker(null);
        }
    }
}
