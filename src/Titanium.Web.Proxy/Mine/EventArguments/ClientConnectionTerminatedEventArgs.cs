using System;
using System.Net;
using Titanium.Web.Proxy.Network.Tcp;

namespace Titanium.Web.Proxy.EventArguments;

public class ClientConnectionTerminatedEventArgs : ProxyEventArgsBase
{
    public ClientConnectionTerminatedEventArgs(ProxyServer server, TcpClientConnection clientConnection) : base(server, clientConnection)
    {
    }

    public Guid ClientConnectionId => ClientConnection.Id;

    /// <summary>
    ///     Client Local End Point.
    /// </summary>
    public IPEndPoint ClientLocalEndPoint => (IPEndPoint)ClientConnection.LocalEndPoint;

    /// <summary>
    ///     Client Remote End Point.
    /// </summary>
    public IPEndPoint ClientRemoteEndPoint => (IPEndPoint)ClientConnection.RemoteEndPoint;
}