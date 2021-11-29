using System;
using System.Net;
using Titanium.Web.Proxy.Models;
using Titanium.Web.Proxy.Network.Tcp;

namespace Titanium.Web.Proxy.EventArguments;

public class ClientConnectionCreatedEventArgs : ProxyEventArgsBase
{
    public ClientConnectionCreatedEventArgs(ProxyServer server, TcpClientConnection clientConnection, ProxyEndPoint proxyEndPoint) 
        : base(server, clientConnection)
    {
        ProxyEndPoint = proxyEndPoint;
        ProcessId = new Lazy<int>(ClientConnection.GetProcessId(ProxyEndPoint));
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
        
    public ProxyEndPoint ProxyEndPoint { get; }

    public Lazy<int> ProcessId { get; }
}