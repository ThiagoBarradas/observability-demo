using System;
using System.Collections.Generic;

namespace ObservabilityDemo.Api.Models.Request
{
    public class TestRequest
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public int SleepMongo { get; set; }

        public int SleepApi { get; set; }

        public IDictionary<string, string> Metadata { get; set; }
    }
}
