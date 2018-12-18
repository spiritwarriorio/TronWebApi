using Grpc.Core;
using System.Collections.Generic;
using TRON.WebApi.Grpc.Configuration;

namespace TRON.WebApi.Grpc
{
    public sealed class GrpcGrpcChannelFactory : IGrpcChannelFactory
    {
        private readonly IChannelConfiguration _channelConfiguration;

        public GrpcGrpcChannelFactory(IChannelConfiguration channelConfiguration)
        {
            _channelConfiguration = channelConfiguration;
        }

        public Channel Create()
        {
            return _channelConfiguration.MaxConcurrentStreams.HasValue == false ?
                new Channel(_channelConfiguration.Host, _channelConfiguration.Port, ChannelCredentials.Insecure) :
                new Channel(_channelConfiguration.Host, _channelConfiguration.Port, ChannelCredentials.Insecure,
                    new List<ChannelOption>()
                    {
                        new ChannelOption(ChannelOptions.MaxConcurrentStreams, _channelConfiguration.MaxConcurrentStreams.Value)
                    });
        }

    }
}