namespace Obscuro.Abstract.Writing
{
    public interface IObscuroWriterFactory
    {
        IObscuroAssemblyWriter Create(IObscuroContext context);
    }
}
