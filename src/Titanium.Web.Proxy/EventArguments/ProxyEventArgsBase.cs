using System;
using Titanium.Web.Proxy.Network.Tcp;

namespace Titanium.Web.Proxy.EventArguments;

/// <summary>
///     The base event arguments
/// </summary>
/// <seealso cref="System.EventArgs" />
public abstract class ProxyEventArgsBase : EventArgs
{
    protected readonly TcpClientConnection ClientConnection;
    internal readonly ProxyServer Server;

    public object ClientUserData
    {
        get => ClientConnection.ClientUserData;
        set => ClientConnection.ClientUserData = value;
    }

    internal ProxyEventArgsBase(ProxyServer server, TcpClientConnection clientConnection)
    {
        ClientConnection = clientConnection;
        Server = server;

        clientConnection.DataRead += (sender, args) => DataRead?.Invoke(sender, args);
        clientConnection.DataWrite += (sender, args) => DataWrite?.Invoke(sender, args);
    }

    public event EventHandler<TrafficDataEventArgs>? DataRead;
    public event EventHandler<TrafficDataEventArgs>? DataWrite;
}