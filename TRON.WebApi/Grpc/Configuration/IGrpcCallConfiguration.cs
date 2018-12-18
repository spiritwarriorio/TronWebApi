using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TRON.WebApi.Grpc.Configuration
{
    public interface IGrpcCallConfiguration
    {
        TimeSpan? TimeOutMs { get; }

        CallOptions GetCallOptions(CancellationToken token);
    }
}
