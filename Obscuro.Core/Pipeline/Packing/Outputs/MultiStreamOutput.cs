using System.Collections.Generic;
using System.IO;

using Obscuro.Abstract;
using Obscuro.Abstract.Packing.Outputs;

namespace Obscuro.Pipeline.Packing.Outputs
{
    class MultiStreamOutput : IObscuroOutput
    {
        private Dictionary<string, Stream> _assemblies;
        private IObscuroContext _context;

        public MultiStreamOutput(IObscuroContext context)
        {
            _context = context;
            _assemblies = new Dictionary<string, Stream>(0);
        }

        public Stream GetOutputStream(string name)
        {
            var stream = new MemoryStream();
            _assemblies.Add(_context.GetResourceName(name), stream);
            return stream;
        }

        public void Save()
        {
            // NAIL: Do nothing for now, generation is broken!!!
            //PackedAssemblyGenerator.GenerateAssembly(_context, _assemblies);
        }

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~MultiStreamOutput() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        #endregion
    }
}
