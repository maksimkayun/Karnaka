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
using Org.OpenAPITools.Model;

// uncomment below to import models
//using Org.OpenAPITools.Model;

namespace Org.OpenAPITools.Test.Api
{
    /// <summary>
    ///  Class for testing ConspiracyApi
    /// </summary>
    /// <remarks>
    /// This file is automatically generated by OpenAPI Generator (https://openapi-generator.tech).
    /// Please update the test case below to test the API endpoint.
    /// </remarks>
    public class ConspiracyApiTests : IDisposable
    {
        private ConspiracyApi instance;

        public ConspiracyApiTests()
        {
            Configuration config = new Configuration();
            config.BasePath = "https://localhost:7228";
            this.instance = new ConspiracyApi(config);
        }

        public void Dispose()
        {
            // Cleanup when everything is done.
        }

        /// <summary>
        /// Test an instance of ConspiracyApi
        /// </summary>
        [Fact]
        public void InstanceTest()
        {
            // TODO uncomment below to test 'IsType' ConspiracyApi
            //Assert.IsType<ConspiracyApi>(instance);
        }

        /// <summary>
        /// Test ApiConspiratorsGet
        /// </summary>
        [Fact]
        public void ApiConspiratorsGetTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //var response = instance.ApiConspiratorsGet();
            //Assert.IsType<List<ConspiratorDto>>(response);
        }

        /// <summary>
        /// Test ApiConspiratorsIdDelete
        /// </summary>
        [Fact]
        public void ApiConspiratorsIdDeleteTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //int id = null;
            //var response = instance.ApiConspiratorsIdDelete(id);
            //Assert.IsType<ConspiratorDto>(response);
        }

        /// <summary>
        /// Test ApiConspiratorsIdGet
        /// </summary>
        [Fact]
        public void ApiConspiratorsIdGetTest()
        {
            int id = 5;
            var response = instance.ApiConspiratorsIdGet(id);
            Assert.IsType<ConspiratorDto>(response);
        }

        /// <summary>
        /// Test ApiConspiratorsPost
        /// </summary>
        [Fact]
        public void ApiConspiratorsPostTest()
        {
            ConspiratorDto conspiratorDto = new ConspiratorDto()
            {
                Name = "Test",
                PartPlan = "TestPlan",
                Location = "Test, Test, Test"
            };
            var response = instance.ApiConspiratorsPost(conspiratorDto);
            Assert.IsType<ConspiratorDto>(response);
            var rm = instance.ApiConspiratorsIdDelete((int) response.Id);
        }

        /// <summary>
        /// Test HalConspiratorsGet
        /// </summary>
        [Fact]
        public void HalConspiratorsGetTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //int? index = null;
            //int? count = null;
            //var response = instance.HalConspiratorsGet(index, count);
            //Assert.IsType<List<ConspiratorDto>>(response);
        }

        /// <summary>
        /// Test HalConspiratorsIdGet
        /// </summary>
        [Fact]
        public void HalConspiratorsIdGetTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //int id = null;
            //var response = instance.HalConspiratorsIdGet(id);
            //Assert.IsType<ConspiratorDto>(response);
        }
    }
}
