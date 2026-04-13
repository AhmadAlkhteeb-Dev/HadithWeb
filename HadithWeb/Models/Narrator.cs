namespace HadithWeb.Models
{
    public class Narrator
    {
        public int Id { get; set; }
        public string Name { get; set; } // اسم الراوي (مثل: أبي هريرة)
        public string Description { get; set; } // نبذة عنه

        // العلاقة: الراوي الواحد له مجموعة أحاديث
        public List<Hadith> Hadiths { get; set; }
    }
}