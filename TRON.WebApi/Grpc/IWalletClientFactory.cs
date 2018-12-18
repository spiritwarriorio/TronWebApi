
namespace TRON.WebApi.Grpc
{
    public interface IWalletClientFactory
    {
        Tron.Net.Protocol.Wallet.WalletClient Create();
    }
}
