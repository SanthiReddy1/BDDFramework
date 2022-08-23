using Newtonsoft.Json;

namespace APIAutomation.Utilities
{
    using Newtonsoft.Json.Linq;

    using RestSharp;

    public static class ObjectSerializer
    {
        public static string SerializeJsonObject<TObject>(TObject objectToSerialize)
            where TObject : class
            => JsonConvert.SerializeObject(objectToSerialize);

        public static string getResponseToken(RestResponse response, string path)
        {
            JObject jObject = JObject.Parse(response.Content);
            return (string)jObject.SelectToken(path);
        }
    }
}
