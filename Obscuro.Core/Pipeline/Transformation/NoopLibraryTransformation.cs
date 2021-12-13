using System.Collections.Generic;

using Obscuro.Abstract.Transformation;

namespace Obscuro.Pipeline.Transformation
{
    /// <summary>
    /// A default transformation - does nothing
    /// </summary>
    /// <seealso cref="IObscuroTransformation" />
    public class NoopLibraryTransformation : IObscuroTransformation
    {
        public IReadOnlyList<byte> GetRawBytes(IReadOnlyList<byte> transformed) => transformed;

        public IReadOnlyList<byte> GetTransfromedBytes(IReadOnlyList<byte> original) => original;
    }
}
