using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace CarInfo.API
{
    public class KeyCloackAuth
    {
        public string VerifyTokenKeyCloak(string jwt, string realm)
        {   

            Respons resp = Call(jwt, realm);
            if (resp.error != null || resp.message != null)
            {
                return (resp.error_description != null) ? resp.error_description : resp.message;
            }
            else 
            {
                return "200";
            }
            //return null;
        }

        public static Respons Call(string bearerKey, string realm)
        {
            string urlToRequest = "http://localhost:8093/auth/realms/" + realm + "/protocol/openid-connect/userinfo";
            Respons objReponse = null;
            Uri requestUri = new Uri(urlToRequest);
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(requestUri);
            httpWebRequest.Method = "GET";
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Accept = "application/json";

            WebHeaderCollection myWebHeaderCollection = httpWebRequest.Headers;
            myWebHeaderCollection.Add("Authorization", "Bearer " + bearerKey);

            try
            {   
                /*
                using (StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    //streamWriter.Write(parameters);
                    streamWriter.Flush();
                }
                */

                var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                var stream = httpWebResponse.GetResponseStream();

                if ((httpWebResponse.StatusCode == HttpStatusCode.OK))
                {
                    var encoding = ASCIIEncoding.ASCII;
                    using (var reader = new StreamReader(stream))
                    {
                        DataContractJsonSerializer sr = new DataContractJsonSerializer(typeof(Respons));
                        objReponse = (Respons)sr.ReadObject(stream);
                    }
                    return objReponse;
                }
                else
                {
                    string statusError = httpWebResponse.StatusCode.ToString();
                    return null;
                }
            }
            catch (Exception ex)
            {
                return new Respons() { message = ex.Message };
            }
        }
    }

    public class Respons
    {
        public string sub { get; set; }
        public bool email_verified { get; set; }
        public string name { get; set; }
        public string preferred_username { get; set; }
        public string given_name { get; set; }
        public string locale { get; set; }
        public string family_name { get; set; }
        public string email { get; set; }
        public string error { get; set; }
        public string error_description { get; set; }
        public string message { get; set; }
    }
}
