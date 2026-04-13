using Microsoft.EntityFrameworkCore;
using HadithWeb.Models; // هذا السطر يخبره أن يبحث عن الـ Classes في مجلد Models

namespace HadithWeb.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // تأكد من أن الأسماء هنا تطابق أسماء الـ Classes التي أنشأتها
        public DbSet<Hadith> Hadiths { get; set; }
        public DbSet<Narrator> Narrators { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<User> Users { get; set; }
    }
}