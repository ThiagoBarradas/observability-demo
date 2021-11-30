using RestSharp.Easy.Interfaces;
using System;
using System.Net.Http;

namespace ObservabilityDemo.Api.External
{
    public interface IInternalApiClient
    {
        void CallTest();
    }

    public class InternalApiClient : IInternalApiClient
    {
        private readonly IEasyRestClient Client;

        public InternalApiClient(IEasyRestClient client)
        {
            this.Client = client;
        }

        public void CallTest()
        {
            var body = new
            {
                SomethingNumber = 1,
                SometginGuid = Guid.NewGuid()
            };

            this.Client.SendRequest<object, object>(HttpMethod.Post, "other-test", body);
        }
    }
}
