namespace HadithWeb.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } // في المشاريع الحقيقية نقوم بتشفيرها
        public string Role { get; set; } = "User"; // مدير أم مستخدم عادي
    }
}