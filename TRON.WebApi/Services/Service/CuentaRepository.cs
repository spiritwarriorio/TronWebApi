using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Tron.Net.Crypto;
using TRON.WebApi.Common;
using TRON.WebApi.Models;

namespace TRON.WebApi.Services.Service
{
    public class CuentaRepository
    {
        public Cuenta getaccount(string address)
        {     
            byte[] addressHex = Base58CheckEncoding.Decode(address);
            address = BitConverter.ToString(addressHex);
            address = address.Replace("-", "");

            string URL = Constantes.defaultNodes.solidityNode + "/walletsolidity/getaccount";
            var retorno = new HelperConsumoRest().PeticionRespuestaObj<Cuenta>(URL, "?address=" + address);

            return retorno;
        }

        public bool saveaccount(CUE_Cuentas value)
        {
            if (!string.IsNullOrEmpty(value.CUE_NickName) && !string.IsNullOrEmpty(value.CUE_ClavePublica))
            {
                var retorno = new DBSWEntities().CrearCuenta(value.CUE_NickName, value.CUE_ClavePublica, value.CUE_Puntos);
                return retorno > 0;
            }
            else {
                return false;
            }
        }

        public List<ListarClasificacion_Result> listarClasificacion(int numeroMaximoRegistros)
        {
            var retorno = new DBSWEntities().ListarClasificacion(numeroMaximoRegistros).ToList();
            return retorno;
            
        }

        /// <summary>
        /// Metodo para verificar si estist el NickName en la base de datos
        /// </summary>
        /// <param name="nickName">nickName</param>
        /// <returns>bool</returns>
        public bool existeNickName(string nickName)
        {
            var retorno = new DBSWEntities().CUE_Cuentas.FirstOrDefault(x => x.CUE_NickName == nickName);
            if (retorno != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Metodo para consultar todas las cuentas 
        /// </summary>     
        /// <returns>CUE_Cuentas</returns>
        public List<CUE_Cuentas> getAllAccounts()
        {
            return new DBSWEntities().CUE_Cuentas.OrderByDescending(x => x.CUE_Puntos).ToList();            
        }

        /// <summary>
        /// Metodo para consultar la posición de un jugador 
        /// </summary>     
        /// <returns>CUE_Cuentas</returns>
        public CUE_Cuentas getUserPositionPoints(string key)
        {
            List<CUE_Cuentas> listaCuentas = this.getAllAccounts();
            CUE_Cuentas cuenta = null;
            int contador = 0;
            
            foreach (var item in listaCuentas)
            {
                contador++;

                if (item.CUE_ClavePublica == key)
                {
                    cuenta = new CUE_Cuentas {
                        CUE_Id = contador,
                        CUE_NickName = item.CUE_NickName,
                        CUE_ClavePublica = item.CUE_ClavePublica,
                        CUE_FechaRegistro = item.CUE_FechaRegistro,
                        CUE_Puntos = item.CUE_Puntos,
                        CUE_FechaUltimaActualizacion = item.CUE_FechaUltimaActualizacion                        
                    };                   
                }
            }
            return cuenta;
        }

        public Cuenta getaccountprivate(string privateKey)
        {

            ECKey eCkey;
            eCkey = ECKey.FromPrivateHexString(privateKey);
            WalletAddress addressWallet = WalletAddress.MainNetWalletAddress(eCkey);

            string address = addressWallet.ToString();

            byte[] addressHex = Base58CheckEncoding.Decode(address);
            address = BitConverter.ToString(addressHex);
            address = address.Replace("-", "");

            string URL = Constantes.defaultNodes.solidityNode + "/walletsolidity/getaccount";
            var retorno = new HelperConsumoRest().PeticionRespuestaObj<Cuenta>(URL, "?address=" + address);

            return retorno;
        }
    }
}