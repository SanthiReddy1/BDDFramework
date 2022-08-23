namespace Framework.Common.Api.Utilities
{
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.IO;

    using APIAutomation.Interfaces;
    using APIAutomation.Utilities;

    using RestSharp;

    /// <summary>
    /// The request builder.
    /// </summary>
    public class RequestBuilder : IRequestBuilder
    {
        private const string JsonContentType = "application/json";
        private const string XmlContentType = "text/xml";

        public RequestBuilder()
        {
        }

        /// <summary>
        /// Build Request
        /// </summary>
        /// <param name="arguments"> Request Arguments </param>
        /// <returns> IRestRequest </returns>
        public RestRequest BuildRequest(RequestArguments arguments)
        {
            var request = new RestRequest(arguments.Resource, arguments.Method) { RequestFormat = arguments.DataFormat };
            this.AddHeaders(arguments, request);
            this.AddParameters(request, arguments);
            //this.AddJsonBodyFromFile(request, arguments);
            this.AddBody(request, arguments);

            return request;
        }

        /// <summary>
        /// Add Body
        /// </summary>
        /// <param name="request"> IRestRequest </param>
        /// <param name="arguments"> Request Arguments </param>
        private void AddBody(RestRequest request, RequestArguments arguments)
        {
            string serializedObject = string.Empty;
            string contentType = null;

            if (arguments.Data != null)
            {
                switch (arguments.DataFormat)
                {
                    case DataFormat.Json:
                        serializedObject = ObjectSerializer.SerializeJsonObject(arguments.Data);
                        contentType = JsonContentType;
                        break;

                    /*case DataFormat.Xml:
                        serializedObject = request.XmlSerializer.Serialize(arguments.Data);
                        contentType = XmlContentType;
                        break;*/
                }

                request.AddParameter(contentType, serializedObject, ParameterType.RequestBody);
            }
        }

        /// <summary>
        /// Add Headers If Present
        /// </summary>
        /// <param name="arguments"> Arguments </param>
        /// <param name="request"> Rest Request </param>
        private void AddHeaders(RequestArguments arguments, RestRequest request)
        {
            if (arguments.Headers != null)
            {
                foreach (KeyValuePair<string, string> head in arguments.Headers)
                {
                    request.AddHeader(head.Key, head.Value);
                }
            }
        }

        /// <summary>
        /// Add Parameters
        /// </summary>
        /// <param name="request"> The request. </param>
        /// <param name="arguments"> The arguments. </param>
        private void AddParameters(RestRequest request, RequestArguments arguments)
        {
            if (arguments.Parameters != null)
            {
                foreach (Parameter item in arguments.Parameters)
                {
                    request.AddParameter(item.Name, item.Value, item.Type);
                }
            }
        }


        /// <summary>
        /// Add Json Body to a Request
        /// </summary>
        /// <param name="request"> Rest Requst </param>
        /// <param name="arguments"> The arguments. </param>
        private void AddJsonBodyFromFile(RestRequest request, RequestArguments arguments)
        {
            if (arguments.JsonSourcePath != null)
            {
                string json = null;

                using (var reader = new StreamReader(arguments.JsonSourcePath))
                {
                    json = reader.ReadToEnd();
                }

                request.AddParameter(JsonContentType, json, ParameterType.RequestBody);
            }
        }
    }
}
