
namespace TRON.WebApi.Grpc
{
    public interface IWalletSolidityClientFactory
    {
        Tron.Net.Protocol.WalletSolidity.WalletSolidityClient Create();
    }
}
