# NLog.AspNetCore.TargetGelf
NLog.AspNetCore.TargetGelf is an NLog target implementation to push log messages to GrayLog. It implements the Gelf specification and communicates with GrayLog server via UDP.

### History

Code forked from https://github.com/akurdyukov/Gelf4NLog which is a fork from https://github.com/RickyKeane/Gelf4NLog who forked the origonal code from https://github.com/seymen/Gelf4NLog

### Installing NLog.AspNetCore.TargetGelf

You should install [NLog.AspNetCore.TargetGelf with NuGet](https://www.nuget.org/packages/NLog.AspNetCore.TargetGelf):

    Install-Package NLog.AspNetCore.TargetGelf
    
Or via the .NET Core command line interface:

    dotnet add package NLog.AspNetCore.TargetGelf
    
### Configuration

Here is a sample nlog configuration snippet:

```csharp
    <?xml version="1.0" encoding="utf-8" ?>
    <nlog xmlns="http://www.nlog-project.org/schemas/NLog.xsd"
          xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
          autoReload="true"
          throwExceptions="false"
          internalLogLevel="Off"
          internalLogFile="c:\temp\internal-nlog.txt">

      <extensions>
        <add assembly="Nlog.AspNetCore.TargetGelf"/>
      </extensions>

      <targets>
        <target name="graylog"
                xsi:type="Gelf"
                endpoint="udp://127.0.0.1:12201"
                facility="console-runner"
                sendLastFormatParameter="true">
          <!-- Optional parameters -->
          <!--<parameter name="param1" layout="${longdate}"/>
          <parameter name="param2" layout="${callsite}"/>-->
        </target>
      </targets>

      <rules>
        <logger name="*" minlevel="Error" writeTo="graylog" />
      </rules>
    </nlog>
```
