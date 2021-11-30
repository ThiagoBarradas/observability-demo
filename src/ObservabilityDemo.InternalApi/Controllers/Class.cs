using AspNetScaffolding.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace ObservabilityDemo.InternalApi.Controllers
{
    public class DemoController : BaseController
    {
        [HttpPost("other-test")]
        public IActionResult OtherTest()
        {
            Thread.Sleep(500);

            return Ok(new { message = "success!" });
        }
    }
}
