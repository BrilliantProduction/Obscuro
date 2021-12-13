using System.IO;

using Obscuro.Models.Data;

namespace Obscuro.Abstract.Writing
{
    public interface IObscuroAssemblyWriter
    {
        void Write(ObscuroAssembly assembly, Stream outputStream);
    }
}
