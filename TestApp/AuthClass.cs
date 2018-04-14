using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TestApp
{
    static class AuthClass
    {
        internal static string ServerUrl = "http://localhost:63134";
        private static string _token;
        private static string _userName;
        private static string _password;
        internal static string Token
        {
            get
            {
                if (String.IsNullOrEmpty(_token))
                    Auth(_userName, _password);

                return "Bearer " + _token;
            }
        }

        internal static bool Auth(string email, string password)
        {
            _userName = email;
            _password = password;
            try
            {
                if (String.IsNullOrEmpty(_token))
                {
                    var pairs = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>( "grant_type", "password" ),
                        new KeyValuePair<string, string>( "username", email ),
                        new KeyValuePair<string, string>( "password", password )
                    };
                    var requestMessage = new HttpRequestMessage(HttpMethod.Post, ServerUrl + "/Token") { Content = new FormUrlEncodedContent(pairs) };
                    using (var client = new HttpClient())
                    {
                        var response =
                            client.SendAsync(requestMessage).Result;
                        var result = response.Content.ReadAsStringAsync().Result;
                        Dictionary<string, string> responseDictionary =
                            JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
                        _token = responseDictionary["access_token"];
                    }
                }
            }
            catch (Exception e)
            {
                //processing error
            }

            return !String.IsNullOrEmpty(_token);
        }

        internal static bool Register(string email, string password, string confirmPassword)
        {
            bool registrationResult = false;
            var registerModel = new
            {
                Email = email,
                Password = password,
                ConfirmPassword = confirmPassword
            };

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ServerUrl + "/api/Account/Register");
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            string json = JsonConvert.SerializeObject(registerModel);
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(bytes, 0, bytes.Length);
            }

            try
            {
                var response = request.GetResponse();
                //processing response
                registrationResult = true;
            }
            catch (Exception ex)
            {
                //processing error
                registrationResult = false;
            }

            if (registrationResult)
                Auth(email, password);

            return registrationResult;

        }

        static Dictionary<string, string> GetTokenDictionary(string userName, string password)
        {



            var pairs = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>( "grant_type", "password" ),
                new KeyValuePair<string, string>( "username", userName ),
                new KeyValuePair<string, string> ( "password", password )
            };
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, ServerUrl + "/Token") { Content = new FormUrlEncodedContent(pairs) };
            using (var client = new HttpClient())
            {
                var response =
                    client.SendAsync(requestMessage).Result;
                var result = response.Content.ReadAsStringAsync().Result;
                Dictionary<string, string> responseDictionary =
                    JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
                return responseDictionary;
            }

        }

    }
}
