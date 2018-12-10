using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using NLog;

namespace NLog.AspNetCore.TargetGelf
{
    public interface IConverter
    {
        JObject GetGelfJson(LogEventInfo logEventInfo, string facility);
    }
}
