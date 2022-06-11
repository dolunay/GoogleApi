using System;
using System.Linq;
using GoogleApi.Entities.Common.Enums;
using GoogleApi.Entities.Places.Details.Request;
using GoogleApi.Entities.Places.Details.Request.Enums;
using NUnit.Framework;

namespace GoogleApi.UnitTests.Places.Details
{
    [TestFixture]
    public class DetailsRequestTests
    {
        [Test]
        public void ConstructorDefaultTest()
        {
            var request = new PlacesDetailsRequest();

            Assert.AreEqual(Language.English, request.Language);
        }

        [Test]
        public void GetQueryStringParametersTest()
        {
            var request = new PlacesDetailsRequest
            {
                Key = "key",
                PlaceId = "placeId"
            };

            var queryStringParameters = request.GetQueryStringParameters();
            Assert.IsNotNull(queryStringParameters);

            var key = queryStringParameters.FirstOrDefault(x => x.Key == "key");
            var keyExpected = request.Key;
            Assert.IsNotNull(key);
            Assert.AreEqual(keyExpected, key.Value);

            var placeId = queryStringParameters.FirstOrDefault(x => x.Key == "placeid");
            var placeIdExpected = request.PlaceId;
            Assert.IsNotNull(placeId);
            Assert.AreEqual(placeIdExpected, placeId.Value);

            var language = queryStringParameters.FirstOrDefault(x => x.Key == "language");
            Assert.IsNotNull(language);
            Assert.AreEqual("en", language.Value);
        }

        [Test]
        public void GetQueryStringParametersWhenFieldsTest()
        {
            var request = new PlacesDetailsRequest
            {
                Key = "key",
                PlaceId = "placeId",
                Fields = FieldTypes.Basic
            };

            var queryStringParameters = request.GetQueryStringParameters();
            Assert.IsNotNull(queryStringParameters);

            var fields = queryStringParameters.FirstOrDefault(x => x.Key == "fields");
            var fieldsExpected = Enum.GetValues(typeof(FieldTypes))
                .Cast<FieldTypes>()
                .Where(x => request.Fields.HasFlag(x) && x != FieldTypes.Basic && x != FieldTypes.Contact && x != FieldTypes.Atmosphere)
                .Aggregate(string.Empty, (current, x) => $"{current}{x.ToString().ToLowerInvariant()},");
            fieldsExpected = fieldsExpected.EndsWith(",") ? fieldsExpected.Substring(0, fieldsExpected.Length - 1) : fieldsExpected;

            Assert.IsNotNull(fields);
            Assert.AreEqual(fieldsExpected, fields.Value);
        }

        [Test]
        public void GetQueryStringParametersWhenRegionTest()
        {
            var request = new PlacesDetailsRequest
            {
                Key = "key",
                PlaceId = "placeId",
                Region = "region"
            };

            var queryStringParameters = request.GetQueryStringParameters();
            Assert.IsNotNull(queryStringParameters);

            var region = queryStringParameters.FirstOrDefault(x => x.Key == "region");
            var regionExpected = request.Region;
            Assert.IsNotNull(region);
            Assert.AreEqual(regionExpected, region.Value);
        }

        [Test]
        public void GetQueryStringParametersWhenSessionTokenTest()
        {
            var request = new PlacesDetailsRequest
            {
                Key = "key",
                PlaceId = "placeId",
                SessionToken = "sessiontoken"
            };

            var queryStringParameters = request.GetQueryStringParameters();
            Assert.IsNotNull(queryStringParameters);

            var sessiontoken = queryStringParameters.FirstOrDefault(x => x.Key == "sessiontoken");
            var sessiontokenExpected = request.SessionToken;
            Assert.IsNotNull(sessiontoken);
            Assert.AreEqual(sessiontokenExpected, sessiontoken.Value);
        }

        [Test]
        public void GetQueryStringParametersWhenKeyIsNullTest()
        {
            var request = new PlacesDetailsRequest
            {
                Key = null
            };

            var exception = Assert.Throws<ArgumentException>(() =>
            {
                var parameters = request.GetQueryStringParameters();
                Assert.IsNull(parameters);
            });
            Assert.IsNotNull(exception);
            Assert.AreEqual(exception.Message, "'Key' is required");
        }

        [Test]
        public void GetQueryStringParametersWhenKeyIsStringEmptyTest()
        {
            var request = new PlacesDetailsRequest
            {
                Key = string.Empty
            };

            var exception = Assert.Throws<ArgumentException>(() =>
            {
                var parameters = request.GetQueryStringParameters();
                Assert.IsNull(parameters);
            });
            Assert.IsNotNull(exception);
            Assert.AreEqual(exception.Message, "'Key' is required");
        }

        [Test]
        public void GetQueryStringParametersWhenPlaceIdIsNullTest()
        {
            var request = new PlacesDetailsRequest
            {
                Key = "key",
                PlaceId = null
            };

            var exception = Assert.Throws<ArgumentException>(() =>
            {
                var parameters = request.GetQueryStringParameters();
                Assert.IsNull(parameters);
            });
            Assert.IsNotNull(exception);
            Assert.AreEqual(exception.Message, "'PlaceId' is required");
        }

        [Test]
        public void GetQueryStringParametersWhenPlaceIdIsStringEmptyTest()
        {
            var request = new PlacesDetailsRequest
            {
                Key = "key",
                PlaceId = string.Empty
            };

            var exception = Assert.Throws<ArgumentException>(() =>
            {
                var parameters = request.GetQueryStringParameters();
                Assert.IsNull(parameters);
            });
            Assert.IsNotNull(exception);
            Assert.AreEqual(exception.Message, "'PlaceId' is required");
        }
    }
}