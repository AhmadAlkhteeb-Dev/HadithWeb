namespace HadithWeb.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } // (صلاة، صوم، أخلاق)

        public List<Hadith> Hadiths { get; set; }
    }
}