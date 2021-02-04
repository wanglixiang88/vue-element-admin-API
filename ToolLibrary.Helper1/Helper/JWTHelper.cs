using JWT;
using JWT.Algorithms;
using Newtonsoft.Json;
using System.Text;

namespace ToolLibrary.Helper.Helper
{
    public class JWTHelper
    {
        //密钥
        private string key = "vueElementAdminApi_WangLixiang";

        /// <summary>
        /// GWT加密
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public string GetToken(UU u)
        {
            JWT.Algorithms.IJwtAlgorithm Algorithm = new JWT.Algorithms.HMACSHA256Algorithm();
            JWT.IJsonSerializer json = new JS();
            JWT.IBase64UrlEncoder Base64 = new JWT.JwtBase64UrlEncoder();

            JwtEncoder en = new JwtEncoder(Algorithm, json, Base64);
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            return en.Encode(u, keyBytes);
        }

        /// <summary>
        /// GWT解密
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public UU GetUU(string token)
        {
            IJsonSerializer js = new JS();
            IJwtValidator validator = new JwtValidator(js, new JWT.UtcDateTimeProvider());
            JWT.IBase64UrlEncoder Base64 = new JWT.JwtBase64UrlEncoder();
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            JwtDecoder en = new JwtDecoder(js, validator, Base64,algorithm);
            return en.DecodeToObject<UU>(token);
        }
    }

    public class UU
    {
        public string mme { get; set; }
        public string Inat { get; set; }
        public string iae { get; set; }
    }

    class JS : JWT.IJsonSerializer
    {
        public T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
