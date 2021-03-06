﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace NLog.AspNetCore.TargetGelf
{
    [Target("Gelf")]
    public class GelfTarget : TargetWithLayout
    {
        private readonly Lazy<IPEndPoint> _lazyIpEndoint;
        private readonly Lazy<ITransport> _lazyITransport;
        private string _facility;
        private Uri _endpoint;

        [Required]
        public string Endpoint
        {
            get => _endpoint.ToString();
            set => _endpoint = value != null ? new Uri(Environment.ExpandEnvironmentVariables(value)) : null;
        }

        [ArrayParameter(typeof(GelfParameterInfo), "parameter")]
        public IList<GelfParameterInfo> Parameters { get; private set; }

        public string Facility
        {
            get => _facility;
            set => _facility = value != null ? Environment.ExpandEnvironmentVariables(value) : null;
        }

        public bool SendLastFormatParameter { get; set; }

        public IConverter Converter { get; private set; }
        public IEnumerable<ITransport> Transports { get; private set; }
        public DnsBase Dns { get; private set; }

        public GelfTarget() : this(new[] { new UdpTransport(new UdpTransportClient()) },
            new GelfConverter(),
            new DnsWrapper())
        {
        }

        public GelfTarget(IEnumerable<ITransport> transports, IConverter converter, DnsBase dns)
        {
            Dns = dns;
            Transports = transports;
            Converter = converter;
            this.Parameters = new List<GelfParameterInfo>();

            _lazyIpEndoint = new Lazy<IPEndPoint>(() =>
            {
                var addresses = Dns.GetHostAddresses(_endpoint.Host);
                var ip = addresses.FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork);

                return new IPEndPoint(ip, _endpoint.Port);
            });

            _lazyITransport = new Lazy<ITransport>(() =>
            {
                return Transports.Single(x => x.Scheme.ToUpper() == _endpoint.Scheme.ToUpper());
            });
        }

        public void WriteLogEventInfo(LogEventInfo logEvent)
        {
            Write(logEvent);
        }

        protected override void Write(LogEventInfo logEvent)
        {
            foreach (var par in this.Parameters)
            {
                if (!logEvent.Properties.ContainsKey(par.Name))
                {
                    string stringValue = par.Layout.Render(logEvent);

                    logEvent.Properties.Add(par.Name, stringValue);
                }
            }

            if (SendLastFormatParameter && logEvent.Parameters != null && logEvent.Parameters.Any())
            {
                logEvent.Properties.Add(ConverterConstants.PromoteObjectPropertiesMarker, logEvent.Parameters.Last());
            }

            var jsonObject = Converter.GetGelfJson(logEvent, Facility);

            if (jsonObject == null) return;
            {
                _lazyITransport.Value.Send(_lazyIpEndoint.Value, jsonObject.ToString(Formatting.None, null));
            }
        }
    }
}
