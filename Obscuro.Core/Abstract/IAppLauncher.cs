namespace Obscuro.Abstract
{
    /// <summary>
    /// Represents an abstraction over app launching
    /// or packing
    /// </summary>
    public interface IAppLauncher
    {
        /// <summary>
        /// Launches the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        void Launch(IObscuroContext context);
    }
}
