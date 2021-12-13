using System;

namespace Obscuro.Abstract.Running
{
    public interface IObscuroAppRunner : IDisposable
    {
        int RunApp();
    }
}
