namespace Fly.Handler.IO
{
    internal enum BufferType
    {
        Single = 0,
        Composit
    }

    public interface IBuffer
    {
        int Size { get; }

        byte[] HashCode { get; }

        byte[] GetBytes();
    }
}
