namespace Obscuro.Abstract.Unpacking.Inputs
{
    public interface IObscuroInputProvider
    {
        IObscuroInput Create(IObscuroContext context);
    }
}
