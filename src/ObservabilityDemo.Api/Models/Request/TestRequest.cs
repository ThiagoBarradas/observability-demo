using System;
using System.Collections.Generic;

namespace ObservabilityDemo.Api.Models.Request
{
    public class TestRequest
    {
        public TestRequest()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid? Id { get; set; }

        public int SleepMongo { get; set; }

        public int SleepApi { get; set; }

        public IDictionary<string, string> Metadata { get; set; }
    }
}
