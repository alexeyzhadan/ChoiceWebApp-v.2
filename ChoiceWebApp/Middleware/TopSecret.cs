using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace ChoiceWebApp.Middleware
{
    public class TopSecret
    {
        private const string SECRET_FILE_PATH = "secret";
        private const string SECRET_FILE_FULLNAME = "secret.secret";

        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;

        public TopSecret(RequestDelegate next, 
                            IWebHostEnvironment env) 
        {
            _next = next;
            _env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.User.Identity.IsAuthenticated &&
                context.Request.Path.Value.Split("/").Last() == SECRET_FILE_FULLNAME)
            {
                var path = Path.Combine(_env.WebRootPath, SECRET_FILE_PATH, SECRET_FILE_FULLNAME);
                using (FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read))
                {
                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, (int)fs.Length);

                    context.Response.ContentType = MediaTypeNames.Application.Octet;
                    context.Response.ContentLength = fs.Length;
                    await context.Response.Body.WriteAsync(buffer, 0, (int)fs.Length);
                }
            }
            else
            {
                await _next.Invoke(context);
            }
        }
    }
}
