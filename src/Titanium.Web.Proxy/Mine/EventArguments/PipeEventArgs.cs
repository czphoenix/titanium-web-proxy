using System;
using Titanium.Web.Proxy.Network.Tcp;
using Titanium.Web.Proxy.StreamExtended.Network;

namespace Titanium.Web.Proxy.EventArguments;

public class PipeEventArgs : ProxyEventArgsBase
{
    public PipeEventArgs(ProxyServer server, TcpClientConnection clientConnection, ExceptionHandler exceptionFunc, string host, int port)
        : base(server, clientConnection)
    {
        ExceptionFunc = exceptionFunc;
        Host = host;
        Port = port;
    }

    public Guid ClientConnectionId => ClientConnection.Id;

    public string Host { get; }

    public int Port { get; }

    protected readonly ExceptionHandler ExceptionFunc;

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (disposed)
        {
            return;
        }

        disposed = true;

        if (disposing)
        {
            DataSent = null;
            DataReceived = null;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~PipeEventArgs()
    {
        Dispose(false);
    }

    /// <summary>
    ///     Fired when data is sent within this session to server/client.
    /// </summary>
    public event EventHandler<DataEventArgs>? DataSent;

    /// <summary>
    ///     Fired when data is received within this session from client/server.
    /// </summary>
    public event EventHandler<DataEventArgs>? DataReceived;

    internal void OnDataSent(byte[] buffer, int offset, int count)
    {
        try
        {
            DataSent?.Invoke(this, new DataEventArgs(buffer, offset, count));
        }
        catch (Exception ex)
        {
            ExceptionFunc(new Exception("Exception thrown in user event", ex));
        }
    }

    internal void OnDataReceived(byte[] buffer, int offset, int count)
    {
        try
        {
            DataReceived?.Invoke(this, new DataEventArgs(buffer, offset, count));
        }
        catch (Exception ex)
        {
            ExceptionFunc(new Exception("Exception thrown in user event", ex));
        }
    }
}