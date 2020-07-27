using System.Security.Claims;
using System.Threading.Tasks;
using ChoiceWebApp.Data;
using ChoiceWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChoiceWebApp.Controllers
{
    public class RegisterController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public RegisterController(ApplicationDbContext db,
                                UserManager<IdentityUser> userManager)
        {
            _context = db;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("Name,Email,Group,Password,PasswordConfirm")] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { Email = model.Email, UserName = model.Email };
                if (_userManager.FindByNameAsync(user.UserName).Result == null)
                {
                    await _userManager.CreateAsync(user, model.Password);
                    await _userManager.AddToRoleAsync(user, "User");
                    await _userManager.AddClaimAsync(user, new Claim("FullName", model.Name));

                    var student = new Student { Id = user.Id, Name = model.Name, Group = model.Group };
                    await _context.Students.AddAsync(student);
                    await _context.SaveChangesAsync();

                    return Redirect("/Identity/Account/Login");
                }
                else
                {
                    ModelState.AddModelError("", "Email already exists!");
                }
            }
            return View(model);
        }
    }
}