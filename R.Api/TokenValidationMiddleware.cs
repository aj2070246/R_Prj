
using Microsoft.AspNetCore.Routing; // برای دسترسی به Endpoint

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using R.Database;
using System.Threading.Tasks;

namespace R.Api
{


    public class TokenValidationMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly IServiceScopeFactory _scopeFactory;

        public TokenValidationMiddleware(RequestDelegate next, IServiceScopeFactory scopeFactory)
        {
            _next = next;
            _scopeFactory = scopeFactory;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var endpoint = context.GetEndpoint();
            if (endpoint != null)
            {
                List<string> trustedActions = new List<string> { "login", "getcaptcha", "registeruser", "GetAllDropDownsItems".ToLower() };
                var actionName = endpoint.Metadata.GetMetadata<Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor>()?.ActionName;
                if (!trustedActions.Contains(actionName.ToLower()))
                {
                    // بررسی توکن
                    var token = context.Request.Headers["token"].ToString().Replace("Bearer ", "");
                    var userId = context.Request.Headers["currentUserId"].ToString();

                    if (IsValid(token) && IsValid(userId)) // متد بررسی اعتبار توکن
                    {
                        // ایجاد یک دامنه جدید برای دسترسی به DbContext
                        using (var scope = _scopeFactory.CreateScope())
                        {
                            var db = scope.ServiceProvider.GetRequiredService<RDbContext>();
                            var user = db.Users.FirstOrDefault(x => x.Id.ToLower() == userId && x.Token == token);

                            if (user == null)
                            {
                                // اگر توکن نامعتبر است، مدل مورد نظر را برگردانید
                                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                                await context.Response.WriteAsync("Token is invalid");
                                return; // توقف پردازش درخواست
                            }
                            else
                            {
                                user.TokenExpireDate = DateTime.Now.AddHours(1);
                                user.LastActivityDate = DateTime.Now;
                            }
                            db.SaveChanges();
                        }

                    }
                    else
                    {
                        // اگر توکن نامعتبر است، مدل مورد نظر را برگردانید
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        await context.Response.WriteAsync("Token is invalid");
                        return; // توقف پردازش درخواست
                    }
                    // ادامه پردازش درخواست
                    await _next(context);
                }
                else
                    await _next(context);

            }
            else
                await _next(context);

        }
        private bool IsValid(string token)
        {
            // منطق بررسی اعتبار توکن
            return !string.IsNullOrEmpty(token); // به عنوان یک مثال ساده
        }
    }
}


