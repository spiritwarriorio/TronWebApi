using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRON.WebApi.Grpc.Configuration
{
    public interface IChannelConfiguration
    {
        int Port { get; }

        string Host { get; }

        int? MaxConcurrentStreams { get; }

        TimeSpan? TimeOutMs { get; }
    }
}
