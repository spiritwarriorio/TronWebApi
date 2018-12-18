using Grpc.Core;

namespace TRON.WebApi.Grpc
{
    public interface IGrpcChannelFactory
    {
        Channel Create();

    }
}
