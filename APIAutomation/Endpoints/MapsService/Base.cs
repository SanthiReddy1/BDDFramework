namespace APIAutomation.Endpoints.MapsService
{
    using APIAutomation.Interfaces;

    using Framework.Common.Api.Utilities;

    using NUnit.Framework;

    using RestSharp;

    using SnapDealTestProject.Utils.Assertions;

    public class Base
    {
        protected string BaseUrl;

        protected RestClient RestClient;

        protected RestResponse LastResponse { get; set; }

        protected IRequestBuilder RequestBuilder { get; set; }

        public QualityTestCase QualityTestCase { get; set; }

        public Base()
        {
            this.BaseUrl = "https://rahulshettyacademy.com/";

            this.RestClient = new RestClient(this.BaseUrl);
            this.RequestBuilder = new RequestBuilder();
        }

        [SetUp]
        public void setUp()
        {
            this.QualityTestCase = new QualityTestCase();
        }
    }
}
