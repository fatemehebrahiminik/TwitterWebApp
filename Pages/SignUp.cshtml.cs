using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TwitterClient.Models;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using GraphQL;
using TwitterClient.Services;

namespace TwitterClient.Pages
{
    public class SignUpModel : PageModel
    {
        private readonly GraphQlCallServices _graphQlCallServices;
        public SignUpModel(GraphQlCallServices graphQlCallServices)
        {
            _graphQlCallServices = graphQlCallServices;
        }
        public void OnGet()
        {
        }
        [BindProperty]
        public Models.SignUpModel signUpModel { get; set; }
        [HttpPost]
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                //check validate again
                var result = await _graphQlCallServices.sendRequest<RegisterUserOutPut>("mutation {  registerUser(input: {  email: \"" + signUpModel.email + "\", fullName: \"" + signUpModel.fullname + "\" password: \"" + signUpModel.password + "\", username: \"" + signUpModel.username + "\" })   }");
                if (result is not null)
                {
                    if (result.RegisterUser)
                    {
                        //redirect to login

                        return RedirectToPage("./login");
                    }
                    else TempData["message"] = "not register";
                }
            }
            catch (Exception ex)
            {
                //add error to model state

            }

            return RedirectToPage("./SignUp");
        }
    }
}
