using System;
using System.IO;

namespace Obscuro.Abstract.Unpacking.Inputs
{
    public interface IObscuroInput : IDisposable
    {
        Stream GetInputStream(string name);
    }
}
