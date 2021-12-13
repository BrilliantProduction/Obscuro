namespace Obscuro.Abstract.Unpacking
{
    public interface IObscuroUnpackerFactory
    {
        IObscuroUnpacker GetUnpacker(IObscuroContext context);
    }
}
