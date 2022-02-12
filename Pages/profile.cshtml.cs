using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TwitterClient.Models;
using TwitterClient.Services;

namespace TwitterClient.Pages
{
    public class profileModel : PageModel
    {
        private readonly GraphQlCallServices _graphQlCallServices;
        public profileModel(GraphQlCallServices graphQlCallServices)
        {
            _graphQlCallServices = graphQlCallServices;
        }
        [BindProperty]
        public UserProfile profile { set; get; }
        public async Task<IActionResult> OnGet()
        {
            var token = HttpContext.Session.GetString("JwtToken");
            if (!string.IsNullOrEmpty(token))
            {
                var result = await _graphQlCallServices.sendRequest<TwitterUserProfile>("query {  userProfile {  fullName  email  followerCount  followingCount  twittes {  id   textTwite  }  reTwittes {  id   textTwite  }}}", token);

                if (result is not null)
                    profile = result.UserProfile;

                //get all user
                var resultUser = await _graphQlCallServices.sendRequest<UserProfile>("query{ allUser {   fullName email  id}}", token);
                if (result is not null)
                    profile.AllUser = resultUser.AllUser;
                return Page();
            }
            return RedirectToPage("./login");
        }
        public async Task<IActionResult> OnPostReTwite(string twiteId)
        {
            var token = HttpContext.Session.GetString("JwtToken");

            if (!string.IsNullOrEmpty(token))
            {
                var result = await _graphQlCallServices.sendRequest<CreateReTwitteResult>("mutation { createReTwitte(twitteId:\"" + twiteId + "\") { result}} ", token);
                if (result is not null && result.CreateReTwitte.Result)
                    return RedirectToPage("./profile");
                return Page();
            }
            return RedirectToPage("./login");
        }
        public async Task<IActionResult> OnPostTwite(string twite)
        {
            var token = HttpContext.Session.GetString("JwtToken");

            if (!string.IsNullOrEmpty(token))
            {
                var result = await _graphQlCallServices.sendRequest<CreateReTwitteResult>("mutation { createTwitte(twitteText:\"" + twite + "\") { result}} ", token);
                if (result is not null && result.CreateTwitte.Result)
                    return RedirectToPage("./profile");
                return Page();
            }
            return RedirectToPage("./login");
        }
        public async Task<IActionResult> OnPostFollow(string userId)
        {
            var token = HttpContext.Session.GetString("JwtToken");

            if (!string.IsNullOrEmpty(token))
            {
                var result = await _graphQlCallServices.sendRequest<FollowOutPut>("mutation { addFollow(followUserId: \"" + userId + "\") }", token);
                if (result is not null && result.addFollow)
                  return RedirectToPage("./profile");
                return Page();
            }
            return RedirectToPage("./login");
        }
        
    }
}
