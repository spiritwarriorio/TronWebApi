
namespace TRON.WebApi.Grpc
{
    public class WalletSolidityClientFactory : IWalletSolidityClientFactory
    {
        private readonly IGrpcChannelFactory _grpcChannelFactory;

        public WalletSolidityClientFactory(IGrpcChannelFactory grpcChannelFactory)
        {
            _grpcChannelFactory = grpcChannelFactory;
        }

        public Tron.Net.Protocol.WalletSolidity.WalletSolidityClient Create()
        {
            return new Tron.Net.Protocol.WalletSolidity.WalletSolidityClient(_grpcChannelFactory.Create());
        }

    }
}