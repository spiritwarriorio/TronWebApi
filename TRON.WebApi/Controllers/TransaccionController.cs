
namespace TRON.WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Tron.Net.Protocol;
    using TRON.WebApi.Common;
    using TRON.WebApi.Models;
    using TRON.WebApi.Services.Service;

    public class TransaccionController : ApiController
    {
        private TransaccionRepository _transaccionRepository;
        public TransaccionController()
        {
            _transaccionRepository = new TransaccionRepository();
        }

        [Route("api/v1/Transaccion/TransferAssetAsync/{token}")]
        [HttpPost]
        public bool transferAssetAsync([FromBody]Cuenta cuenta)
        {
            return  _transaccionRepository.transferAssetAsync(cuenta.privateKey);
        }
    }
}
