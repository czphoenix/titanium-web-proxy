using System;
using System.Threading.Tasks;
using Titanium.Web.Proxy.EventArguments;
using Titanium.Web.Proxy.Extensions;
using Titanium.Web.Proxy.Models;

namespace Titanium.Web.Proxy;

public partial class ProxyServer
{
    public event AsyncEventHandler<ClientConnectionCreatedEventArgs>? OnClientConnectionCreated;
    public event AsyncEventHandler<ClientConnectionTerminatedEventArgs>? OnClientConnectionTerminated;
    public event AsyncEventHandler<SessionEventArgsBase>? OnClientServerLinked;
    public event AsyncEventHandler<PipeEventArgs>? BeforePipe;
    public event AsyncEventHandler<PipeEventArgs>? AfterPipe;

    public Func<GetCustomUpStreamProxyEventArgs, Task<IExternalProxy?>>? GetCustomHttpsUpStreamProxyFunc { get; set; }

    /// <summary>
    ///     Invoke client tcp connection events if subscribed by API user.
    /// </summary>
    /// <param name="args">The TcpClient object.</param>
    /// <returns></returns>
    internal async Task InvokeClientConnectionCreatedEvent(ClientConnectionCreatedEventArgs args)
    {
        // client connection created
        if (OnClientConnectionCreated != null)
        {
            await OnClientConnectionCreated.InvokeAsync(this, args, ExceptionFunc);
        }
    }

    internal async Task InvokeClientConnectionTerminatedEvent(ClientConnectionTerminatedEventArgs args)
    {
        // client connection terminated
        if (OnClientConnectionTerminated != null)
        {
            await OnClientConnectionTerminated.InvokeAsync(this, args, ExceptionFunc);
        }
    }

    internal async Task InvokeClientServerLinkedEvent(SessionEventArgsBase args)
    {
        // server connection created
        if (OnClientServerLinked != null)
        {
            await OnClientServerLinked.InvokeAsync(this, args, ExceptionFunc);
        }
    }

    internal async Task onBeforePipe(PipeEventArgs args)
    {
        if (BeforePipe != null)
        {
            await BeforePipe.InvokeAsync(this, args, ExceptionFunc);
        }
    }

    internal async Task onAfterPipe(PipeEventArgs args)
    {
        if (AfterPipe != null)
        {
            await AfterPipe.InvokeAsync(this, args, ExceptionFunc);
        }
    }
}