using System.Threading.Tasks;
using Cinode.Samples.Core;
using Cinode.Samples.Core.Abstractions;
using Cinode.Samples.Core.Options;
using Cinode.Samples.Search.Models;
using Cinode.Samples.Search.Models.Builders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Cinode.Samples.Search.Controllers
{
    [Route("/")]
    public class HomeController : Core.Controllers.ControllerBase
    {
        private readonly IApiUriHelper _uriHelper;
        public HomeController(IOptions<TokenOptions> tokenOptionsAccessor, IApiUriHelper uriHelper, IMemoryCache memoryCache) : base(tokenOptionsAccessor, uriHelper, memoryCache)
        {
            _uriHelper = uriHelper;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("/search/skills", Name = "SkillSearch")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SkillSearch(SkillSearchModel model)
        {
            if (ModelState.IsValid)
            {
                var tokenResponse =await GetAccessToken(true);
                if (tokenResponse == null) return Unauthorized();
                     
                var builder = new SkillSearchModelBuilder(CustomHttpClientWrapper.Instance,_uriHelper, tokenResponse, model.Term);
                var result = await builder.BuildAsync();
                if (result != null)
                {
                    return View(result);
                }
            }

            return View(new SkillSearchModel(){Term = model.Term});
        }
        [HttpPost("/search/keyword", Name = "KeywordSearch")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> KeywordSearch([FromForm]string term)
        {
            if (ModelState.IsValid)
            {
                var tokenResponse = await GetAccessToken(true);
                if (tokenResponse == null) return Unauthorized();
                var builder = new KeywordSearchModelBuilder(CustomHttpClientWrapper.Instance, _uriHelper, tokenResponse, term);
                var model = await builder.BuildAsync();

                return View(model);
            }

            return View(new KeywordSearchModel(){Term = term});
        }
    }
}