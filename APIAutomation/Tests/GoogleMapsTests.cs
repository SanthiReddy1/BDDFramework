namespace APIAutomation.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Net;

    using APIAutomation.DataModel;
    using APIAutomation.Endpoints;
    using APIAutomation.Utilities;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RestSharp;
    using SnapDealTestProject.Utils.Assertions;

    [TestClass]
    public class GoogleMapsTests : GoogleMapsServiceClient
    {
        [TestMethod]
        [TestCategory("API")]
        [DeploymentItem(@"Resources/TestData/GoogleMaps/AddPlaceRequestData.xml")]
        [DataSource(
            "Microsoft.VisualStudio.TestTools.DataSource.XML",
            "|DataDirectory|\\AddPlaceRequestData.xml",
            "testData",
            DataAccessMethod.Sequential)]
        public void VerifyPlaceHasBeenProperlyUpdated()
        {
            const string UpdatedAddress = "70 winter walk, USA";
            string name = this.TestContext.DataRow["Name"].ToString();
            string placeAddress = this.TestContext.DataRow["Address"].ToString();
            
            QualityCheck placeAddedSuccessfullyCheck = new QualityCheck("Verify place has been added successfully");
            QualityCheck placeUpdatedSuccessfullyCheck = new QualityCheck("Verify place has been updated successfully");
            QualityCheck placeRetrievedSuccessfullyCheck = new QualityCheck("Verify place has been retrieved successfully");
            QualityCheck addressVerifCheck = new QualityCheck("Verify address of the place has been successfully updated");
            this.QualityTestCase.AddQualityChecks(placeAddedSuccessfullyCheck, placeUpdatedSuccessfullyCheck, placeRetrievedSuccessfullyCheck);
            
            //Add Place
            RestResponse addPlaceResponse = this.AddPlace(this.createPlaceRequestBody(name, placeAddress));
            Console.WriteLine(addPlaceResponse.Content);
            QualityVerify.IsTrue(
                placeAddedSuccessfullyCheck,
                addPlaceResponse.StatusCode == HttpStatusCode.OK,
                "Place has not been added successfully");

            string placeId = ObjectSerializer.getResponseToken(addPlaceResponse, "place_id");
            Console.WriteLine(placeId);

            //Update Place
            RestResponse updatePlaceResponse =
                this.UpdatePlace(new UpdatePlaceBodyDataModel { place_id = placeId, address = UpdatedAddress, key = "qaclick123" });
            QualityVerify.IsTrue(
                placeUpdatedSuccessfullyCheck,
                updatePlaceResponse.StatusCode == HttpStatusCode.OK,
                "Place has not been updated successfully");

            //GetPlace
            RestResponse getPlaceResponse = this.GetPlace(placeId);
            QualityVerify.IsTrue(
                placeRetrievedSuccessfullyCheck,
                getPlaceResponse.StatusCode == HttpStatusCode.OK,
                "Place has not been retrieved successfully");

            string address = ObjectSerializer.getResponseToken(getPlaceResponse, "address");

            QualityVerify.IsTrue(
                addressVerifCheck,
                UpdatedAddress.Equals(address),
                "Address of the place has not been updated successfully");
        }

        private CreatePlaceBodyDataModel createPlaceRequestBody(string name, string address)
        {
            LocationDataModel locationDataModel = new LocationDataModel(-38.383494, 33.427362);
            CreatePlaceBodyDataModel createPlaceBodyDataModel = new CreatePlaceBodyDataModel
                                                                    {
                                                                        location = locationDataModel,
                                                                        name = name,
                                                                        accuracy = 50,
                                                                        phone_number = "(+91) 983 893 3937",
                                                                        address = address,
                                                                        types = new List<string> { "shoe park", "shop" },
                                                                        website = "http://google.com",
                                                                        language = "French-IN"
                                                                    };
            return createPlaceBodyDataModel;
        }
    }
}
