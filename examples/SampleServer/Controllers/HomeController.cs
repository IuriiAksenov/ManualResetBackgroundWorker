using Microsoft.AspNetCore.Mvc;
using SampleServer.BackgroundServices;

namespace SampleServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly SampleBackgroundWorker _sampleBackgroundWorker;

        public HomeController(SampleBackgroundWorker sampleBackgroundWorker)
        {
            _sampleBackgroundWorker = sampleBackgroundWorker;
        }


        [HttpGet]
        public string ResetBackgroundWorker()
        {
            return _sampleBackgroundWorker.TryReleaseDelay() ? "Restarted" : "Failed to restart";
        }
    }
}