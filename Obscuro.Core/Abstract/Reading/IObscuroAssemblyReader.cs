using System.IO;

using Obscuro.Models.Data;

namespace Obscuro.Abstract.Reading
{
    public interface IObscuroAssemblyReader
    {
        ObscuroAssembly Read(Stream stream);
    }
}
