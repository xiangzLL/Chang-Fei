using Orleans;

namespace ChangFei.Interfaces
{
    /// <summary>
    /// Interface of observers of an <see cref="IMessageViewer"/> instance.
    /// </summary>
    public interface IMessageViewer:IGrainObserver
    {
    }
}
