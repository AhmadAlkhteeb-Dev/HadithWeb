namespace HadithWeb.Models
{
    public class Hadith
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string? Source { get; set; } // تأكد من وجود هذا السطر
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int NarratorId { get; set; }
        public Narrator Narrator { get; set; }
    }
}