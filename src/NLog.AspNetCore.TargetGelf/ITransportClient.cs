using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace NLog.AspNetCore.TargetGelf
{
    public interface ITransportClient
    {
        void Send(byte[] datagram, int bytes, IPEndPoint ipEndPoint);
    }
}
