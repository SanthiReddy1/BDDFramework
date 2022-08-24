namespace APIAutomation.Endpoints.MapsService
{
    using System.Net;

    using APIAutomation.Interfaces;

    using Framework.Common.Api.Utilities;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RestSharp;

    using SnapDealTestProject.Utils.Assertions;

    public class Base
    {
        public string BaseUrl;

        protected CookieContainer Cookies;

        protected RestClientOptions RestClientOptions;

        protected RestClient RestClient;

        protected RestResponse LastResponse { get; set; }

        protected IRequestBuilder RequestBuilder { get; set; }

        public QualityTestCase QualityTestCase { get; set; }

        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void SetUp()
        {
            this.QualityTestCase = new QualityTestCase();
            this.Cookies = Cookies ?? new CookieContainer();
            this.RestClientOptions = new RestClientOptions(this.BaseUrl) { CookieContainer = this.Cookies };
            this.RestClient = new RestClient(this.RestClientOptions);
            this.RequestBuilder = new RequestBuilder();
        }
    }
}
