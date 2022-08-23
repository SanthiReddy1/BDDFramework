using APIAutomation.Endpoints;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace APIAutomation.Tests
{
    using System.Net;

    using APIAutomation.DataModel;
    using APIAutomation.Utilities;

    using RestSharp;
    using SnapDealTestProject.Utils.Assertions;

    public class GoogleMapsTests : GoogleMapsServiceClient
    {
        [Test]
        [Category("API")]
        public void VerifyPlaceHasBeenProperlyUpdated()
        {
            const string UpdatedAddress = "70 winter walk, USA";

            QualityCheck placeAddedSuccessfullyCheck = new QualityCheck("Verify place has been added successfully");
            QualityCheck placeUpdatedSuccessfullyCheck = new QualityCheck("Verify place has been updated successfully");
            QualityCheck placeRetrievedSuccessfullyCheck = new QualityCheck("Verify place has been retrieved successfully");
            QualityCheck addressVerifCheck = new QualityCheck("Verify address of the place has been successfully updated");
            this.QualityTestCase.AddQualityChecks(placeAddedSuccessfullyCheck, placeUpdatedSuccessfullyCheck, placeRetrievedSuccessfullyCheck);
            
            //Add Place
            RestResponse addPlaceResponse = this.AddPlace(this.createPlaceRequestBody());
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

        private CreatePlaceBodyDataModel createPlaceRequestBody()
        {
            LocationDataModel locationDataModel = new LocationDataModel(-38.383494, 33.427362);
            CreatePlaceBodyDataModel createPlaceBodyDataModel = new CreatePlaceBodyDataModel
                                                                    {
                                                                        location = locationDataModel,
                                                                        name = "Frontline house",
                                                                        accuracy = 50,
                                                                        phone_number = "(+91) 983 893 3937",
                                                                        address = "UMKC, Kansas City, Missourie",
                                                                        types = new List<string> { "shoe park", "shop" },
                                                                        website = "http://google.com",
                                                                        language = "French-IN"
                                                                    };
            return createPlaceBodyDataModel;
        }
    }
}
