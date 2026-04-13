using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HadithWeb.Models;

namespace HadithWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(); // سيعرض صفحة البحث والزرين
        }

        public async Task<IActionResult> Categories()
        {
            var categories = await _context.Categories.ToListAsync();
            return View(categories);
        }

        public IActionResult Narrators()
        {
            // جلب الأسماء فقط ومنع تكرارها
            var uniqueNarrators = _context.Narrators
                .Select(n => n.Name)
                .Distinct()
                .ToList();

            return View(uniqueNarrators);
        }
    }
}