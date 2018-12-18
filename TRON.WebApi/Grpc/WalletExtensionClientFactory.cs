namespace TRON.WebApi.Grpc
{
    public class WalletExtensionClientFactory : IWalletExtensionClientFactory
    {
        private readonly IGrpcChannelFactory _grpcChannelFactory;

        public WalletExtensionClientFactory(IGrpcChannelFactory grpcChannelFactory)
        {
            _grpcChannelFactory = grpcChannelFactory;
        }

        public Tron.Net.Protocol.WalletExtension.WalletExtensionClient Create()
        {
            return new Tron.Net.Protocol.WalletExtension.WalletExtensionClient(_grpcChannelFactory.Create());
        }


    }
}