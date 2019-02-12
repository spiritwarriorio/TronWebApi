using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Tron.Net.Common;
using Tron.Net.Protocol;
using TRON.WebApi.Common;
using TRON.WebApi.Configuration;
using TRON.WebApi.Grpc;
using TRON.WebApi.Grpc.Configuration;
using TRON.WebApi.Models;

namespace TRON.WebApi.Services.Service
{
    public class TransaccionRepository
    {
        public bool transferAssetAsync(string privateKey)
        {
            bool resultado = false;
            resultado= this.transferSign(privateKey);

            if(resultado)
            {
                return resultado;
            }
            else
            {
                return false;
            }
            
        }


        public bool transferSign(string privateKey)
        {
            string address = Extension.privKey2PubKey(privateKey);

            byte[] addressOwnerHex = Base58CheckEncoding.Decode(address);
            address = BitConverter.ToString(addressOwnerHex);
            address = address.Replace("-", "");

            string addressTo = Constantes.addressSW;

            byte[] addressToHex = Base58CheckEncoding.Decode(addressTo);
            addressTo = BitConverter.ToString(addressToHex);
            addressTo = addressTo.Replace("-", "");

            string token = Constantes.tokenSW;

            byte[] plainTextBytes = System.Text.Encoding.UTF8.GetBytes(token);
            token = Utils.ToHexString(plainTextBytes);


            if (address != null)
            {
                AssetContract transfer = new AssetContract();
                transfer.owner_address = address; // TNjfeULTYArKhwtVRvgnzxrDdDABGPdqh6  CLAVE PUBLICA DE DIEGO
                transfer.to_address = addressTo; //TKm3HuiZ3EiFnRc1idkUyuMgKG86t3SVXk CLAVE PUBLICA DE SPIRIT WARRIOR
                transfer.asset_name = token; //SpiritWarrior
                transfer.amount = 1;

                string URL = Constantes.defaultNodes.solidityNode + "/wallet/transferasset";
                var retorno = new HelperConsumoRest().PeticionRespuesta<AssetContractResponse, AssetContract>(transfer, URL);
                
                if (retorno != null)
                {
                    TransactionSign transaction = new TransactionSign();
                    transaction.transaction = new transaction();

                    transaction.transaction.txID = retorno.txID;

                    transaction.transaction.raw_data = new raw_data();
                    transaction.transaction.raw_data = retorno.raw_data;

                    transaction.transaction.privateKey = privateKey;

                    string URLSign = Constantes.defaultNodes.solidityNode + "/wallet/gettransactionsign";
                    var singRetorno = new HelperConsumoRest().PeticionRespuesta<TransactionSignResponse, TransactionSign>(transaction, URLSign);
                    if (singRetorno != null)
                    {
                        Signature broadcast = new Signature();
                        broadcast.signature = singRetorno.signature;
                        broadcast.txID = singRetorno.txID;
                        broadcast.raw_data = new raw_data();
                        broadcast.raw_data = singRetorno.raw_data;

                        string URLBroadcast = Constantes.defaultNodes.solidityNode + "/wallet/broadcasttransaction";
                        var broadcastRetorno = new HelperConsumoRest().PeticionRespuesta<SignatureResponse, Signature>(broadcast, URLBroadcast);
                        // broadcastRetorno retorna un TRUE o FALSE
                        if (broadcastRetorno != null)
                        {
                            return broadcastRetorno.result;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
        
}