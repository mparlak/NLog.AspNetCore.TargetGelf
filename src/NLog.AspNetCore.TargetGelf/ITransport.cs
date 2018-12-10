using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace NLog.AspNetCore.TargetGelf
{
    public interface ITransport
    {
        string Scheme { get; }
        void Send(IPEndPoint target, string message);
    }
}
