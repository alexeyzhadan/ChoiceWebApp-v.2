using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Collections.Generic;
using ChoiceWebApp.Data;
using ChoiceWebApp.Models;
using ChoiceWebApp.Attributes;
using System;

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
        public IActionResult Select(string studentId, int[] selectedDiscId)
        {
            var student = _context.Students
                                    .Include(s => s.StudDiscs)
                                    .SingleOrDefault(s => s.Id == studentId);
            student.StudDiscs = new List<StudDisc>();
            foreach (var item in selectedDiscId)
            {
                student.StudDiscs.Add(new StudDisc { StudentId = student.Id, DisciplineId = item });
            }
            _context.SaveChanges();

            return RedirectToAction(nameof(Select));
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