using System;
using System.Collections.Generic;
using System.Text;

namespace NLog.AspNetCore.TargetGelf
{
    public class NLogGelfProviderOptions
    {
        /// <summary>Default options</summary>
        internal static readonly NLogGelfProviderOptions Default = new NLogGelfProviderOptions();

        /// <summary>
        /// Separator between for EventId.Id and EventId.Name. Default to .
        /// </summary>
        public string EventIdSeparator { get; set; }

        /// <summary>
        /// Skip allocation of <see cref="P:NLog.LogEventInfo.Properties" />-dictionary
        /// </summary>
        /// <remarks>
        /// using
        ///     <c>default(EventId)</c></remarks>
        public bool IgnoreEmptyEventId { get; set; }

        /// <summary>
        /// Enable structured logging by capturing message template parameters and inject into the <see cref="P:NLog.LogEventInfo.Properties" />-dictionary
        /// </summary>
        public bool CaptureMessageTemplates { get; set; }

        /// <summary>
        /// Enable capture of properties from the ILogger-State-object, both in <see cref="M:Microsoft.Extensions.Logging.ILogger.Log``1(Microsoft.Extensions.Logging.LogLevel,Microsoft.Extensions.Logging.EventId,``0,System.Exception,System.Func{``0,System.Exception,System.String})" /> and <see cref="M:Microsoft.Extensions.Logging.ILogger.BeginScope``1(``0)" />
        /// </summary>
        public bool CaptureMessageProperties { get; set; }

        /// <summary>
        /// Use the NLog engine for parsing the message template (again) and format using the NLog formatter
        /// </summary>
        public bool ParseMessageTemplates { get; set; }

        /// <summary>
        /// Enable capture of scope information and inject into <see cref="T:NLog.NestedDiagnosticsLogicalContext" /> and <see cref="T:NLog.MappedDiagnosticsLogicalContext" />
        /// </summary>
        public bool IncludeScopes { get; set; }

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public NLogGelfProviderOptions()
        {
            this.EventIdSeparator = "_";
            this.IgnoreEmptyEventId = true;
            this.CaptureMessageTemplates = true;
            this.CaptureMessageProperties = true;
            this.ParseMessageTemplates = false;
            this.IncludeScopes = true;
        }
    }
}
