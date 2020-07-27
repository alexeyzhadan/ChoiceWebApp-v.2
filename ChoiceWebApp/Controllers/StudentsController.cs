using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChoiceWebApp.Data;
using ChoiceWebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace ChoiceWebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Students.Include(s => s.User).OrderBy(s => s.Name);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.User)
                .Include(s => s.StudDiscs)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            var selectedDiscId = student.StudDiscs.Select(sd => sd.DisciplineId).ToList(); 

            var model = new StudentDetailsViewModel { Student = student };
            model.SelectedDisc = _context.Disciplines
                                        .Include(d => d.Teacher)
                                        .Where(d => selectedDiscId.Contains(d.Id))
                                        .ToList();

            return View(model);
        }

    }
}
