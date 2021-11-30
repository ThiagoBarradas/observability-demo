using AspNetScaffolding.Controllers;
using Microsoft.AspNetCore.Mvc;
using Mongo.CRUD;
using ObservabilityDemo.Api.External;
using ObservabilityDemo.Api.Models.Request;
using System;
using System.Threading;

namespace ObservabilityDemo.Api.Controllers
{
    public class DemoController : BaseController
    {
        private readonly IMongoCRUD<TestRequest> MongoClient;

        private readonly IInternalApiClient InternalApiClient;

        public DemoController(
            IMongoCRUD<TestRequest> mongoClient,
            IInternalApiClient internalApiClient)
        {
            this.MongoClient = mongoClient;
            this.InternalApiClient = internalApiClient;
        }

        [HttpPost("test")]
        public IActionResult Test([FromBody] TestRequest request)
        {
            if (request.SleepApi < 0 || request.SleepMongo < 0)
            {
                throw new ArgumentOutOfRangeException("some exception");
            }

            this.CallOtherApi(request);
            this.CallMongo(request);

            return Ok(request);
        }

        private void CallOtherApi(TestRequest request)
        {
            if (request.SleepApi > 0)
            {
                Thread.Sleep(request.SleepApi);
            }

            this.InternalApiClient.CallTest();
        }

        private void CallMongo(TestRequest request)
        {
            if (request.SleepMongo > 0)
            {
                Thread.Sleep(request.SleepMongo);
            }

            this.MongoClient.Upsert(request);
        }       
    }
}
