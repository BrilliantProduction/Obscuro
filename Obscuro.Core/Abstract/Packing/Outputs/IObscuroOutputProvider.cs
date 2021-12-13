namespace Obscuro.Abstract.Packing.Outputs
{
    public interface IObscuroOutputProvider
    {
        IObscuroOutput Create(IObscuroContext context);
    }
}
