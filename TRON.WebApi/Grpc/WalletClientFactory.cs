
namespace TRON.WebApi.Grpc
{
    public class WalletClientFactory : IWalletClientFactory
    {
        private readonly IGrpcChannelFactory _grpcChannelFactory;

        public WalletClientFactory(IGrpcChannelFactory grpcChannelFactory)
        {
            _grpcChannelFactory = grpcChannelFactory;
        }

        public Tron.Net.Protocol.Wallet.WalletClient Create()
        {
            return new Tron.Net.Protocol.Wallet.WalletClient(_grpcChannelFactory.Create());
        }

    }
}