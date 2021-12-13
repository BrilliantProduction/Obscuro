namespace Obscuro.Abstract.Reading
{
    public interface IObscuroReaderFactory
    {
        IObscuroAssemblyReader Create(IObscuroContext context);
    }
}
