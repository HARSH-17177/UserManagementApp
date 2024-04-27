using Microsoft.Extensions.Options;
using UserManagement.TableCreation;


namespace UsersAPI
{
    public class CustomAuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;
        public CustomAuthMiddleware(RequestDelegate next, IOptions<AppSettings> options)
        {
            _next = next;
            _appSettings = options.Value;
        }
        public async Task Invoke(HttpContext context)
        {
            var authHeader = context.Request.Headers["Authorization"];
            var authHeaderValue = authHeader.FirstOrDefault();
            if (authHeaderValue != null)
            {
                var token = authHeaderValue.Split(" ").Last();

                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_appSettings.AuthAPIBaseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();


                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                    //here it is  sending request to NorthWindAuthenticationService api which will will check the token passed in products api is true or false

                    var response = await client.GetAsync(_appSettings.AuthAPIValidateUrl);
                    response.EnsureSuccessStatusCode();
                    var result = await response.Content.ReadFromJsonAsync<User>();
                    if (result != null)
                    {
                        context.Items["User"] = result; //this will be used by CustomAuthorizationAttribute.cs 
                    }
                }
            }
            await _next(context);
        }
    }
}
