namespace APIAutomation.Endpoints
{
    using APIAutomation.DataModel;
    using APIAutomation.Endpoints.MapsService;

    using Framework.Common.Api.Utilities;

    using RestSharp;

    public class GoogleMapsServiceClient : Base
    {
        public GoogleMapsServiceClient()
        {
            this.BaseUrl = "https://rahulshettyacademy.com/";
        }

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
