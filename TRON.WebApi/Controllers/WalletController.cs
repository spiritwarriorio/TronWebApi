using Google.Protobuf;
using System.Threading.Tasks;
using System.Web.Http;
using Tron.Net.Protocol;
using TRON.WebApi.Configuration;
using TRON.WebApi.Grpc;
using TRON.WebApi.Grpc.Configuration;

namespace TRON.WebApi.Controllers
{
    public class WalletController : ApiController
    {
        [Route("api/v1/Wallet/GetAccountAsync")]
        [HttpGet]
        public async Task<Account> GetAccountAsync(string address)
        {
            ByteString byteAddress = ByteString.CopyFromUtf8(address);

            if (address != null)
            {
                Account account = new Account();
                account.Address = byteAddress;
                var configuration = new AppSettingsChannelConfiguration();
                var grpcChanngelFactory = new GrpcGrpcChannelFactory(configuration);
                var walletClientFactory = new WalletClientFactory(grpcChanngelFactory);
                var wallet = new Services.Service.Wallet(walletClientFactory, new AllClientsDefaultCallConfiguration());

                return await wallet.GetAccountAsync(account);
            }
            else
            {
                return null;
            }
        }

        [Route("api/v1/Wallet/ListNodesAsync")]
        [HttpGet]        
        public async Task<NodeList> ListNodesAsync()
        {            
                var configuration = new AppSettingsChannelConfiguration();
                var grpcChanngelFactory = new GrpcGrpcChannelFactory(configuration);
                var walletClientFactory = new WalletClientFactory(grpcChanngelFactory);
                var wallet = new Services.Service.Wallet(walletClientFactory, new AllClientsDefaultCallConfiguration());

                return await wallet.ListNodesAsync();            
        }

        [Route("api/v1/Wallet/CreateAccountAsync")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<Transaction> CreateAccountAsync()
        {
            string address = "1";
            string privateKey = "2";

            if (address != null && privateKey != null )
            {

                AccountCreateContract account = new AccountCreateContract
                {
                    AccountAddress = ByteString.CopyFromUtf8("fcdd3d8bf1e8d29ae154b3f72c8102cccb7382650f4a41f817e8693f8ac4fd4a"),
                    OwnerAddress = ByteString.CopyFromUtf8("TEu23mtvEJ4MsC74BNavFXy5AexyZ3YRzS"),
                    Type = AccountType.Normal
                };
                var configuration = new AppSettingsChannelConfiguration();
                var grpcChanngelFactory = new GrpcGrpcChannelFactory(configuration);
                var walletClientFactory = new WalletClientFactory(grpcChanngelFactory);
                var wallet = new Services.Service.Wallet(walletClientFactory, new AllClientsDefaultCallConfiguration());

                return await wallet.CreateAccountAsync(account);
            }
            else
            {
                return null;
            }
        }
    }
}
