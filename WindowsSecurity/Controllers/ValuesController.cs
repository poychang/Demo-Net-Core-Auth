using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WindowsSecurity.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values/iisAuthorize
        [Route("iisAuthorize")]
        [HttpGet]
        public IActionResult IisAuthorize()
        {
            var name = HttpContext.User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Name)?.Value;
            return new ContentResult() { Content = $@"IIS authorize. {name}" };
        }

        // GET api/values/anonymous
        [AllowAnonymous]
        [Route("anonymous")]
        [HttpGet]
        public IActionResult Anonymous()
        {
            var name = HttpContext.User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Name)?.Value;
            return new ContentResult() {Content = $@"For all anonymous. {name}"};
        }

        // GET api/values/authorizeWithMvc
        [Authorize]
        [Route("authorize")]
        [HttpGet]
        public IActionResult AuthorizeWithMvc()
        {
            var name = HttpContext.User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Name)?.Value;
            return new ContentResult() {Content = $@"For all client who authorize within MVC. {name}"};
        }

        // GET api/values/onlyAdministrator
        [Authorize(Policy = "OnlyAdministrator")]
        [Route("onlyAdministrator")]
        [HttpGet]
        public IActionResult OnlyAdministrator()
        {
            var name = HttpContext.User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Name)?.Value;
            return new ContentResult() { Content = $@"For all client who authorize with Administrator role. {name}" };
        }
    }
}