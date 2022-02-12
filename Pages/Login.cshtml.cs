using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TwitterClient.Models;
using TwitterClient.Services;

namespace TwitterClient.Pages
{
    public class LoginModel : PageModel
    {
        private readonly GraphQlCallServices _graphQlCallServices;
        public LoginModel(GraphQlCallServices graphQlCallServices)
        {
            _graphQlCallServices = graphQlCallServices;
        }
        public void OnGet()
        {
            TempData["message"] = "";
        }
        [BindProperty]
        public Models.SignUpModel signUpModel { get; set; }
        [HttpPost]
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                //check validate again
                var result = await _graphQlCallServices.sendRequest<LoginResultOutput>("mutation { userLogin(input: { password: \"" + signUpModel.password + "\", username: \"" + signUpModel.username + "\" }) {  message  result  value } }");
                TempData["message"] = result.UserLogin.message;
                if (result is not null)
                    if (result.UserLogin.result)
                    {

                        //save token in session
                        HttpContext.Session.SetString("JwtToken", result.UserLogin.value);
                        return RedirectToPage("./profile");
                    }

            }
            catch (Exception ex)
            {
                //add error to model state

            }

            return RedirectToPage("./login");
        }
    }
}
