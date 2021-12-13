using System.Collections.Generic;

namespace Obscuro.Abstract.Transformation
{
    /// <summary>
    /// Represents an abstraction over library
    /// transformation
    /// </summary>
    public interface IObscuroTransformation
    {
        /// <summary>
        /// Gets the transfromed bytes.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <returns></returns>
        IReadOnlyList<byte> GetTransfromedBytes(IReadOnlyList<byte> original);

        /// <summary>
        /// Gets the raw bytes.
        /// </summary>
        /// <param name="transformed">The transformed.</param>
        /// <returns></returns>
        IReadOnlyList<byte> GetRawBytes(IReadOnlyList<byte> transformed);
    }
}
