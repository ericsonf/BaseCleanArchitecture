using Microsoft.AspNetCore.Mvc;

namespace BaseCleanArchitecture.API.Controllers
{
    public class BaseController : Controller
    {
        [Route("api/[controller]")]
        [ApiController]
        public abstract class BaseApiController : Controller
        {
        }
    }
}
