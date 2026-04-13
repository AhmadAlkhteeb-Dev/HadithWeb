using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HadithWeb.Models; // هذا سيجلب AppDbContext والأقسام والموديلات
using System.Linq;

namespace HadithWeb.Controllers
{
    public class HadithController : Controller
    {
        // تم تغيير الاسم هنا ليطابق ملفك الفعلي
        private readonly AppDbContext _context;

        public HadithController(AppDbContext context)
        {
            _context = context;
        }

        // 1. عرض الأحاديث حسب التصنيف
        public async Task<IActionResult> Index(int? categoryId)
        {
            var query = _context.Hadiths.Include(h => h.Narrator).AsQueryable();

            // إذا كان هناك تصنيف محدد، قم بالفلترة، وإلا اجلب الجميع
            if (categoryId != null)
            {
                query = query.Where(h => h.CategoryId == categoryId);
            }

            var hadiths = await query.ToListAsync();
            return View(hadiths);
        }

        // 2. محرك البحث
        public async Task<IActionResult> Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return View(new List<Hadith>());
            }

            // البحث في محتوى الحديث أو في اسم الراوي المرتبط به
            var results = await _context.Hadiths
                .Include(h => h.Narrator) // ضروري جداً لجلب بيانات الراوي مع الحديث
                .Where(h => h.Content.Contains(searchTerm) || h.Narrator.Name.Contains(searchTerm))
                .ToListAsync();

            ViewBag.SearchTerm = searchTerm;
            return View(results);
        }
        public async Task<IActionResult> ByNarrator(int? narratorId)
        {
            if (narratorId == null)
            {
                return RedirectToAction("Narrators", "Home");
            }

            // جلب الأحاديث المرتبطة بهذا الراوي وتضمين بيانات الراوي نفسه
            var hadiths = await _context.Hadiths
                .Include(h => h.Narrator)
                .Where(h => h.NarratorId == narratorId)
                .ToListAsync();

            // جلب اسم الراوي لعرضه في عنوان الصفحة
            var narrator = await _context.Narrators.FindAsync(narratorId);
            ViewBag.NarratorName = narrator?.Name ?? "غير معروف";

            return View(hadiths);
        }
    }
}