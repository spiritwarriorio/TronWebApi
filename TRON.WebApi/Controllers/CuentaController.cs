using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TRON.WebApi.Common;
using TRON.WebApi.Models;
using TRON.WebApi.Services.Service;


namespace TRON.WebApi.Controllers
{
    public class CuentaController : ApiController
    {
        private CuentaRepository cuentaRepository;
        public CuentaController()
        {
            this.cuentaRepository = new CuentaRepository();
        }

        [Route("api/v1/Cuenta/GetAccount")]
        [HttpGet]
        public Cuenta GetAccount(string address)
        {
            return cuentaRepository.getaccount(address);
        }

        [Route("api/v1/Cuenta/GetAccountPrivate")]
        [HttpGet]
        public Cuenta GetAccountPrivate(string privateKey)
        {
            return cuentaRepository.getaccountprivate(privateKey);
        }

        [Route("api/v1/Cuenta/SaveAccount/{token}")]
        [HttpPost]
        [AllowAnonymous]
        public bool? SaveAccount(string token, [FromBody]CUE_Cuentas value)
        {
            if (token.IsValid())
            {
                return cuentaRepository.saveaccount(value);
            }

            return null;
        }

        [Route("api/v1/Cuenta/GetClasification")]
        [HttpGet]
        public List<ListarClasificacion_Result> GetClasification(int numeroMaximoRegistros)
        {
            return cuentaRepository.listarClasificacion(numeroMaximoRegistros);
        }

        [Route("api/v1/Cuenta/ExistNickName")]
        [HttpGet]
        public bool ExistNickName(string nickName)
        {
            return cuentaRepository.existeNickName(nickName);
        }
        [Route("api/v1/Cuenta/GetUserPositionPoints")]
        [HttpGet]
        public CUE_Cuentas GetUserPositionPoints(string address)
        {
            return cuentaRepository.getUserPositionPoints(address);
        }
    }
}
