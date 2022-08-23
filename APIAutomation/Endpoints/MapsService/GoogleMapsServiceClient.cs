using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAutomation.Endpoints
{
    using System.Collections.Specialized;

    using APIAutomation.DataModel;
    using APIAutomation.Endpoints.MapsService;
    using APIAutomation.Interfaces;
    using APIAutomation.Utilities;

    using Framework.Common.Api.Utilities;

    using RestSharp;

    public class GoogleMapsServiceClient : Base
    {
        public RestResponse AddPlace(CreatePlaceBodyDataModel data)
        {
            string url = "maps/api/place/add/json";

            RestRequest request = this.RequestBuilder.BuildRequest(
                new RequestArguments
                    {
                        Method = Method.Post, 
                        Resource = url, 
                        Data = data, 
                        DataFormat = DataFormat.Json,
                });

            request.AddQueryParameter("key", "qaclick123");

            this.LastResponse = this.RestClient.Execute(request);

            return LastResponse;
        }

        public RestResponse UpdatePlace(UpdatePlaceBodyDataModel data)
        {
            string url = "maps/api/place/update/json";

            RestRequest request = this.RequestBuilder.BuildRequest(
                new RequestArguments
                    {
                        Method = Method.Put,
                        Resource = url,
                        Data = data,
                        DataFormat = DataFormat.Json,
                    });

            request.AddQueryParameter("key", "qaclick123");

            this.LastResponse = this.RestClient.Execute(request);

            return LastResponse;
        }

        public RestResponse GetPlace(string placeId)
        {
            string url = "maps/api/place/get/json";

            RestRequest request = this.RequestBuilder.BuildRequest(
                new RequestArguments
                    {
                        Method = Method.Get,
                        Resource = url,
                        DataFormat = DataFormat.Json,
                    });

            request.AddQueryParameter("place_id", placeId);
            request.AddQueryParameter("key", "qaclick123");

            this.LastResponse = this.RestClient.Execute(request);

            return LastResponse;
        }
    }
}
