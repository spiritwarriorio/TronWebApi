namespace TRON.WebApi.Common
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using Newtonsoft.Json;
     public class HelperConsumoRest
    {
        private HttpClient ContextHttpClient { set; get; }

        private HttpClient GetContextHttpClient(Uri uri)
        {
            if ((ContextHttpClient != null && ContextHttpClient.BaseAddress != null) &&
                    ContextHttpClient.BaseAddress.AbsoluteUri != uri.AbsoluteUri)
            { ContextHttpClient.Dispose(); ContextHttpClient = null; }

            if (!ContextHttpClient.IsNotNull())
            {
                ContextHttpClient = new HttpClient(new HttpClientHandler { UseDefaultCredentials = true });
                ContextHttpClient.DefaultRequestHeaders.Accept.Clear();
                ContextHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                ContextHttpClient.BaseAddress = uri;
            }

            return ContextHttpClient;
        }

        #region Encapsulamientos.

        public T PeticionRespuestaObj<T, TO>(TO peticion, string urlApi)
            where T : new()
        {
            return PeticionRespuestaObj<T, TO>(peticion, urlApi, false);
        }

        public T PeticionRespuesta<T, TO>(TO peticion, string urlApi)
        {
            return PeticionRespuesta<T, TO>(peticion, urlApi, false);
        }
        public T PeticionGetRespuestaObj<T>(string urlApi, string parametros)                   
        {
            return PeticionGetRespuestaObj<T>(urlApi, parametros, false);
        }      
        /// <summary>
        /// Encapsula el Consumo de un servicio tipo rest por post con HttpClient, enviandole parametros.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TO"></typeparam>
        /// <param name="peticion">Parametros de consulta.</param>
        /// <param name="urlApi">Ruta completa del servicio a consumir.</param>
        /// <param name="desrException">Bandera que indica el desencadenamiento de la Exception.</param>
        /// <returns>Retorna un Objeto Tipo (T)</returns>
        public T PeticionRespuestaObj<T, TO>(TO peticion, string urlApi, bool desrException)
          where T : new()
        {
            var objPeticionRespuesta = new T();

            try
            {
                var respuestaRest = ConsumirRestPost(peticion, urlApi, null);
                objPeticionRespuesta = RespuestaPeticionObj(objPeticionRespuesta, respuestaRest);
            }
            catch (Exception) { if (desrException) { throw; } }

            return objPeticionRespuesta;
        }

        /// <summary>
        /// Encapsula el Consumo de un servicio tipo rest por post con HttpClient, enviandole parametros.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TO"></typeparam>
        /// <param name="peticion">Parametros de consulta.</param>
        /// <param name="urlApi">Ruta completa del servicio a consumir.</param>
        /// <param name="desrException">Bandera que indica el desencadenamiento de la Exception.</param>
        /// <returns>Retorna un Objeto Tipo (T)</returns>
        public T PeticionRespuesta<T, TO>(TO peticion, string urlApi, bool desrException)
        {
            var objPeticionRespuesta = default(T);

            try
            {
                var respuestaRest = ConsumirRestPost(peticion, urlApi, null);
                objPeticionRespuesta = RespuestaPeticion(objPeticionRespuesta, respuestaRest);
            }
            catch (Exception) { if (desrException) { throw; } }

            return objPeticionRespuesta;
        }

        /// <summary>
        /// Encapsula el Consumo de un servicio tipo rest por post con HttpClient, enviandole parametros.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TO"></typeparam>
        /// <param name="peticion">Parametros de consulta.</param>
        /// <param name="urlApi">Ruta completa del servicio a consumir.</param>
        /// <param name="header">Adicionar atributo en el header para enviar al servicio.</param>
        /// <returns>Retorna un Objeto Tipo (T)</returns>
        public T PeticionRespuestaObj<T, TO>(TO peticion, string urlApi, Tuple<string, string> header)
            where T : new()
        {
            return PeticionRespuestaObj<T, TO>(peticion, urlApi, header, false);
        }

        /// <summary>
        /// Encapsula el Consumo de un servicio tipo rest por post con HttpClient, enviandole parametros.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TO"></typeparam>
        /// <param name="peticion">Parametros de consulta.</param>
        /// <param name="urlApi">Ruta completa del servicio a consumir.</param>
        /// <param name="header">Adicionar atributo en el header para enviar al servicio.</param>
        /// <param name="desrException">Bandera que indica el desencadenamiento de la Exception.</param>
        /// <returns>Retorna un Objeto Tipo (T)</returns>
        public T PeticionRespuestaObj<T, TO>(TO peticion, string urlApi, Tuple<string, string> header, bool desrException)
            where T : new()
        {
            var objPeticionRespuesta = new T();

            try
            {
                var respuestaRest = ConsumirRestPost(peticion, urlApi, header);
                objPeticionRespuesta = RespuestaPeticionObj(objPeticionRespuesta, respuestaRest);
            }
            catch (Exception) { if (desrException) { throw; } }

            return objPeticionRespuesta;
        }

        /// <summary>
        /// Encapsula el Consumo de un servicio tipo rest por Get con HttpClient.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="urlApi">Ruta del servicio a consumir.</param>
        /// <param name="urlParametros">Parametros de consulta.</param>
        /// <param name="desrException">Bandera que indica el desencadenamiento de la Exception.</param>
        /// <returns>Retorna un Objeto Tipo (T)</returns>
        public T PeticionRespuestaObj<T>(string urlApi, string urlParametros, bool desrException = true)
            where T : new()
        {
            var objPeticionRespuesta = new T();

            try
            {
                var respuestaRest = ConsumirRestGet(urlApi, urlParametros, null);
                objPeticionRespuesta = RespuestaPeticionObj(objPeticionRespuesta, respuestaRest);
            }
            catch (Exception) { if (desrException) { throw; } }

            return objPeticionRespuesta;
        }
        /// <summary>
        /// Encapsula el Consumo de un servicio tipo rest por Get con HttpClient.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="urlApi">Ruta del servicio a consumir.</param>
        /// <param name="urlParametros">Parametros de consulta.</param>
        /// <param name="desrException">Bandera que indica el desencadenamiento de la Exception.</param>
        /// <returns>Retorna un Objeto Tipo (T)</returns>
        public T PeticionGetRespuestaObj<T>(string urlApi, string urlParametros, bool desrException = true)           
        {
            var objPeticionRespuesta = default(T);

            try
            {
                var respuestaRest = ConsumirRestGet(urlApi, urlParametros, null);
                objPeticionRespuesta = RespuestaPeticion(objPeticionRespuesta, respuestaRest);
            }
            catch (Exception) { if (desrException) { throw; } }

            return objPeticionRespuesta;
        }
        #region Genericos

        /// <summary>
        /// Procesar Respuesta de los Encapsulamientos de los servicios tipo Rests
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objPeticionRespuesta"></param>
        /// <param name="respuestaApi">Cadena con el resultado del llamado al servicio.</param>
        /// <param name="urlPeticion">Ruta del servicio consumido.</param>
        /// <returns>Retorna un Objeto Tipo (T)</returns>
        private static T RespuestaPeticionObj<T>(T objPeticionRespuesta, string respuestaApi)
            where T : new()
        {
            return RespuestaPeticiones(objPeticionRespuesta, respuestaApi, null);
        }

        /// <summary>
        /// Procesar Respuesta de los Encapsulamientos de los servicios tipo Rests
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objPeticionRespuesta"></param>
        /// <param name="respuestaApi">Cadena con el resultado del llamado al servicio.</param>
        /// <param name="urlPeticion">Ruta del servicio consumido.</param>
        /// <returns>Retorna un Objeto Tipo (T)</returns>
        private static T RespuestaPeticion<T>(T objPeticionRespuesta, string respuestaApi)
        {
            return RespuestaPeticiones(objPeticionRespuesta, respuestaApi, null);
        }

        /// <summary>
        /// Procesar Respuesta de los Encapsulamientos de los servicios tipo Rests
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objPeticionRespuesta"></param>
        /// <param name="respuestaApi">Cadena con el resultado del llamado al servicio.</param>
        /// <param name="urlPeticion">Ruta del servicio consumido.</param>
        /// <param name="callBackCompleteError">Método delegado para setear valores en caso de que la respuesta sea incorrecta</param>
        /// <returns></returns>
        private static T RespuestaPeticiones<T>(T objPeticionRespuesta, string respuestaApi, Action callBackCompleteError)
        {
            if (!string.IsNullOrEmpty(respuestaApi) && !respuestaApi.StartsWith("Falló Respuesta:"))
            {
                objPeticionRespuesta = JsonConvert.DeserializeObject<T>(respuestaApi);
            }
            else
            {
                if (respuestaApi != null && respuestaApi.StartsWith("Falló Respuesta:"))
                {
                    callBackCompleteError?.Invoke();
                }
            }

            return objPeticionRespuesta;
        }

        #endregion

        #endregion

        /// <summary>
        /// Consume un servicio tipo rest por post con HttpClient, enviandole parametros.
        /// </summary>
        /// <param name="peticion">Parametros de consulta.</param>
        /// <param name="urlApi">Ruta completa del servicio a consumir.</param>
        /// <param name="defaultHeader">Parametros para setear en el header.</param>
        /// <returns>Cadena con el resultado del llamado al servicio.</returns>
        private string ConsumirRestPost(object peticion, string urlApi, Tuple<string, string> defaultHeader = null)
        {
            return ConsumirPeticionRest(urlApi, defaultHeader, (contextHttp) =>
            {
                string datosStringPeticion;
                if (typeof(string) != peticion.GetType())
                {
                    datosStringPeticion = JsonConvert.SerializeObject(peticion, Formatting.Indented);
                }
                else { datosStringPeticion = (string)peticion; }


                // HTTP POST
                string respuestaApi;
                HttpContent httpContent = new StringContent(datosStringPeticion, Encoding.UTF8, "application/json");
                using (var response = contextHttp.PostAsync(urlApi, httpContent).Result)
                {
                    respuestaApi = ResponseMessage(response);
                }

                return respuestaApi;
            });
        }

        /// <summary>
        /// Consume un servicio tipo rest por Get con HttpClient.
        /// </summary>
        /// <param name="urlApi">Ruta del servicio a consumir.</param>
        /// <param name="urlParametros">Parametros de consulta.</param>
        /// <param name="defaultHeader">Parametros para setear en el header.</param>
        /// <returns>Cadena con el resultado del llamado al servicio.</returns>
        private string ConsumirRestGet(string urlApi, string urlParametros, Tuple<string, string> defaultHeader = null)
        {
            return ConsumirPeticionRest(urlApi, defaultHeader, (contextHttp) =>
            {
                // HTTP GET
                string respuestaApi;

                using (var response = contextHttp.GetAsync(urlParametros).Result)
                {
                    respuestaApi = ResponseMessage(response);
                }

                return respuestaApi;
            });
        }

        #region Genericos

        /// <summary>
        /// Método para encapsular el proceso de consumo de peticiones Http (RestFull)
        /// </summary>
        /// <param name="urlApi">Ruta del servicio a consumir.</param>
        /// <param name="defaultHeader">Parametros para setear en el header.</param>
        /// <param name="ejecutarPeticionRest">Método delegado a ejecutar dentro de la ejecución</param>
        /// <returns></returns>
        private string ConsumirPeticionRest(string urlApi, Tuple<string, string> defaultHeader, Func<HttpClient, string> ejecutarPeticionRest)
        {
            try
            {
                var contextHttp = GetContextHttpClient(new Uri(urlApi));
                if (defaultHeader != null && (!string.IsNullOrEmpty(defaultHeader.Item1) && !string.IsNullOrEmpty(defaultHeader.Item2)))
                {
                    contextHttp.DefaultRequestHeaders.Clear();
                    contextHttp.DefaultRequestHeaders.Add(defaultHeader.Item1, defaultHeader.Item2);
                }

                // HTTP
                var respuestaApi = ejecutarPeticionRest(contextHttp);
                return respuestaApi;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Método para encapsular el proceso de captura y procesado de respuesta Http (RestFull)
        /// </summary>
        /// <param name="response">Respuesta de la Petición ejecutada</param>
        /// <returns>Respuesta de la petición</returns>
        private static string ResponseMessage(HttpResponseMessage response)
        {
            string respuestaRest;

            if (response.IsSuccessStatusCode)
            {
                var dataObjects = response.Content.ReadAsStringAsync();
                respuestaRest = dataObjects.Result;
            }
            else
            {
                respuestaRest = $"Falló Respuesta: {response.StatusCode} ({response.ReasonPhrase})";
            }

            return respuestaRest;
        }

        #endregion
    }
}