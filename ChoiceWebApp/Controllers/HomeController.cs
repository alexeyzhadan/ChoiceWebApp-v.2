using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using ChoiceWebApp.Data;
using ChoiceWebApp.Models;
using ChoiceWebApp.Attributes;

namespace ChoiceWebApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(ILogger<HomeController> logger,
                            ApplicationDbContext context,
                            UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ForStudent]
        public IActionResult Select()
        {
            var student = _context.Students
                                .Include(s => s.StudDiscs)
                                .SingleOrDefault(s => s.Id == _userManager.GetUserId(HttpContext.User));

            if (student == null)
            {
                return NotFound();
            }

            var selectedDisc = student.StudDiscs.Select(sd => sd.DisciplineId).ToList();
            var disciplines = _context.Disciplines
                                    .Include(d => d.Teacher)
                                    .ToList();

            var model = new StudDiscViewModel { Student = student };
            model.Selected = disciplines.Where(d => selectedDisc.Contains(d.Id))
                                        .OrderBy(d => d.Title)
                                        .ToList();
            model.NoSelected = disciplines.Except(model.Selected)
                                            .OrderBy(d => d.Title)
                                            .ToList();
            return View(model);
        }

        [ForStudent]
        [HttpPost]
        public IActionResult Select([FromBody] string disciplineId)
        {
            int disciplineIdInt = int.Parse(disciplineId);

            if (!_context.Disciplines.Any(d => d.Id == disciplineIdInt))
            {
                return Json(new { success = false, message = "Discipline not found!" });
            }

            var student = _context.Students
                                    .Include(s => s.StudDiscs)
                                    .SingleOrDefault(s => s.Id == _userManager.GetUserId(HttpContext.User));
            if (student.StudDiscs.Any(sd => sd.DisciplineId == disciplineIdInt))
            {
                var studDisc = student.StudDiscs.SingleOrDefault(sd => sd.DisciplineId == disciplineIdInt);
                student.StudDiscs.Remove(studDisc);
            }
            else
            {
                student.StudDiscs.Add(new StudDisc { StudentId = student.Id, DisciplineId = disciplineIdInt });
            }
            _context.SaveChanges();

            return Json(new { success = true, message = "The choice of disciplines has been changed!" });
        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}