using Microsoft.AspNetCore.Mvc;
using HadithWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace HadithWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        // الربط مع قاعدة البيانات من خلال الـ Constructor
        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        // --- أولاً: عمليات إنشاء الحساب (Register) ---

        // عرض صفحة الإنشاء
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // استقبال بيانات المستخدم وحفظها
        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                // إضافة المستخدم الجديد لجدول Users
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                // التوجه لصفحة تسجيل الدخول بعد النجاح
                return RedirectToAction("Login");
            }
            return View(user);
        }


        // --- ثانياً: عمليات تسجيل الدخول (Login) ---

        // عرض صفحة تسجيل الدخول
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // التحقق من الإيميل وكلمة المرور
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            // البحث عن مستخدم يطابق البيانات المدخلة
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

            if (user != null)
            {
                // إذا نجح الدخول، نتوجه للرئيسية
                // ملاحظة: سنحتاج لاحقاً لإضافة الـ Cookies لحفظ الجلسة
                return RedirectToAction("Index", "Home");
            }

            // إذا فشل، نرسل رسالة خطأ تظهر في الصفحة
            ViewBag.Error = "عذراً، البريد الإلكتروني أو كلمة المرور غير صحيحة.";
            return View();
        }

        // --- ثالثاً: تسجيل الخروج ---
        public IActionResult Logout()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}