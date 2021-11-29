using System;

namespace Titanium.Web.Proxy.EventArguments;

public class TrafficDataEventArgs : EventArgs
{
    public TrafficDataEventArgs(int count)
    {
        Count = count;
    }

    public int Count { get; }
}