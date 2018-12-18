
namespace TRON.WebApi.Grpc
{
    public interface IWalletExtensionClientFactory
    {
        Tron.Net.Protocol.WalletExtension.WalletExtensionClient Create();
    }
}
