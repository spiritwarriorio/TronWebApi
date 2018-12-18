using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TRON.WebApi.Grpc.Configuration
{
    public class AllClientsDefaultCallConfiguration : GrpcCallConfiguration, IWalletClientCallConfiguration, IWalletExtensionCallConfiguration, IWalletSolidityClientCallConfiguration
    {

    }
}