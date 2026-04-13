using Microsoft.EntityFrameworkCore;
using HadithWeb.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. إضافة خدمات الـ Controllers والـ Views
builder.Services.AddControllersWithViews();

// 2. ربط قاعدة البيانات (SQL Server) باستخدام Connection String الموجود في appsettings.json
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// --- كود إضافة البيانات التجريبية ---
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();

    // التأكد من أن قاعدة البيانات تحتوي على بيانات أم لا
    if (!context.Narrators.Any())
    {
        var narrator = new Narrator { Name = "أبو هريرة", Description = "صحابي جليل" };
        var category = new Category { Name = "أخلاق" };

        context.Narrators.Add(narrator);
        context.Categories.Add(category);
        context.SaveChanges();

        context.Hadiths.Add(new Hadith
        {
            Content = "إِنَّمَا بُعِثْتُ لِأُتَمِّمَ مَكَارِمَ الْأَخْلَاقِ",
            NarratorId = narrator.Id,
            CategoryId = category.Id
        });
        context.SaveChanges();
    }
}
// ------------------------------------

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();