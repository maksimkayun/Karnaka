/*
 * Мой API
 *
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: v1
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */

using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using RestSharp;
using Xunit;

using Org.OpenAPITools.Client;
using Org.OpenAPITools.Api;
// uncomment below to import models
//using Org.OpenAPITools.Model;

namespace Org.OpenAPITools.Test.Api
{
    /// <summary>
    ///  Class for testing LocationApi
    /// </summary>
    /// <remarks>
    /// This file is automatically generated by OpenAPI Generator (https://openapi-generator.tech).
    /// Please update the test case below to test the API endpoint.
    /// </remarks>
    public class LocationApiTests : IDisposable
    {
        private LocationApi instance;

        public LocationApiTests()
        {
            instance = new LocationApi();
        }

        public void Dispose()
        {
            // Cleanup when everything is done.
        }

        /// <summary>
        /// Test an instance of LocationApi
        /// </summary>
        [Fact]
        public void InstanceTest()
        {
            // TODO uncomment below to test 'IsType' LocationApi
            //Assert.IsType<LocationApi>(instance);
        }

        /// <summary>
        /// Test ApiLocationsGet
        /// </summary>
        [Fact]
        public void ApiLocationsGetTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //var response = instance.ApiLocationsGet();
            //Assert.IsType<List<LocationDto>>(response);
        }

        /// <summary>
        /// Test ApiLocationsIdDelete
        /// </summary>
        [Fact]
        public void ApiLocationsIdDeleteTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //int id = null;
            //var response = instance.ApiLocationsIdDelete(id);
            //Assert.IsType<LocationDto>(response);
        }

        /// <summary>
        /// Test ApiLocationsIdGet
        /// </summary>
        [Fact]
        public void ApiLocationsIdGetTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //int id = null;
            //var response = instance.ApiLocationsIdGet(id);
            //Assert.IsType<LocationDto>(response);
        }

        /// <summary>
        /// Test ApiLocationsIdPut
        /// </summary>
        [Fact]
        public void ApiLocationsIdPutTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //int id = null;
            //LocationDto locationDto = null;
            //var response = instance.ApiLocationsIdPut(id, locationDto);
            //Assert.IsType<LocationDto>(response);
        }

        /// <summary>
        /// Test ApiLocationsPost
        /// </summary>
        [Fact]
        public void ApiLocationsPostTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //LocationDto locationDto = null;
            //var response = instance.ApiLocationsPost(locationDto);
            //Assert.IsType<LocationDto>(response);
        }
    }
}