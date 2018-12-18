using System.Threading;
using System.Threading.Tasks;
using Tron.Net.Protocol;
using TRON.WebApi.Grpc;
using TRON.WebApi.Grpc.Configuration;
using TRON.WebApi.Services.Interface;

namespace TRON.WebApi.Services.Service
{
    public class WalletExtension : IWalletExtension
    {
        private readonly IWalletExtensionClientFactory _clientFactory;
        private readonly IWalletExtensionCallConfiguration _configuration;

        public WalletExtension(IWalletExtensionClientFactory clientFactory, IWalletExtensionCallConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
        }


        private Tron.Net.Protocol.WalletExtension.WalletExtensionClient GetWalletExtension()
        {
            return _clientFactory.Create();
        }

        public async Task<TransactionList> GetTransactionsFromThisAsync(AccountPaginated accountPagination, CancellationToken token = default(CancellationToken))
        {
            var walletExtension = GetWalletExtension();
            return await walletExtension.GetTransactionsFromThisAsync(accountPagination, _configuration.GetCallOptions(token));
        }

        public async Task<TransactionList> GetTransactionsToThisAsync(AccountPaginated accountPagination, CancellationToken token = default(CancellationToken))
        {
            var walletExtension = GetWalletExtension();
            return await walletExtension.GetTransactionsToThisAsync(accountPagination, _configuration.GetCallOptions(token));
        }
    }
}