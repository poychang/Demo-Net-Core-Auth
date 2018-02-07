using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookieSecurity.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values/anonymous
        [AllowAnonymous]
        [Route("anonymous")]
        [HttpGet]
        public IActionResult Anonymous()
        {
            var name = HttpContext.User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Name)?.Value;
            return new ContentResult() { Content = $@"For all anonymous. {name}" };
        }

        // GET api/values/authorize
        [Authorize]
        [Route("authorize")]
        [HttpGet]
        public IActionResult All()
        {
            var name = HttpContext.User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Name)?.Value;
            return new ContentResult() { Content = $@"For all client who authorize. {name}" };
        }

        // GET api/values/roles
        [Authorize(Roles = "admin,administrator")]
        [Route("roles")]
        public IActionResult Admin()
        {
            var name = HttpContext.User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Name)?.Value;
            return new ContentResult() { Content = $@"For all client who's role is admin or administrator. {name}" };
        }

        // GET api/values/policy
        [Authorize(Policy = "admin")]
        [Route("policy")]
        public IActionResult AdminPolicy()
        {
            var name = HttpContext.User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Name)?.Value;
            return new ContentResult() { Content = $@"For all client who's is under admin policy. {name}" };
        }
    }
}
