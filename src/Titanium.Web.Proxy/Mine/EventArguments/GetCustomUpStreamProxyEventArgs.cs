using Titanium.Web.Proxy.Models;
using Titanium.Web.Proxy.Network.Tcp;

namespace Titanium.Web.Proxy.EventArguments;

public class GetCustomUpStreamProxyEventArgs : ProxyEventArgsBase
{
    public GetCustomUpStreamProxyEventArgs(ProxyServer server, TcpClientConnection clientConnection, ProxyEndPoint proxyEndPoint, string host, int port) : base(server, clientConnection)
    {
        Host = host;
        Port = port;
        ProxyEndPoint = proxyEndPoint;
    }

    public ProxyEndPoint ProxyEndPoint { get; set; }

    public string Host { get; }

    public int Port { get; }
}