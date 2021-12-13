using System.IO;

using Obscuro.Abstract;
using Obscuro.Abstract.Unpacking.Inputs;

namespace Obscuro.Pipeline.Unpacking.Inputs
{
    class SingleStreamInput : IObscuroInput
    {
        private string _fileName;
        private IObscuroContext _context;
        private readonly Stream _stream;

        public SingleStreamInput(IObscuroContext context)
        {
            _context = context;
            _fileName = context.FileName;
            _stream = new FileStream(_fileName, FileMode.Open, FileAccess.Read);
        }

        public Stream GetInputStream(string name) => _stream;

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    _stream?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~SingleStreamInput() {
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
