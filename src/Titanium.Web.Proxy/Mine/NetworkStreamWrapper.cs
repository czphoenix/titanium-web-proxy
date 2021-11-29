using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Titanium.Web.Proxy.EventArguments;

namespace Titanium.Web.Proxy;

internal class NetworkStreamWrapper : NetworkStream
{
    public NetworkStreamWrapper(Socket socket, bool ownsSocket) : base(socket, ownsSocket)
    {
    }

    public event EventHandler<TrafficDataEventArgs>? DataRead;

    public event EventHandler<TrafficDataEventArgs>? DataWrite;

    private void OnRead(int count)
    {
        DataRead?.Invoke(this, new TrafficDataEventArgs(count));
    }

    private void OnWrite(int count)
    {
        DataWrite?.Invoke(this, new TrafficDataEventArgs(count));
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        var n = base.Read(buffer, offset, count);
        OnRead(n);
        return n;
    }

    public override int Read(Span<byte> buffer)
    {
        var n = base.Read(buffer);
        OnRead(n);
        return n;
    }

    public override int ReadByte()
    {
        var b = base.ReadByte();
        if (b >= 0)
            OnRead(1);
        return b;
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
        base.Write(buffer, offset, count);
        OnWrite(count);
    }

    public override void Write(ReadOnlySpan<byte> buffer)
    {
        base.Write(buffer);
        OnWrite(buffer.Length);
    }

    public override void WriteByte(byte value)
    {
        base.WriteByte(value);
        OnWrite(1);
    }

    public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
    {
        var n = await base.ReadAsync(buffer, offset, count, cancellationToken);
        OnRead(n);
        return n;
    }

    public override async ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default)
    {
        var n = await base.ReadAsync(buffer, cancellationToken);
        OnRead(n);
        return n;
    }

    public override async Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
    {
        await base.WriteAsync(buffer, offset, count, cancellationToken);
        OnWrite(count);
    }

    public override async ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default)
    {
        await base.WriteAsync(buffer, cancellationToken);
        OnWrite(buffer.Length);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            DataRead = null;
            DataWrite = null;
        }
        base.Dispose(disposing);
    }
}