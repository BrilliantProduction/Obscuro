using System;
using System.IO;

namespace Obscuro.Abstract.Packing.Outputs
{
    public interface IObscuroOutput : IDisposable
    {
        Stream GetOutputStream(string name);

        void Save();
    }
}
