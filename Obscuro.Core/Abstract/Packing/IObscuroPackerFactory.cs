namespace Obscuro.Abstract.Packing
{
    public interface IObscuroPackerFactory
    {
        IObscuroPacker GetPacker(IObscuroContext context);
    }
}
